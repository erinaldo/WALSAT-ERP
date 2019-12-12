using SYS.QUERYS.Cadastros.Gourmet;
using SYS.UTILS;
using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SYS.FORMS.Cadastros.Gourmet
{
    public partial class FAmbiente_Busca : SYS.FORMS.FBase_CadastroBusca
    {
        public FAmbiente_Busca()
        {
            InitializeComponent();
        }

        public override void Adicionar()
        {
            base.Adicionar();

            using (var adicionar = new FAmbiente_Cadastro() { Modo = Modo.Cadastrar })
            {

                if (adicionar.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    beIdentificador.Text = adicionar.Ambiente.ID_AMBIENTE.ToString();
                    Mensagens.Sucesso();
                    Buscar();
                }
            }
        }

        public override void Alterar()
        {
            base.Alterar();

            var selecionado = gvAmbiente.GetSelectedRow();

            if (selecionado == null)
                Mensagens.Selecionar();
            else
            {
                var ambiente = new QAmbiente().Buscar((selecionado.ID as int?).Padrao()).FirstOrDefaultDynamic();

                using (var alterar = new FAmbiente_Cadastro() { Ambiente = ambiente, Modo = Modo.Alterar })
                {
                    if (alterar.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        beIdentificador.Text = alterar.Ambiente.ID_AMBIENTE.ToString();
                        Mensagens.Sucesso();
                        Buscar();
                    }
                }
            }
        }

        public override void Deletar()
        {
            base.Deletar();

            var selecionado = gvAmbiente.GetSelectedRow();

            if (selecionado == null)
                Mensagens.Selecionar();
            else
            {
                int ID = selecionado.ID;

                var consulta = new QAmbiente();

                var ambiente = consulta.Buscar(ID).FirstOrDefaultDynamic();

                if (Mensagens.Deletar() == System.Windows.Forms.DialogResult.Yes)
                {
                    var posicaoTransacao = 0;
                    consulta.Deletar(ambiente, ref posicaoTransacao);
                    Mensagens.Deletado();
                    Buscar();
                }
            }
        }

        public override void Buscar()
        {
            base.Buscar();

            var consulta = (from a in new QAmbiente().Buscar(beIdentificador.Text.ToInt32(true) ?? 0)
                            select new 
                            {
                                ID = a.ID_AMBIENTE,
                                NM = a.NM
                            });

            teNM.Text.Validar(true);
            if (teNM.Text.TemValor())
                consulta = consulta.Where(a => a.NM.Contains(teNM.Text));

            gcAmbiente.DataSource = consulta;
            gvAmbiente.BestFitColumns(true);
        }

        public override void Detalhar()
        {
            base.Detalhar();
        }
    }
}
