using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SYS.UTILS;
using System.Data.SqlClient;
using SYS.QUERYS.Cadastros.Fiscal;

namespace SYS.QUERYS.Cadastros.Estoque
{
    public class QProduto
    {
        public class ListaMProduto : MProduto { }

        public class MProduto
        {
            public int ID_PRODUTO { get; set; }
            public int ID_GRUPO { get; set; }
            public int ID_UNIDADE { get; set; }
            public int ID_MARCA { get; set; }
            public string NM { get; set; }
            public string ID_BARRA { get; set; }
            public string ID_NCM { get; set; }
            public string ID_CST { get; set; }
            public string ID_CFOP { get; set; }
            public string ID_FCI { get; set; }
            public int ID_CLASSEENQUADRAMENTO { get; set; }
            public int ID_ENQUADRAMENTO { get; set; }
            public bool ST_ATIVO { get; set; }
            public DateTime DT_CADASTRO { get; set; }

            public MProduto()
            {
                this.ID_PRODUTO = 0;
                this.ID_GRUPO = 0;
                this.ID_UNIDADE = 0;
                this.ID_MARCA = 0;
                this.NM = "";
                this.ID_BARRA = "";
                this.ID_NCM = "";
                this.ID_CST = "";
                this.ID_CFOP = "";
                this.ID_FCI = "";
                this.ID_CLASSEENQUADRAMENTO = 0;
                this.ID_ENQUADRAMENTO = 0;
                this.ST_ATIVO = false;
            }

        }

        public IQueryable<TB_EST_PRODUTO> Buscar(int id_produto = 0)
        {
            var consulta = from a in Conexao.BancoDados.TB_EST_PRODUTOs
                           select a;

            if (id_produto > 0)
                consulta = consulta.Where(a => a.ID_PRODUTO == id_produto);

            return consulta;
        }

