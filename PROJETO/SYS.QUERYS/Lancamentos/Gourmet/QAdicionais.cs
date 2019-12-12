using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SYS.UTILS;

namespace SYS.QUERYS.Lancamentos.Gourmet
{
    public class ListaAdicionais : List<MAdicionais>
    { }

    public class MAdicionais
    {
        public int ID_ITEMADICIONAL { get; set; }
        public int ID_ITEMPEDIDO { get; set; }
        public int ID_ADICIONAL { get; set; }
        public int ID_GRUPO { get; set; }
        public string NM_ADICIONAL { get; set; }
        public string ST_TIPO { get; set; }

        public MAdicionais()
        {
            this.ID_ITEMADICIONAL = 0;
            this.ID_ITEMPEDIDO = 0;
            this.ID_ADICIONAL = 0;
            this.NM_ADICIONAL = "";
            this.ST_TIPO = "";
            this.ID_GRUPO = 0;
        }
    }  

    public class QAdicionais 
    {
        public List<TB_EST_GRUPO_ADICIONAI> BuscarAdicionais(string codigo, string tipo)
        {

            var lresult = from i in Conexao.BancoDados.TB_EST_GRUPO_ADICIONAIs                          
                          select i;
            
            var codigoTratado = codigo.Validar().ToInt32().Padrao();
            if (codigoTratado.TemValor())
                lresult = from i in lresult where i.ID_GRUPO == codigoTratado select i;

            tipo.Validar(true);
            if (tipo.TemValor())
                lresult = from i in lresult where i.TP == tipo select i;

            return lresult.ToList();
        }
    }
}
