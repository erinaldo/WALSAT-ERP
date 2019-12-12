using SYS.QUERYS;
using SYS.QUERYS.Cadastros.Relacionamento;
using SYS.UTILS;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Linq;

namespace SYS.FORMS.Cadastros.Relacionamento
{
    public partial class FClifor_Cadastro : SYS.FORMS.FBase_Cadastro
    {
        #region Declarações

        public TB_REL_CLIFOR Clifor = new TB_REL_CLIFOR();

        #endregion

        #region Métodos

        public FClifor_Cadastro()
        {
            InitializeComponent();

            this.Shown += FClifor_Cadastro_Shown;
        }
        
        public override void Gravar()
        {
            try
            {
                Validar();

                Clifor = new TB_REL_CLIFOR();
                Clifor.ID_CLIFOR = teID_CLIFOR.Text.ToInt32().Padrao();
                Clifor.NM = teNM.Text.Validar(true);
                Clifor.ST_ATIVO = ceST_ATIVO.Checked;
                Clifor.CPF = teCNPJ_CPF.Text.Validar(true).Length == 11 ? teCNPJ_CPF.Text.Validar(true) : null;
                Clifor.CNPJ = teCNPJ_CPF.Text.Validar(true).Length == 14 ? teCNPJ_CPF.Text.Validar(true) : null;
                Clifor.NM_FANTASIA = teNM_FANTASIA.Text.Validar(true);
                Clifor.RG = teRG.Text.Validar(true);
                Clifor.CNAE = beCNAE.Text.Validar(true);
                Clifor.ID_REGIMETRIBUTARIO = (cbeID_REGIMETRIBUTARIO.SelectedIndex + 1).ToString();
                Clifor.IM = teIM.Text.Validar(true);
                Clifor.IE_INDICADOR = cbeIE_INDICADOR.SelectedIndex == 3 ? "9" : (cbeIE_INDICADOR.SelectedIndex + 1).ToString();
                Clifor.IE = teIE.Text.Validar(true);
                Clifor.IE_SUBSTITUTOTRIBUTARIO = teIE_SUBSTITUTOTRIBUTARIO.Text.Validar(true);

                var posicaoTransacao = 0;

                new QClifor().Gravar(Clifor, ref posicaoTransacao);

                base.Gravar();
            }
            catch (Exception excessao)
            {
                excessao.Validar();
            }
        }


        #endregion

        #region Métodos

        private void FClifor_Cadastro_Shown(object sender, EventArgs e)
        {
            try
            {
                if (Modo == Modo.Cadastrar)
                    Clifor = new TB_REL_CLIFOR();
                else if (Modo == Modo.Alterar)
                {
                    teID_CLIFOR.Text = Clifor.ID_CLIFOR.ToString();
                    teNM.Text = Clifor.NM.Validar();
                    ceST_ATIVO.Checked = Clifor.ST_ATIVO.Padrao();
                    teCNPJ_CPF.Text = Clifor.CPF == null ? Clifor.CNPJ.Validar() : Clifor.CPF.Validar();
                    teNM_FANTASIA.Text = Clifor.NM_FANTASIA.Validar();
                    teRG.Text = Clifor.RG.Validar();
                    beCNAE.Text = Clifor.CNAE.Validar();
                    cbeID_REGIMETRIBUTARIO.SelectedIndex = Clifor.ID_REGIMETRIBUTARIO.ToInt32().Padrao();
                    teIM.Text = Clifor.IM.Validar();
                    cbeIE_INDICADOR.SelectedIndex = Clifor.IE_INDICADOR.ToInt32().Padrao() == 9 ? 3 : Clifor.IE_INDICADOR.ToInt32().Padrao();
                    teIE.Text = Clifor.IE.Validar();
                    teIE_SUBSTITUTOTRIBUTARIO.Text = Clifor.IE_SUBSTITUTOTRIBUTARIO.Validar();
                    deDT_CADASTRO.DateTime = Clifor.DT_CADASTRO.Padrao();

                    if (Clifor.TB_REL_CLIFOR_X_ENDERECOs != null && Clifor.TB_REL_CLIFOR_X_ENDERECOs.Count > 0)
                        gcEndereco.DataSource = Clifor.TB_REL_CLIFOR_X_ENDERECOs.Select(a => a.TB_REL_ENDERECO) as BindingList<TB_REL_ENDERECO>;

                    if (Clifor.TB_REL_CLIFOR_X_BANCO_CONTAs != null && Clifor.TB_REL_CLIFOR_X_BANCO_CONTAs.Count > 0)
                        gcDadosBancarios.DataSource = Clifor.TB_REL_CLIFOR_X_BANCO_CONTAs.Select(a => a.TB_FIN_BANCO_CONTA) as BindingList<TB_FIN_BANCO_CONTA>;
                }

                teNM.Focus();
            }
            catch (Exception excessao)
            {
                excessao.Validar();
            }
        }

