using DevExpress.XtraEditors;
using DevExpress.XtraLayout.Utils;
using SYS.FORMS.Lancamentos;
using SYS.QUERYS;
using SYS.QUERYS.Cadastros.Estoque;
using SYS.QUERYS.Cadastros.Fiscal;
using SYS.QUERYS.Cadastros.Gourmet;
using SYS.QUERYS.Lancamentos.Gourmet;
using SYS.UTILS;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;

namespace SYS.FORMS.Cadastros.Estoque
{
    public partial class FProduto_Cadastro : SYS.FORMS.FBase_Cadastro
    {
        #region Declarações

        public TB_EST_PRODUTO Produto = null;

        #endregion

        #region Métodos

        public FProduto_Cadastro()
        {
            InitializeComponent();
        }

        public override void Padroes()
        {
            // Pradonização do form
            this.Padronizar();

            lcgGourmet.Visibility = Parametros.ST_Gourmet ? LayoutVisibility.Always : LayoutVisibility.Never;

            // Padronização da aba Detalhes
            teID_PRODUTO.Padronizar(true);
            teNM_PRODUTO.Padronizar(false);
            beID_UNIDADE.Padronizar(true);
            teNM_UNIDADE.Padronizar(false);
            beID_GRUPO.Padronizar(true);
            teNM_GRUPO.Padronizar(false);
            beID_MARCA.Padronizar(true);
            teNM_MARCA.Padronizar(false);
            beID_DEPARTAMENTO.Padronizar(true);
            teNM_DEPARTAMENTO.Padronizar(false);

            // Padronização da aba Fiscal
            // Aba Detalhes
            teFCI.Padronizar(false);
            beID_NCM.Padronizar(true);
            teNM_NCM.Padronizar(false);
            beID_CFOP.Padronizar(true);
            teNM_CFOP.Padronizar(false);
            beID_CST.Padronizar(true);
            teNM_CST_ORIGEM.Padronizar(false);
            teNM_CST.Padronizar(false);
            beID_CSOSN.Padronizar(true);
            teNM_CSOSN.Padronizar(false);
            beID_CE.Padronizar(true);
            teNM_CE.Padronizar(false);
            
            // Aba Tributos
            beID_TRIBUTO.Padronizar(true);
            teTP_TRIBUTO.Padronizar(false);
            gcTributo.Padronizar();

            // Padronização da aba Código de barra
            beID_BARRA_REFERENCIA.Padronizar(true);
            gcCodigosBarra.Padronizar();

            // Padronização da aba Gourmet
            // Aba Complemento
            beID_GRUPO_COMPLEMENTO.Padronizar(true);
            teNM_GRUPO_COMPLEMENTO.Padronizar(false);
            gcComplemento.Padronizar();
            // Aba Composição
            beID_PRODUTO_COMPOSICAO.Padronizar(true);
            teNM_PRODUTO_COMPOSICAO.Padronizar(false);
            
            gcComposicao.Padronizar();

            // Aba Impressora
            beID_IMPRESSORA.Padronizar(true);
            teNM_IMPRESSORA.Padronizar(false);
        }
        
        private IQueryable Unidades(bool leave)
        {
            var unidade = beID_UNIDADE.Text.ToInt32(true).Padrao();

            if (leave && unidade <= 0)
                return null;

            var consulta = new QUnidade();
            var retorno = (from a in consulta.Unidades
                           where a.ID == (leave ? unidade : a.ID)
                           select new
                           {
                               ID = a.ID,
                               NM = a.NM
                           }).AsQueryable();

            if (leave)
                retorno = retorno.Take(1);

            return retorno;
        }

        private IQueryable Grupos(bool leave)
        {
            var grupo = beID_GRUPO.Text.ToInt32(true).Padrao();

            if (leave && grupo <= 0)
                return null;

            var consulta = new QGrupo();
            var retorno = from a in consulta.Buscar((leave ? grupo : 0))
                          select new
                          {
                              ID = a.ID_GRUPO,
                              NM = a.NM,
                          };

            if (leave)
                retorno = retorno.Take(1);

            return retorno;
        }

        private IQueryable Marcas(bool leave)
        {
            var marca = beID_MARCA.Text.ToInt32(true).Padrao();

            if (leave && marca <= 0)
                return null;

            var consulta = new QMarca();
            var retorno = from a in consulta.Buscar((leave ? marca : 0))
                          select new
                          {
                              ID = a.ID_MARCA,
                              NM = a.NM,
                          };

            if (leave)
                retorno = retorno.Take(1);

            return retorno;
        }

