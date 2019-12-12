using SYS.QUERYS;
using SYS.QUERYS.Cadastros.Configuracao;
using SYS.QUERYS.Cadastros.Relacionamento;
using SYS.QUERYS.Lancamentos.Financeiro;
using SYS.QUERYS.Cadastros.Financeiro;
using SYS.UTILS;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SYS.FORMS.Lancamentos.Financeiro
{
    public partial class FContas_Cadastro : SYS.FORMS.FBase_Cadastro
    {
        public TB_FIN_DUPLICATA duplicata;

        public FContas_Cadastro()
        {
            InitializeComponent();
            this.Shown += delegate
            {
                try
                {

                    if (Modo == Modo.Cadastrar)
                        duplicata = new TB_FIN_DUPLICATA();
                    else if (Modo == Modo.Alterar)
                    {
                        if (duplicata == null)
                            Excessoes.Alterar();

                        if (new QLiquidacao().Buscar(0, duplicata.ID_DUPLICATA).Count() > 0)
                            lcGeral.Enabled = false;

                        teIdentificador.Text = duplicata.ID_DUPLICATA.ToString();
                        teNrDocumento.Text = duplicata.ID_DOCUMENTO;
                        deEmissao.Text = duplicata.DT_EMISSAO.ToString();
                        rbPagar.Checked = duplicata.TP == "P";
                        rbReceber.Checked = duplicata.TP == "R";
                        beEmpresa.Text = duplicata.ID_EMPRESA.ToString();
                        beEmpresa_Leave(null,null);
                        beClifor.Text = duplicata.ID_CLIFOR.ToString();
                        beClifor_Leave(null, null);
                        beCCusto.Text = duplicata.ID_CENTROCUSTO.ToString();
                        beCCusto_Leave(null, null);
                        beCondicao.Text = duplicata.ID_CONDICAOPAGAMENTO.ToString();
                        beCondicao_Leave(null, null);
                        seQtdParcelas.Value = duplicata.QT_PARCELAS ?? 0m;
                        seValor.Value = duplicata.VL ?? 0m;
                    }
                }
                catch (Exception excessao)
                {
                    excessao.Validar();
                }
            };
        }

        private IQueryable Empresa(bool leave)
        {
            var empresa = beEmpresa.Text.ToInt32(true).Padrao();

            if (leave && empresa <= 0)
                return null;

            var consulta = new QEmpresa();
            var retorno = (from a in consulta.Buscar(empresa)
                           select new
                           {
                               ID = a.ID_EMPRESA,
                               NM = a.TB_REL_CLIFOR.NM
                           }).AsQueryable();

            if (leave)
                retorno = retorno.Take(1);

            return retorno;
        }

        private IQueryable Clifor(bool leave)
        {
            var Clifor = beClifor.Text.ToInt32(true).Padrao();

            if (leave && Clifor <= 0)
                return null;

            var consulta = new QClifor();
            var retorno = (from a in consulta.Buscar(Clifor)
                           select new
                           {
                               ID = a.ID_CLIFOR,
                               NM = a.NM
                           }).AsQueryable();

            if (leave)
                retorno = retorno.Take(1);

            return retorno;
        }

        private IQueryable CentroCusto(bool leave)
        {
            var CCusto = beCCusto.Text.ToInt32(true).Padrao();

            if (leave && CCusto <= 0)
                return null;

            var consulta = new QCCusto();
            var retorno = (from a in consulta.Buscar(CCusto)
                           select new
                           {
                               ID = a.ID_CENTROCUSTO,
                               NM = a.NM
                           }).AsQueryable();

            if (leave)
                retorno = retorno.Take(1);

            return retorno;
        }

        private IQueryable CondicaoPagamento(bool leave)
        {
            var Condicao = beCondicao.Text.ToInt32(true).Padrao();

            if (leave && Condicao <= 0)
                return null;

            var consulta = new QCondicaoPagamento();
            var retorno = (from a in consulta.Buscar(Condicao)
                           select new
                           {
                               ID = a.ID_CONDICAOPAGAMENTO,
                               NM = a.DS
                           }).AsQueryable();

            if (leave)
                retorno = retorno.Take(1);

            return retorno;
        }

        public override void Gravar()
        {
            try
            {
                Validar();

                duplicata = new TB_FIN_DUPLICATA();

                duplicata.ID_DUPLICATA = teIdentificador.Text.ToInt32().Padrao();
                duplicata.ID_DOCUMENTO = teNrDocumento.Text.Padrao();
                duplicata.DT_EMISSAO = Convert.ToDateTime(deEmissao.Text);
                duplicata.TP = rbPagar.Checked ? "P" : "R";
                duplicata.ID_EMPRESA = beEmpresa.Text.ToInt32().Padrao();
                duplicata.ID_CLIFOR = beClifor.Text.ToInt32().Padrao();
                duplicata.ID_CENTROCUSTO = beCCusto.Text.ToInt32().Padrao();
                duplicata.ID_CONDICAOPAGAMENTO = beCondicao.Text.ToInt32().Padrao();
                duplicata.QT_PARCELAS = seQtdParcelas.Value;
                duplicata.VL = seValor.Value;

                var posicaoTransacao = 0;
                new QDuplicata().Gravar(duplicata,0m,0m,null, ref posicaoTransacao);

                base.Gravar();
            }
            catch (Exception excessao)
            {
                excessao.Validar();
            }
        }

        private void beEmpresa_Leave(object sender, EventArgs e)
        {
            try
            {
                var dados = Empresa(true).FirstOrDefaultDynamic();

                beEmpresa.Text = dados != null ? (dados.ID as int?).Padrao().ToString() : "";
                teEmpresa.Text = dados != null ? (dados.NM as string).Validar() : "";

            }
            catch (Exception ex)
            {
                ex.Validar();
                throw;
            }
        }

        private void beEmpresa_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            try
            {
                using (var filtro = new SYS.FORMS.FFiltro()
                {
                    Consulta = Empresa(false),
                    Colunas = new List<SYS.FORMS.Coluna>()
                                {
                                    new SYS.FORMS.Coluna { Nome = "ID", Descricao = "Identificador da empresa", Tamanho = 100},
                                    new SYS.FORMS.Coluna { Nome = "NM",Descricao = "Nome da empresa", Tamanho = 350}
                                }
                })
                {
                    if (filtro.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        beEmpresa.Text = (filtro.Selecionados.FirstOrDefault().ID as int?).Padrao().ToString();
                        teEmpresa.Text = (filtro.Selecionados.FirstOrDefault().NM as string).Validar();
                    }
                }
            }
            catch (Exception excessao)
            {
                excessao.Validar();
            }
        }

        private void beClifor_Leave(object sender, EventArgs e)
        {
            try
            {
                var dados = Clifor(true).FirstOrDefaultDynamic();

                beClifor.Text = dados != null ? (dados.ID as int?).Padrao().ToString() : "";
                teClifor.Text = dados != null ? (dados.NM as string).Validar() : "";

            }
            catch (Exception ex)
            {
                ex.Validar();
                throw;
            }
        }

        private void beClifor_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            try
            {
                using (var filtro = new SYS.FORMS.FFiltro()
                {
                    Consulta = Clifor(false),
                    Colunas = new List<SYS.FORMS.Coluna>()
                                {
                                    new SYS.FORMS.Coluna { Nome = "ID", Descricao = "Identificador do clifor", Tamanho = 100},
                                    new SYS.FORMS.Coluna { Nome = "NM",Descricao = "Nome do clifor", Tamanho = 350}
                                }
                })
                {
                    if (filtro.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        beClifor.Text = (filtro.Selecionados.FirstOrDefault().ID as int?).Padrao().ToString();
                        teClifor.Text = (filtro.Selecionados.FirstOrDefault().NM as string).Validar();
                    }
                }
            }
            catch (Exception excessao)
            {
                excessao.Validar();
            }
        }

        private void beCCusto_Leave(object sender, EventArgs e)
        {
            try
            {
                var dados = CentroCusto(true).FirstOrDefaultDynamic();

                beCCusto.Text = dados != null ? (dados.ID as int?).Padrao().ToString() : "";
                teCCusto.Text = dados != null ? (dados.NM as string).Validar() : "";

            }
            catch (Exception ex)
            {
                ex.Validar();
                throw;
            }
        }

        private void beCCusto_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            try
            {
                using (var filtro = new SYS.FORMS.FFiltro()
                {
                    Consulta = CentroCusto(false),
                    Colunas = new List<SYS.FORMS.Coluna>()
                                {
                                    new SYS.FORMS.Coluna { Nome = "ID", Descricao = "Identificador do centro de custo", Tamanho = 100},
                                    new SYS.FORMS.Coluna { Nome = "NM",Descricao = "Nome do centro de custo", Tamanho = 350}
                                }
                })
                {
                    if (filtro.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        beCCusto.Text = (filtro.Selecionados.FirstOrDefault().ID as int?).Padrao().ToString();
                        teCCusto.Text = (filtro.Selecionados.FirstOrDefault().NM as string).Validar();
                    }
                }
            }
            catch (Exception excessao)
            {
                excessao.Validar();
            }
        }

        private void beCondicao_Leave(object sender, EventArgs e)
        {
            try
            {
                var dados = CondicaoPagamento(true).FirstOrDefaultDynamic();

                beCondicao.Text = dados != null ? (dados.ID as int?).Padrao().ToString() : "";
                teCondicao.Text = dados != null ? (dados.NM as string).Validar() : "";

            }
            catch (Exception ex)
            {
                ex.Validar();
                throw;
            }
        }

        private void beCondicao_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            try
            {
                using (var filtro = new SYS.FORMS.FFiltro()
                {
                    Consulta = CondicaoPagamento(false),
                    Colunas = new List<SYS.FORMS.Coluna>()
                                {
                                    new SYS.FORMS.Coluna { Nome = "ID", Descricao = "Identificador da condição de pagamento", Tamanho = 100},
                                    new SYS.FORMS.Coluna { Nome = "NM",Descricao = "Nome da condição de pagamento", Tamanho = 350}
                                }
                })
                {
                    if (filtro.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        beCondicao.Text = (filtro.Selecionados.FirstOrDefault().ID as int?).Padrao().ToString();
                        teCondicao.Text = (filtro.Selecionados.FirstOrDefault().NM as string).Validar();
                    }
                }
            }
            catch (Exception excessao)
            {
                excessao.Validar();
            }
        }
    }
}
