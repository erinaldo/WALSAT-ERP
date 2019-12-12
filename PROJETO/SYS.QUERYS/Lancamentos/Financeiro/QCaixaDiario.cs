using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SYS.UTILS;

namespace SYS.QUERYS.Lancamentos.Financeiro
{

    public class MCaixaDiario
    {
        public string ID_USUARIO { get; set; }
        public DateTime? DT_INICIAL { get; set; }
        public DateTime? DT_FINAL { get; set; }
        public decimal VALOR { get; set; }

        public MCaixaDiario()
        {
            this.ID_USUARIO = "";
            this.VALOR = 0M;
        }
    }

    public class QCaixaDiario
    {
        public IQueryable<TB_FIN_CAIXA_LANCAMENTO_X_USUARIO> Buscar(string vUsuario = "", string vDataAbertura = "", string vDataFechamento = "")
        {
            var lresult = from i in Conexao.BancoDados.TB_FIN_CAIXA_LANCAMENTO_X_USUARIOs
                          select i;

            if (vUsuario.Length > 0)
                lresult = from i in lresult where i.ID_USUARIO == vUsuario select i;
            
            if (vDataAbertura.Length > 0)
            {
                var data = new DateTime(Convert.ToDateTime(vDataAbertura).Year, Convert.ToDateTime(vDataAbertura).Month, Convert.ToDateTime(vDataAbertura).Day);
                lresult = from i in lresult where i.DT_INICIAL >= Convert.ToDateTime(data) && i.DT_INICIAL < Convert.ToDateTime(data.AddDays(1)) select i;
            }

            if (vDataFechamento.Length > 0)
            {
                var data = new DateTime(Convert.ToDateTime(vDataFechamento).Year, Convert.ToDateTime(vDataFechamento).Month, Convert.ToDateTime(vDataFechamento).Day);
                lresult = from i in lresult where i.DT_FINAL >= Convert.ToDateTime(data) && i.DT_FINAL < Convert.ToDateTime(data.AddDays(1)) select i;
            }

            return lresult;
        }

        public IQueryable<MCaixaDiario> BuscarTotais(string vUsuario = "", string vDataAbertura = "", string vDataFechamento = "")
        { 
            var db = Conexao.BancoDados;

            var lresult = from i in db.TB_FIN_CAIXA_LANCAMENTO_X_USUARIOs
                          select new MCaixaDiario
                          {
                              ID_USUARIO = i.ID_USUARIO,
                              DT_INICIAL = i.DT_INICIAL,
                              DT_FINAL = i.DT_FINAL,
                              VALOR = i.VL_INICIAL ?? 0m
                          };

            if (vUsuario.Length > 0)
                lresult = from i in lresult where i.ID_USUARIO == vUsuario select i;

            if (vDataAbertura.Length > 0)
            {
                var data = new DateTime(Convert.ToDateTime(vDataAbertura).Year, Convert.ToDateTime(vDataAbertura).Month, Convert.ToDateTime(vDataAbertura).Day);
                lresult = from i in lresult where i.DT_INICIAL >= Convert.ToDateTime(data) && i.DT_INICIAL < Convert.ToDateTime(data.AddDays(1)) select i;
            }

            if (vDataFechamento.Length > 0)
            {
                var data = new DateTime(Convert.ToDateTime(vDataFechamento).Year, Convert.ToDateTime(vDataFechamento).Month, Convert.ToDateTime(vDataFechamento).Day);
                lresult = from i in lresult where i.DT_FINAL >= Convert.ToDateTime(data) && i.DT_FINAL < Convert.ToDateTime(data.AddDays(1)) select i;
            }

            return lresult;
        }

        public void Gravar(TB_FIN_CAIXA_LANCAMENTO_X_USUARIO caixa, ref int posicaoTransacao)
        {
            try
            {
                Conexao.Iniciar(ref posicaoTransacao);

                var existente = Conexao.BancoDados.TB_FIN_CAIXA_LANCAMENTO_X_USUARIOs.FirstOrDefault(a => a.ID_USUARIO == caixa.ID_USUARIO && a.DT_INICIAL != null);

                // Abre o caixa geral
                if (existente == null)
                {
                    var caixaGeral = new QCaixaGeral();
                    caixaGeral.Abrir(ref posicaoTransacao);

                    Conexao.BancoDados.TB_FIN_CAIXA_LANCAMENTOs.InsertOnSubmit(new TB_FIN_CAIXA_LANCAMENTO
                    {
                        ID_CAIXA = Conexao.BancoDados.TB_FIN_CAIXAs.Max(p => p.ID_CAIXA),
                        ID_LANCAMENTO = (Conexao.BancoDados.TB_FIN_CAIXA_LANCAMENTOs.Any(a => a.ID_LANCAMENTO == caixa.ID_LANCAMENTO) ? Conexao.BancoDados.TB_FIN_CAIXA_LANCAMENTOs.Where(a => a.ID_LANCAMENTO == caixa.ID_LANCAMENTO).Max(a => a.ID_LANCAMENTO) : 0) + 1,
                        TP_MOVIMENTO = "E",
                        VL = caixa.VL_FINAL == null ? caixa.VL_INICIAL : caixa.VL_FINAL
                    });
                    Conexao.Enviar();

                    var vCaixa = new TB_FIN_CAIXA_LANCAMENTO_X_USUARIO
                    {
                        //(QQuery.BancoDados.TB_EST_GRUPOs.Any(a => a.ID_GRUPO == grupo.ID_GRUPO) ? QQuery.BancoDados.TB_EST_GRUPOs.Where(a => a.ID_GRUPO == grupo.ID_GRUPO).Max(a => a.ID_GRUPO) : 0) + 1;
                        ID_CAIXA = (Conexao.BancoDados.TB_FIN_CAIXA_LANCAMENTO_X_USUARIOs.Any(a => a.ID_CAIXA == caixa.ID_CAIXA) ? Conexao.BancoDados.TB_FIN_CAIXA_LANCAMENTO_X_USUARIOs.Where(a => a.ID_CAIXA == caixa.ID_CAIXA).Max(a => a.ID_CAIXA) : 0) + 1,
                        ID_USUARIO = caixa.ID_USUARIO,
                        ID_LANCAMENTO = (Conexao.BancoDados.TB_FIN_CAIXA_LANCAMENTOs.Any(a => a.ID_LANCAMENTO == caixa.ID_LANCAMENTO) ? Conexao.BancoDados.TB_FIN_CAIXA_LANCAMENTOs.Where(a => a.ID_LANCAMENTO == caixa.ID_LANCAMENTO).Max(a => a.ID_LANCAMENTO) : 0) + 1,
                        VL_INICIAL = caixa.VL_INICIAL,
                        VL_FINAL = caixa.VL_FINAL,
                        DT_INICIAL = caixa.DT_INICIAL,
                        DT_FINAL = caixa.DT_FINAL
                    };
                    Conexao.BancoDados.TB_FIN_CAIXA_LANCAMENTO_X_USUARIOs.InsertOnSubmit(vCaixa);
                }
                else
                {
                    existente.VL_FINAL = caixa.VL_FINAL;
                    existente.DT_FINAL = caixa.DT_FINAL; 
                }
                    


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
