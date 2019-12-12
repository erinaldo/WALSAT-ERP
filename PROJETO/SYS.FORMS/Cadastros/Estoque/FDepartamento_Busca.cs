using SYS.QUERYS.Cadastros.Estoque;
using System.Linq;
using SYS.UTILS;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SYS.FORMS.Cadastros.Estoque
{
    public partial class FDepartamento_Busca : SYS.FORMS.FBase_CadastroBusca
    {
        public FDepartamento_Busca()
        {
            InitializeComponent();
        }

        public override void Adicionar()
        {
            base.Adicionar();

            using (var adicionar = new FDepartamento_Cadastro() { Modo = Modo.Cadastrar })
            {

                if (adicionar.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    beIdentificador.Text = adicionar.Departamento.ID_DEPARTAMENTO.ToString();
                    Mensagens.Sucesso();
                    Buscar();
                }
            }
        }

        public override void Alterar()
        {
            base.Alterar();

            var selecionado = gvDepartamento.GetSelectedRow();

            if (selecionado == null)
                Mensagens.Selecionar();
            else
            {
                var grupo = new QDepartamento().Buscar((selecionado.ID as int?).Padrao()).FirstOrDefaultDynamic();

                using (var alterar = new FDepartamento_Cadastro() { Departamento = grupo, Modo = Modo.Alterar })
                {
                    if (alterar.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        beIdentificador.Text = alterar.Departamento.ID_DEPARTAMENTO.ToString();
                        Mensagens.Sucesso();
                        Buscar();
                    }
                }
            }
        }

        public override void Deletar()
        {
            base.Deletar();

            var selecionado = gvDepartamento.GetSelectedRow();

            if (selecionado == null)
                Mensagens.Selecionar();
            else
            {
                int ID = selecionado.ID;

                var consulta = new QDepartamento();

                var departamento = consulta.Buscar(ID).FirstOrDefaultDynamic();

                if (Mensagens.Deletar() == System.Windows.Forms.DialogResult.Yes)
                {
                    var posicaoTransacao = 0;
                    consulta.Deletar(departamento, ref posicaoTransacao);
                    Mensagens.Deletado();
                    Buscar();
                }
            }
        }

        public override void Buscar()
        {
            base.Buscar();

            var consulta = (from a in new QDepartamento().Buscar(beIdentificador.Text.ToInt32(true) ?? 0)
                            select new
                            {
                                ID = a.ID_DEPARTAMENTO,
                                NM = a.NM,
                            });

            teNM.Text.Validar(true);
            if (teNM.Text.TemValor())
                consulta = consulta.Where(a => a.NM.Contains(teNM.Text));

            gcDepartamento.DataSource = consulta;
            gvDepartamento.BestFitColumns(true);
        }

        public override void Detalhar()
        {
            base.Detalhar();
        }
    }
}
