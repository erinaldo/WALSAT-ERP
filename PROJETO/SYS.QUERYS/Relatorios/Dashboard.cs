using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SYS.QUERYS.Relatorios
{
    public class Dashboard
    {
        public decimal ProdutosCadastrados()
        {
            decimal produto = 0m;

            produto = (from i in Conexao.BancoDados.TB_EST_PRODUTOs
                       select i.ID_PRODUTO).Count();

            return produto;
        }

        public decimal CliforsCadastrados()
        {
            decimal clifor = 0m;

            clifor = (from i in Conexao.BancoDados.TB_REL_CLIFORs
                       select i.ID_CLIFOR).Count();

            return clifor;
        }

        public decimal ValorReceber()
        {
            decimal receber = 0m;

            receber = (from i in Conexao.BancoDados.TB_FIN_DUPLICATAs
                       join y in Conexao.BancoDados.TB_FIN_LIQUIDACAOs on i.ID_DUPLICATA equals y.ID_DUPLICATA
                       where i.TP == "R"
                       select new
                       {
                           Total = i.VL - (y.VL + y.VL_JUROS - y.VL_DESCONTO)
                       }).Sum(p=>p.Total) ?? 0m;

            return receber;
        }

        public decimal ValorPagar()
        {
            decimal receber = 0m;

            receber = (from i in Conexao.BancoDados.TB_FIN_DUPLICATAs
                       join y in Conexao.BancoDados.TB_FIN_LIQUIDACAOs on i.ID_DUPLICATA equals y.ID_DUPLICATA
                       where i.TP == "P"
                       select new
                       {
                           Total = i.VL - (y.VL + y.VL_JUROS - y.VL_DESCONTO)
                       }).Sum(p => p.Total) ?? 0m;

            return receber;
        }
    }
}
