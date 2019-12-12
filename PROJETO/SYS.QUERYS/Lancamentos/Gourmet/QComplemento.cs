using System;
using SYS.QUERYS;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SYS.UTILS;

namespace SYS.QUERYS.Lancamentos.Gourmet
{
    public class ListaComplemento : List<MComplemento>
    { }

    public class MComplemento
    {
        public int ID_COMPLEMENTO { get; set; }
        public int ID_ITEMPEDIDO { get; set; }
        public int ID_PRODUTO_PEDIDO { get; set; }
        public int ID_PRODUTO { get; set; }
        public string NM_PRODUTO { get; set; }
        public decimal QUANTIDADE { get; set; }
        public decimal VALOR { get; set; }

        public MComplemento()
        {
            this.ID_PRODUTO_PEDIDO = 0;
            this.ID_PRODUTO = 0;
            this.NM_PRODUTO = "";
            this.QUANTIDADE = 0m;
            this.VALOR = 0m;
        }
    }

    public class QComplemento 
    {
        public List<MComplemento> Busca(int vCD_Produto, int vCD_Grupo)
        {
            var lresult = from i in Conexao.BancoDados.TB_GOU_PRODUTO_X_GRUPOs
                          join x in Conexao.BancoDados.TB_EST_GRUPOs on i.ID_GRUPO equals x.ID_GRUPO
                          join y in Conexao.BancoDados.TB_EST_PRODUTOs on i.ID_GRUPO equals y.ID_GRUPO
                          where i.ID_PRODUTO == vCD_Produto
                          && i.ID_GRUPO == vCD_Grupo
                          let preco = Conexao.BancoDados.TB_EST_PRECOs.FirstOrDefault(p => p.ID_PRODUTO == y.ID_PRODUTO && p.ST_ATIVO.Padrao()).VL_PRECO ?? 0m
                          select new MComplemento
                          {
                              ID_PRODUTO = y.ID_PRODUTO,
                              NM_PRODUTO = y.NM,
                              VALOR = preco
                          };

            return lresult.ToList();
        }
    }


}
