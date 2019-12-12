using SYS.UTILS;
using System;
using System.Linq;

namespace SYS.QUERYS.Cadastros.Estoque
{
    public class QCodigoBarra
    {
        public IQueryable<TB_EST_PRODUTO_BARRA> Buscar(int id_barra = 0, int id_produto = 0)
        {
            var consulta = from a in Conexao.BancoDados.TB_EST_PRODUTO_BARRAs
                           select a;

            if (id_barra.TemValor())
                consulta = consulta.Where(a => a.ID_BARRA == id_barra);

            if (id_produto.TemValor())
                consulta = consulta.Where(a => a.ID_PRODUTO == id_produto);

            return consulta;
        }

        public void Gravar(TB_EST_PRODUTO_BARRA barra, ref int posicaoTransacao)
        {
            try
            {
                Conexao.Iniciar(ref posicaoTransacao);

                var existente = Conexao.BancoDados.TB_EST_PRODUTO_BARRAs.FirstOrDefault(a => a.ID_BARRA == barra.ID_BARRA && a.ID_PRODUTO == barra.ID_PRODUTO);

                #region Inserção
                if (existente == null)
                {
                    barra.ID_BARRA = (Conexao.BancoDados.TB_EST_PRODUTO_BARRAs.Any(a => a.ID_PRODUTO == barra.ID_PRODUTO) ? Conexao.BancoDados.TB_EST_PRODUTO_BARRAs.Where(a => a.ID_PRODUTO == barra.ID_PRODUTO).Max(a => a.ID_BARRA) : 0) + 1;
                    Conexao.BancoDados.TB_EST_PRODUTO_BARRAs.InsertOnSubmit(barra);
                }
                #endregion

                #region Atualização
                else
                    existente.ID_BARRA_REFERENCIA = barra.ID_BARRA_REFERENCIA;

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

        public void Deletar(TB_EST_PRODUTO_BARRA barra, ref int posicaoTransacao)
        {
            try
            {
                Conexao.Iniciar(ref posicaoTransacao);

                var existente = Conexao.BancoDados.TB_EST_PRODUTO_BARRAs.FirstOrDefault(a => a.ID_BARRA == barra.ID_BARRA && a.ID_PRODUTO == barra.ID_PRODUTO);
                if (existente != null)
                    Conexao.BancoDados.TB_EST_PRODUTO_BARRAs.DeleteOnSubmit(existente);

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