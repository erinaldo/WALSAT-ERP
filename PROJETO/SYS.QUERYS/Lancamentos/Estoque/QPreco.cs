using SYS.UTILS;
using System;
using System.Linq;

namespace SYS.QUERYS.Lancamentos.Estoque
{
    public class QPreco
    {
        public IQueryable<TB_EST_PRECO> Buscar(int id_produto = 0)
        {
            var consulta = from a in Conexao.BancoDados.TB_EST_PRECOs
                           where (a.ST_ATIVO ?? false)
                           select a;

            if (id_produto.TemValor())
                consulta = consulta.Where(a => a.ID_PRODUTO == id_produto);

            return consulta;
        }

        public IQueryable Buscar(int id_produto = 0, string tp_preco = "")
        {
            var consulta = from a in Conexao.BancoDados.TB_EST_PRECOs
                           join b in Conexao.BancoDados.TB_EST_PRODUTOs on a.ID_PRODUTO equals b.ID_PRODUTO
                           where a.ST_ATIVO.Padrao()
                           select new
                           {
                               ID_PRECO = a.ID_PRECO,
                               ID_PRODUTO = a.ID_PRODUTO,
                               NM_PRODUTO = b.NM,
                               PC_MARCKUP = a.PC_MARCKUP.Padrao(),
                               VL_PRECO = a.VL_PRECO.Padrao(),
                               TP_PRECO = a.TP_PRECO,
                               DT_CAD = a.DT_CADASTRO.Padrao()
                           };

            if (id_produto.TemValor())
                consulta = consulta.Where(a => a.ID_PRODUTO == id_produto);

            if (tp_preco.TemValor())
                consulta = consulta.Where(a => a.TP_PRECO == tp_preco);

            return consulta;
        }

        public void Gravar(TB_EST_PRECO preco, ref int posicaoTransacao)
        {
            try
            {
                Conexao.Iniciar(ref posicaoTransacao);

                var cancelar = Conexao.BancoDados.TB_EST_PRECOs.Where(a => a.ID_PRODUTO == preco.ID_PRODUTO && a.ID_PRECO != preco.ID_PRECO);
                foreach (var registro in cancelar)
                    Deletar(registro, ref posicaoTransacao);

                preco.ID_PRECO = (Conexao.BancoDados.TB_EST_PRECOs.Where(a => a.ID_PRODUTO == preco.ID_PRODUTO).Any() ? Conexao.BancoDados.TB_EST_PRECOs.Where(a => a.ID_PRODUTO == preco.ID_PRODUTO).Max(a => a.ID_PRECO) : 0) + 1;
                Conexao.BancoDados.TB_EST_PRECOs.InsertOnSubmit(preco);                

                Conexao.Enviar();

                Conexao.Finalizar(ref posicaoTransacao);
            }
            catch (Exception excessao)
            {
                Conexao.Voltar(ref posicaoTransacao);
                throw excessao;
            }
        }

        public void Deletar(TB_EST_PRECO preco, ref int posicaoTransacao)
        {
            try
            {
                Conexao.Iniciar(ref posicaoTransacao);

                var existente = Conexao.BancoDados.TB_EST_PRECOs.FirstOrDefault(a => a.ID_PRODUTO == preco.ID_PRODUTO && a.ST_ATIVO == true);
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