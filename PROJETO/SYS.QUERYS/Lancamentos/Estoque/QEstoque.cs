using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SYS.UTILS;

namespace SYS.QUERYS.Lancamentos.Estoque
{
    public class QEstoque
    {
        public IQueryable<TB_EST_ESTOQUE> Buscar(int id_estoque = 0, int id_produto = 0, int id_empresa = 0)
        {
            var consulta = from a in Conexao.BancoDados.TB_EST_ESTOQUEs
                           select a;

            if (id_estoque.TemValor())
                consulta = consulta.Where(a => a.ID_ESTOQUE == id_estoque);

            if (id_produto.TemValor())
                consulta = consulta.Where(a => a.ID_PRODUTO == id_produto);

            if (id_produto.TemValor())
                consulta = consulta.Where(a => a.ID_PRODUTO == id_produto);

            return consulta;
        }

        public decimal BuscarSaldo(int id_produto = 0, int id_empresa = 0)
        {
            return Buscar(0, id_produto, id_empresa).Sum(a => a.QT.Padrao());
        }

        public void Gravar(TB_EST_ESTOQUE estoque, ref int posicaoTransacao)
        {
            try
            {
                Conexao.Iniciar(ref posicaoTransacao);

                estoque.ID_ESTOQUE = (Conexao.BancoDados.TB_EST_ESTOQUEs.Where(a => a.ID_PRODUTO == estoque.ID_PRODUTO && a.ID_EMPRESA == estoque.ID_EMPRESA).Any() ? Conexao.BancoDados.TB_EST_ESTOQUEs.Where(a => a.ID_PRODUTO == estoque.ID_PRODUTO && a.ID_EMPRESA == estoque.ID_EMPRESA).Max(a => a.ID_ESTOQUE) : 0) + 1;
                Conexao.BancoDados.TB_EST_ESTOQUEs.InsertOnSubmit(estoque);

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