        public void Gravar(TB_EST_PRODUTO produto, ref int posicaoTransacao)
        {
            try
            {
                Conexao.Iniciar(ref posicaoTransacao);

                var existente = Conexao.BancoDados.TB_EST_PRODUTOs.FirstOrDefault(a => a.ID_PRODUTO == produto.ID_PRODUTO);

                #region Inserção

                if (existente == null)
                {
                    produto.ID_PRODUTO = (Conexao.BancoDados.TB_EST_PRODUTOs.Any() ? Conexao.BancoDados.TB_EST_PRODUTOs.Max(a => a.ID_PRODUTO) : 0) + 1;
                    Conexao.BancoDados.TB_EST_PRODUTOs.InsertOnSubmit(produto);

                    foreach (var tributo in produto.TB_EST_PRODUTO_TRIBUTOs)
                    {
                        tributo.ID_PRODUTO = produto.ID_PRODUTO;
                        Conexao.BancoDados.TB_EST_PRODUTO_TRIBUTOs.InsertOnSubmit(tributo);
                        Conexao.Enviar();
                    }

                    foreach (var barra in produto.TB_EST_PRODUTO_BARRAs)
                    {
                        barra.ID_PRODUTO = produto.ID_PRODUTO;
                        barra.ID_BARRA = (Conexao.BancoDados.TB_EST_PRODUTO_BARRAs.Where(a => a.ID_PRODUTO == produto.ID_PRODUTO).Any() ? Conexao.BancoDados.TB_EST_PRODUTO_BARRAs.Where(a => a.ID_PRODUTO == produto.ID_PRODUTO).Max(a => a.ID_BARRA) : 0) + 1;
                        Conexao.BancoDados.TB_EST_PRODUTO_BARRAs.InsertOnSubmit(barra);
                        Conexao.Enviar();
                    }

                    if (Parametros.ST_Gourmet)
                    {
                        var st_balanca = produto.TB_GOU_PRODUTO.ST_BALANCA;
                        var id_impressora = produto.TB_GOU_PRODUTO.ID_IMPRESSORA;
                        produto.TB_GOU_PRODUTO = new TB_GOU_PRODUTO();
                        produto.TB_GOU_PRODUTO.ID_PRODUTO = produto.ID_PRODUTO;
                        produto.TB_GOU_PRODUTO.ST_BALANCA = st_balanca;
                        if(id_impressora >0)
                            produto.TB_GOU_PRODUTO.ID_IMPRESSORA = id_impressora;
                        Conexao.BancoDados.TB_GOU_PRODUTOs.InsertOnSubmit(produto.TB_GOU_PRODUTO);
                        Conexao.Enviar();

                        foreach (var complemento in produto.TB_GOU_PRODUTO.TB_GOU_PRODUTO_X_GRUPOs)
                        {
                            complemento.ID_PRODUTO = produto.ID_PRODUTO;
                            Conexao.BancoDados.TB_GOU_PRODUTO_X_GRUPOs.InsertOnSubmit(complemento);
                            Conexao.Enviar();
                        }

                        foreach (var composicao in produto.TB_GOU_PRODUTO.TB_GOU_COMPOSICAOs)
                        {
                            composicao.ID_PRODUTO = produto.ID_PRODUTO;
                            Conexao.BancoDados.TB_GOU_COMPOSICAOs.InsertOnSubmit(composicao);
                            Conexao.Enviar();
                        }
                    }
                }

                #endregion

                #region Atualização

                else
                {
                    existente.NM = produto.NM;
                    existente.ID_UNIDADE = produto.ID_UNIDADE;
                    existente.ID_GRUPO = produto.ID_GRUPO;
                    existente.ID_MARCA = produto.ID_MARCA;
                    existente.ID_DEPARTAMENTO = produto.ID_DEPARTAMENTO;
                    existente.ST_ALMOXARIFADO = produto.ST_ALMOXARIFADO;
                    existente.ST_FRACAO = produto.ST_FRACAO;
                    existente.ST_COMPLEMENTO = produto.ST_COMPLEMENTO;
                    existente.ST_SERVICO = produto.ST_SERVICO;

                    existente.ID_FCI = produto.ID_FCI;
                    existente.ID_NCM = produto.ID_NCM;
                    existente.ID_CFOP = produto.ID_CFOP;
                    existente.ID_CST = produto.ID_CST;
                    existente.ID_CSOSN = produto.ID_CSOSN;
                    existente.ID_CLASSEENQUADRAMENTO = produto.ID_CLASSEENQUADRAMENTO;
                    existente.ID_ENQUADRAMENTO = produto.ID_ENQUADRAMENTO;
                    
                    var tributosDeletar = from a in Conexao.BancoDados.TB_EST_PRODUTO_TRIBUTOs
                                          where
                                          a.ID_PRODUTO == produto.ID_PRODUTO &&
                                          !produto.TB_EST_PRODUTO_TRIBUTOs.Select(b => b.ID_TRIBUTO).Contains(a.ID_TRIBUTO)
                                          select a;

                    if (tributosDeletar != null && tributosDeletar.Count() > 0)
                    {
                        Conexao.BancoDados.TB_EST_PRODUTO_TRIBUTOs.DeleteAllOnSubmit(tributosDeletar);
                        Conexao.Enviar();
                    }

                    var tributosCadastrar = from a in produto.TB_EST_PRODUTO_TRIBUTOs
                                            where Conexao.BancoDados.TB_EST_PRODUTO_TRIBUTOs.Where(b => b.ID_PRODUTO == produto.ID_PRODUTO).Select(b => b.ID_TRIBUTO).ToList().Contains(a.ID_TRIBUTO)
                                            select a;

                    if (tributosCadastrar != null && tributosCadastrar.Count() > 0)
                    {
                        Conexao.BancoDados.TB_EST_PRODUTO_TRIBUTOs.InsertAllOnSubmit(tributosCadastrar);
                        Conexao.Enviar();
                    }

                    var barrasDeletar = from a in Conexao.BancoDados.TB_EST_PRODUTO_BARRAs
                                        where
                                        a.ID_PRODUTO == produto.ID_PRODUTO &&
                                        !produto.TB_EST_PRODUTO_BARRAs.Select(b => b.ID_BARRA).Contains(a.ID_BARRA)
                                        select a;

                    if (barrasDeletar != null && barrasDeletar.Count() > 0)
                    {
                        Conexao.BancoDados.TB_EST_PRODUTO_BARRAs.DeleteAllOnSubmit(barrasDeletar);
                        Conexao.Enviar();
                    }

                    var barrasCadastrar = from a in produto.TB_EST_PRODUTO_BARRAs
                                          where Conexao.BancoDados.TB_EST_PRODUTO_BARRAs.Where(b => b.ID_PRODUTO == produto.ID_PRODUTO).Select(b => b.ID_BARRA).ToList().Contains(a.ID_BARRA)
                                          select a;

                    if (tributosCadastrar != null && tributosCadastrar.Count() > 0)
                    {
                        Conexao.BancoDados.TB_EST_PRODUTO_TRIBUTOs.InsertAllOnSubmit(tributosCadastrar);
                        Conexao.Enviar();
                    }

                    if (Parametros.ST_Gourmet)
                    {
                        var complementosDeletar = from a in Conexao.BancoDados.TB_GOU_PRODUTO_X_GRUPOs
                                                  where
                                                  a.ID_PRODUTO == produto.ID_PRODUTO &&
                                                  !produto.TB_GOU_PRODUTO.TB_GOU_PRODUTO_X_GRUPOs.Select(b => b.ID_GRUPO).Contains(a.ID_GRUPO)
                                                  select a;

                        if (complementosDeletar != null && complementosDeletar.Count() > 0)
                        {
                            Conexao.BancoDados.TB_GOU_PRODUTO_X_GRUPOs.DeleteAllOnSubmit(complementosDeletar);
                            Conexao.Enviar();
                        }

                        var complementosCadastrar = from a in produto.TB_GOU_PRODUTO.TB_GOU_PRODUTO_X_GRUPOs
                                                    where Conexao.BancoDados.TB_GOU_PRODUTO_X_GRUPOs.Where(b => b.ID_PRODUTO == produto.ID_PRODUTO).Select(b => b.ID_GRUPO).ToList().Contains(a.ID_GRUPO)
                                                    select a;

                        if (complementosCadastrar != null && complementosCadastrar.Count() > 0)
                        {
                            Conexao.BancoDados.TB_GOU_PRODUTO_X_GRUPOs.InsertAllOnSubmit(complementosCadastrar);
                            Conexao.Enviar();
                        }

                        var composicoesDeletar = from a in Conexao.BancoDados.TB_GOU_COMPOSICAOs
                                                 where
                                                 a.ID_PRODUTO == produto.ID_PRODUTO &&
                                                 !produto.TB_GOU_PRODUTO.TB_GOU_COMPOSICAOs.Select(b => b.ID_PRODUTO_COMPOSTO).Contains(a.ID_PRODUTO_COMPOSTO)
                                                 select a;

                        if (composicoesDeletar != null && composicoesDeletar.Count() > 0)
                        {
                            Conexao.BancoDados.TB_GOU_COMPOSICAOs.DeleteAllOnSubmit(composicoesDeletar);
                            Conexao.Enviar();
                        }

                        var composicoesCadastrar = from a in produto.TB_GOU_PRODUTO.TB_GOU_COMPOSICAOs
                                                   where Conexao.BancoDados.TB_GOU_COMPOSICAOs.Where(b => b.ID_PRODUTO == produto.ID_PRODUTO).Select(b => b.ID_PRODUTO_COMPOSTO).ToList().Contains(a.ID_PRODUTO_COMPOSTO)
                                                   select a;

                        if (composicoesCadastrar != null && composicoesCadastrar.Count() > 0)
                        {
                            Conexao.BancoDados.TB_GOU_COMPOSICAOs.InsertAllOnSubmit(composicoesCadastrar);
                            Conexao.Enviar();
                        }
                    }
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

        public void Deletar(TB_EST_PRODUTO produto, ref int posicaoTransacao)
        {
            try
            {
                Conexao.Iniciar(ref posicaoTransacao);

                var existente = Conexao.BancoDados.TB_EST_PRODUTOs.FirstOrDefault(a => a.ID_PRODUTO == produto.ID_PRODUTO);
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