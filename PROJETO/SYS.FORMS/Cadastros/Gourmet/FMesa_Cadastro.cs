using SYS.QUERYS;
using SYS.QUERYS.Cadastros.Gourmet;
using SYS.UTILS;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SYS.FORMS.Cadastros.Gourmet
{
    public partial class FMesa_Cadastro : SYS.FORMS.FBase_Cadastro
    {
        public TB_GOU_MESA Mesa = null;

        public FMesa_Cadastro()
        {
            InitializeComponent();

            this.Shown += delegate
            {
                try
                {

                    beAmbiente.ButtonClick += (s, e) =>
                    {
                        if (e.Button.Tag.ToString() == "adicionar")
                        {
                            try
                            {
                                using (var filtro = new SYS.FORMS.FFiltro()
                                {
                                    Consulta = Ambientes(false),
                                    Colunas = new List<SYS.FORMS.Coluna>()
                                {
                                    new SYS.FORMS.Coluna { Nome = "ID", Descricao = "Identificador do ambiente", Tamanho = 100},
                                    new SYS.FORMS.Coluna { Nome = "NM",Descricao = "Nome do ambiente", Tamanho = 350}
                                }
                                })
                                {
                                    if (filtro.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                                    {
                                        beAmbiente.Text = (filtro.Selecionados.FirstOrDefault().ID as int?).Padrao().ToString();
                                        teNMambiente.Text = (filtro.Selecionados.FirstOrDefault().NM as string).Validar();
                                    }
                                }
                            }
                            catch (Exception excessao)
                            {
                                excessao.Validar();
                            }
                        }
                    };

                    Action AmbienteLeave = delegate
                    {
                        try
                        {
                            var complemento = Ambientes(true).FirstOrDefaultDynamic();

                            beAmbiente.Text = complemento != null ? (complemento.ID as int?).Padrao().ToString() : "";
                            teNMambiente.Text = complemento != null ? (complemento.NM as string).Validar() : "";
                        }
                        catch (Exception excessao)
                        {
                            excessao.Validar();
                        }
                    };

                    beAmbiente.Leave += delegate { AmbienteLeave(); };

                    if (Modo == Modo.Cadastrar)
                        Mesa = new TB_GOU_MESA();
                    else if (Modo == Modo.Alterar)
                    {
                        if (Mesa == null)
                            Excessoes.Alterar();

                        teInicial.ReadOnly = true;
                        teFinal.ReadOnly = true;

                        teIdentificador.Text = Mesa.ID_MESA.ToString();
                        beAmbiente.Text = Mesa.ID_AMBIENTE.ToString();
                        AmbienteLeave();
                        ceAtivo.Checked = Mesa.ST_ATIVO ?? false;
                    }
                }
                catch (Exception excessao)
                {
                    excessao.Validar();
                }
            };
        }

        private IQueryable Ambientes(bool leave)
        {
            var ambiente = beAmbiente.Text.ToInt32(true).Padrao();

            if (leave && ambiente <= 0)
                return null;

            var consulta = new QAmbiente();
            var retorno = from a in consulta.Buscar((leave ? ambiente : 0))
                          select new
                          {
                              ID = a.ID_AMBIENTE,
                              NM = a.NM,
                          };

            if (leave)
                retorno = retorno.Take(1);

            return retorno;
        }

        private void buscaMesas()
        {

            if(teInicial.Text.Trim().ToInt32() > teFinal.Text.Trim().ToInt32())
                throw new Exception("Intervalo inicial deve ser menor do que intervalo final!");
            if (!teInicial.Text.Trim().TemValor())
                throw new Exception("Intervalo inicial obrigatório e deve ser maior que zero!");
            if (!teFinal.Text.Trim().TemValor())
                throw new Exception("Intervalo Final obrigatório e deve ser maior que zero!");

            int MesaInicio = Convert.ToInt32(teInicial.Text.Trim());
            int MesaFinal = Convert.ToInt32(teFinal.Text.Trim());

            var consulta = new QMesa();
            var lresult = (from i in consulta.Buscar()
                           select i).ToList();

            if (lresult.Count > 0)
                for (int i = 0; i < lresult.Count; i++)
                    if (lresult[i].ID_MESA >= MesaInicio && lresult[i].ID_MESA <= MesaFinal)
                        throw new Exception("Ja existe mesas no intervalo informado!");

        }

        public override void Validar()
        {
            if (Modo == Modo.Cadastrar)
                buscaMesas();

            if (!beAmbiente.Text.Trim().TemValor())
                throw new Exception("Ambiente obrigatório!");
        }

        public override void Gravar()
        {
            try
            {
                Validar();

                int MesaInicio = teInicial.Text.Trim().ToInt32() ?? 0;
                int MesaFinal = teFinal.Text.Trim().ToInt32() ?? 0;

                for (int i = MesaInicio; i <= MesaFinal; i++)
                {                    
                    Mesa = new TB_GOU_MESA();
                    if (Modo == UTILS.Modo.Alterar)
                        Mesa.ID_MESA = teIdentificador.Text.Trim().ToInt32() ?? 0;

                    Mesa.ID_AMBIENTE = beAmbiente.Text.ToInt32().Padrao();
                    Mesa.ST_ATIVO = ceAtivo.Checked;
                    Mesa.ID_MESA = i;
                    var posicaoTransacao = 0;
                    new QMesa().Gravar(Mesa, ref posicaoTransacao);
                }

                

                base.Gravar();
            }
            catch (Exception excessao)
            {
                excessao.Validar();
            }
        }
    }
}
