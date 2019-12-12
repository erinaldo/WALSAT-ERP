using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SYS.UTILS;
using System.Threading.Tasks;

namespace SYS.QUERYS.Cadastros.Configuracao
{
    public class QRegraEspecial
    {
        public IQueryable<TB_CON_REGRAESPECIAL> BuscarRegraEspecial(string id_usuario,string senha)
        {

            var consulta = from a in Conexao.BancoDados.TB_CON_REGRAESPECIALs
                           select a;

            consulta = from a in consulta
                       join b in Conexao.BancoDados.TB_CON_USUARIO_X_REGRAESPECIALs on a.ID_REGRA equals b.ID_REGRA
                       join c in Conexao.BancoDados.TB_CON_USUARIOs on b.ID_USUARIO equals c.ID_USUARIO
                       where b.ID_USUARIO == id_usuario && c.SENHA == senha
                       select a;

            return consulta;
        }
    }
}
