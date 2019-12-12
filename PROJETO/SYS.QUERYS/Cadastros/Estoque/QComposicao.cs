using System;
using SYS.UTILS;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SYS.QUERYS.Cadastros.Estoque
{
    public class QComposicao
    {
        public IQueryable<TB_GOU_COMPOSICAO> Buscar(int id_produto = 0, int id_produto_composto = 0)
        {
            var consulta = from a in Conexao.BancoDados.TB_GOU_COMPOSICAOs
                           select a;

            if (id_produto.TemValor())
                consulta = consulta.Where(a => a.ID_PRODUTO == id_produto);

            if (id_produto_composto.TemValor())
                consulta = consulta.Where(a => a.ID_PRODUTO_COMPOSTO == id_produto_composto);

            return consulta;
        }

        public void Gravar(TB_GOU_COMPOSICAO composicao, ref int posicaoTransacao)
        {
            try
            {
                Conexao.Iniciar(ref posicaoTransacao);

                var existente = Conexao.BancoDados.TB_GOU_COMPOSICAOs.FirstOrDefault(a => a.ID_PRODUTO == composicao.ID_PRODUTO && a.ID_PRODUTO_COMPOSTO == composicao.ID_PRODUTO_COMPOSTO);

                #region Inserção

                if (existente == null)
                    Conexao.BancoDados.TB_GOU_COMPOSICAOs.InsertOnSubmit(composicao);

                #endregion

                #region Atualização

                else
                    existente.QT = composicao.QT;

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

        public void Deletar(TB_GOU_COMPOSICAO composicao, ref int posicaoTransacao)
        {
            try
            {
                Conexao.Iniciar(ref posicaoTransacao);

                var existente = Conexao.BancoDados.TB_GOU_COMPOSICAOs.FirstOrDefault(a => a.ID_PRODUTO == composicao.ID_PRODUTO && a.ID_PRODUTO_COMPOSTO == composicao.ID_PRODUTO_COMPOSTO);

                if (existente != null)
                    Conexao.BancoDados.TB_GOU_COMPOSICAOs.DeleteOnSubmit(existente);

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