using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SYS.UTILS;
using SYS.QUERYS.Cadastros.Financeiro;

namespace SYS.QUERYS.Lancamentos.Financeiro
{
    public class QDuplicata 
    {
        public IQueryable<TB_FIN_DUPLICATA> Buscar(int id_duplicata = 0)
        {
            var consulta = from a in Conexao.BancoDados.TB_FIN_DUPLICATAs
                           select a;

            if (id_duplicata > 0)
                consulta = consulta.Where(a => a.ID_DUPLICATA == id_duplicata);

            return consulta;
        }

        public void Gravar(TB_FIN_DUPLICATA duplicata, decimal valorDesconto, decimal valorJuros, int? formaPagamento, ref int posicaoTransacao)
        {
            try
            {
                Conexao.Iniciar(ref posicaoTransacao);

                if (duplicata.ID_DUPLICATA.ToString().Length > 0)
                    Deletar(duplicata,ref posicaoTransacao);

                duplicata.ID_DUPLICATA = duplicata.ID_DUPLICATA.TemValor() ? duplicata.ID_DUPLICATA : ((Conexao.BancoDados.TB_FIN_DUPLICATAs.Any() ? Conexao.BancoDados.TB_FIN_DUPLICATAs.Max(a => a.ID_DUPLICATA) : 0) + 1);

                if (!duplicata.ID_CONDICAOPAGAMENTO.TemValor())
                    throw new SYSException(Mensagens.Necessario("informar a condição de pagamento"));

                Conexao.BancoDados.TB_FIN_DUPLICATAs.InsertOnSubmit(duplicata);
                Conexao.Enviar();

                duplicata.TB_FIN_PARCELAs = new System.Data.Linq.EntitySet<TB_FIN_PARCELA>();

                var condicaoPagamento = new QCondicaoPagamento().Buscar(duplicata.ID_CONDICAOPAGAMENTO.Padrao()).FirstOrDefault();

                for (var i = 0; i < (duplicata.QT_PARCELAS ?? 1); i++)
                {
                    var data = condicaoPagamento.QT_DIASDESDOBRO.Padrao() == 0 ? Conexao.Data : Conexao.Data.AddDays(Convert.ToDouble(condicaoPagamento.QT_DIASDESDOBRO.Padrao()) * (i + 1));

                    var parcela = new TB_FIN_PARCELA
                    {
                        ID_PARCELA = i + 1,
                        ID_DUPLICATA = duplicata.ID_DUPLICATA,
                        ID_EMPRESA = duplicata.ID_EMPRESA,
                        VL = duplicata.VL / duplicata.QT_PARCELAS,
                        DT_VENCIMENTO = data,
                        DT_PAGAMENTO = data == Conexao.Data ? Conexao.DataHora : (DateTime?)null
                    };
                    
                    Conexao.BancoDados.TB_FIN_PARCELAs.InsertOnSubmit(parcela);

                    Conexao.Enviar();

                    if (parcela.DT_VENCIMENTO == Conexao.Data)
                        new QLiquidacao().Gravar(parcela, valorDesconto, valorJuros, formaPagamento, ref posicaoTransacao);

                    Conexao.Enviar();
                }

                Conexao.Finalizar(ref posicaoTransacao);
            }
            catch (Exception excessao)
            {
                Conexao.Voltar(ref posicaoTransacao);
                throw excessao;
            }
        }

        public void Deletar(TB_FIN_DUPLICATA duplicata, ref int posicaoTransacao)
        {
            try
            {
                Conexao.Iniciar(ref posicaoTransacao);

                var parcelas = (from i in Conexao.BancoDados.TB_FIN_PARCELAs
                               where i.ID_DUPLICATA == duplicata.ID_DUPLICATA
                               select i).ToList();

                for (int i = 0; i < parcelas.Count(); i++)
                {
                    Conexao.BancoDados.TB_FIN_PARCELAs.DeleteOnSubmit(parcelas[i]);
                    Conexao.Enviar();
                }

                var existente = Conexao.BancoDados.TB_FIN_DUPLICATAs.FirstOrDefault(a => a.ID_DUPLICATA == duplicata.ID_DUPLICATA);
                if (existente != null)
                    Conexao.BancoDados.TB_FIN_DUPLICATAs.DeleteOnSubmit(existente);

                Conexao.Enviar();

                Conexao.Finalizar(ref posicaoTransacao);
            }
            catch (Exception excessao)
            {
                Conexao.Voltar(ref posicaoTransacao);
                throw excessao;
            }
        }

