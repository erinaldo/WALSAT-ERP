using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SYS.UTILS;

namespace SYS.QUERYS.Cadastros.Gourmet
{
    public class QAmbiente 
    {
        public IQueryable<TB_GOU_AMBIENTE> Buscar(int id_ambiente = 0)
        {
            var consulta = from a in Conexao.BancoDados.TB_GOU_AMBIENTEs
                           select a;

            if (id_ambiente > 0)
                consulta = consulta.Where(a => a.ID_AMBIENTE == id_ambiente);

            return consulta;
        }

        public void Gravar(TB_GOU_AMBIENTE ambiente, ref int posicaoTransacao)
        {
            try
            {
                Conexao.Iniciar(ref posicaoTransacao);

                var existente = Conexao.BancoDados.TB_GOU_AMBIENTEs.FirstOrDefault(a => a.ID_AMBIENTE == ambiente.ID_AMBIENTE);

                #region Inserção
                if (existente == null)
                {
                    ambiente.ID_AMBIENTE = (Conexao.BancoDados.TB_GOU_AMBIENTEs.Any() ? Conexao.BancoDados.TB_GOU_AMBIENTEs.Max(a => a.ID_AMBIENTE) : 0) + 1;
                    Conexao.BancoDados.TB_GOU_AMBIENTEs.InsertOnSubmit(ambiente);
                }
                #endregion

                #region Atualização
                else
                {
                    existente.NM = ambiente.NM;
                    existente.ID_AMBIENTE = ambiente.ID_AMBIENTE;
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

        public void Deletar(TB_GOU_AMBIENTE ambiente, ref int posicaoTransacao)
        {
            try
            {
                Conexao.Iniciar(ref posicaoTransacao);

                var existente = Conexao.BancoDados.TB_GOU_AMBIENTEs.FirstOrDefault(a => a.ID_AMBIENTE == ambiente.ID_AMBIENTE);

                Conexao.BancoDados.TB_GOU_AMBIENTEs.DeleteOnSubmit(ambiente);
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