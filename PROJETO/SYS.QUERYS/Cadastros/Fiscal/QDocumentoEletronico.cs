using SYS.QUERYS.Cadastros.Configuracao;
using SYS.UTILS;
using System;
using System.Linq;

namespace SYS.QUERYS.Cadastros.Fiscal
{
    public class QDocumentoEletronico
    {
        public IQueryable<TB_FIS_DOCUMENTOELETRONICO> Buscar(int id_documento = 0, int id_empresa = 0)
        {
            var consulta = from a in Conexao.BancoDados.TB_FIS_DOCUMENTOELETRONICOs
                           select a;

            if (id_documento.TemValor())
                consulta = consulta.Where(a => a.ID_DOCUMENTO == id_documento);

            if (id_empresa.TemValor())
                consulta = consulta.Where(a => a.ID_EMPRESA == id_empresa);

            return consulta;
        }

        public void Gravar(TB_FIS_DOCUMENTOELETRONICO documentoEletronico, ref int posicaoTransacao)
        {
            try
            {
                Conexao.Iniciar(ref posicaoTransacao);

                var existente = Conexao.BancoDados.TB_FIS_DOCUMENTOELETRONICOs.FirstOrDefault(a => a.ID_DOCUMENTO == documentoEletronico.ID_DOCUMENTO && a.ID_EMPRESA == documentoEletronico.ID_EMPRESA);

                #region Inserção

                if (documentoEletronico.TB_CON_ARQUIVO != null)
                {
                    new QArquivo().Gravar(documentoEletronico.TB_CON_ARQUIVO, ref posicaoTransacao);
                    documentoEletronico.DT_SAIDA = Conexao.DataHora;
                }

                if (documentoEletronico.TB_CON_ARQUIVO1 != null)
                {
                    new QArquivo().Gravar(documentoEletronico.TB_CON_ARQUIVO1, ref posicaoTransacao);
                    documentoEletronico.DT_ENTRADA = Conexao.DataHora;
                }

                if (existente == null)
                    Conexao.BancoDados.TB_FIS_DOCUMENTOELETRONICOs.InsertOnSubmit(documentoEletronico);

                #endregion

                #region Atualização

                else
                {
                    existente.CHAVE_ACESSO = documentoEletronico.CHAVE_ACESSO;
                    existente.CHAVE_VERIFICACAO = documentoEletronico.CHAVE_VERIFICACAO;
                    existente.ID_RETORNO = documentoEletronico.ID_RETORNO;
                    existente.DS_RETORNO = documentoEletronico.DS_RETORNO;
                    existente.ID_PROTOCOLO = documentoEletronico.ID_PROTOCOLO;
                    existente.TP_AMBIENTE = documentoEletronico.TP_AMBIENTE;
                    existente.ID_PROCESSO = documentoEletronico.ID_PROCESSO;
                    existente.ID_PROCESSO_VERSAO = documentoEletronico.ID_PROCESSO_VERSAO;
                    existente.TP_EMISSAO = documentoEletronico.TP_EMISSAO;
                    existente.ID_LOTE = documentoEletronico.ID_LOTE;
                    existente.ID_SINCRONO = documentoEletronico.ID_SINCRONO;
                    existente.TP_DOCUMENTO = documentoEletronico.TP_DOCUMENTO;
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

        public void Deletar(TB_FIS_DOCUMENTOELETRONICO documentoEletronico, ref int posicaoTransacao)
        {
            try
            {
                Conexao.Iniciar(ref posicaoTransacao);

                var existente = Conexao.BancoDados.TB_FIS_DOCUMENTOELETRONICOs.FirstOrDefault(a => a.ID_DOCUMENTO == documentoEletronico.ID_DOCUMENTO && a.ID_EMPRESA == documentoEletronico.ID_EMPRESA);
                if (existente != null)
                    Conexao.BancoDados.TB_FIS_DOCUMENTOELETRONICOs.DeleteOnSubmit(existente);

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