using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SYS.UTILS;

namespace SYS.QUERYS.Cadastros.Estoque
{
    public class QMarca 
    {
        public IQueryable<TB_EST_MARCA> Buscar(int id_marca = 0)
        {
            var consulta = from a in Conexao.BancoDados.TB_EST_MARCAs
                           select a;

            if (id_marca.TemValor())
                consulta = consulta.Where(a => a.ID_MARCA == id_marca);

            return consulta;
        }

        public void Gravar(TB_EST_MARCA marca, ref int posicaoTransacao)
        {
            try
            {
                Conexao.Iniciar(ref posicaoTransacao);

                var existente = Conexao.BancoDados.TB_EST_MARCAs.FirstOrDefault(a => a.ID_MARCA == marca.ID_MARCA);

                #region Inserção
                if (existente == null)
                {
                    marca.ID_MARCA = (Conexao.BancoDados.TB_EST_MARCAs.Any() ? Conexao.BancoDados.TB_EST_MARCAs.Max(a => a.ID_MARCA) : 0) + 1;
                    Conexao.BancoDados.TB_EST_MARCAs.InsertOnSubmit(marca);
                }
                #endregion

                #region Atualização
                else
                {
                    existente.NM = marca.NM;
                    existente.ID_MARCA = marca.ID_MARCA;

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

        public void Deletar(TB_EST_MARCA marca, ref int posicaoTransacao)
        {
            try
            {
                Conexao.Iniciar(ref posicaoTransacao);

                var existente = Conexao.BancoDados.TB_EST_MARCAs.FirstOrDefault(a => a.ID_MARCA == marca.ID_MARCA);

                Conexao.BancoDados.TB_EST_MARCAs.DeleteOnSubmit(existente);
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