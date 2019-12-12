using System;
using System.Collections.Generic;
using SYS.UTILS;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SYS.QUERYS.Cadastros.Configuracao
{
    public class QEmpresa
    {
        public IQueryable<TB_CON_EMPRESA> Buscar(int id_empresa = 0, int id_clifor = 0)
        {
            var consulta = from a in Conexao.BancoDados.TB_CON_EMPRESAs
                           select a;

            if (id_empresa.TemValor())
                consulta = consulta.Where(a => a.ID_EMPRESA == id_empresa);

            if (id_clifor.TemValor())
                consulta = consulta.Where(a => a.ID_CLIFOR == id_clifor);

            return consulta;
        }

        public void Gravar(TB_CON_EMPRESA empresa, ref int posicaoTransacao)
        {
            try
            {
                Conexao.Iniciar(ref posicaoTransacao);

                var existente = Conexao.BancoDados.TB_CON_EMPRESAs.FirstOrDefault(a => a.ID_EMPRESA == empresa.ID_EMPRESA);
                if (existente == null)
                {
                    empresa.ID_EMPRESA = (Conexao.BancoDados.TB_CON_EMPRESAs.Any() ? Conexao.BancoDados.TB_CON_EMPRESAs.Max(a => a.ID_EMPRESA) : 0) + 1;
                    Conexao.BancoDados.TB_CON_EMPRESAs.InsertOnSubmit(empresa);
                }
                else
                {
                    existente.ID_CLIFOR = empresa.ID_CLIFOR;
                    existente.ID_EMPRESA = empresa.ID_EMPRESA;
                    existente.ST_GOURMET = empresa.ST_GOURMET;
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

        public void Deletar(TB_CON_EMPRESA empresa, ref int posicaoTransacao)
        {
            try
            {
                Conexao.Iniciar(ref posicaoTransacao);

                var existente = Conexao.BancoDados.TB_CON_EMPRESAs.FirstOrDefault(a => a.ID_EMPRESA == empresa.ID_EMPRESA);
                if (existente != null)
                    Conexao.BancoDados.TB_CON_EMPRESAs.DeleteOnSubmit(existente);

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
