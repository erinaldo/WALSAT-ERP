using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SYS.UTILS;

namespace SYS.QUERYS.Cadastros.Fiscal
{
    public class QConfiguracao
    {
        public IQueryable<TB_FIS_CONFIGURACAO> Buscar(int id_configuracao = 0)
        {
            var consulta = from a in Conexao.BancoDados.TB_FIS_CONFIGURACAOs
                           select a;

            if (id_configuracao.TemValor())
                consulta = consulta.Where(a => a.ID_CONFIGURACAO_FISCAL == id_configuracao);

            return consulta;
        }

        public void Gravar(TB_FIS_CONFIGURACAO configuracao, ref int posicaoTransacao)
        {
            try
            {
                Conexao.Iniciar(ref posicaoTransacao);

                var existente = Conexao.BancoDados.TB_FIS_CONFIGURACAOs.FirstOrDefault(a => a.ID_CONFIGURACAO_FISCAL == configuracao.ID_CONFIGURACAO_FISCAL);

                #region Inserção

                if (existente == null)
                {
                    configuracao.ID_CONFIGURACAO_FISCAL = (Conexao.BancoDados.TB_FIS_CONFIGURACAOs.Any() ? Conexao.BancoDados.TB_FIS_CONFIGURACAOs.Max(a => a.ID_CONFIGURACAO_FISCAL) : 0) + 1;
                    Conexao.BancoDados.TB_FIS_CONFIGURACAOs.InsertOnSubmit(configuracao);
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

        public void Deletar(TB_FIS_CONFIGURACAO configuracao, ref int posicaoTransacao)
        {
            try
            {
                Conexao.Iniciar(ref posicaoTransacao);

                var existente = Conexao.BancoDados.TB_FIS_CONFIGURACAOs.FirstOrDefault(a => a.ID_CONFIGURACAO_FISCAL == configuracao.ID_CONFIGURACAO_FISCAL);
                if (existente != null)
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