        private void beID_ENDERECO_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            try
            {
                var enderecos = new QEndereco();
                var paisesUFsCidades = new QPaisUFCidade();

                if (e.Button.Tag.ToString() == "buscar")
                {
                    using (var filtro = new SYS.FORMS.FFiltro
                    {
                        Consulta = from a in (from a in new QEndereco().Buscar()
                                              select new
                                              {
                                                  ID_ENDERECO = a.ID_ENDERECO,
                                                  NM_RUA = a.NM_RUA,
                                                  NM_BAIRRO = a.NM_BAIRRO,
                                                  NR = a.NR,
                                                  CEP = a.CEP,
                                                  ID_CIDADE = a.ID_CIDADE,
                                                  ID_UF = a.ID_UNIDADEFEDERATIVA,
                                                  ID_PAIS = a.ID_PAIS
                                              }).ToList().AsQueryable() // desprende do banco por causa do join que está local, e não no banco de dados
                                   join b in paisesUFsCidades.Cidades on new { a.ID_CIDADE } equals new { ID_CIDADE = (int?)b.ID_CIDADE }
                                   join c in paisesUFsCidades.UFs on new { a.ID_UF } equals new { ID_UF = (int?)c.ID_UF }
                                   join d in paisesUFsCidades.Paises on new { a.ID_PAIS } equals new { ID_PAIS = (int?)d.ID_PAIS }
                                   select new
                                   {
                                       a.ID_ENDERECO,
                                       a.NM_RUA,
                                       a.NM_BAIRRO,
                                       a.NR,
                                       a.CEP,
                                       NM_CIDADE = b.NM,
                                       NM_UF = c.NM,
                                       NM_PAIS = d.NM
                                   },
                        Colunas = new List<Coluna>()
                            {
                                new Coluna { Nome = "ID_ENDERECO", Descricao = "Identificador do endereço", Tamanho = 100},
                                new Coluna { Nome = "CEP", Descricao = "C.E.P.", Tamanho = 100},
                                new Coluna { Nome = "ID_CIDADE", Descricao = "Identificador da cidade", Tamanho = 100},
                                new Coluna { Nome = "NM_CIDADE",Descricao = "Nome da cidade", Tamanho = 350},
                                new Coluna { Nome = "ID_UF",Descricao = "Identificador da U.F.", Tamanho = 100},
                                new Coluna { Nome = "NM_UF",Descricao = "Nome da U.F.", Tamanho = 350},
                                new Coluna { Nome = "ID_PAIS",Descricao = "Identificador do país", Tamanho = 100},
                                new Coluna { Nome = "NM_PAIS",Descricao = "Nome do país", Tamanho = 350},
                            }
                    })
                    {
                        if (filtro.ShowDialog() == DialogResult.OK)
                        {
                            beID_ENDERECO.Text = (filtro.Selecionados.FirstOrDefault().ID_ENDERECO as int?).Padrao().ToString();
                            teCEP.Text = (filtro.Selecionados.FirstOrDefault().CEP as string).Padrao().ToString();
                            teNM_RUA.Text = (filtro.Selecionados.FirstOrDefault().NM_RUA as string).Padrao().ToString();
                            teNM_BAIRRO.Text = (filtro.Selecionados.FirstOrDefault().NM_BAIRRO as string).Padrao().ToString();
                            teNM_UF.Text = (filtro.Selecionados.FirstOrDefault().NM_UF as string).Padrao().ToString();
                            teNM_PAIS.Text = (filtro.Selecionados.FirstOrDefault().NM_PAIS as string).Padrao().ToString();
                        }
                    }
                }
                else if (e.Button.Tag.ToString() == "adicionar")
                {
                    if (beID_ENDERECO.Text.TemValor())
                    {
                        var endereco = (from a in enderecos.Buscar(beID_ENDERECO.Text.ToInt32().Padrao())
                                        select a).FirstOrDefault();

                        if (endereco != null)
                        {
                            var lista = (gvEndereco.DataSource as BindingList<TB_REL_ENDERECO>) ?? new BindingList<TB_REL_ENDERECO>();
                            lista.Add(endereco);
                            gcEndereco.DataSource = lista;
                        }
                    }
                }
                else if (e.Button.Tag.ToString() == "remover")
                {
                    var selecionado = gvEndereco.GetSelectedRow();

                    if (selecionado != null)
                    {
                        var lista = (gvEndereco.DataSource as BindingList<TB_REL_ENDERECO>) ?? new BindingList<TB_REL_ENDERECO>();
                        lista.Remove(lista.FirstOrDefault(a => a.ID_ENDERECO == selecionado.ID_ENDERECO));
                        gcEndereco.DataSource = lista;
                    }
                }
            }
            catch (Exception excessao)
            {
                excessao.Validar();
            }
        }

