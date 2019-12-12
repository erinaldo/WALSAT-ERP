using System;
using System.Collections.Generic;
using System.Linq;
using SYS.UTILS;
using System.Text;
using System.Threading.Tasks;

namespace SYS.QUERYS.Cadastros.Relacionamento
{
    public class QEndereco
    {
        public IQueryable<TB_REL_ENDERECO> Buscar(int id_endereco = 0)
        {
            var consulta = from a in Conexao.BancoDados.TB_REL_ENDERECOs
                           select a;

            if (id_endereco.TemValor())
                consulta = consulta.Where(a => a.ID_ENDERECO == id_endereco);

            return consulta;
        }

        public void Gravar(TB_REL_ENDERECO endereco, ref int posicaoTransacao)
        {
            try
            {
                Conexao.Iniciar(ref posicaoTransacao);


                var existente = Conexao.BancoDados.TB_REL_ENDERECOs.FirstOrDefault(a => a.ID_ENDERECO == endereco.ID_ENDERECO);
                if (existente == null)
                {
                    endereco.ID_ENDERECO = (Conexao.BancoDados.TB_REL_ENDERECOs.Any() ? Conexao.BancoDados.TB_REL_ENDERECOs.Max(a => a.ID_ENDERECO) : 0) + 1;
                    Conexao.BancoDados.TB_REL_ENDERECOs.InsertOnSubmit(endereco);
                }
                else
                {
                    existente.ID_CIDADE = endereco.ID_CIDADE;
                    existente.ID_PAIS = endereco.ID_PAIS;
                    existente.ID_UNIDADEFEDERATIVA = endereco.ID_UNIDADEFEDERATIVA;
                    existente.NM_BAIRRO = endereco.NM_BAIRRO;
                    existente.NR = endereco.NR;
                    existente.DS_COMPLEMENTO = endereco.DS_COMPLEMENTO;
                    existente.CEP = endereco.CEP;
                    existente.NM_RUA = endereco.NM_RUA;
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

        public void Deletar(TB_REL_ENDERECO endereco, ref int posicaoTransacao)
        {
            try
            {
                Conexao.Iniciar(ref posicaoTransacao);

                var existente = Conexao.BancoDados.TB_REL_ENDERECOs.FirstOrDefault(a => a.ID_ENDERECO == endereco.ID_ENDERECO);
                if (existente != null)
                    Conexao.BancoDados.TB_REL_ENDERECOs.DeleteOnSubmit(endereco);

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
