using SYS.UTILS;
using System;
using System.Linq;

namespace SYS.QUERYS.Cadastros.Fiscal
{
    public class QDocumento
    {
        public IQueryable<TB_FIS_DOCUMENTO> Buscar(int id_documento = 0, int id_empresa = 0)
        {
            var consulta = from a in Conexao.BancoDados.TB_FIS_DOCUMENTOs
                           select a;

            if (id_documento.TemValor())
                consulta = consulta.Where(a => a.ID_DOCUMENTO == id_documento);

            if (id_empresa.TemValor())
                consulta = consulta.Where(a => a.ID_EMPRESA == id_empresa);

            return consulta;
        }

        public void Gravar(TB_FIS_DOCUMENTO documento, ref int posicaoTransacao)
        {
            try
            {
                Conexao.Iniciar(ref posicaoTransacao);

                var existente = Conexao.BancoDados.TB_FIS_DOCUMENTOs.FirstOrDefault(a => a.ID_DOCUMENTO == documento.ID_DOCUMENTO && a.ID_EMPRESA == documento.ID_EMPRESA);

                #region Inserção

                if (existente == null)
                {
                    documento.ID_DOCUMENTO = (Conexao.BancoDados.TB_FIS_DOCUMENTOs.Where(a => a.ID_EMPRESA == documento.ID_EMPRESA).Any() ? Conexao.BancoDados.TB_FIS_DOCUMENTOs.Where(a => a.ID_EMPRESA == documento.ID_EMPRESA).Max(a => a.ID_DOCUMENTO) : 0) + 1;
                    Conexao.BancoDados.TB_FIS_DOCUMENTOs.InsertOnSubmit(documento);
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

        public void Deletar(TB_FIS_DOCUMENTO documento, ref int posicaoTransacao)
        {
            try
            {
                Conexao.Iniciar(ref posicaoTransacao);

                var existente = Conexao.BancoDados.TB_FIS_DOCUMENTOs.FirstOrDefault(a => a.ID_DOCUMENTO == documento.ID_DOCUMENTO && a.ID_EMPRESA == documento.ID_EMPRESA);
                if (existente != null)
                    Conexao.BancoDados.TB_FIS_DOCUMENTOs.DeleteOnSubmit(existente);

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