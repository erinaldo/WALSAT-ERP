using SYS.UTILS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SYS.QUERYS.Lancamentos.Financeiro
{
    public class QLiquidacao
    {
        public IQueryable<TB_FIN_LIQUIDACAO> Buscar(int id_liquidacao = 0, int id_duplicata = 0)
        {
            var consulta = from a in Conexao.BancoDados.TB_FIN_LIQUIDACAOs
                           select a;

            if (id_liquidacao > 0)
                consulta = consulta.Where(a => a.ID_DUPLICATA == id_liquidacao);

            if (id_duplicata > 0)
                consulta = consulta.Where(a => a.ID_DUPLICATA == id_duplicata);

            return consulta;
        }
        
        public TB_FIN_LIQUIDACAO Gravar(TB_FIN_PARCELA parcela, decimal valorDesconto, decimal valorJuros, int? formaPagamento, ref int posicaoTransacao)
        {
            try
            {
                Conexao.Iniciar(ref posicaoTransacao);

                // Abre o caixa geral
                var caixa = new QCaixaGeral();
                caixa.Abrir(ref posicaoTransacao);

                var liquidacao = new TB_FIN_LIQUIDACAO
                {
                    ID_LIQUIDACAO = (Conexao.BancoDados.TB_FIN_LIQUIDACAOs.Any() ? Conexao.BancoDados.TB_FIN_LIQUIDACAOs.Max(a => a.ID_LIQUIDACAO) : 0) + 1,
                    ID_PARCELA = parcela.ID_PARCELA,
                    ID_DUPLICATA = parcela.ID_DUPLICATA,
                    ID_EMPRESA = parcela.ID_EMPRESA,
                    ID_USUARIO = Parametros.ID_Usuario,
                    VL = parcela.VL,
                    ST = true,
                    VL_DESCONTO = valorDesconto,
                    ID_FORMAPAGAMENTO = formaPagamento
                };

                Conexao.BancoDados.TB_FIN_LIQUIDACAOs.InsertOnSubmit(liquidacao);
                

                Conexao.Enviar();

                Conexao.Finalizar(ref posicaoTransacao);

                return liquidacao;
            }
            catch (Exception excessao)
            {
                Conexao.Voltar(ref posicaoTransacao);
                throw excessao;
            }
        }
    }
}