        private void beID_ENDERECO_Leave(object sender, EventArgs e)
        {
            try
            {
                var enderecos = new QEndereco();
                var paisesUFsCidades = new QPaisUFCidade();

                if (beID_ENDERECO.Text.TemValor())
                {
                    var endereco = (from a in (from a in new QEndereco().Buscar(beID_ENDERECO.Text.ToInt32().Padrao())
                                               select new
                                               {
                                                   ID_ENDERECO = a.ID_ENDERECO,
                                                   NM_RUA = a.NM_RUA,
                                                   NM_BAIRRO = a.NM_BAIRRO,
                                                   NR = a.NR,
                                                   CEP = a.CEP,
                                                   ID_CIDADE = a.ID_CIDADE,
                                                   ID_UF = a.ID_UNIDADEFEDERATIVA,
                                                   ID_PAIS = a.ID_PAIS
                                               }).ToList().AsQueryable() // desprende do banco por causa do join que está local, e não no banco de dados
                                    join b in paisesUFsCidades.Cidades on new { a.ID_CIDADE } equals new { ID_CIDADE = (int?)b.ID_CIDADE }
                                    join c in paisesUFsCidades.UFs on new { a.ID_UF } equals new { ID_UF = (int?)c.ID_UF }
                                    join d in paisesUFsCidades.Paises on new { a.ID_PAIS } equals new { ID_PAIS = (int?)d.ID_PAIS }
                                    select new
                                    {
                                        a.ID_ENDERECO,
                                        a.NM_RUA,
                                        a.NM_BAIRRO,
                                        a.NR,
                                        a.CEP,
                                        NM_CIDADE = b.NM,
                                        NM_UF = c.NM,
                                        NM_PAIS = d.NM
                                    }).FirstOrDefault();

                    beID_ENDERECO.Text = endereco == null ? "" : endereco.ID_ENDERECO.ToString();
                    teCEP.Text = endereco == null ? "" : endereco.CEP.Validar();
                    teNM_RUA.Text = endereco == null ? "" : endereco.NM_RUA.Validar();
                    teNM_BAIRRO.Text = endereco == null ? "" : endereco.NM_BAIRRO.Validar();
                    teNM_CIDADE.Text = endereco == null ? "" : endereco.NM_CIDADE.Validar();
                    teNM_UF.Text = endereco == null ? "" : endereco.NM_UF.Validar();
                    teNM_PAIS.Text = endereco == null ? "" : endereco.NM_PAIS.Validar();
                }
                else
                {
                    beID_ENDERECO.Text = "";
                    teCEP.Text = "";
                    teNM_RUA.Text = "";
                    teNM_BAIRRO.Text = "";
                    teNM_CIDADE.Text = "";
                    teNM_UF.Text = "";
                    teNM_PAIS.Text = "";
                }
            }
            catch (Exception excessao)
            {
                excessao.Validar();
            }
        }

