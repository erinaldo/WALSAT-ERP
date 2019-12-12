using DevExpress.XtraEditors;
using SYS.QUERYS;
using SYS.QUERYS.Cadastros.Relacionamento;
using SYS.UTILS;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace SYS.FORMS.Cadastros.Relacionamento
{
    public partial class FEndereco_Cadastro : SYS.FORMS.FBase_Cadastro
    {
        #region Declarações

        public TB_REL_ENDERECO Endereco;

        #endregion

        #region Métodos

        public FEndereco_Cadastro()
        {
            InitializeComponent();

            Shown += FEndereco_Cadastro_Shown;
        }

        public override void Gravar()
        {
            try
            {
                Validar();

                Endereco.ID_ENDERECO = teID_ENDERECO.Text.ToInt32().Padrao();
                Endereco.NM_RUA = teNM_RUA.Text.Validar();
                Endereco.NM_BAIRRO = teNM_BAIRRO.Text.Validar();
                Endereco.NR = teNR.Text.Validar();
                Endereco.DS_COMPLEMENTO = meDS_COMPLEMENTO.Text.Validar();
                Endereco.CEP = teCEP.Text.Validar().Replace("-", "");
                Endereco.ID_CIDADE = beID_CIDADE.Text.ToInt32().Padrao();
                Endereco.ID_UNIDADEFEDERATIVA = beID_UF.Text.ToInt32();
                Endereco.ID_PAIS = beID_PAIS.Text.ToInt32();

                var posicaoTransacao = 0;
                new QEndereco().Gravar(Endereco, ref posicaoTransacao);

                base.Gravar();
            }
            catch (Exception excessao)
            {
                excessao.Validar();
            }
        }

        #endregion

        #region Métodos

        private void FEndereco_Cadastro_Shown(object sender, EventArgs e)
        {
            try
            {
                if (Modo == Modo.Cadastrar)
                    Endereco = new TB_REL_ENDERECO();
                else if (Modo == Modo.Alterar)
                {
                    teID_ENDERECO.Text = Endereco.ID_ENDERECO.ToString();
                    teCEP.Text = Endereco.CEP.Validar();
                    teNM_RUA.Text = Endereco.NM_RUA.Validar();
                    teNM_BAIRRO.Text = Endereco.NM_BAIRRO.Validar();
                    teNR.Text = Endereco.NR.Validar();
                    beID_CIDADE.Text = Endereco.ID_CIDADE.Padrao().ToString();
                    beID_CIDADE_Leave(null, null);
                    beID_PAIS.Text = Endereco.ID_PAIS.Padrao().ToString();
                    beID_PAIS_Leave(null, null);
                    meDS_COMPLEMENTO.Text = Endereco.DS_COMPLEMENTO.Validar();
                }
            }
            catch (Exception excessao)
            {
                excessao.Validar();
            }
        }

        private void teCEP_Leave(object sender, EventArgs e)
        {
            try
            {
                if (teCEP.Text.TemValor() && teCEP.Text.Length == 9 && XtraMessageBox.Show("Deseja buscar o C.E.P. nos correios?", "Atenção!", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    var cep = new correios.AtendeClienteService();

                    var dados = cep.consultaCEP(teCEP.Text.Replace("-", ""));
                    if (dados != null)
                    {
                        teNM_RUA.Text = dados.end.Validar();
                        teNM_BAIRRO.Text = dados.bairro.Validar();
                        meDS_COMPLEMENTO.Text = dados.complemento.Validar() + Environment.NewLine + dados.complemento2.Validar();

                        var consulta = new QPaisUFCidade();

                        var uf = consulta.UFs.FirstOrDefault(a => a.SIGLA == dados.uf.Validar());

                        if (uf != null)
                        {
                            var cidade = consulta.Cidades.FirstOrDefault(a => a.NM == dados.cidade.Validar() && a.ID_UF == uf.ID_UF);

                            if (cidade != null)
                            {
                                beID_CIDADE.Text = cidade.ID_CIDADE.ToString();
                                beID_CIDADE_Leave(null, null);
                            }
                        }
                    }
                }
            }
            catch (Exception excessao)
            {
                excessao.Validar();
            }
        }

        private void beID_CIDADE_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            try
            {
                var consulta = new QPaisUFCidade();

                using (var filtro = new SYS.FORMS.FFiltro
                {
                    Consulta = (from a in consulta.Cidades
                                join b in consulta.UFs on new { a.ID_UF } equals new { b.ID_UF }
                                join c in consulta.Paises on new { b.ID_PAIS } equals new { c.ID_PAIS }
                                select new
                                {
                                    ID_CIDADE = a.ID_CIDADE,
                                    NM_CIDADE = a.NM,
                                    ID_UF = b.ID_UF,
                                    NM_UF = b.NM,
                                    ID_PAIS = c.ID_PAIS,
                                    NM_PAIS = c.NM
                                }).AsQueryable(),
                    Colunas = new List<SYS.FORMS.Coluna>()
                                {
                                    new SYS.FORMS.Coluna { Nome = "ID_CIDADE", Descricao = "Identificador da cidade", Tamanho = 100},
                                    new SYS.FORMS.Coluna { Nome = "NM_CIDADE",Descricao = "Nome da cidade", Tamanho = 350},
                                    new SYS.FORMS.Coluna { Nome = "ID_UF",Descricao = "Identificador da U.F.", Tamanho = 100},
                                    new SYS.FORMS.Coluna { Nome = "NM_UF",Descricao = "Nome da U.F.", Tamanho = 350},
                                    new SYS.FORMS.Coluna { Nome = "ID_PAIS",Descricao = "Identificador do país", Tamanho = 100},
                                    new SYS.FORMS.Coluna { Nome = "NM_PAIS",Descricao = "Nome do país", Tamanho = 350},
                                }
                })
                {
                    if (filtro.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        beID_CIDADE.Text = (filtro.Selecionados.FirstOrDefault().ID_CIDADE as int?).Padrao().ToString();
                        teNM_CIDADE.Text = (filtro.Selecionados.FirstOrDefault().NM_CIDADE as string).Padrao().ToString();
                        beID_UF.Text = (filtro.Selecionados.FirstOrDefault().ID_UF as int?).Padrao().ToString();
                        teNM_UF.Text = (filtro.Selecionados.FirstOrDefault().NM_UF as string).Padrao().ToString();
                        beID_PAIS.Text = (filtro.Selecionados.FirstOrDefault().ID_PAIS as int?).Padrao().ToString();
                        teNM_PAIS.Text = (filtro.Selecionados.FirstOrDefault().NM_PAIS as string).Padrao().ToString();
                    }
                }
            }
            catch (Exception excessao)
            {
                excessao.Validar();
            }
        }

        private void beID_CIDADE_Leave(object sender, EventArgs e)
        {
            try
            {
                if (beID_CIDADE.Text.TemValor())
                {
                    var consulta = new QPaisUFCidade();

                    var cidade = (from a in consulta.Cidades
                                  where a.ID_CIDADE == beID_CIDADE.Text.ToInt32()
                                  select a).FirstOrDefault();

                    if (cidade != null)
                    {
                        beID_CIDADE.Text = cidade.ID_CIDADE.ToString();
                        teNM_CIDADE.Text = cidade.NM.Validar();

                        var uf = consulta.UFs.FirstOrDefault(a => a.ID_UF == cidade.ID_UF);

                        if (uf != null)
                        {
                            beID_UF.Text = uf.ID_UF.ToString();
                            teNM_UF.Text = uf.NM.Validar();
                            teSIGLA.Text = uf.SIGLA.Validar();

                            if (!uf.EXTERIOR)
                            {
                                beID_PAIS.Properties.Buttons[0].Visible = false;
                                beID_PAIS.ReadOnly = true;

                                var pais = consulta.Paises.FirstOrDefault(a => a.ID_PAIS == uf.ID_PAIS);

                                if (pais != null)
                                {
                                    beID_PAIS.Text = pais.ID_PAIS.ToString();
                                    teNM_PAIS.Text = pais.NM.Validar();
                                }
                            }
                            else
                            {
                                beID_PAIS.Properties.Buttons[0].Visible = true;
                                beID_PAIS.ReadOnly = false;
                            }
                        }
                    }
                }
                else
                {
                    beID_CIDADE.Text = "";
                    teNM_CIDADE.Text = "";
                    beID_UF.Text = "";
                    teNM_UF.Text = "";
                    teSIGLA.Text = "";
                    beID_PAIS.Text = "";
                    teNM_PAIS.Text = "";
                }
            }
            catch (Exception excessao)
            {
                excessao.Validar();
            }
        }

        private void beID_PAIS_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            try
            {
                var consulta = new QPaisUFCidade();

                using (var filtro = new FFiltro
                {
                    Consulta = (from a in consulta.Paises
                                select new
                                {
                                    ID_PAIS = a.ID_PAIS,
                                    NM_PAIS = a.NM
                                }).AsQueryable(),
                    Colunas = new List<Coluna>()
                                {
                                    new Coluna { Nome = "ID_PAIS",Descricao = "Identificador do país", Tamanho = 100},
                                    new Coluna { Nome = "NM_PAIS",Descricao = "Nome do país", Tamanho = 350},
                                }
                })
                {
                    if (filtro.ShowDialog() == DialogResult.OK)
                    {
                        beID_PAIS.Text = (filtro.Selecionados.FirstOrDefault().ID_PAIS as int?).Padrao().ToString();
                        teNM_PAIS.Text = (filtro.Selecionados.FirstOrDefault().NM_PAIS as string).Padrao().ToString();
                    }
                }
            }
            catch (Exception excessao)
            {
                excessao.Validar();
            }
        }

        private void beID_PAIS_Leave(object sender, EventArgs e)
        {
            try
            {
                if (beID_PAIS.Text.TemValor() && !beID_PAIS.ReadOnly)
                {
                    var pais = new QPaisUFCidade().Paises.FirstOrDefault(a => a.ID_PAIS == beID_PAIS.Text.ToInt32());

                    beID_PAIS.Text = pais != null ? pais.ID_PAIS.ToString() : "";
                    teNM_PAIS.Text = pais != null ? pais.NM.Validar() : "";
                }
            }
            catch (Exception excessao)
            {
                excessao.Validar();
            }
        }

        #endregion
    }
}