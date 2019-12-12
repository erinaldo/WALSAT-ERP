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
    public partial class FGrupo_Busca : SYS.FORMS.FBase_CadastroBusca
    {
        public FGrupo_Busca()
        {
            InitializeComponent();
        }

        public override void Adicionar()
        {
            base.Adicionar();

            using (var adicionar = new FGrupo_Cadastro() { Modo = Modo.Cadastrar })
            {

                if (adicionar.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    beIdentificador.Text = adicionar.Grupo.ID_GRUPO.ToString();
                    Mensagens.Sucesso();
                    Buscar();
                }
            }
        }

        public override void Alterar()
        {
            base.Alterar();

            var selecionado = gvGrupo.GetSelectedRow();

            if (selecionado == null)
                Mensagens.Selecionar();
            else
            {
                var grupo = new QGrupo().Buscar((selecionado.ID as int?).Padrao()).FirstOrDefaultDynamic();

                using (var alterar = new FGrupo_Cadastro() { Grupo = grupo, Modo = Modo.Alterar })
                {
                    if (alterar.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        beIdentificador.Text = alterar.Grupo.ID_GRUPO.ToString();
                        Mensagens.Sucesso();
                        Buscar();
                    }
                }
            }
        }

        public override void Deletar()
        {
            base.Deletar();

            var selecionado = gvGrupo.GetSelectedRow();

            if (selecionado == null)
                Mensagens.Selecionar();
            else
            {
                int ID = selecionado.ID;

                var consulta = new QGrupo();

                var grupo = consulta.Buscar(ID).FirstOrDefault();

                if (Mensagens.Deletar() == System.Windows.Forms.DialogResult.Yes)
                {
                    var posicaoTransacao = 0;
                    consulta.Deletar(grupo, ref posicaoTransacao);
                    Mensagens.Deletado();
                    Buscar();
                }
            }
        }

        public override void Buscar()
        {
            base.Buscar();

            var consulta = (from a in new QGrupo().Buscar(beIdentificador.Text.ToInt32(true) ?? 0)
                            select new
                            {
                                ID = a.ID_GRUPO,
                                NM = a.NM,
                            });

            beNome.Text.Validar(true);
            if (beNome.Text.TemValor())
                consulta = consulta.Where(a => a.NM.Contains(beNome.Text));

            gcGrupo.DataSource = consulta;
            gvGrupo.BestFitColumns(true);
        }

        public override void Detalhar()
        {
            base.Detalhar();
        }
    }
}
