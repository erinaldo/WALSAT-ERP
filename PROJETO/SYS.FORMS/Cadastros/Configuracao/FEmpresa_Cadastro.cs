using SYS.QUERYS;
using SYS.QUERYS.Cadastros.Configuracao;
using SYS.QUERYS.Cadastros.Relacionamento;
using SYS.UTILS;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SYS.FORMS.Cadastros.Configuracao
{
    public partial class FEmpresa_Cadastro : SYS.FORMS.FBase_Cadastro
    {
        public TB_CON_EMPRESA empresa = null;

        public FEmpresa_Cadastro()
        {
            InitializeComponent();

            this.Shown += delegate
            {
                try
                {
                    beClifor.ButtonClick += delegate
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
                                    teRazao.Text = (filtro.Selecionados.FirstOrDefault().NM as string).Validar();
                                }
                            }
                        }
                        catch (Exception excessao)
                        {
                            excessao.Validar();
                        }
                    };
                    Action CliforLeave = delegate
                    {
                        try
                        {
                            var grupo = Clifor(true).FirstOrDefaultDynamic();

                            beClifor.Text = grupo != null ? (grupo.ID as int?).Padrao().ToString() : "";
                            teRazao.Text = grupo != null ? (grupo.NM as string).Validar() : "";
                        }
                        catch (Exception excessao)
                        {
                            excessao.Validar();
                        }
                    };
                    beClifor.Leave += delegate { CliforLeave(); };

                    if (Modo == Modo.Cadastrar)
                        empresa = new TB_CON_EMPRESA();
                    else if (Modo == Modo.Alterar)
                    {
                        if (empresa == null)
                            Excessoes.Alterar();

                        teIdentificador.Text = empresa.ID_EMPRESA.ToString();
                        beClifor.Text = empresa.ID_CLIFOR.ToString().Validar();
                        CliforLeave();
                        ceGourmet.Checked = empresa.ST_GOURMET.Padrao();// ?? false;
                    }
                    

                }
                catch (Exception excessao)
                {
                    excessao.Validar();
                }
            };
        }

        private IQueryable Clifor(bool leave)
        {
            var clifor = beClifor.Text.ToInt32(true).Padrao();

            if (leave && clifor <= 0)
                return null;

            var consulta = new QClifor();
            var retorno = from a in consulta.Buscar((leave ? clifor : 0))
                          select new
                          {
                              ID = a.ID_CLIFOR,
                              NM = a.NM,
                          };

            if (leave)
                retorno = retorno.Take(1);

            return retorno;
        }

        public override void Gravar()
        {
            try
            {
                Validar();

                empresa = new TB_CON_EMPRESA();

                empresa.ID_EMPRESA = teIdentificador.Text.ToInt32().Padrao();
                empresa.ID_CLIFOR = beClifor.Text.Trim().ToInt32().Padrao();
                empresa.ST_GOURMET = ceGourmet.Checked;                

                var posicaoTransacao = 0;
                new QEmpresa().Gravar(empresa, ref posicaoTransacao);

                base.Gravar();
            }
            catch (Exception excessao)
            {
                excessao.Validar();
            }
        }
    }
}
