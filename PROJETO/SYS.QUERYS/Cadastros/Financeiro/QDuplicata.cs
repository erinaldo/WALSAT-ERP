using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SYS.QUERYS.Cadastros.Financeiro
{
    public class QDuplicata : QQuery
    {
        public IQueryable<TB_FIN_DUPLICATA> Buscar(int id_duplicata = 0)
        {
            var consulta = from a in BancoDados.TB_FIN_DUPLICATAs
                           select a;

            if (id_duplicata > 0)
                consulta = consulta.Where(a => a.ID_DUPLICATA == id_duplicata);

            return consulta;
        }

        public decimal _ID_Parcela = 0m;
        public decimal _ID_Duplicata = 0m;

        public void Gravar(TB_FIN_DUPLICATA duplicata)
        {
            try
            {
                Iniciar();

                var existente = BancoDados.TB_FIN_DUPLICATAs.FirstOrDefault(a => a.ID_DUPLICATA == duplicata.ID_DUPLICATA);

                #region Inserção

                if (existente == null)
                {
                    duplicata.ID_DUPLICATA = (BancoDados.TB_FIN_DUPLICATAs.Any() ? BancoDados.TB_FIN_DUPLICATAs.Max(a => a.ID_DUPLICATA) : 0) + 1;
                    BancoDados.TB_FIN_DUPLICATAs.InsertOnSubmit(duplicata);

                    _ID_Duplicata = duplicata.ID_DUPLICATA;

                    for (int i = 0; i < duplicata.TB_FIN_PARCELAs.Count; i++)
                    {
                        duplicata.TB_FIN_PARCELAs[i].ID_PARCELA = (BancoDados.TB_FIN_PARCELAs.Any() ? BancoDados.TB_COM_PEDIDO_ITEMs.Max(a => a.ID_ITEM) : 0) + 1;
                        duplicata.TB_FIN_PARCELAs[i].ID_DUPLICATA = duplicata.ID_DUPLICATA;
                        duplicata.TB_FIN_PARCELAs[i].VL = duplicata.VL / duplicata.QT_PARCELAS;
                        duplicata.TB_FIN_PARCELAs[i].DT_VENCIMENTO = duplicata.TB_FIN_CONDICAOPAGAMENTO.QT_DIASDESDOBRO == 1 ? DataHora : DataHora.AddDays(Convert.ToDouble(duplicata.TB_FIN_CONDICAOPAGAMENTO.QT_DIASDESDOBRO) * i);                        
                        BancoDados.TB_FIN_PARCELAs.InsertOnSubmit(duplicata.TB_FIN_PARCELAs[i]);
                        _ID_Parcela = duplicata.TB_FIN_PARCELAs[i].ID_PARCELA;
                    }

                }

                #endregion

                #region Atualização

                else
                {
                    existente.ID_EMPRESA = duplicata.ID_EMPRESA;
                    existente.ID_CLIFOR = duplicata.ID_CLIFOR;
                    existente.ID_CONDICAOPAGAMENTO = duplicata.ID_CONDICAOPAGAMENTO;
                    existente.ID_CENTROCUSTO = duplicata.ID_CENTROCUSTO;
                    existente.ID_DOCUMENTO = duplicata.ID_DOCUMENTO;
                    existente.QT_PARCELAS = duplicata.QT_PARCELAS;
                    existente.VL = duplicata.VL;
                    existente.DT_EMISSAO = duplicata.DT_EMISSAO;
                    existente.TP = duplicata.TP;

                    Enviar();

                    for (int i = 0; i < duplicata.TB_FIN_PARCELAs.Count; i++)
                    {
                        duplicata.TB_FIN_PARCELAs[i].ID_PARCELA = duplicata.TB_FIN_PARCELAs[i].ID_PARCELA;
                        duplicata.TB_FIN_PARCELAs[i].ID_DUPLICATA = duplicata.ID_DUPLICATA;
                        duplicata.TB_FIN_PARCELAs[i].VL = duplicata.VL / duplicata.QT_PARCELAS;
                        duplicata.TB_FIN_PARCELAs[i].DT_VENCIMENTO = duplicata.TB_FIN_CONDICAOPAGAMENTO.QT_DIASDESDOBRO == 1 ? DataHora : DataHora.AddDays(Convert.ToDouble(duplicata.TB_FIN_CONDICAOPAGAMENTO.QT_DIASDESDOBRO) * i);
                        BancoDados.TB_FIN_PARCELAs.InsertOnSubmit(duplicata.TB_FIN_PARCELAs[i]);
                        _ID_Parcela = duplicata.TB_FIN_PARCELAs[i].ID_PARCELA;
                    }                    
                }

                
                #endregion

                Enviar();

                Finalizar();
            }
            catch (Exception excessao)
            {
                Voltar();
                throw excessao;
            }
        }

        public void Deletar(TB_FIN_DUPLICATA duplicata)
        {
            try
            {
                Iniciar();

                var parcelas = (from i in BancoDados.TB_FIN_PARCELAs
                               where i.ID_DUPLICATA == duplicata.ID_DUPLICATA
                               select i).ToList();

                for (int i = 0; i < parcelas.Count(); i++)
                {
                    BancoDados.TB_FIN_PARCELAs.DeleteOnSubmit(parcelas[i]);
                    Enviar();
                }

                var existente = BancoDados.TB_FIN_DUPLICATAs.FirstOrDefault(a => a.ID_DUPLICATA == duplicata.ID_DUPLICATA);
                if (existente != null)
                    BancoDados.TB_FIN_DUPLICATAs.DeleteOnSubmit(existente);

                Enviar();

                Finalizar();
            }
            catch (Exception excessao)
            {
                Voltar();
                throw excessao;
            }
        }

        public decimal BuscaTotalLiquidado(decimal NrPedido)
        {
            decimal saldo = 0m;

            var lresult = (from i in BancoDados.TB_FIN_DUPLICATAs
                           join x in BancoDados.TB_FIN_PARCELAs on i.ID_DUPLICATA equals x.ID_DUPLICATA
                           join y in BancoDados.TB_FIN_LIQUIDACAOs on new { x.ID_PARCELA, i.ID_DUPLICATA } equals new { y.ID_PARCELA, y.ID_DUPLICATA }
                           where i.ID_DOCUMENTO == NrPedido.ToString()
                           select y).ToList();


            for (int i = 0; i < lresult.Count; i++)
                saldo += lresult[i].VL ?? 0m;


            return saldo;
        }

        public decimal BuscaTotalDesconto(decimal NrPedido)
        {
            decimal desc = 0m;

            var lresult = (from i in BancoDados.TB_FIN_DUPLICATAs
                           join x in BancoDados.TB_FIN_PARCELAs on i.ID_DUPLICATA equals x.ID_DUPLICATA
                           join y in BancoDados.TB_FIN_LIQUIDACAOs on new { x.ID_PARCELA, i.ID_DUPLICATA } equals new { y.ID_PARCELA, y.ID_DUPLICATA }
                           where i.ID_DOCUMENTO == NrPedido.ToString()
                           select y).ToList();


            for (int i = 0; i < lresult.Count; i++)
                desc += lresult[i].VL_DESCONTO ?? 0m;


            return desc;
        }

        public decimal BuscaTotalAprazo(decimal NrPedido)
        {
            var lresult = (from i in BancoDados.TB_FIN_DUPLICATAs
                           join x in BancoDados.TB_FIN_PARCELAs on i.ID_DUPLICATA equals x.ID_DUPLICATA
                           join y in BancoDados.TB_FIN_CONDICAOPAGAMENTOs on i.ID_CONDICAOPAGAMENTO equals y.ID_CONDICAOPAGAMENTO
                           where i.ID_DOCUMENTO == NrPedido.ToString()
                           && y.QT_DIASDESDOBRO > 1
                           select new
                           {
                               TotalPrazo = i.VL ?? 0m
                           }).ToList().Sum(p => p.TotalPrazo);

            return lresult;
        }
    }
}
