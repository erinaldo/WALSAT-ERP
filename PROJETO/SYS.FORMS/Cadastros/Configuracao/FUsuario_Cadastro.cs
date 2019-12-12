using SYS.UTILS;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using SYS.QUERYS;
using System.Text;
using System.Windows.Forms;
using SYS.QUERYS.Cadastros.Configuracao;
using System.Linq;
using SYS.QUERYS.Cadastros.Relacionamento;

namespace SYS.FORMS.Cadastros.Configuracao
{
    public partial class FUsuario_Cadastro : SYS.FORMS.FBase_Cadastro
    {
        public TB_CON_USUARIO usuario = null;           
        
        public FUsuario_Cadastro()
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
                            using (var filtro = new SYS.FORMS.FFiltro
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
                                    teNMClifor.Text = (filtro.Selecionados.FirstOrDefault().NM as string).Validar();
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
                            var unidade = Clifor(true).FirstOrDefaultDynamic();

                            beClifor.Text = unidade != null ? (unidade.ID as int?).Padrao().ToString() : "";
                            teNMClifor.Text = unidade != null ? (unidade.NM as string).Validar() : "";
                        }
                        catch (Exception excessao)
                        {
                            excessao.Validar();
                        }
                    };
                    beClifor.Leave += delegate { CliforLeave(); };


                    if (Modo == Modo.Cadastrar)
                        usuario = new TB_CON_USUARIO();
                    else if (Modo == Modo.Alterar)
                    {
                        if (usuario == null)
                            Excessoes.Alterar();

                        teIdentificador.Text = usuario.ID_USUARIO.ToString();
                        teSenha.Text = usuario.SENHA.Validar();
                        rgTP.SelectedIndex = usuario.TP == "N" ? 0 : 1;
                        ceAtivo.Checked = usuario.ST_ATIVO ?? false;
                        beClifor.Text = usuario.ID_CLIFOR.ToString();
                        CliforLeave();

                        teIdentificador.ReadOnly = true;


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
            var retorno = from a in consulta.Buscar(leave ? clifor : 0)
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

                usuario = new TB_CON_USUARIO();

                usuario.ID_USUARIO = teIdentificador.Text.Trim();
                usuario.SENHA = teSenha.Text.Trim();
                usuario.TP = rgTP.SelectedIndex == 0 ? "N" : "G";
                usuario.ID_CLIFOR = beClifor.Text.Trim().ToInt32();
                usuario.ST_ATIVO = ceAtivo.Checked;

                var posicaoTransacao = 0;
                new QUsuario().Gravar(usuario, ref posicaoTransacao);

                base.Gravar();
            }
            catch (Exception excessao)
            {
                excessao.Validar();
            }
        }
    }
}
