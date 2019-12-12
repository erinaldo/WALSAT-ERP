using SYS.QUERYS;
using System.Linq;
using SYS.QUERYS.Cadastros.Configuracao;
using SYS.UTILS;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SYS.FORMS.Cadastros.Configuracao
{
    public partial class FEmpresa_Busca : SYS.FORMS.FBase_CadastroBusca
    {
        public FEmpresa_Busca()
        {
            InitializeComponent();

            this.Padronizar();
        }

        public override void Adicionar()
        {
            base.Adicionar();

            using (var adicionar = new FEmpresa_Cadastro() { Modo = Modo.Cadastrar })
            {

                if (adicionar.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    beEmpresa.Text = adicionar.empresa.ID_EMPRESA.ToString();
                    Mensagens.Sucesso();
                    Buscar();
                }
            }
        }

        public override void Alterar()
        {
            base.Alterar();

            var selecionado = gvEmpresa.GetSelectedRow();

            if (selecionado == null)
                Mensagens.Selecionar();
            else
            {
                var empresa = new QEmpresa().Buscar((selecionado.ID as int?).Padrao()).FirstOrDefault();

                using (var alterar = new FEmpresa_Cadastro() { empresa = empresa, Modo = Modo.Alterar })
                {
                    if (alterar.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        beEmpresa.Text = alterar.empresa.ID_EMPRESA.ToString();
                        Mensagens.Sucesso();
                        Buscar();
                    }
                }
            }
        }

        public override void Deletar()
        {
            base.Deletar();

            var selecionado = gvEmpresa.GetSelectedRow();

            if (selecionado == null)
                Mensagens.Selecionar();
            else
            {
                int ID = selecionado.ID;

                var consulta = new QEmpresa();

                var empresa = consulta.Buscar(ID).FirstOrDefaultDynamic();

                if (Mensagens.Deletar() == System.Windows.Forms.DialogResult.Yes)
                {
                    var posicaoTransacao = 0;
                    consulta.Deletar(empresa, ref posicaoTransacao);
                    Mensagens.Deletado();
                    Buscar();
                }
            }
        }

        public override void Buscar()
        {
            base.Buscar();

            var consulta = (from a in new QEmpresa().Buscar(beEmpresa.Text.Trim().ToInt32() ?? 0)
                            join b in Conexao.BancoDados.TB_REL_CLIFORs on a.ID_CLIFOR equals b.ID_CLIFOR
                            select new
                            {
                                ID = a.ID_EMPRESA,
                                NM = b.NM,
                            });

            teNMEmpresa.Text.Validar(true);
            if (teNMEmpresa.Text.TemValor())
                consulta = consulta.Where(a => a.NM.Contains(teNMEmpresa.Text));

            gcEmpresa.DataSource = consulta;
            gvEmpresa.BestFitColumns(true);
        }

        public override void Detalhar()
        {
            base.Detalhar();
        }
    }

}