        private IQueryable Departamento(bool leave)
        {
            var marca = beID_DEPARTAMENTO.Text.ToInt32(true).Padrao();

            if (leave && marca <= 0)
                return null;

            var consulta = new QDepartamento();
            var retorno = from a in consulta.Buscar((leave ? marca : 0))
                          select new
                          {
                              ID = a.ID_DEPARTAMENTO,
                              NM = a.NM,
                          };

            if (leave)
                retorno = retorno.Take(1);

            return retorno;
        }

        private IQueryable Tributo(bool leave)
        {
            var tributo = beID_TRIBUTO.Text.ToInt32(true).Padrao();

            if (leave && tributo <= 0)
                return null;

            var consulta = new QTributo();
            var retorno = from a in consulta.Buscar((leave ? tributo : 0))
                          select new
                          {
                              ID = a.ID_TRIBUTO,
                              TP = a.TB_FIS_IMPOSTO != null ? "IMPOSTO" : (a.TB_FIS_TAXA != null ? "TAXA" : (a.TB_FIS_CONTRIBUICAO != null ? "CONTRIBUIÇÃO" : "")),
                          };

            if (leave)
                retorno = retorno.Take(1);

            return retorno;
        }

        private IQueryable Complemento(bool leave)
        {
            var complemento = beID_GRUPO_COMPLEMENTO.Text.ToInt32(true).Padrao();

            if (leave && complemento <= 0)
                return null;

            var consulta = new QGrupo();
            var retorno = from a in consulta.Buscar((leave ? complemento : 0))
                          where a.ST_COMPLEMENTO.Padrao()
                          select new
                          {
                              ID = a.ID_GRUPO,
                              NM = a.NM,
                          };

            if (leave)
                retorno = retorno.Take(1);

            return retorno;
        }

        private IQueryable Composicao(bool leave)
        {
            var produto = beID_PRODUTO_COMPOSICAO.Text.ToInt32(true).Padrao();

            if (leave && produto <= 0)
                return null;

            var consulta = new QProduto();
            var retorno = from a in consulta.Buscar((leave ? produto : 0))
                          where a.ST_ATIVO.Padrao()
                          select new
                          {
                              ID = a.ID_PRODUTO,
                              NM = a.NM,
                          };

            if (leave)
                retorno = retorno.Take(1);

            return retorno;
        }

        private IQueryable Impressora(bool leave)
        {
            var impressora = beID_IMPRESSORA.Text.ToInt32(true).Padrao();

            if (leave && impressora <= 0)
                return null;

            var consulta = new QImpressora();
            var retorno = from a in consulta.Buscar((leave ? impressora : 0))
                          select new
                          {
                              ID = a.ID_IMPRESSORA,
                              NM = a.NM,
                          };

            if (leave)
                retorno = retorno.Take(1);

            return retorno;
        }

        private IQueryable NCM(bool leave)
        {
            //var ncm = beID_NCM.Text.ToInt32(true).Padrao();

            //if (leave && ncm <= 0)
            //    return null;

            //var consulta = new QTributo();
            //var retorno = (from a in consulta.IBPT
            //              where a.ID == (((leave ? ncm.ToString() : a.ID)))
            //              select new
            //              {
            //                  ID_NCM = a.ID,
            //                  NM_NCM = a.NM,
            //              }).AsQueryable();

            //if (leave)
            //    retorno = retorno.Take(1);

            

            //return retorno;

            return null;
        }

