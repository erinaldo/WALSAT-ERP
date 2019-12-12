using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SYS.QUERYS.Lancamentos.Gourmet
{
    public class QDelivery
    {
        public IQueryable<TB_GOU_DELIVERY> Buscar(int id_delivery = 0)
        {
            var consulta = from a in Conexao.BancoDados.TB_GOU_DELIVERies
                           select a;

            if (id_delivery > 0)
                consulta = consulta.Where(a => a.ID_PEDIDO == id_delivery);

            return consulta;
        }

        public void Gravar(TB_GOU_DELIVERY delivery, ref int posicaoTransacao)
        {
            try
            {
                Conexao.Iniciar(ref posicaoTransacao);

                var existente = Conexao.BancoDados.TB_GOU_DELIVERies.FirstOrDefault(a => a.ID_DELIVERY == delivery.ID_DELIVERY);

                #region Inserção

                if (existente == null)
                {
                    delivery.ID_DELIVERY = (Conexao.BancoDados.TB_GOU_DELIVERies.Any() ? Conexao.BancoDados.TB_GOU_DELIVERies.Max(a => a.ID_DELIVERY) : 0) + 1;
                    Conexao.BancoDados.TB_GOU_DELIVERies.InsertOnSubmit(delivery);
                }

                #endregion

                #region Atualização

                else
                {
                    existente.ID_DELIVERY = delivery.ID_DELIVERY;
                    existente.ID_PEDIDO = delivery.ID_PEDIDO;
                    existente.ST = delivery.ST;
                    existente.VL = delivery.VL;

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

        public void Deletar(TB_GOU_DELIVERY delivery, ref int posicaoTransacao)
        {
            try
            {
                Conexao.Iniciar(ref posicaoTransacao);

                var existente = Conexao.BancoDados.TB_GOU_DELIVERies.FirstOrDefault(a => a.ID_DELIVERY == delivery.ID_DELIVERY);
                if (existente != null)
                    Conexao.BancoDados.TB_GOU_DELIVERies.DeleteOnSubmit(existente);

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