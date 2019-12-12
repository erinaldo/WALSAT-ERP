using DevExpress.XtraGrid.Columns;
using SYS.UTILS;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace SYS.FORMS
{
    public partial class FFiltro : FBase
    {
        public List<Coluna> Colunas = new List<Coluna>();
        public DataTable SelecionadosDataTable;
        public List<dynamic> Selecionados = new List<dynamic>();
        public IQueryable Consulta = null;
        public Boolean Multiplos = false;

        public Action Cadastrar;
        public Action Alterar;
        private Action DuploClique;
        private Action<KeyEventArgs> TeclaPressionada;

        public FFiltro()
        {
            InitializeComponent();
            gcFiltro.Padronizar();
            
            sbCadastrar.Visible = Cadastrar == null;
            sbAlterar.Visible = Alterar == null;

            TeclaPressionada = e =>
            {
                if (e.KeyCode == Keys.Enter)
                    DuploClique();
                else if (e.KeyCode == Keys.Escape)
                {
                    this.DialogResult = DialogResult.Cancel;
                    this.Dispose();
                }
            };

            DuploClique = delegate
            {
                try
                {
                    var selecteds = gvFiltro.GetSelectedRows();

                    if (selecteds.Count() > 0)
                        foreach (var selected in selecteds)
                            Selecionados.Add(gvFiltro.GetRow(selected));

                    SelecionadosDataTable = Selecionados.AsQueryable().ToDataTable();

                    this.DialogResult = Selecionados.Count > 0 ? DialogResult.OK : DialogResult.Cancel;
                    this.Dispose();
                }
                catch (Exception excessao)
                {
                    excessao.Validar();
                }
            };

            this.Shown += delegate
            {
                try
                {
                    gvFiltro.ShowLoadingPanel();

                    var posicao = 0;
                    gvFiltro.OptionsSelection.MultiSelect = Multiplos;

                    Colunas.ForEach(a =>
                    {
                        var coluna = new GridColumn
                        {
                            Name = string.Format(@"col{0}_{1}", posicao, a.Nome),
                            FieldName = a.Nome,
                            Caption = a.Descricao,
                            Width = a.Tamanho,
                            Visible = true
                        };

                        coluna.OptionsColumn.AllowEdit = false;
                        coluna.OptionsColumn.AllowFocus = false;

                        gvFiltro.Columns.Add(coluna);

                        posicao++;
                    });

                    gcFiltro.DataSource = Consulta;
                    gvFiltro.BestFitColumns();

                    gvFiltro.HideLoadingPanel();
                }
                catch (Exception excessao)
                {
                    excessao.Validar();
                }
            };

            this.KeyDown += (s, e) => { TeclaPressionada(e); };

            gvFiltro.DoubleClick += delegate { DuploClique(); };
            gvFiltro.KeyDown += (s, e) => { TeclaPressionada(e); };

            sbCadastrar.Click += (s, e) =>
            {
                if (Cadastrar != null)
                    Cadastrar();
            };
            sbAlterar.Click += (s, e) =>
            {
                if (Alterar != null)
                    Alterar();
            };
            sbSelecionar.Click += (s, e) => DuploClique();
            sbCancelar.Click += (s, e) => this.Dispose();
        }
    }

    public class Coluna
    {
        public String Nome { get; set; }
        public String Descricao { get; set; }
        public Int32 Tamanho { get; set; }
    }
}