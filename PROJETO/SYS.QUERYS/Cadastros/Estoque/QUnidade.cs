using System.Collections.Generic;

namespace SYS.QUERYS.Cadastros.Estoque
{
    public class QUnidade
    {
        public List<MUnidade> Unidades
        {
            get
            {
                return new List<MUnidade>
                {
                    new MUnidade {ID = 01, NM = "BILHÃO DE UNIDADE INTERNACIONAL" },
                    new MUnidade {ID = 02, NM = "DÚZIA" },
                    new MUnidade {ID = 03, NM = "GRAMA" },
                    new MUnidade {ID = 04, NM = "LITRO" },
                    new MUnidade {ID = 05, NM = "MEGAWATT HORA" },
                    new MUnidade {ID = 06, NM = "METRO" },
                    new MUnidade {ID = 07, NM = "METRO CÚBICO" },
                    new MUnidade {ID = 08, NM = "METRO QUADRADO" },
                    new MUnidade {ID = 09, NM = "MIL UNIDADES" },
                    new MUnidade {ID = 10, NM = "PARES" },
                    new MUnidade {ID = 11, NM = "QUILATES" },
                    new MUnidade {ID = 12, NM = "QUILOGRAMA BRUTO" },
                    new MUnidade {ID = 13, NM = "QUILOGRAMA LÍQUIDO" },
                    new MUnidade {ID = 14, NM = "TONELADA MÉTRICA LÍQUIDA" },
                    new MUnidade {ID = 15, NM = "UNIDADE" }
                };
            }
        }
    }

    public class MUnidade
    {
        public int ID { get; set; }
        public string NM { get; set; }
    }
}