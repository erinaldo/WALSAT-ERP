using SYS.QUERYS.Cadastros.Configuracao;
using SYS.UTILS;
using System.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SYS.QUERYS;

namespace SYS.FORMS.Cadastros.Configuracao
{
    public partial class FUsuario_Busca : SYS.FORMS.FBase_CadastroBusca
    {
        public FUsuario_Busca()
        {
            InitializeComponent();
        }

        public override void Adicionar()
        {
            base.Adicionar();

            using (var adicionar = new FUsuario_Cadastro() { Modo = Modo.Cadastrar })
            {

                if (adicionar.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    beIdentificador.Text = adicionar.usuario.ID_USUARIO.ToString();
                    Mensagens.Sucesso();
                    Buscar();
                }
            }
        }

        public override void Alterar()
        {
            base.Alterar();

            var selecionado = gvUsuario.GetSelectedRow();

            if (selecionado == null)
                Mensagens.Selecionar();
            else
            {
                string ID = selecionado.ID.ToString();

                var usuario = new QUsuario().Buscar(ID).FirstOrDefault();

                using (var alterar = new FUsuario_Cadastro() { usuario = usuario, Modo = Modo.Alterar })
                {
                    if (alterar.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        beIdentificador.Text = alterar.usuario.ID_USUARIO.ToString();
                        Mensagens.Sucesso();
                        Buscar();
                    }
                }
            }
        }

        public override void Deletar()
        {
            base.Deletar();

            var selecionado = gvUsuario.GetSelectedRow();

            if (selecionado == null)
                Mensagens.Selecionar();
            else
            {
                int ID = selecionado.ID;

                var consulta = new QUsuario();

                var usuario = consulta.Buscar(ID.ToString().Trim()).FirstOrDefault();

                if (Mensagens.Deletar() == System.Windows.Forms.DialogResult.Yes)
                {
                    var posicaoTransacao = 0;
                    consulta.Deletar(usuario, ref posicaoTransacao);
                    Mensagens.Deletado();
                    Buscar();
                }
            }
        }

        public override void Buscar()
        {
            base.Buscar();

            var consulta = (from a in new QUsuario().Buscar(beIdentificador.Text.Trim())
                            join b in Conexao.BancoDados.TB_REL_CLIFORs on a.ID_CLIFOR equals b.ID_CLIFOR
                            select new
                            {
                                ID = a.ID_USUARIO,
                                NM = b.NM,
                            });

            teNM.Text.Validar(true);
            if (teNM.Text.TemValor())
                consulta = consulta.Where(a => a.NM.Contains(teNM.Text));

            gcUsuario.DataSource = consulta;
            gvUsuario.BestFitColumns(true);
        }

        public override void Detalhar()
        {
            base.Detalhar();
        }
    }
}
