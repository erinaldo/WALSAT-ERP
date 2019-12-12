using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SYS.UTILS;

namespace SYS.QUERYS.Cadastros.Gourmet
{
    public class QMesa 
    {
        public IQueryable<TB_GOU_MESA> Buscar(int id_mesa = 0)
        {
            var consulta = from a in Conexao.BancoDados.TB_GOU_MESAs
                           select a;

            if (id_mesa.TemValor())
                consulta = consulta.Where(a => a.ID_MESA == id_mesa);

            return consulta;
        }

        public void Gravar(TB_GOU_MESA mesa, ref int posicaoTransacao)
        {
            try
            {
                Conexao.Iniciar(ref posicaoTransacao);

                var existente = Conexao.BancoDados.TB_GOU_MESAs.FirstOrDefault(a => a.ID_MESA == mesa.ID_MESA);

                #region Inserção
                if (existente == null)
                {
                    Conexao.BancoDados.TB_GOU_MESAs.InsertOnSubmit(mesa);
                }
                #endregion

                #region Atualização
                else
                {
                    existente.ID_MESA = mesa.ID_MESA;
                    existente.NM = mesa.NM;
                    existente.ST_ATIVO = mesa.ST_ATIVO;
                    existente.ID_AMBIENTE = mesa.ID_AMBIENTE;
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

        public void Deletar(TB_GOU_MESA mesa, ref int posicaoTransacao)
        {
            try
            {
                Conexao.Iniciar(ref posicaoTransacao);

                var existente = Conexao.BancoDados.TB_GOU_MESAs.FirstOrDefault(a => a.ID_MESA == mesa.ID_MESA);

                Conexao.BancoDados.TB_GOU_MESAs.DeleteOnSubmit(mesa);
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