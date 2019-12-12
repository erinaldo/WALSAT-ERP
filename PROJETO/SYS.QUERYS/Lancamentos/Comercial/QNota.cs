using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SYS.UTILS;
using SYS.QUERYS.Lancamentos.Financeiro;
using SYS.QUERYS.Lancamentos.Estoque;
using SYS.QUERYS.Cadastros.Fiscal;
using SYS.QUERYS.Cadastros.Relacionamento;

namespace SYS.QUERYS.Lancamentos.Comercial
{
    public class QNota
    {
        public int ID_NOTA = 0;

        public IQueryable<TB_FAT_NOTA> Buscar(int id_nota = 0, int id_empresa = 0)
        {
            var consulta = from a in Conexao.BancoDados.TB_FAT_NOTAs
                           select a;

            if (id_nota.TemValor())
                consulta = consulta.Where(a => a.ID_NOTA == id_nota);

            if (id_empresa.TemValor())
                consulta = consulta.Where(a => a.ID_EMPRESA == id_empresa);

            return consulta;
        }

       
        public void Gravar(TB_FAT_NOTA nota, int? formaPagamento, ref int posicaoTransacao)
        {
            try
            {
                Conexao.Iniciar(ref posicaoTransacao);

                var itens = nota.TB_FAT_NOTA_ITEMs;
                var itensNovos = new List<TB_FAT_NOTA_ITEM>();

                nota = new TB_FAT_NOTA
                {
                    ID_NOTA = nota.ID_NOTA,
                    ID_EMPRESA = nota.ID_EMPRESA,
                    ID_NOTA_REFERENCIA = nota.ID_NOTA_REFERENCIA,
                    ID_CONFIGURACAO_FISCAL = nota.ID_CONFIGURACAO_FISCAL,
                    ID_CONFIGURACAO_FINANCEIRO = nota.ID_CONFIGURACAO_FINANCEIRO,
                    ID_USUARIO = nota.ID_USUARIO,
                    ID_CLIFOR = nota.ID_CLIFOR,
                    ID_TRANSPORTADORA = nota.ID_TRANSPORTADORA,
                    ID_VEICULO = nota.ID_VEICULO,
                    ID_MOEDA = nota.ID_MOEDA,
                    TP_MOVIMENTO = nota.TP_MOVIMENTO,
                    TP_NOTA = nota.TP_NOTA,
                    DT_ENTRADASAIDA = nota.DT_ENTRADASAIDA,
                    DT_EMISSAO = Conexao.DataHora
                };
                
                if(!nota.ID_EMPRESA.TemValor())
                    throw new SYSException(Mensagens.Necessario("empresa da nota"));

                if (!nota.ID_CLIFOR.TemValor())
                    throw new SYSException(Mensagens.Necessario("clifor da nota"));

                nota.TP_MOVIMENTO.Validar(true, 1);

                if (!(nota.TP_MOVIMENTO == "E" | nota.TP_MOVIMENTO == "S"))
                    throw new SYSException(Mensagens.Necessario("o tipo do movimento da nota"));
                
                if (nota.TP_MOVIMENTO == "E" && !nota.ID_NOTA_REFERENCIA.TemValor())
                    throw new SYSException(Mensagens.Necessario("o identificador da nota de referência"));

                if (nota.TP_MOVIMENTO == "E" && !nota.DT_ENTRADASAIDA.DataValida())
                    throw new SYSException(Mensagens.Necessario("a data de entrada da nota"));

                if (!nota.ID_CONFIGURACAO_FISCAL.TemValor())
                    throw new SYSException(Mensagens.Necessario("o identificador da configuração fiscal"));

                if (!nota.ID_CONFIGURACAO_FINANCEIRO.TemValor())
                    throw new SYSException(Mensagens.Necessario("o identificador da configuração financeiro"));

                if (!nota.ID_USUARIO.TemValor())
                    nota.ID_USUARIO = Parametros.ID_Usuario;

                if (!nota.ID_MOEDA.TemValor())
                    nota.ID_MOEDA = Parametros.ID_MoedaPadrao;

                if(itens == null || !itens.Count.TemValor())
                    throw new SYSException(Mensagens.Necessario("os itens da nota"));

                nota.ID_NOTA = (Conexao.BancoDados.TB_FAT_NOTAs.Where(a => a.ID_EMPRESA == nota.ID_EMPRESA).Any() ? Conexao.BancoDados.TB_FAT_NOTAs.Where(a => a.ID_EMPRESA == nota.ID_EMPRESA).Max(a => a.ID_NOTA) : 0) + 1;

                Conexao.BancoDados.TB_FAT_NOTAs.InsertOnSubmit(nota);
                Conexao.Enviar();
                
                // Itens da nota
                foreach (var item in itens)
                {
                    var novoItem = new TB_FAT_NOTA_ITEM
                    {
                        ID_NOTA = nota.ID_NOTA,
                        ID_EMPRESA = nota.ID_EMPRESA,
                        ID_ITEM = item.ID_ITEM,
                        ID_PRODUTO = item.ID_PRODUTO,
                        QT = item.QT,
                        VL_UNITARIO = item.VL_UNITARIO,
                        VL_FRETE = item.VL_FRETE,
                        VL_SEGURO = item.VL_SEGURO,
                        VL_DESCONTO = item.VL_DESCONTO,
                        VL_ACRESCIMO = item.VL_ACRESCIMO,
                        VL_SUBTOTAL= (item.VL_UNITARIO.Padrao() * item.QT.Padrao()) + item.VL_FRETE.Padrao() + item.VL_SEGURO.Padrao() + item.VL_ACRESCIMO.Padrao() - item.VL_DESCONTO.Padrao(),
                        ST_COMPOE = item.ST_COMPOE,
                        DS_OBSERVACAO = item.DS_OBSERVACAO,
                        DS_OBSERVACAOFISCAL = item.DS_OBSERVACAOFISCAL
                    };
                    
                    if (!novoItem.ID_PRODUTO.TemValor())
                        throw new SYSException(Mensagens.Necessario("o produto do item"));

                    novoItem.VL_DESCONTO.Validar();
                    novoItem.QT.Validar();
                    novoItem.VL_UNITARIO.Validar();

                    if (!novoItem.VL_UNITARIO.TemValor() && !novoItem.QT.TemValor())
                        throw new SYSException(Mensagens.Necessario("valor e/ou quantidade do item"));

                    novoItem.ID_ITEM = (Conexao.BancoDados.TB_FAT_NOTA_ITEMs.Where(a => a.ID_EMPRESA == nota.ID_EMPRESA && a.ID_NOTA == a.ID_NOTA).Any() ? Conexao.BancoDados.TB_FAT_NOTA_ITEMs.Where(a => a.ID_EMPRESA == nota.ID_EMPRESA && a.ID_NOTA == a.ID_NOTA).Max(a => a.ID_ITEM) : 0) + 1;


                    var novoItemTributos = novoItem.TB_FAT_NOTA_ITEM_TRIBUTOs;
                    
                    Conexao.BancoDados.TB_FAT_NOTA_ITEMs.InsertOnSubmit(novoItem);
                    Conexao.Enviar();

                    itensNovos.Add(novoItem);

                    // Remove o item do estoque
                    new QEstoque().Gravar(new TB_EST_ESTOQUE
                    {
                        DT = Conexao.DataHora,
                        ID_EMPRESA = nota.ID_EMPRESA,
                        ID_PRODUTO = novoItem.ID_PRODUTO.Padrao(),
                        QT = item.QT.Padrao() * -1 // Saída
                    }, ref posicaoTransacao);

                    // tributos do item
                    if (novoItemTributos == null || !novoItemTributos.Count.TemValor())
                    {
                        var tributos = Conexao.BancoDados.TB_EST_PRODUTO_TRIBUTOs.Where(a => a.ID_PRODUTO == novoItem.ID_PRODUTO).Select(a => a.TB_FIS_TRIBUTO).ToList();

                        if (tributos == null || !tributos.Count.TemValor())
                            tributos = Conexao.BancoDados.TB_EST_GRUPO_TRIBUTOs.Where(a => a.ID_GRUPO == novoItem.TB_EST_PRODUTO.ID_GRUPO).Select(a => a.TB_FIS_TRIBUTO).ToList();
                        
                        if (tributos != null && tributos.Count.TemValor())
                            novoItemTributos = tributos.Select(a => new TB_FAT_NOTA_ITEM_TRIBUTO
                            {
                                ID_EMPRESA = nota.ID_EMPRESA,
                                ID_NOTA = nota.ID_NOTA,
                                ID_ITEM = item.ID_ITEM,
                                ID_TRIBUTO = a.ID_TRIBUTO
                            }).ToEntitySet();
                    }

                    if (novoItemTributos == null || !novoItemTributos.Count.TemValor())
                        throw new SYSException(Mensagens.Necessario("os tributos do item"));

                    foreach (var tributo in novoItemTributos)
                    {
                        tributo.ID_EMPRESA = nota.ID_EMPRESA;
                        tributo.ID_NOTA = nota.ID_NOTA;
                        tributo.ID_ITEM = novoItem.ID_ITEM;

                        if (!tributo.ID_TRIBUTO.TemValor())
                            throw new SYSException(Mensagens.Necessario("o tributo do item"));

                        tributo.VL_BASECALCULO = novoItem.VL_SUBTOTAL.Validar();

                        if (nota.TB_CON_EMPRESA.TB_REL_CLIFOR.TB_REL_CLIFOR_X_ENDERECOs == null || nota.TB_CON_EMPRESA.TB_REL_CLIFOR.TB_REL_CLIFOR_X_ENDERECOs.Count == 0)
                            throw new SYSException(Mensagens.Necessario("os endereços da empresa"));

                        var enderecoEmpresa = nota.TB_CON_EMPRESA.TB_REL_CLIFOR.TB_REL_CLIFOR_X_ENDERECOs.FirstOrDefault().TB_REL_ENDERECO;
                        var enderecoCliente = nota.TB_REL_CLIFOR.TB_REL_CLIFOR_X_ENDERECOs.FirstOrDefault().TB_REL_ENDERECO;
                        //var produto = new Cadastros.Estoque.QProduto().Buscar(item.ID_PRODUTO.Padrao()).FirstOrDefault();

                        //tributo.PC_ALIQUOTA = enderecoEmpresa.ID_CIDADE == enderecoCliente.ID_CIDADE ? produto.PC_ALIQUOTA_MUNICIPAL.Padrao() :
                        //                     (enderecoEmpresa.ID_UF == enderecoCliente.ID_UF ? produto.PC_ALIQUOTA_ESTADUAL.Padrao() :
                        //                    ((enderecoEmpresa.ID_PAIS == enderecoCliente.ID_PAIS ? produto.PC_ALIQUOTA_FEDERAL.Padrao() : produto.PC_ALIQUOTA_INTERNACIONAL.Padrao())));

                        //tributo.VL = (tributo.VL_BASECALC.Padrao() / 100) * tributo.PC_ALIQUOTA.Padrao();

                        Conexao.BancoDados.TB_FAT_NOTA_ITEM_TRIBUTOs.InsertOnSubmit(tributo);
                        Conexao.Enviar();
                    }
                }
               
                Conexao.Enviar();

                var configuracaoFinanceiro = Conexao.BancoDados.TB_FIN_CONFIGURACAOs.FirstOrDefault(a => a.ID_CONFIGURACAO_FINANCEIRO == nota.ID_CONFIGURACAO_FINANCEIRO);
                
                var duplicata = new TB_FIN_DUPLICATA
                {
                    ID_EMPRESA = nota.ID_EMPRESA,
                    ID_CLIFOR = nota.ID_CLIFOR.Padrao(),
                    ID_MOEDA = nota.ID_MOEDA.Padrao(),
                    ID_CENTROCUSTO = configuracaoFinanceiro.ID_CENTROCUSTO,
                    ID_CONDICAOPAGAMENTO = configuracaoFinanceiro.ID_CONDICAOPAGAMENTO,
                    ID_DOCUMENTO = nota.ID_NOTA.ToString().Validar(true, 16),
                    VL = itensNovos.Sum(a => a.VL_SUBTOTAL.Padrao()),
                    QT_PARCELAS = configuracaoFinanceiro.QT_PARCELAS,
                    DT_EMISSAO =  Conexao.DataHora,
                    TP = nota.TP_MOVIMENTO == "E" ? "P" : "R"
                };

                new QDuplicata().Gravar(duplicata, itensNovos.Sum(a => a.VL_DESCONTO.Padrao()), 0m, formaPagamento, ref posicaoTransacao);
                
                Conexao.BancoDados.TB_FIN_DUPLICATA_X_NOTAs.InsertOnSubmit(new TB_FIN_DUPLICATA_X_NOTA
                {
                    ID_DUPLICATA = duplicata.ID_DUPLICATA,
                    ID_NOTA = nota.ID_NOTA,
                    ID_EMPRESA = nota.ID_EMPRESA
                });

                var documento = new TB_FIS_DOCUMENTO { ID_EMPRESA = nota.ID_EMPRESA};
                new QDocumento().Gravar(documento, ref posicaoTransacao);

                Conexao.BancoDados.TB_FAT_NOTA_X_DOCUMENTOs.InsertOnSubmit(new TB_FAT_NOTA_X_DOCUMENTO
                {
                    ID_NOTA = nota.ID_NOTA,
                    ID_EMPRESA = nota.ID_EMPRESA,
                    ID_DOCUMENTO = documento.ID_DOCUMENTO
                });

                Conexao.Enviar();

                ID_NOTA = nota.ID_NOTA;

                Conexao.BancoDados.TB_FAT_NOTA_X_CPFs.InsertOnSubmit(new TB_FAT_NOTA_X_CPF
                {
                    ID_EMPRESA = nota.ID_EMPRESA,
                    ID_NOTA = nota.ID_NOTA,
                    CPF = new QClifor().Buscar(nota.ID_CLIFOR ?? 0).ToList()[0].CPF
                });

                Conexao.Enviar();

                Conexao.Finalizar(ref posicaoTransacao);
            }
            catch (Exception excessao)
            {
                Conexao.Voltar(ref posicaoTransacao);
                throw excessao;
            }
        }
    }
}