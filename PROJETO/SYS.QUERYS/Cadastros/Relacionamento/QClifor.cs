using SYS.UTILS;
using System;
using System.Linq;

namespace SYS.QUERYS.Cadastros.Relacionamento
{
    public class QClifor 
    {
        public IQueryable<TB_REL_CLIFOR> Buscar(int id_clifor = 0)
        {
            var consulta = from a in Conexao.BancoDados.TB_REL_CLIFORs
                           select a;

            if (id_clifor.TemValor())
                consulta = consulta.Where(a => a.ID_CLIFOR == id_clifor);

            return consulta;
        }

        public void Gravar(TB_REL_CLIFOR clifor, ref int posicaoTransacao)
        {
            try
            {
                Conexao.Iniciar(ref posicaoTransacao);


                var existente = Conexao.BancoDados.TB_REL_CLIFORs.FirstOrDefault(a => a.ID_CLIFOR == clifor.ID_CLIFOR);
                if (existente == null)
                {
                    clifor.ID_CLIFOR = (Conexao.BancoDados.TB_REL_CLIFORs.Any() ? Conexao.BancoDados.TB_REL_CLIFORs.Max(a => a.ID_CLIFOR) : 0) + 1;
                    clifor.DT_CADASTRO = Convert.ToDateTime(Conexao.DataHora);
                    Conexao.BancoDados.TB_REL_CLIFORs.InsertOnSubmit(clifor);
                }
                else
                {
                    existente.CNPJ = clifor.CNPJ;
                    existente.CPF = clifor.CPF;
                    existente.IE = clifor.IE;
                    existente.IM = clifor.IM;
                    existente.NM = clifor.NM;
                    existente.NM_FANTASIA = clifor.NM_FANTASIA;
                    existente.RG = clifor.RG;
                    existente.ST_ATIVO = clifor.ST_ATIVO;
                }

                Conexao.Enviar();

                Conexao.Finalizar(ref posicaoTransacao);
            }
            catch (Exception excessao)
            {
                Conexao.Voltar(ref posicaoTransacao);
                throw excessao;
            }
        }

        public void Deletar(TB_REL_CLIFOR clifor, ref int posicaoTransacao)
        {
            try
            {
                Conexao.Iniciar(ref posicaoTransacao);

                var existente = Conexao.BancoDados.TB_REL_CLIFORs.FirstOrDefault(a => a.ID_CLIFOR == clifor.ID_CLIFOR);
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