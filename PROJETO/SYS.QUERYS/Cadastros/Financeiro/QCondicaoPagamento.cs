using SYS.UTILS;
using System;
using System.Linq;

namespace SYS.QUERYS.Cadastros.Financeiro
{
    public class QCondicaoPagamento
    {
        public IQueryable<TB_FIN_CONDICAOPAGAMENTO> Buscar(int id_condicaoPagamento = 0)
        {
            var consulta = from a in Conexao.BancoDados.TB_FIN_CONDICAOPAGAMENTOs
                           select a;

            if (id_condicaoPagamento.TemValor())
                consulta = consulta.Where(a => a.ID_CONDICAOPAGAMENTO == id_condicaoPagamento);

            return consulta;
        }

        public void Gravar(TB_FIN_CONDICAOPAGAMENTO condicaoPagamento, ref int posicaoTransacao)
        {
            try
            {
                Conexao.Iniciar(ref posicaoTransacao);

                var existente = Conexao.BancoDados.TB_FIN_CONDICAOPAGAMENTOs.FirstOrDefault(a => a.ID_CONDICAOPAGAMENTO == condicaoPagamento.ID_CONDICAOPAGAMENTO);

                #region Inserção

                if (existente == null)
                {
                    condicaoPagamento.ID_CONDICAOPAGAMENTO = (Conexao.BancoDados.TB_FIN_CONDICAOPAGAMENTOs.Any() ? Conexao.BancoDados.TB_FIN_CONDICAOPAGAMENTOs.Max(a => a.ID_CONDICAOPAGAMENTO) : 0) + 1;
                    Conexao.BancoDados.TB_FIN_CONDICAOPAGAMENTOs.InsertOnSubmit(condicaoPagamento);
                }

                #endregion

                #region Atualização

                else
                {
                    existente.DS = condicaoPagamento.DS;
                    existente.QT_DIASDESDOBRO = condicaoPagamento.QT_DIASDESDOBRO;
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

        public void Deletar(TB_FIN_CONDICAOPAGAMENTO condicaoPagamento, ref int posicaoTransacao)
        {
            try
            {
                Conexao.Iniciar(ref posicaoTransacao);

                var existente = Conexao.BancoDados.TB_FIN_CONDICAOPAGAMENTOs.FirstOrDefault(a => a.ID_CONDICAOPAGAMENTO == condicaoPagamento.ID_CONDICAOPAGAMENTO);

                Conexao.BancoDados.TB_FIN_CONDICAOPAGAMENTOs.DeleteOnSubmit(existente);
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