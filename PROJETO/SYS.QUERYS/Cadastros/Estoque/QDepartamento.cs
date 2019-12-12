using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SYS.UTILS;

namespace SYS.QUERYS.Cadastros.Estoque
{
    public class QDepartamento
    {
        public IQueryable<TB_EST_DEPARTAMENTO> Buscar(int id_departamento = 0)
        {
            var consulta = from a in Conexao.BancoDados.TB_EST_DEPARTAMENTOs
                           select a;

            if (id_departamento.TemValor())
                consulta = consulta.Where(a => a.ID_DEPARTAMENTO == id_departamento);

            return consulta;
        }

        public void Gravar(TB_EST_DEPARTAMENTO departamento, ref int posicaoTransacao)
        {
            try
            {
                Conexao.Iniciar(ref posicaoTransacao);

                var existente = Conexao.BancoDados.TB_EST_DEPARTAMENTOs.FirstOrDefault(a => a.ID_DEPARTAMENTO == departamento.ID_DEPARTAMENTO);

                #region Inserção
                if (existente == null)
                {
                    departamento.ID_DEPARTAMENTO = (Conexao.BancoDados.TB_EST_DEPARTAMENTOs.Any() ? Conexao.BancoDados.TB_EST_DEPARTAMENTOs.Max(a => a.ID_DEPARTAMENTO) : 0) + 1;
                    Conexao.BancoDados.TB_EST_DEPARTAMENTOs.InsertOnSubmit(departamento);
                }
                #endregion

                #region Atualização
                else
                {
                    existente.NM = departamento.NM;
                    existente.ID_DEPARTAMENTO = departamento.ID_DEPARTAMENTO;

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

        public void Deletar(TB_EST_DEPARTAMENTO departamento, ref int posicaoTransacao)
        {
            try
            {
                Conexao.Iniciar(ref posicaoTransacao);

                var existente = Conexao.BancoDados.TB_EST_DEPARTAMENTOs.FirstOrDefault(a => a.ID_DEPARTAMENTO == departamento.ID_DEPARTAMENTO);

                Conexao.BancoDados.TB_EST_DEPARTAMENTOs.DeleteOnSubmit(existente);
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