        private void beID_CONTA_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            //try
            //{
            //    var enderecos = new QEndereco();
            //    var paisesUFsCidades = new QPaisUFCidade();

            //    if (e.Button.Tag.ToString() == "buscar")
            //    {
            //        using (var filtro = new SYS.FORMS.FFiltro
            //        {
            //            Consulta = from a in new QBanco,
            //            Colunas = new List<Coluna>()
            //                {
            //                    new Coluna { Nome = "ID_ENDERECO", Descricao = "Identificador do endereço", Tamanho = 100},
            //                    new Coluna { Nome = "CEP", Descricao = "C.E.P.", Tamanho = 100},
            //                    new Coluna { Nome = "ID_CIDADE", Descricao = "Identificador da cidade", Tamanho = 100},
            //                    new Coluna { Nome = "NM_CIDADE",Descricao = "Nome da cidade", Tamanho = 350},
            //                    new Coluna { Nome = "ID_UF",Descricao = "Identificador da U.F.", Tamanho = 100},
            //                    new Coluna { Nome = "NM_UF",Descricao = "Nome da U.F.", Tamanho = 350},
            //                    new Coluna { Nome = "ID_PAIS",Descricao = "Identificador do país", Tamanho = 100},
            //                    new Coluna { Nome = "NM_PAIS",Descricao = "Nome do país", Tamanho = 350},
            //                }
            //        })
            //        {
            //            if (filtro.ShowDialog() == DialogResult.OK)
            //            {
            //                beID_ENDERECO.Text = (filtro.Selecionados.FirstOrDefault().ID_ENDERECO as int?).Padrao().ToString();
            //                teCEP.Text = (filtro.Selecionados.FirstOrDefault().CEP as string).Padrao().ToString();
            //                teNM_RUA.Text = (filtro.Selecionados.FirstOrDefault().NM_RUA as string).Padrao().ToString();
            //                teNM_BAIRRO.Text = (filtro.Selecionados.FirstOrDefault().NM_BAIRRO as string).Padrao().ToString();
            //                teNM_UF.Text = (filtro.Selecionados.FirstOrDefault().NM_UF as string).Padrao().ToString();
            //                teNM_PAIS.Text = (filtro.Selecionados.FirstOrDefault().NM_PAIS as string).Padrao().ToString();
            //            }
            //        }
            //    }
            //    else if (e.Button.Tag.ToString() == "adicionar")
            //    {
            //        if (beID_ENDERECO.Text.TemValor())
            //        {
            //            var endereco = (from a in enderecos.Buscar(beID_ENDERECO.Text.ToInt32().Padrao())
            //                            select a).FirstOrDefault();

            //            if (endereco != null)
            //            {
            //                var lista = gvEndereco.DataSource as List<TB_REL_ENDERECO>;
            //                lista.Add(endereco);
            //                gcEndereco.DataSource = lista;
            //            }
            //        }
            //    }
            //    else if (e.Button.Tag.ToString() == "remover")
            //    {
            //        var selecionado = gvEndereco.GetSelectedRow();

            //        if (selecionado != null)
            //        {
            //            var lista = gvEndereco.DataSource as List<TB_REL_ENDERECO>;
            //            lista.Remove(lista.FirstOrDefault(a => a.ID_ENDERECO == selecionado.ID_ENDERECO));
            //            gcEndereco.DataSource = lista;
            //        }
            //    }
            //}
            //catch (Exception excessao)
            //{
            //    excessao.Validar();
            //}
        }

        #endregion
    }
}