        public override void Gravar()
        {
            try
            {
                Validar();

                #region Aba "Detalhes"

                Produto.ID_PRODUTO = teID_PRODUTO.Text.ToInt32().Padrao();
                Produto.NM = teNM_PRODUTO.Text.Validar();
                Produto.ID_UNIDADE = beID_UNIDADE.Text.ToInt32(true, "unidade").Padrao();
                Produto.ID_GRUPO = beID_GRUPO.Text.ToInt32(true, "grupo").Padrao();
                Produto.ID_MARCA = beID_MARCA.Text.ToInt32(true, "marca");
                Produto.ID_DEPARTAMENTO = beID_DEPARTAMENTO.Text.ToInt32(true, "departamento");
                Produto.ST_ALMOXARIFADO = ceST_ALMOXARIFADO.Checked;
                Produto.ST_FRACAO = ceST_FRACAO.Checked;
                Produto.ST_COMPLEMENTO = ceST_COMPLEMENTO.Checked;
                Produto.ST_SERVICO = ceST_SERVICO.Checked;
                Produto.ST_ATIVO = true;

                #endregion

                #region Aba "Fiscal"

                #region Aba "Detalhes"

                Produto.ID_FCI = teFCI.Text;
                Produto.ID_NCM = beID_NCM.Text.ToInt32();
                Produto.ID_CFOP = beID_CFOP.Text.ToInt32();
                Produto.ID_CST = beID_CST.Text.ToInt32();
                Produto.ID_CSOSN = beID_CSOSN.Text.ToInt32();
                Produto.ID_CLASSEENQUADRAMENTO = beID_CE.Text.Validar().ToInt32();
                Produto.ID_ENQUADRAMENTO = beID_E.Text.Validar().ToInt32();

                #endregion

                #region Aba "Tributos"

                Produto.TB_EST_PRODUTO_TRIBUTOs = new System.Data.Linq.EntitySet<TB_EST_PRODUTO_TRIBUTO>();

                if(gvTributo.DataSource != null)
                    Produto.TB_EST_PRODUTO_TRIBUTOs.AddRange((gvTributo.DataSource as BindingList<TB_FIS_TRIBUTO>).Select(a => new TB_EST_PRODUTO_TRIBUTO { ID_TRIBUTO = a.ID_TRIBUTO }));
                
                #endregion

                #endregion

                #region Aba "Código de barra"

                //Produto.TB_EST_PRODUTO_BARRAs = new System.Data.Linq.EntitySet<TB_EST_PRODUTO_BARRA>();
                
                //if(gvCodigosBarra.DataSource != null)
                //    Produto.TB_EST_PRODUTO_BARRAs.AddRange((gvCodigosBarra.DataSource as BindingList<TB_EST_PRODUTO_BARRA>));

                if (teCodBarra.Text.Trim() != "")
                    Produto.TB_EST_PRODUTO_BARRAs.Add(new TB_EST_PRODUTO_BARRA
                    {
                        ID_BARRA_REFERENCIA = teCodBarra.Text.Validar()
                    });

                #endregion

                #region Aba "Gourmet"

                if (Parametros.ST_Gourmet)
                {
                    Produto.TB_GOU_PRODUTO = new TB_GOU_PRODUTO();

                    #region Aba "Complemento"

                    Produto.TB_GOU_PRODUTO.TB_GOU_PRODUTO_X_GRUPOs = new System.Data.Linq.EntitySet<TB_GOU_PRODUTO_X_GRUPO>();
                    if (gvComplemento.DataSource != null)
                        Produto.TB_GOU_PRODUTO.TB_GOU_PRODUTO_X_GRUPOs.AddRange((gvComplemento.DataSource as BindingList<TB_EST_GRUPO>).Select(a => new TB_GOU_PRODUTO_X_GRUPO { ID_GRUPO = a.ID_GRUPO }));

                    #endregion

                    #region Aba "Composição"

                    Produto.TB_GOU_PRODUTO.TB_GOU_COMPOSICAOs = new System.Data.Linq.EntitySet<TB_GOU_COMPOSICAO>();
                    if (gvComposicao.DataSource != null)
                        Produto.TB_GOU_PRODUTO.TB_GOU_COMPOSICAOs.AddRange(gvComposicao.DataSource as BindingList<TB_GOU_COMPOSICAO>);

                    #endregion

                    #region Aba "Impressora"

                    Produto.TB_GOU_PRODUTO.ID_IMPRESSORA = beID_IMPRESSORA.Text.ToInt32(true,"impressora");
                    Produto.TB_GOU_PRODUTO.ST_BALANCA = ceST_BALANCA.Checked;

                    #endregion
                }

                #endregion

                var posicaoTransacao = 0;
                new QProduto().Gravar(Produto, ref posicaoTransacao);

                base.Gravar();
            }
            catch (Exception excessao)
            {
                excessao.Validar();
            }
        }

        public override void Validar()
        {
            if(teNM_PRODUTO.Text.Trim() == "")
                throw new SYSException("Informe o nome do produto!", "Menssagem");
            if (beID_UNIDADE.Text.Trim() == "")
                throw new SYSException("Informe a unidade de medida!", "Menssagem");
            if (beID_GRUPO.Text.Trim() == "")
                throw new SYSException("Informe o grupo!", "Menssagem"); 
        }

        #endregion

        #region Eventos

