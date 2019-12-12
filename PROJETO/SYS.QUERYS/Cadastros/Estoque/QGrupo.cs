using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SYS.UTILS;

namespace SYS.QUERYS.Cadastros.Estoque
{
    public class QGrupo
    {
        public IQueryable<TB_EST_GRUPO> Buscar(int id_grupo = 0)
        {
            var consulta = from a in Conexao.BancoDados.TB_EST_GRUPOs
                           select a;

            if (id_grupo.TemValor())
                consulta = consulta.Where(a => a.ID_GRUPO == id_grupo);

            return consulta;
        }

        public void Gravar(TB_EST_GRUPO grupo, ref int posicaoTransacao)
        {
            try
            {
                Conexao.Iniciar(ref posicaoTransacao);

                var existente = Conexao.BancoDados.TB_EST_GRUPOs.FirstOrDefault(a => a.ID_GRUPO == grupo.ID_GRUPO);

                #region Inserção

                if (existente == null)
                {
                    grupo.ID_GRUPO = (Conexao.BancoDados.TB_EST_GRUPOs.Any() ? Conexao.BancoDados.TB_EST_GRUPOs.Max(a => a.ID_GRUPO) : 0) + 1;                        
                    Conexao.BancoDados.TB_EST_GRUPOs.InsertOnSubmit(grupo);
                }

                #endregion

                #region Atualização

                else
                {
                    existente.NM = grupo.NM;
                    existente.ST_ALMOXARIFADO = grupo.ST_ALMOXARIFADO;
                    existente.ST_FRACAO = grupo.ST_FRACAO;
                    existente.ST_COMPLEMENTO = grupo.ST_COMPLEMENTO;
                    existente.ST_SERVICO = grupo.ST_SERVICO;
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

        public void Deletar(TB_EST_GRUPO grupo, ref int posicaoTransacao)
        {
            try
            {
                Conexao.Iniciar(ref posicaoTransacao);

                var existente = Conexao.BancoDados.TB_EST_GRUPOs.FirstOrDefault(a => a.ID_GRUPO == grupo.ID_GRUPO);
                if (existente != null)
                    Conexao.BancoDados.TB_EST_GRUPOs.DeleteOnSubmit(existente);

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