using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SYS.UTILS;

namespace SYS.QUERYS.Lancamentos.Financeiro
{
    public class QCaixaGeral
    {
        public int ID_CAIXA;

        public IQueryable<TB_FIN_CAIXA> Buscar(int id_caixa = 0)
        {
            var consulta = from a in Conexao.BancoDados.TB_FIN_CAIXAs
                           select a;

            if (id_caixa > 0)
                consulta = consulta.Where(a => a.ID_CAIXA == id_caixa);

            return consulta;
        }

        public void Lancamento(TB_FIN_CAIXA_LANCAMENTO lancamento, ref int posicaoTransacao)
        {
            try
            {
                Conexao.Iniciar(ref posicaoTransacao);

                Abrir(ref posicaoTransacao);

                var caixa = Caixa();

                lancamento.ID_CAIXA = Caixa().ID_CAIXA;
                lancamento.ID_LANCAMENTO = (Conexao.BancoDados.TB_FIN_CAIXA_LANCAMENTOs.Any() ? Conexao.BancoDados.TB_FIN_CAIXA_LANCAMENTOs.Max(a => a.ID_CAIXA) : 0) + 1;

                lancamento.TP_MOVIMENTO.Validar(true, 1);

                if (!(lancamento.TP_MOVIMENTO == "E" || lancamento.TP_MOVIMENTO == "S"))
                    throw new SYSException(Mensagens.Necessario("tipo do movimento na liquidação da parcela"));

                lancamento.VL.Validar();

                Conexao.BancoDados.TB_FIN_CAIXA_LANCAMENTOs.InsertOnSubmit(lancamento);

                Conexao.Enviar();

                Conexao.Finalizar(ref posicaoTransacao);
            }
            catch (Exception excessao)
            {
                Conexao.Voltar(ref posicaoTransacao);
                throw excessao;
            }
        }

        public void Abrir(ref int posicaoTransacao)
        {
            try
            {
                Conexao.Iniciar(ref posicaoTransacao);

                if (CaixaAberto())
                {
                    //throw new SYSException("Favor finalizar o caixa do dia " + DataCaixaAberto().PTBR());
                }
                else
                {
                    Conexao.BancoDados.TB_FIN_CAIXAs.InsertOnSubmit(new TB_FIN_CAIXA
                    {
                        ID_CAIXA = (Conexao.BancoDados.TB_FIN_CAIXAs.Any() ? Conexao.BancoDados.TB_FIN_CAIXAs.Max(a => a.ID_CAIXA) : 0) + 1,
                        DT = Conexao.DataHora
                    });

                    Conexao.Enviar();
                }

                Conexao.Finalizar(ref posicaoTransacao);
            }
            catch (Exception excessao)
            {
                Conexao.Voltar(ref posicaoTransacao);
                throw excessao;
            }
        }

        public bool CaixaAberto()
        {
            return Buscar().OrderByDescending(a => a.DT).FirstOrDefault() != null;
        }

        public DateTime DataCaixaAberto()
        {
            if (!CaixaAberto())
                throw new SYSException(Mensagens.Necessario("caixa geral", "abrir"));

            return Buscar().OrderByDescending(a => a.DT).Where(a => a.VL_FINAL == null).FirstOrDefault().DT.Padrao();
        }

        public TB_FIN_CAIXA Caixa()
        {
            if (!CaixaAberto())
                throw new SYSException(Mensagens.Necessario("caixa geral", "abrir"));

            return Buscar().OrderByDescending(a => a.DT).Where(a => a.VL_FINAL == null).FirstOrDefault();
        }
    }
}