        private void FProduto_Cadastro_Shown(object sender, EventArgs e)
        {
            try
            {
                // Seta os padrões para os componentes
                Padroes();
                
                if (Modo == Modo.Cadastrar)
                    Produto = new TB_EST_PRODUTO();
                else if (Modo == Modo.Alterar)
                {
                    if (Produto == null)
                        Excessoes.Alterar();

                    #region Aba "Detalhes"

                    teID_PRODUTO.Text = Produto.ID_PRODUTO.ToString();
                    teNM_PRODUTO.Text = Produto.NM.Validar();

                    beID_UNIDADE.Text = Produto.ID_UNIDADE.ToString();
                    beID_UNIDADE_Leave(null, null);

                    beID_GRUPO.Text = Produto.ID_GRUPO.ToString();
                    beID_GRUPO_Leave(null, null);

                    beID_MARCA.Text = Produto.ID_MARCA != null ? Produto.ID_MARCA.ToString() : beID_MARCA.Text;
                    beID_MARCA_Leave(null, null);

                    beID_DEPARTAMENTO.Text = Produto.ID_DEPARTAMENTO != null ? Produto.ID_DEPARTAMENTO.ToString() : beID_DEPARTAMENTO.Text;
                    beID_DEPARTAMENTO_Leave(null, null);

                    ceST_ALMOXARIFADO.Checked = Produto.ST_ALMOXARIFADO.Padrao();
                    ceST_FRACAO.Checked = Produto.ST_FRACAO.Padrao();
                    ceST_COMPLEMENTO.Checked = Produto.ST_COMPLEMENTO.Padrao();
                    ceST_SERVICO.Checked = Produto.ST_SERVICO.Padrao();

                    #endregion

                    #region Aba "Fiscal"

                    #region Aba "Detalhes"

                    teFCI.Text = Produto.ID_FCI.Padrao();
                    beID_NCM.Text = Produto.ID_NCM.Padrao().ToString();
                    beID_CFOP.Text = Produto.ID_CFOP.Padrao().ToString();
                    beID_CST.Text = Produto.ID_CST.Padrao().ToString();
                    beID_CSOSN.Text = Produto.ID_CSOSN.Padrao().ToString();
                    beID_CE.Text = Produto.ID_CLASSEENQUADRAMENTO.Padrao().ToString();
                    beID_E.Text = Produto.ID_ENQUADRAMENTO.Padrao().ToString();

                    #endregion

                    #endregion

                    #region CODBARRA

                    teCodBarra.Text = Produto.TB_EST_PRODUTO_BARRAs.FirstOrDefault().ID_BARRA_REFERENCIA ?? "";

                    #endregion

                    // Aba gourmet
                    if (lcgGourmet.Visibility == LayoutVisibility.Always)
                    {
                        ceST_BALANCA.Checked = Produto.TB_GOU_PRODUTO != null ? (Produto.TB_GOU_PRODUTO.ST_BALANCA != null ? Produto.TB_GOU_PRODUTO.ST_BALANCA.Padrao() : false) : false;

                        beID_IMPRESSORA.Text = Produto.TB_GOU_PRODUTO != null ? (Produto.TB_GOU_PRODUTO.ID_IMPRESSORA != null ? Produto.TB_GOU_PRODUTO.TB_GOU_IMPRESSORA.ID_IMPRESSORA.ToString() : beID_IMPRESSORA.Text) : beID_IMPRESSORA.Text;
                        beID_IMPRESSORA_Leave(null, null);

                        //// Aba composição
                        //if (Produto.TB_GOU_PRODUTO != null && Produto.TB_GOU_PRODUTO.TB_GOU_COMPOSICAOs != null)
                        //    composicao = (from a in QQuery.BancoDados.TB_GOU_COMPOSICAOs
                        //                               join b in QQuery.BancoDados.TB_EST_PRODUTOs on a.ID_PRODUTO_COMPOSTO equals b.ID_PRODUTO
                        //                               where a.ID_PRODUTO == Produto.ID_PRODUTO
                        //                               select new QComposicao.MComposicao
                        //                               {
                        //                                   ID_PRODUTO = a.ID_PRODUTO_COMPOSTO,
                        //                                   NM = b.NM,
                        //                                   QT = a.QT ?? 0m
                        //                               }).ToList();


                        //// Aba complemento
                        //if (Produto.TB_GOU_PRODUTO != null && Produto.TB_GOU_PRODUTO.TB_GOU_PRODUTO_X_GRUPOs != null)
                        //    bsComplemento.DataSource = (from a in QQuery.BancoDados.TB_GOU_PRODUTO_X_GRUPOs
                        //                                where a.ID_PRODUTO == Produto.ID_PRODUTO
                        //                                select new SYS.QUERYS.Cadastros.Estoque.QGrupo.MGrupo
                        //                                {
                        //                                    ID_GRUPO = a.ID_GRUPO,
                        //                                    NM = a.TB_EST_GRUPO.NM
                        //                                }).ToList();
                    }
                }

                teNM_PRODUTO.Focus();
            }
            catch (Exception excessao)
            {
                excessao.Validar();
            }
        }

