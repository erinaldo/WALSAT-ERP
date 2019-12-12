using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SYS.UTILS;

namespace SYS.QUERYS.Cadastros.Fiscal
{
    public class QCertificadoDigital
    {
        public IQueryable<TB_FIS_CERTIFICADODIGITAL> Buscar(int id_certificado = 0, int id_empresa = 0)
        {
            var consulta = from a in Conexao.BancoDados.TB_FIS_CERTIFICADODIGITALs
                           select a;

            if (id_certificado.TemValor())
                consulta = consulta.Where(a => a.ID_CERTIFICADODIGITAL == id_certificado);

            if (id_empresa.TemValor())
                consulta = consulta.Where(a => a.ID_EMPRESA == id_empresa);

            return consulta;
        }

        public void Gravar(TB_FIS_CERTIFICADODIGITAL certificadoDigital, ref int posicaoTransacao)
        {
            try
            {
                Conexao.Iniciar(ref posicaoTransacao);

                var existente = Conexao.BancoDados.TB_FIS_CERTIFICADODIGITALs.FirstOrDefault(a => a.ID_CERTIFICADODIGITAL == certificadoDigital.ID_CERTIFICADODIGITAL&& a.ID_EMPRESA == certificadoDigital.ID_EMPRESA);

                #region Inserção

                if (existente == null)
                {
                    certificadoDigital.ID_CERTIFICADODIGITAL = (Conexao.BancoDados.TB_FIS_CERTIFICADODIGITALs.Any(a => a.ID_EMPRESA == certificadoDigital.ID_EMPRESA) ? Conexao.BancoDados.TB_FIS_CERTIFICADODIGITALs.Where(a => a.ID_EMPRESA == certificadoDigital.ID_EMPRESA).Max(a => a.ID_CERTIFICADODIGITAL) : 0) + 1;
                    Conexao.BancoDados.TB_FIS_CERTIFICADODIGITALs.InsertOnSubmit(certificadoDigital);
                }

                #endregion

                #region Atualização

                else
                {
                    existente.ID_SERIAL = certificadoDigital.ID_SERIAL;
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

        public void Deletar(TB_FIS_CERTIFICADODIGITAL certificadoDigital, ref int posicaoTransacao)
        {
            try
            {
                Conexao.Iniciar(ref posicaoTransacao);

                var existente = Conexao.BancoDados.TB_FIS_CERTIFICADODIGITALs.FirstOrDefault(a => a.ID_CERTIFICADODIGITAL == certificadoDigital.ID_CERTIFICADODIGITAL && a.ID_EMPRESA == certificadoDigital.ID_EMPRESA);
                if (existente != null)
                    Conexao.BancoDados.TB_FIS_CERTIFICADODIGITALs.DeleteOnSubmit(existente);

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
