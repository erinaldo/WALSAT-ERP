using SYS.QUERYS.Cadastros.Gourmet;
using System.Linq;
using SYS.UTILS;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SYS.FORMS.Cadastros.Gourmet
{
    public partial class FImpressora_Busca : SYS.FORMS.FBase_CadastroBusca
    {
        public FImpressora_Busca()
        {
            InitializeComponent();
        }

        public override void Adicionar()
        {
            base.Adicionar();

            using (var adicionar = new FImpressora_Cadastro() { Modo = Modo.Cadastrar })
            {

                if (adicionar.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    teIdentificador.Text = adicionar.impressora.ID_IMPRESSORA.ToString();
                    Mensagens.Sucesso();
                    Buscar();
                }
            }
        }

        public override void Alterar()
        {
            base.Alterar();

            var selecionado = gvImpressora.GetSelectedRow();

            if (selecionado == null)
                Mensagens.Selecionar();
            else
            {
                var ambiente = new QImpressora().Buscar((selecionado.ID as int?).Padrao()).FirstOrDefaultDynamic();

                using (var alterar = new FImpressora_Cadastro() { impressora = ambiente, Modo = Modo.Alterar })
                {
                    if (alterar.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        teIdentificador.Text = alterar.impressora.ID_IMPRESSORA.ToString();
                        Mensagens.Sucesso();
                        Buscar();
                    }
                }
            }
        }

        public override void Deletar()
        {
            base.Deletar();

            var selecionado = gvImpressora.GetSelectedRow();

            if (selecionado == null)
                Mensagens.Selecionar();
            else
            {
                int ID = selecionado.ID;

                var consulta = new QImpressora();

                var impressora = consulta.Buscar(ID).FirstOrDefaultDynamic();

                if (Mensagens.Deletar() == System.Windows.Forms.DialogResult.Yes)
                {
                    var posicaoTransacao = 0;
                    consulta.Deletar(impressora, ref posicaoTransacao);
                    Mensagens.Deletado();
                    Buscar();
                }
            }
        }

        public override void Buscar()
        {
            base.Buscar();

            var consulta = (from a in new QImpressora().Buscar(teIdentificador.Text.ToInt32(true).Padrao())
                            select new
                            {
                                ID = a.ID_IMPRESSORA,
                                NM = a.NM
                            });

            teNM.Text.Validar(true);
            if (teNM.Text.TemValor())
                consulta = consulta.Where(a => a.NM.Contains(teNM.Text));

            gcImpressora.DataSource = consulta;
            gvImpressora.BestFitColumns(true);
        }

        public override void Detalhar()
        {
            base.Detalhar();
        }

    }
}