        #region Aba "Detalhes"

        private void beID_UNIDADE_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            try
            {
                using (var filtro = new FFiltro
                {
                    Consulta = Unidades(false),
                    Colunas = new List<Coluna>()
                    {
                        new Coluna { Nome = "ID", Descricao = "Id. da unidade", Tamanho = 100},
                        new Coluna { Nome = "NM", Descricao = "Nome da unidade", Tamanho = 350}
                    }
                })
                {
                    if (filtro.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        beID_UNIDADE.Text = (filtro.Selecionados.FirstOrDefault().ID as int?).Padrao().ToString();
                        teNM_UNIDADE.Text = (filtro.Selecionados.FirstOrDefault().NM as string).Validar();
                    }
                }
            }
            catch (Exception excessao)
            {
                excessao.Validar();
            }
        }
        private void beID_UNIDADE_Leave(object sender, EventArgs e)
        {
            try
            {
                var unidade = Unidades(true).FirstOrDefaultDynamic();

                beID_UNIDADE.Text = unidade != null ? (unidade.ID as int?).Padrao().ToString() : "";
                teNM_UNIDADE.Text = unidade != null ? (unidade.NM as string).Validar() : "";
            }
            catch (Exception excessao)
            {
                excessao.Validar();
            }
        }

        private void beID_GRUPO_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            try
            {
                using (var filtro = new FFiltro()
                {
                    Consulta = Grupos(false),
                    Colunas = new List<Coluna>()
                    {
                        new Coluna { Nome = "ID", Descricao = "Id. do grupo", Tamanho = 100},
                        new Coluna { Nome = "NM",Descricao = "Nome do grupo", Tamanho = 350}
                    }
                })
                {
                    if (filtro.ShowDialog() == DialogResult.OK)
                    {
                        beID_GRUPO.Text = (filtro.Selecionados.FirstOrDefault().ID as int?).Padrao().ToString();
                        teNM_GRUPO.Text = (filtro.Selecionados.FirstOrDefault().NM as string).Validar();
                    }
                }
            }
            catch (Exception excessao)
            {
                excessao.Validar();
            }
        }
        private void beID_GRUPO_Leave(object sender, EventArgs e)
        {
            try
            {
                var grupo = Grupos(true).FirstOrDefaultDynamic();

                beID_GRUPO.Text = grupo != null ? (grupo.ID as int?).Padrao().ToString() : "";
                teNM_GRUPO.Text = grupo != null ? (grupo.NM as string).Validar() : "";
            }
            catch (Exception excessao)
            {
                excessao.Validar();
            }
        }

        private void beID_MARCA_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            try
            {
                using (var filtro = new FFiltro()
                {
                    Consulta = Marcas(false),
                    Colunas = new List<Coluna>()
                    {
                        new Coluna { Nome = "ID", Descricao = "Id. da marca", Tamanho = 100},
                        new Coluna { Nome = "NM", Descricao = "Nome da marca", Tamanho = 350}
                    }
                })
                {
                    if (filtro.ShowDialog() == DialogResult.OK)
                    {
                        beID_MARCA.Text = (filtro.Selecionados.FirstOrDefault().ID as int?).Padrao().ToString();
                        teNM_MARCA.Text = (filtro.Selecionados.FirstOrDefault().NM as string).Validar();
                    }
                }
            }
            catch (Exception excessao)
            {
                excessao.Validar();
            }
        }
        private void beID_MARCA_Leave(object sender, EventArgs e)
        {
            try
            {
                var marca = Marcas(true).FirstOrDefaultDynamic();

                beID_MARCA.Text = marca != null ? (marca.ID as int?).Padrao().ToString() : "";
                teNM_MARCA.Text = marca != null ? (marca.NM as string).Validar() : "";
            }
            catch (Exception excessao)
            {
                excessao.Validar();
            }
        }

        private void beID_DEPARTAMENTO_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            try
            {
                using (var filtro = new FFiltro()
                {
                    Consulta = Departamento(false),
                    Colunas = new List<Coluna>()
                    {
                        new Coluna { Nome = "ID", Descricao = "Id. do departamento", Tamanho = 100},
                        new Coluna { Nome = "NM", Descricao = "Nome do departamento", Tamanho = 350}
                    }
                })
                {
                    if (filtro.ShowDialog() == DialogResult.OK)
                    {
                        beID_DEPARTAMENTO.Text = (filtro.Selecionados.FirstOrDefault().ID as int?).Padrao().ToString();
                        teNM_DEPARTAMENTO.Text = (filtro.Selecionados.FirstOrDefault().NM as string).Validar();
                    }
                }
            }
            catch (Exception excessao)
            {
                excessao.Validar();
            }
        }
        private void beID_DEPARTAMENTO_Leave(object sender, EventArgs e)
        {
            try
            {
                var departamento = Departamento(true).FirstOrDefaultDynamic();

                beID_DEPARTAMENTO.Text = departamento != null ? (departamento.ID as int?).Padrao().ToString() : "";
                teNM_DEPARTAMENTO.Text = departamento != null ? (departamento.NM as string).Validar() : "";
            }
            catch (Exception excessao)
            {
                excessao.Validar();
            }
        }

        #endregion

        #region Aba "Tributos"

        private void beID_TRIBUTO_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            try
            {
                if (e.Button.Tag.ToString() == "pesquisar")
                    using (var filtro = new FFiltro()
                    {
                        Consulta = Tributo(false),
                        Colunas = new List<Coluna>()
                        {
                            new Coluna { Nome = "ID", Descricao = "Id. do tributo", Tamanho = 100},
                            new Coluna { Nome = "TP", Descricao = "Tipo", Tamanho = 350}
                        }
                    })
                    {
                        if (filtro.ShowDialog() == DialogResult.OK)
                        {
                            beID_TRIBUTO.Text = (filtro.Selecionados.FirstOrDefault().ID as int?).Padrao().ToString();
                            teTP_TRIBUTO.Text = (filtro.Selecionados.FirstOrDefault().TP as string).Validar();
                        }
                    }
                else if (e.Button.Tag.ToString() == "adicionar")
                {
                    beID_TRIBUTO_Leave(null, null);

                    if (!beID_TRIBUTO.Text.TemValor())
                        throw new SYSException(Mensagens.Necessario("um tributo válido"));

                    var existentes = gvTributo.DataSource as BindingList<TB_FIS_TRIBUTO>;

                    if (existentes.Any(a => a.ID_TRIBUTO == beID_TRIBUTO.Text.ToInt32().Padrao()))
                        throw new SYSException("O tributo já consta adicionado na lista!");

                    existentes.Add(new QTributo().Buscar(beID_TRIBUTO.Text.ToInt32().Padrao()).FirstOrDefault());

                    gcTributo.DataSource = existentes;
                }
                else if (e.Button.Tag.ToString() == "remover")
                {
                    var selecionado = gvTributo.GetSelectedRow<TB_FIS_TRIBUTO>();
                    if (selecionado == null)
                        return;

                    var existentes = gvTributo.DataSource as BindingList<TB_FIS_TRIBUTO>;
                    existentes.Remove(selecionado);
                    gcTributo.DataSource = existentes;
                }
            }
            catch (Exception excessao)
            {
                excessao.Validar();
            }
        }
        private void beID_TRIBUTO_Leave(object sender, EventArgs e)
        {
            try
            {
                var tributo = Tributo(true).FirstOrDefaultDynamic();

                beID_TRIBUTO.Text = tributo != null ? (tributo.ID as int?).Padrao().ToString() : "";
                teTP_TRIBUTO.Text = tributo != null ? (tributo.TP as string).Validar() : "";
            }
            catch (Exception excessao)
            {
                excessao.Validar();
            }
        }

        #endregion

        #region Aba "Códigos de barra"

        private void beID_BARRA_REFERENCIA_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            try
            {
                if (e.Button.Tag.ToString() == "adicionar")
                {
                    var existentes = gvCodigosBarra.DataSource as BindingList<TB_EST_PRODUTO_BARRA>;

                    if(existentes != null)
                        if (existentes.Any(a => a.ID_BARRA_REFERENCIA == beID_BARRA_REFERENCIA.Text.Padrao()))
                            throw new SYSException("O código de barra já consta adicionado na lista!");

                    if (existentes == null)
                        existentes = new BindingList<TB_EST_PRODUTO_BARRA>();

                    existentes.Add(new TB_EST_PRODUTO_BARRA { ID_BARRA_REFERENCIA = beID_BARRA_REFERENCIA.Text.Validar() });

                    gcCodigosBarra.DataSource = existentes;
                }
                else if (e.Button.Tag.ToString() == "remover")
                {
                    var selecionado = gvCodigosBarra.GetSelectedRow<TB_EST_PRODUTO_BARRA>();
                    if (selecionado == null)
                        return;

                    var existentes = gvCodigosBarra.DataSource as BindingList<TB_EST_PRODUTO_BARRA>;
                    existentes.Remove(selecionado);
                    gcCodigosBarra.DataSource = existentes;
                }
            }
            catch (Exception excessao)
            {
                excessao.Validar();
            }
        }

        #endregion

        #region Aba "Gourmet"

        #region Aba "Complemento"

        private void beID_GRUPO_COMPLEMENTO_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            try
            {
                if (e.Button.Tag.ToString() == "pesquisar")
                    using (var filtro = new FFiltro()
                    {
                        Consulta = Complemento(false),
                        Colunas = new List<Coluna>()
                        {
                            new Coluna { Nome = "ID", Descricao = "Id. do grupo", Tamanho = 100},
                            new Coluna { Nome = "NM", Descricao = "Nome do grupo", Tamanho = 350}
                        }
                    })
                    {
                        if (filtro.ShowDialog() == DialogResult.OK)
                        {
                            beID_GRUPO_COMPLEMENTO.Text = (filtro.Selecionados.FirstOrDefault().ID as int?).Padrao().ToString();
                            teNM_GRUPO_COMPLEMENTO.Text = (filtro.Selecionados.FirstOrDefault().NM as string).Validar();
                        }
                    }
                else if (e.Button.Tag.ToString() == "adicionar")
                {
                    beID_GRUPO_COMPLEMENTO_Leave(null, null);

                    if (!beID_GRUPO_COMPLEMENTO.Text.TemValor())
                        throw new SYSException(Mensagens.Necessario("um grupo válido"));

                    var existentes = gvComplemento.DataSource as BindingList<TB_EST_GRUPO>;

                    if (existentes.Any(a => a.ID_GRUPO == beID_GRUPO_COMPLEMENTO.Text.ToInt32().Padrao()))
                        throw new SYSException("O grupo já consta adicionado na lista!");

                    existentes.Add(new QGrupo().Buscar(beID_GRUPO_COMPLEMENTO.Text.ToInt32().Padrao()).FirstOrDefault());

                    gcTributo.DataSource = existentes;
                }
                else if (e.Button.Tag.ToString() == "remover")
                {
                    var selecionado = gvComplemento.GetSelectedRow<TB_EST_GRUPO>();
                    if (selecionado == null)
                        return;

                    var existentes = gvComplemento.DataSource as BindingList<TB_EST_GRUPO>;
                    existentes.Remove(selecionado);
                    gcComplemento.DataSource = existentes;
                }
            }
            catch (Exception excessao)
            {
                excessao.Validar();
            }
        }
        private void beID_GRUPO_COMPLEMENTO_Leave(object sender, EventArgs e)
        {
            try
            {
                var complemento = Complemento(true).FirstOrDefaultDynamic();

                beID_GRUPO_COMPLEMENTO.Text = complemento != null ? (complemento.ID as int?).Padrao().ToString() : "";
                teNM_GRUPO_COMPLEMENTO.Text = complemento != null ? (complemento.NM as string).Validar() : "";
            }
            catch (Exception excessao)
            {
                excessao.Validar();
            }
        }

        #endregion

        #region Aba "Composição"

        private void beID_PRODUTO_COMPOSICAO_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            try
            {
                if (e.Button.Tag.ToString() == "pesquisar")
                    using (var filtro = new FFiltro()
                    {
                        Consulta = Composicao(false),
                        Colunas = new List<Coluna>()
                        {
                            new Coluna { Nome = "ID", Descricao = "Id. do produto", Tamanho = 100},
                            new Coluna { Nome = "NM", Descricao = "Nome do produto", Tamanho = 350}
                        }
                    })
                    {
                        if (filtro.ShowDialog() == DialogResult.OK)
                        {
                            beID_PRODUTO_COMPOSICAO.Text = (filtro.Selecionados.FirstOrDefault().ID as int?).Padrao().ToString();
                            teNM_PRODUTO_COMPOSICAO.Text = (filtro.Selecionados.FirstOrDefault().NM as string).Validar();
                        }
                    }
                else if (e.Button.Tag.ToString() == "adicionar")
                {
                    beID_PRODUTO_COMPOSICAO_Leave(null, null);

                    if (!beID_PRODUTO_COMPOSICAO.Text.TemValor())
                        throw new SYSException(Mensagens.Necessario("um produto válido"));

                    var existentes = gvComposicao.DataSource as BindingList<TB_EST_PRODUTO>;

                    if (existentes.Any(a => a.ID_PRODUTO == beID_PRODUTO_COMPOSICAO.Text.ToInt32().Padrao()))
                        throw new SYSException("O produto já consta adicionado na lista!");

                    existentes.Add(new QProduto().Buscar(beID_PRODUTO_COMPOSICAO.Text.ToInt32().Padrao()).FirstOrDefault());

                    gcComposicao.DataSource = existentes;
                }
                else if (e.Button.Tag.ToString() == "remover")
                {
                    var selecionado = gvComposicao.GetSelectedRow<TB_EST_PRODUTO>();
                    if (selecionado == null)
                        return;

                    var existentes = gvComposicao.DataSource as BindingList<TB_EST_PRODUTO>;
                    existentes.Remove(selecionado);
                    gcComposicao.DataSource = existentes;
                }
            }
            catch (Exception excessao)
            {
                excessao.Validar();
            }
        }

        private void beID_PRODUTO_COMPOSICAO_Leave(object sender, EventArgs e)
        {
            try
            {
                var complemento = Complemento(true).FirstOrDefaultDynamic();

                beID_GRUPO_COMPLEMENTO.Text = complemento != null ? (complemento.ID as int?).Padrao().ToString() : "";
                teNM_GRUPO_COMPLEMENTO.Text = complemento != null ? (complemento.NM as string).Validar() : "";
            }
            catch (Exception excessao)
            {
                excessao.Validar();
            }
        }

        #endregion

        #region Aba "Impressora"

        private void beID_IMPRESSORA_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            try
            {
                using (var filtro = new FFiltro()
                {
                    Consulta = Impressora(false),
                    Colunas = new List<Coluna>()
                    {
                        new Coluna { Nome = "ID", Descricao = "Id. da impressora", Tamanho = 100},
                        new Coluna { Nome = "NM",Descricao = "Nome da impressora", Tamanho = 350}
                    }
                })
                {
                    if (filtro.ShowDialog() == DialogResult.OK)
                    {
                        beID_IMPRESSORA.Text = (filtro.Selecionados.FirstOrDefault().ID as int?).Padrao().ToString();
                        teNM_IMPRESSORA.Text = (filtro.Selecionados.FirstOrDefault().NM as string).Validar();
                    }
                }
            }
            catch (Exception excessao)
            {
                excessao.Validar();
            }
        }
        private void beID_IMPRESSORA_Leave(object sender, EventArgs e)
        {
            try
            {
                var impressora = Impressora(true).FirstOrDefaultDynamic();

                beID_IMPRESSORA.Text = impressora != null ? (impressora.ID as int?).Padrao().ToString() : "";
                teNM_IMPRESSORA.Text = impressora != null ? (impressora.NM as string).Validar() : "";
            }
            catch (Exception excessao)
            {
                excessao.Validar();
            }
        }

        #endregion

        #endregion

        #endregion

        private void beID_NCM_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            try
            {
                using (var filtro = new FFiltro()
                {
                    Consulta = NCM(false),
                    Colunas = new List<Coluna>()
                    {
                        new Coluna { Nome = "ID_NCM", Descricao = "Id. do NCM", Tamanho = 100},
                        new Coluna { Nome = "NM_NCM",Descricao = "Nome do NCM", Tamanho = 350}
                    }
                })
                {
                    if (filtro.ShowDialog() == DialogResult.OK)
                    {
                        beID_NCM.Text = (filtro.Selecionados.FirstOrDefault().ID_NCM as int?).Padrao().ToString();
                        teNM_NCM.Text = (filtro.Selecionados.FirstOrDefault().NM_NCM as string).Validar();
                    }
                }
            }
            catch (Exception excessao)
            {
                excessao.Validar();
            }
        }

        private void beID_NCM_Leave(object sender, EventArgs e)
        {
            try
            {
                var ncm = NCM(true).FirstOrDefaultDynamic();

                beID_NCM.Text = ncm != null ? (ncm.ID_NCM as string).Validar() : "";
                teNM_NCM.Text = ncm != null ? (ncm.NM_NCM as string).Validar() : "";
            }
            catch (Exception excessao)
            {
                excessao.Validar();
            }
        }
        
      

        private void beID_CFOP_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {

        }

        private void beID_CFOP_Leave(object sender, EventArgs e)
        {

        }

        private void beID_CST_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {

        }

        private void beID_CST_Leave(object sender, EventArgs e)
        {

        }

        private void beID_CSOSN_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {

        }

        private void beID_CSOSN_Leave(object sender, EventArgs e)
        {

        }

        private void beID_CE_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {

        }

        private void beID_CE_Leave(object sender, EventArgs e)
        {

        }

        private void beID_E_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {

        }

        private void beID_E_Leave(object sender, EventArgs e)
        {

        }
    }
}