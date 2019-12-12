using Microsoft.Win32.SafeHandles;
using SYS.UTILS;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.InteropServices;

namespace SYS.QUERYS
{
    public static class Conexao
    {
        public static ModelDataContext BancoDados
        {
            get
            {
                _BancoDados = _BancoDados ?? new ModelDataContext(Parametros.StringConexao);
                _BancoDados.CommandTimeout = 60000;

                return _BancoDados;
            }
            set
            {
                _BancoDados = value;
            }
        }
        private static ModelDataContext _BancoDados;
        private static bool Testando = false;

        public static void Reiniciar()
        {
            _BancoDados = null;
        }

        public static bool Testar()
        {
            Testando = true;
            var posicaoTransacao = 0;

            if (Iniciar(ref posicaoTransacao))
            {
                Finalizar(ref posicaoTransacao); // Se conectou, é necessário fechar a conexão.
                Testando = false;
                return true;
            }
            else
            {
                Testando = false;
                return false;
            }
        }

        public static bool Iniciar(ref int posicaoTransacao)
        {
            try
            {
                if (posicaoTransacao == 0)
                {
                    BancoDados.Connection.Open();
                    BancoDados.Transaction = BancoDados.Connection.BeginTransaction(IsolationLevel.ReadUncommitted); ;
                }

                posicaoTransacao++;
            }
            catch (SqlException excessao)
            {
                if ((excessao.Number == 67 || excessao.Number == 2 || excessao.Number == 4060 || excessao.Number == -1) && Testando)
                    Excessoes.Generico(string.Format("Não foi possível conectar-se ao banco de dados!{0}Por favor, verifique os dados de conexão e/ou sua conexão!", Environment.NewLine));
                else if (excessao.Number == 67 || excessao.Number == 2 || excessao.Number == 4060 || excessao.Number == -1)
                    Excessoes.Generico(string.Format(@"Você está desconectado do banco de dados!{0}Por favor, verifique os dados de conexão e/ou sua conexão!{0}{0}Obs.: Os processos realizados não serão salvos!", Environment.NewLine));
                else
                    throw new SYSException(Excessoes.Tratar(excessao));

                return false;
            }

            return true;
        }

        public static bool Enviar()
        {
            try
            {
                BancoDados.SubmitChanges();
            }
            catch (SqlException excessao)
            {
                if (excessao.Number == 1205) // Deadlock
                    Enviar();
                else
                    throw new SYSException(Excessoes.Tratar(excessao));

                return false;
            }

            return true;
        }

        public static bool Finalizar(ref int posicaoTransacao)
        {
            try
            {
                if (posicaoTransacao == 1)
                {
                    Enviar();

                    BancoDados.Transaction.Commit();
                    BancoDados.Transaction.Dispose();
                    BancoDados.Transaction = null;
                    BancoDados.Connection.Close();

                    Reiniciar();
                }

                posicaoTransacao--;
            }
            catch (SqlException excessao)
            {
                if (excessao.Number == 1205) // Deadlock
                    Finalizar(ref posicaoTransacao);
                else
                    throw new Exception(excessao.Tratar());
            }

            return true;
        }

        public static bool Voltar(ref int posicaoTransacao)
        {
            try
            {
                if (posicaoTransacao == 1 && BancoDados.Transaction != null)
                {
                    BancoDados.Transaction.Rollback();
                    BancoDados.Transaction.Dispose();
                    BancoDados.Transaction = null;
                    BancoDados.Connection.Close();

                    Reiniciar();
                }

                posicaoTransacao--;
            }
            catch (SqlException excessao)
            {
                if (excessao.Number == 1205) // Deadlock
                    Voltar(ref posicaoTransacao);
                else
                    throw new Exception(excessao.Tratar());

                return false;
            }

            return true;
        }

        public static DateTime DataHora
        {
            get
            {
                return BancoDados.ExecuteQuery<DateTime>("select getdate()").SingleOrDefault();
            }
        }

        public static DateTime Data
        {
            get
            {
                var dataHora = DataHora;

                return new DateTime(dataHora.Year, dataHora.Month, dataHora.Day);
            }
        }
    }
}