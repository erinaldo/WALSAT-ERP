using SYS.UTILS;
using System;
using System.Linq;

namespace SYS.QUERYS.Cadastros.Configuracao
{
    public class QArquivo
    {
        public IQueryable<TB_CON_ARQUIVO> Buscar(int id_arquivo = 0)
        {
            var consulta = from a in Conexao.BancoDados.TB_CON_ARQUIVOs
                           select a;

            if (id_arquivo.TemValor())
                consulta = consulta.Where(a => a.ID_ARQUIVO == id_arquivo);

            return consulta;
        }

        public void Gravar(TB_CON_ARQUIVO arquivo, ref int posicaoTransacao)
        {
            try
            {
                Conexao.Iniciar(ref posicaoTransacao);

                var existente = Conexao.BancoDados.TB_CON_ARQUIVOs.FirstOrDefault(a => a.ID_ARQUIVO == arquivo.ID_ARQUIVO);

                #region Inserção

                if (existente == null)
                {
                    arquivo.ID_ARQUIVO = (Conexao.BancoDados.TB_CON_ARQUIVOs.Any() ? Conexao.BancoDados.TB_CON_ARQUIVOs.Max(a => a.ID_ARQUIVO) : 0) + 1;
                    Conexao.BancoDados.TB_CON_ARQUIVOs.InsertOnSubmit(arquivo);
                }

                #endregion

                #region Atualização

                else
                {
                    existente.NM = arquivo.NM;
                    existente.ARQUIVO = arquivo.ARQUIVO;
                    existente.EXTENSAO = arquivo.EXTENSAO;
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

        public void Deletar(TB_CON_ARQUIVO arquivo, ref int posicaoTransacao)
        {
            try
            {
                Conexao.Iniciar(ref posicaoTransacao);

                var existente = Conexao.BancoDados.TB_CON_ARQUIVOs.FirstOrDefault(a => a.ID_ARQUIVO == arquivo.ID_ARQUIVO);
                if (existente != null)
                    Conexao.BancoDados.TB_CON_ARQUIVOs.DeleteOnSubmit(arquivo);

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