using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SYS.UTILS;

namespace SYS.QUERYS.Cadastros.Gourmet
{
    public class QImpressora 
    {
        public IQueryable<TB_GOU_IMPRESSORA> Buscar(int id_impressora = 0)
        {
            var consulta = from a in Conexao.BancoDados.TB_GOU_IMPRESSORAs
                           select a;

            if (id_impressora.TemValor())
                consulta = consulta.Where(a => a.ID_IMPRESSORA == id_impressora);

            return consulta;
        }

        public void Gravar(TB_GOU_IMPRESSORA impressora, ref int posicaoTransacao)
        {
            try
            {
                Conexao.Iniciar(ref posicaoTransacao);

                var existente = Conexao.BancoDados.TB_GOU_IMPRESSORAs.FirstOrDefault(a => a.ID_IMPRESSORA == impressora.ID_IMPRESSORA);

                #region Inserção
                if (existente == null)
                {
                    impressora.ID_IMPRESSORA = (Conexao.BancoDados.TB_GOU_IMPRESSORAs.Any() ? Conexao.BancoDados.TB_GOU_IMPRESSORAs.Max(a => a.ID_IMPRESSORA) : 0) + 1;
                    Conexao.BancoDados.TB_GOU_IMPRESSORAs.InsertOnSubmit(impressora);
                }
                #endregion

                #region Atualização
                else
                {
                    existente.NM = impressora.NM;
                    existente.ID_IMPRESSORA = impressora.ID_IMPRESSORA;
                    existente.NM_CAMINHO = impressora.NM_CAMINHO;
                    Conexao.Enviar();
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

        public void Deletar(TB_GOU_IMPRESSORA impressora, ref int posicaoTransacao)
        {
            try
            {
                Conexao.Iniciar(ref posicaoTransacao);

                var existente = Conexao.BancoDados.TB_GOU_IMPRESSORAs.FirstOrDefault(a => a.ID_IMPRESSORA == impressora.ID_IMPRESSORA);

                Conexao.BancoDados.TB_GOU_IMPRESSORAs.DeleteOnSubmit(impressora);
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