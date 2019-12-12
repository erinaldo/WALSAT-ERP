using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SYS.UTILS;

namespace SYS.QUERYS.Cadastros.Financeiro
{
    public class QCCusto
    {
        public IQueryable<TB_FIN_CENTROCUSTO> Buscar(int id_CCusto = 0)
        {
            var consulta = from a in Conexao.BancoDados.TB_FIN_CENTROCUSTOs
                           select a;

            if (id_CCusto.TemValor())
                consulta = consulta.Where(a => a.ID_CENTROCUSTO == id_CCusto);

            return consulta;
        }

        public void Gravar(TB_FIN_CENTROCUSTO CCusto, ref int posicaoTransacao)
        {
            try
            {
                Conexao.Iniciar(ref posicaoTransacao);

                var existente = Conexao.BancoDados.TB_FIN_CENTROCUSTOs.FirstOrDefault(a => a.ID_CENTROCUSTO == CCusto.ID_CENTROCUSTO);

                #region Inserção

                if (existente == null)
                {
                    CCusto.ID_CENTROCUSTO = (Conexao.BancoDados.TB_FIN_CENTROCUSTOs.Any() ? Conexao.BancoDados.TB_FIN_CENTROCUSTOs.Max(a => a.ID_CENTROCUSTO) : 0) + 1;
                    Conexao.BancoDados.TB_FIN_CENTROCUSTOs.InsertOnSubmit(CCusto);
                }

                #endregion

                #region Atualização

                else
                {
                    existente.NM = CCusto.NM;
                    existente.TP = CCusto.TP;
                    existente.ST_ATIVO = CCusto.ST_ATIVO;
                    existente.ID_CENTROCUSTO_PAI = CCusto.ID_CENTROCUSTO_PAI;
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

        public void Deletar(TB_FIN_CENTROCUSTO CCusto, ref int posicaoTransacao)
        {
            try
            {
                Conexao.Iniciar(ref posicaoTransacao);

                var existente = Conexao.BancoDados.TB_FIN_CENTROCUSTOs.FirstOrDefault(a => a.ID_CENTROCUSTO == CCusto.ID_CENTROCUSTO);

                existente.ST_ATIVO = false;
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
