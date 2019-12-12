using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SYS.UTILS;

namespace SYS.QUERYS.Cadastros.Configuracao
{
    public class QUsuario
    {
        public IQueryable<TB_CON_USUARIO> Buscar(string id_usuario = "", string senha = "", bool validandoBackdoor = false)
        {
            var consulta = from a in Conexao.BancoDados.TB_CON_USUARIOs
                           select a;

            if (id_usuario.TemValor())
                consulta = consulta.Where(a => a.ID_USUARIO == id_usuario);

            if (id_usuario.TemValor() && senha.TemValor())
                consulta = consulta.Where(a => a.ID_USUARIO == id_usuario && a.SENHA == senha);

            if (!validandoBackdoor)
                consulta = consulta.Where(a => a.ID_USUARIO != Parametros.BackdoorUsuario);

            return consulta;
        }

        public void Gravar(TB_CON_USUARIO usuario, ref int posicaoTransacao, bool criarBackdoor = false)
        {
            try
            {
                Conexao.Iniciar(ref posicaoTransacao);

                if (criarBackdoor)
                    usuario = new TB_CON_USUARIO
                    {
                        ID_USUARIO = Parametros.BackdoorUsuario,
                        SENHA = Parametros.BackdoorSenha,
                        ST_ATIVO = true,
                        DT_CADASTRO = Conexao.DataHora,
                        TP = "U"
                    };

                var existente = Conexao.BancoDados.TB_CON_USUARIOs.FirstOrDefault(a => a.ID_USUARIO == usuario.ID_USUARIO);
                if (existente == null)
                    Conexao.BancoDados.TB_CON_USUARIOs.InsertOnSubmit(usuario);
                else
                {
                    existente.DT_CADASTRO = usuario.DT_CADASTRO;
                    existente.ST_ATIVO = usuario.ST_ATIVO;
                    existente.TP = usuario.TP;
                    existente.SENHA = usuario.SENHA;
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

        public void Deletar(TB_CON_USUARIO usuario, ref int posicaoTransacao)
        {
            try
            {
                Conexao.Iniciar(ref posicaoTransacao);

                var existente = Conexao.BancoDados.TB_CON_USUARIOs.FirstOrDefault(a => a.ID_USUARIO == usuario.ID_USUARIO);
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