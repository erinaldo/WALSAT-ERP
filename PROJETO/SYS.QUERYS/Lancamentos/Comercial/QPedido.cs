using FastReport;
using SYS.QUERYS.Lancamentos.Gourmet;
using SYS.UTILS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace SYS.QUERYS.Lancamentos.Comercial
{
    public class QPedido
    {
        public int ID_PEDIDO = 0;
        public int ID_NOTA = 0;

        public IQueryable<TB_COM_PEDIDO> Buscar(int id_pedido = 0)
        {
            var consulta = from a in Conexao.BancoDados.TB_COM_PEDIDOs
                           select a;

            if (id_pedido.TemValor())
                consulta = consulta.Where(a => a.ID_PEDIDO == id_pedido);

            return consulta;
        }        

        public void Gravar(MPedido pedido, bool gerarNotaFiscal, ref int posicaoTransacao, List<MPedidoItem> transferencia = null)
        {
            try
            {
                Conexao.Iniciar(ref posicaoTransacao);

                if(transferencia != null)
                    for (int i = 0; i < transferencia.Count; i++)                
                        DeletarItemParaTransferir(transferencia[i], ref posicaoTransacao);

                var existente = Conexao.BancoDados.TB_COM_PEDIDOs.FirstOrDefault(a => a.ID_PEDIDO == pedido.ID_PEDIDO);

                #region Inserção

                if (existente == null)
                {
                    pedido.ID_PEDIDO = (Conexao.BancoDados.TB_COM_PEDIDOs.Any() ? Conexao.BancoDados.TB_COM_PEDIDOs.Max(a => a.ID_PEDIDO) : 0) + 1;

                    var vPedido = new TB_COM_PEDIDO();
                    vPedido.ID_PEDIDO = pedido.ID_PEDIDO;
                    vPedido.ID_PEDIDO_ORIGEM = pedido.ID_PEDIDO_ORIGEM == 0 ? null : pedido.ID_PEDIDO_ORIGEM;
                    vPedido.ID_EMPRESA = pedido.ID_EMPRESA;
                    vPedido.ID_USUARIO = pedido.ID_USUARIO;
                    vPedido.ID_CLIFOR = pedido.ID_CLIFOR;
                    vPedido.ST_PEDIDO = pedido.ST_PEDIDO;
                    vPedido.ST_ATIVO = pedido.ST_ATIVO;
                    vPedido.DT_CADASTRO = Conexao.DataHora;
                    vPedido.TP_MOVIMENTO = pedido.TP_MOVIMENTO;
                    Conexao.BancoDados.TB_COM_PEDIDOs.InsertOnSubmit(vPedido);

                    Conexao.Enviar();
                }

                #endregion

                #region Atualização

                else
                {
                    existente.ID_EMPRESA = pedido.ID_EMPRESA;
                    existente.ID_CLIFOR = pedido.ID_CLIFOR;
                    existente.ST_ATIVO = pedido.ST_ATIVO;
                    existente.ID_PEDIDO_ORIGEM = pedido.ID_PEDIDO_ORIGEM == 0 ? null : pedido.ID_PEDIDO_ORIGEM;
                    existente.ST_PEDIDO = pedido.ST_PEDIDO;
                    existente.TP_MOVIMENTO = pedido.TP_MOVIMENTO;

                    Conexao.Enviar();
                }

                ID_PEDIDO = pedido.ID_PEDIDO;

                for (int i = 0; i < pedido.Itens.Count; i++)
                    GravaPedidoItem(pedido, pedido.Itens[i]);

                if (gerarNotaFiscal)
                {
                    //GRAVA NOTA FISCAL
                    var nota = new TB_FAT_NOTA();
                    nota.ID_CLIFOR = pedido.ID_CLIFOR;
                    nota.ID_EMPRESA = pedido.ID_EMPRESA;
                    nota.ID_USUARIO = pedido.ID_USUARIO;
                    nota.TP_MOVIMENTO = pedido.TP_MOVIMENTO;
                    nota.DT_ENTRADASAIDA = pedido.DT_CADASTRO;
                    nota.ID_NOTA_REFERENCIA = pedido.ID_PEDIDO;

                    // Revisar
                    nota.ID_CONFIGURACAO_FISCAL = 1;
                    nota.ID_CONFIGURACAO_FINANCEIRO = 1;

                    for (int i = 0; i < pedido.Itens.Count; i++)
                    {
                        nota.TB_FAT_NOTA_ITEMs.Add(new TB_FAT_NOTA_ITEM
                        {
                            ID_PRODUTO = pedido.Itens[i].ID_PRODUTO,
                            QT = pedido.Itens[i].QUANTIDADE,
                            VL_UNITARIO = pedido.Itens[i].VL_UNITARIO,
                            VL_DESCONTO = pedido.Itens[i].VL_DESCONTO,
                            ID_EMPRESA = pedido.ID_EMPRESA
                        });
                    }

                    var referencia = new QNota();
                    referencia.Gravar(nota, pedido.CONDICAO_PAGAMENTO, ref posicaoTransacao);

                    ID_NOTA = referencia.ID_NOTA;
                }
                #endregion

                Conexao.Enviar();

                Conexao.Finalizar(ref posicaoTransacao);                
            }
            catch (Exception excessao)
            {
                Conexao.Voltar(ref posicaoTransacao);
                throw excessao;
            }
        }

        public void GravaPedidoItem(MPedido pedido, MPedidoItem Itens)
        {
            var existenteItem = Conexao.BancoDados.TB_COM_PEDIDO_ITEMs.FirstOrDefault(a => a.ID_PEDIDO == pedido.ID_PEDIDO && a.ID_ITEM == Itens.ID_ITEM);

            if (existenteItem == null)
            {

                Itens.ID_ITEM = (Conexao.BancoDados.TB_COM_PEDIDO_ITEMs.Any() ? Conexao.BancoDados.TB_COM_PEDIDO_ITEMs.Max(a => a.ID_ITEM) : 0) + 1;

                var vItens = new TB_COM_PEDIDO_ITEM();
                vItens.ID_PEDIDO = pedido.ID_PEDIDO;
                vItens.ID_EMPRESA = pedido.ID_EMPRESA;
                vItens.ID_ITEM = Itens.ID_ITEM;
                vItens.ID_PRODUTO = Itens.ID_PRODUTO;
                vItens.VL_UNITARIO = Itens.VL_UNITARIO;
                vItens.QT = Itens.QUANTIDADE;
                vItens.VL_DESCONTO = Itens.VL_DESCONTO;
                vItens.VL_ACRESCIMO = Itens.VL_ACRESCIMO;
                vItens.VL_SUBTOTAL = Itens.VL_SUBTOTAL;
                vItens.DS_OBSERVACAO = Itens.OBS;
                vItens.ST_ATIVO = Itens.ST_ATIVO;

                Conexao.BancoDados.TB_COM_PEDIDO_ITEMs.InsertOnSubmit(vItens);
                Conexao.Enviar();
            }
            else
            {
                existenteItem.ID_EMPRESA = pedido.ID_EMPRESA;
                existenteItem.ID_PRODUTO = Itens.ID_PRODUTO;
                existenteItem.VL_UNITARIO = Itens.VL_UNITARIO;
                existenteItem.QT = Itens.QUANTIDADE;
                existenteItem.VL_DESCONTO = Itens.VL_DESCONTO;
                existenteItem.VL_ACRESCIMO = Itens.VL_ACRESCIMO;
                existenteItem.VL_SUBTOTAL = Itens.VL_SUBTOTAL;
                existenteItem.DS_OBSERVACAO = Itens.OBS;
                existenteItem.ST_ATIVO = Itens.ST_ATIVO;

                Conexao.Enviar();
            }
        }

        public void DeletarItemParaTransferir(MPedidoItem item, ref int posicaoTransacao)
        {
            try
            {
                Conexao.Iniciar(ref posicaoTransacao);

                var itemGouCom = Conexao.BancoDados.TB_GOU_PEDIDO_ITEM_COMPLEMENTOs.Where(p => p.ID_PEDIDO == item.ID_PEDIDO && p.ID_ITEM == item.ID_ITEM).ToList();
                if (itemGouCom.Count > 0)
                    Conexao.BancoDados.TB_GOU_PEDIDO_ITEM_COMPLEMENTOs.DeleteAllOnSubmit(itemGouCom);
                Conexao.Enviar();

                var itemGouAdi = Conexao.BancoDados.TB_GOU_PEDIDO_ITEM_ADICIONALs.Where(p => p.ID_PEDIDO == item.ID_PEDIDO && p.ID_ITEM == item.ID_ITEM).ToList();
                if (itemGouAdi.Count > 0)
                    Conexao.BancoDados.TB_GOU_PEDIDO_ITEM_ADICIONALs.DeleteAllOnSubmit(itemGouAdi);
                Conexao.Enviar();

                var itemGouIte = Conexao.BancoDados.TB_GOU_PEDIDO_ITEMs.Where(p => p.ID_PEDIDO == item.ID_PEDIDO && p.ID_ITEM == item.ID_ITEM).ToList();
                if (itemGouIte.Count > 0)
                    Conexao.BancoDados.TB_GOU_PEDIDO_ITEMs.DeleteAllOnSubmit(itemGouIte);
                Conexao.Enviar();

                var itemComIte = Conexao.BancoDados.TB_COM_PEDIDO_ITEMs.Where(p => p.ID_PEDIDO == item.ID_PEDIDO && p.ID_ITEM == item.ID_ITEM).ToList();
                if (itemComIte.Count > 0)
                    Conexao.BancoDados.TB_COM_PEDIDO_ITEMs.DeleteAllOnSubmit(itemComIte);
                Conexao.Enviar();

                Conexao.Finalizar(ref posicaoTransacao);
            }
            catch (Exception excessao)
            {
                Conexao.Voltar(ref posicaoTransacao);
                throw excessao;
            }
        }

        public void Deletar(TB_COM_PEDIDO pedido, ref int posicaoTransacao)
        {
            try
            {
                Conexao.Iniciar(ref posicaoTransacao);

                var existente = Conexao.BancoDados.TB_COM_PEDIDOs.FirstOrDefault(a => a.ID_PEDIDO == pedido.ID_PEDIDO);
                if (existente != null)
                    existente.ST_ATIVO = false;

                Conexao.Enviar();

                Conexao.Finalizar(ref posicaoTransacao);
            }
            catch (Exception excessao)
            {
                Conexao.Voltar(ref posicaoTransacao);
                throw excessao;
            }
        }

        public void ImprimirConsumacao(int ID_Pedido)
        {
            Report Relatorio = new Report();
            var db = Conexao.BancoDados;                

            Relatorio.Load(UTILS.Parametros.CaminhoPath() + "\\ExtratoConsumo.frx");

            var Dados = (from i in db.TB_COM_PEDIDOs
                         where i.ID_PEDIDO == ID_Pedido
                         select i).AsParallel();

            var caminhoImpressora = db.TB_GOU_IMPRESSORAs.FirstOrDefault().NM_CAMINHO ?? "";
            
            Relatorio.RegisterData(Dados, "Dados_Pedido");
            PrintSettings config = new PrintSettings();
            config.Printer = @"" + caminhoImpressora;
            config.ShowDialog = false;
            config.PrintMode = PrintMode.Split;
            Relatorio.PrintSettings.Assign(config);
            Relatorio.Prepare();
            Relatorio.Print();
        }
    }
}