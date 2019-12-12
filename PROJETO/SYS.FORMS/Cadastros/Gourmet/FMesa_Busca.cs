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
using SYS.QUERYS;

namespace SYS.FORMS.Cadastros.Gourmet
{
    public partial class FMesa_Busca : SYS.FORMS.FBase_CadastroBusca
    {
        public FMesa_Busca()
        {
            InitializeComponent();
        }

        public override void Adicionar()
        {
            base.Adicionar();

            using (var adicionar = new FMesa_Cadastro() { Modo = Modo.Cadastrar })
            {

                if (adicionar.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    beMesa.Text = adicionar.Mesa.ID_MESA.ToString();
                    Mensagens.Sucesso();
                    Buscar();
                }
            }
        }

        public override void Alterar()
        {
            base.Alterar();

            var selecionado = gvMesa.GetSelectedRow();

            if (selecionado == null)
                Mensagens.Selecionar();
            else
            {
                var grupo = new QMesa().Buscar((selecionado.ID as int?).Padrao()).FirstOrDefaultDynamic();

                using (var alterar = new FMesa_Cadastro() { Mesa = grupo, Modo = Modo.Alterar })
                {
                    if (alterar.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        beMesa.Text = alterar.Mesa.ID_MESA.ToString();
                        Mensagens.Sucesso();
                        Buscar();
                    }
                }
            }
        }

        public override void Deletar()
        {
            base.Deletar();

            var selecionado = gvMesa.GetSelectedRow();

            if (selecionado == null)
                Mensagens.Selecionar();
            else
            {
                int ID = selecionado.ID;

                var consulta = new QMesa();

                var mesa = consulta.Buscar(ID).FirstOrDefaultDynamic();

                if (Mensagens.Deletar() == System.Windows.Forms.DialogResult.Yes)
                {
                    var posicaoTransacao = 0;
                    consulta.Deletar(mesa, ref posicaoTransacao);
                    Mensagens.Deletado();
                    Buscar();
                }
            }
        }

        public override void Buscar()
        {
            base.Buscar();

            var consulta = (from a in new QMesa().Buscar(beMesa.Text.ToInt32(true) ?? 0)
                            join b in Conexao.BancoDados.TB_GOU_AMBIENTEs on a.ID_AMBIENTE equals b.ID_AMBIENTE
                            select new
                            {
                                ID = a.ID_MESA,
                                NM_Ambiente = b.NM,
                                NM = a.NM
                            });

            beAmbiente.Text.Validar(true);
            if (beAmbiente.Text.TemValor())
                consulta = consulta.Where(a => a.NM_Ambiente.Contains(beAmbiente.Text.Trim()));

            gcMesa.DataSource = consulta;
            gvMesa.BestFitColumns(true);
        }

        public override void Detalhar()
        {
            base.Detalhar();
        }
    }
}