        public decimal BuscaTotalLiquidado(int ID_NOTA)
        {
            decimal saldo = 0m;

            var lresult = (from i in Conexao.BancoDados.TB_FIN_DUPLICATAs
                           join x in Conexao.BancoDados.TB_FIN_PARCELAs on i.ID_DUPLICATA equals x.ID_DUPLICATA
                           join y in Conexao.BancoDados.TB_FIN_LIQUIDACAOs on new { x.ID_PARCELA, i.ID_DUPLICATA } equals new { y.ID_PARCELA, y.ID_DUPLICATA }
                           join w in Conexao.BancoDados.TB_FIN_DUPLICATA_X_NOTAs on new { ID_NOTA = i.ID_DOCUMENTO, ID_EMPRESA = i.ID_EMPRESA } equals new { ID_NOTA = w.ID_NOTA.ToString(), ID_EMPRESA = w.ID_EMPRESA }
                           where w.ID_NOTA == ID_NOTA
                           select y).ToList();


            for (int i = 0; i < lresult.Count; i++)
                saldo += lresult[i].VL ?? 0m;


            return saldo;
        }

        public decimal BuscaTotalDesconto(int ID_NOTA)
        {
            decimal desc = 0m;

            var lresult = (from i in Conexao.BancoDados.TB_FIN_DUPLICATAs
                           join x in Conexao.BancoDados.TB_FIN_PARCELAs on i.ID_DUPLICATA equals x.ID_DUPLICATA
                           join y in Conexao.BancoDados.TB_FIN_LIQUIDACAOs on new { x.ID_PARCELA, i.ID_DUPLICATA } equals new { y.ID_PARCELA, y.ID_DUPLICATA }
                           join w in Conexao.BancoDados.TB_FIN_DUPLICATA_X_NOTAs on new { ID_NOTA = i.ID_DOCUMENTO, ID_EMPRESA = i.ID_EMPRESA } equals new { ID_NOTA = w.ID_NOTA.ToString(), ID_EMPRESA = w.ID_EMPRESA }
                           where w.ID_NOTA == ID_NOTA
                           select y).ToList();


            for (int i = 0; i < lresult.Count; i++)
                desc += lresult[i].VL_DESCONTO ?? 0m;


            return desc;
        }

        public decimal BuscaTotalAprazo(int ID_NOTA)
        {
            var lresult = (from i in Conexao.BancoDados.TB_FIN_DUPLICATAs
                           join x in Conexao.BancoDados.TB_FIN_PARCELAs on i.ID_DUPLICATA equals x.ID_DUPLICATA
                           join y in Conexao.BancoDados.TB_FIN_CONDICAOPAGAMENTOs on i.ID_CONDICAOPAGAMENTO equals y.ID_CONDICAOPAGAMENTO
                           join w in Conexao.BancoDados.TB_FIN_DUPLICATA_X_NOTAs on new { ID_NOTA = i.ID_DOCUMENTO, ID_EMPRESA = i.ID_EMPRESA } equals new { ID_NOTA = w.ID_NOTA.ToString(), ID_EMPRESA = w.ID_EMPRESA }
                           where w.ID_NOTA == ID_NOTA
                           && y.QT_DIASDESDOBRO > 1
                           select new
                           {
                               TotalPrazo = i.VL ?? 0m
                           }).ToList().Sum(p => p.TotalPrazo);

            return lresult;
        }
    }
}
