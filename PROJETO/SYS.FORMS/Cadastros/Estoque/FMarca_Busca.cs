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
    public partial class FMarca_Busca : SYS.FORMS.FBase_CadastroBusca
    {
        #region Métodos

        public FMarca_Busca()
        {
            InitializeComponent();
        }

        public override void Adicionar()
        {
            using (var adicionar = new FMarca_Cadastro() { Modo = Modo.Cadastrar })
            {

                if (adicionar.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    beID_MARCA.Text = adicionar.marca.ID_MARCA.ToString();
                    Mensagens.Sucesso();
                    Buscar();
                }
            }
        }

        public override void Alterar()
        {
            base.Alterar();

            var selecionado = gvMarca.GetSelectedRow();

            if (selecionado == null)
                Mensagens.Selecionar();
            else
            {
                var marca = new QMarca().Buscar((selecionado.ID as int?).Padrao()).FirstOrDefaultDynamic();

                using (var alterar = new FMarca_Cadastro() { marca = marca, Modo = Modo.Alterar })
                {
                    if (alterar.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        beID_MARCA.Text = alterar.marca.ID_MARCA.ToString();
                        Mensagens.Sucesso();
                        Buscar();
                    }
                }
            }
        }

        public override void Deletar()
        {
            var selecionado = gvMarca.GetSelectedRow();

            if (selecionado == null)
                Mensagens.Selecionar();
            else
            {
                int ID = selecionado.ID;

                var consulta = new QMarca();

                var marca = consulta.Buscar(ID).FirstOrDefaultDynamic();

                if (Mensagens.Deletar() == System.Windows.Forms.DialogResult.Yes)
                {
                    var posicaoTransacao = 0;
                    consulta.Deletar(marca, ref posicaoTransacao);
                    Mensagens.Deletado();
                    Buscar();
                }
            }
        }

        public override void Buscar()
        {
            base.Buscar();

            var consulta = (from a in new QMarca().Buscar(beID_MARCA.Text.ToInt32(true) ?? 0)
                            select new
                            {
                                ID = a.ID_MARCA,
                                NM = a.NM,
                            });

            teNM_MARCA.Text.Validar(true);
            if (teNM_MARCA.Text.TemValor())
                consulta = consulta.Where(a => a.NM.Contains(teNM_MARCA.Text));

            gcMarca.DataSource = consulta;
            gvMarca.BestFitColumns(true);
        }

        public override void Detalhar()
        {
            base.Detalhar();
        }

        public override void Padroes()
        {
            beID_MARCA.Padronizar(true);
            teNM_MARCA.Padronizar(false);

            gcMarca.Padronizar();
        }

        #endregion
    }
}
