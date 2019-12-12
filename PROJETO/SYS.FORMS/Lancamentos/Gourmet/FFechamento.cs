using SYS.QUERYS.Cadastros.Relacionamento;
using SYS.UTILS;
using SYS.QUERYS.Lancamentos.Financeiro;
using SYS.QUERYS.Lancamentos.Gourmet;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SYS.QUERYS.Lancamentos.Comercial;
using DevExpress.XtraEditors;

namespace SYS.FORMS.Lancamentos.Gourmet
{
    public partial class FFechamento : SYS.FORMS.FBase_Cadastro
    {
        public FFechamento()
        {
            InitializeComponent();
        }

        public MPedido vPedido = new MPedido();
        public decimal vl_troco = 0m;
        public decimal ValorPago = 0m;
        public int ID_Pedido = 0;
        public int ID_Nota = 0;
        public bool GravaPedido = false;
        public bool ST_delivery = false;
        private bool editarExtrato = false;
        public decimal vlPrazo = 0;
        public bool ConfirmarOP = false;
        public bool VendaDireta = false;

        #region Metodos

        private void CalculaSubtotal()
        {
            try
            {
                decimal desconto = seDesconto.Value;
                decimal subtotal = 0m;
                decimal apagar = 0m;
                decimal entrega = seEntrega.Value;
                decimal liquidado = 0m;
                decimal descontado = 0m;

                if (ID_Nota != 0)
                {
                    liquidado = new QDuplicata().BuscaTotalLiquidado(ID_Nota);
                    descontado = new QDuplicata().BuscaTotalDesconto(ID_Nota);
                    vlPrazo = new QDuplicata().BuscaTotalAprazo(ID_Nota);
                }

                var PagamentoPedido = new SYS.QUERYS.Lancamentos.Comercial.QPedido().Buscar(ID_Pedido).ToList();

                for (int i = 0; i < bsItens.Count; i++)
                    subtotal += (bsItens[i] as MPedidoItem).VL_SUBTOTAL;

                foreach (var posicaoRow in gvItens.GetSelectedRows())
                    apagar += ((decimal?)gvItens.GetRowCellValue(posicaoRow, colVL_SUBTOTAL)).Padrao();

                seSubtotal.Value = subtotal;
                seApagar.Value = (apagar == 0 ? subtotal : apagar) - desconto - liquidado - descontado - vlPrazo + entrega;

                //if (ID_Pedido != 0 && PagamentoPedido[0].ID_FORMAPAGAMENTO != "")
                //    seApagar.Value = 0m;

                if (seApagar.Value <= 0) 
                {
                    seDesconto.Value = 0m;
                    seApagar.Value = ID_Nota > 0 ? seApagar.Value : subtotal;

                    //if (ID_Pedido != 0 && PagamentoPedido[0].ID_FORMAPAGAMENTO != "")
                    //    seApagar.Value = 0m;
                }                
            }
            catch (Exception)
            {
            }
        }

        private void afterGravar(string TpPagamento)
        {
            var teste = gvItens.SelectedRowsCount;
            int transacao = 0;
            decimal valorDoc = 0m;

            decimal liquidado = new QDuplicata().BuscaTotalLiquidado(ID_Nota);
            decimal descontado = new QDuplicata().BuscaTotalDesconto(ID_Nota);

            valorDoc = seApagar.Value > seSubtotal.Value - liquidado ? seSubtotal.Value - liquidado : seApagar.Value;
            vl_troco = TpPagamento == "01" && ValorPago > seApagar.Value - liquidado ? ValorPago - (seApagar.Value - liquidado) : 0;

            if (seSubtotal.Value - (valorDoc + liquidado + descontado + seDesconto.Value) == 0)
                vPedido.ST_PEDIDO = "F";
            else
                vPedido.ST_PEDIDO = "O";

            if (vPedido.ID_PEDIDO == 0 && ID_Pedido > 0)
                vPedido.ID_PEDIDO = ID_Pedido;

            vPedido.CONDICAO_PAGAMENTO = TpPagamento;            

            bool _parcial = ((seApagar.Value + seDesconto.Value - seEntrega.Value) != seSubtotal.Value);

            var _pedido = new MPedido();

            if (_parcial)
            {
                _pedido.ID_PEDIDO_ORIGEM = vPedido.ID_PEDIDO;
                _pedido.ID_EMPRESA = vPedido.ID_EMPRESA;
                _pedido.ID_CLIFOR = vPedido.ID_CLIFOR;
                _pedido.ID_CARTAO = vPedido.ID_CARTAO;
                _pedido.ID_MESA = vPedido.ID_MESA;
                _pedido.ID_USUARIO = vPedido.ID_USUARIO;
                _pedido.CONDICAO_PAGAMENTO = vPedido.CONDICAO_PAGAMENTO;
                _pedido.ST_ATIVO = vPedido.ST_ATIVO;
                _pedido.ST_PEDIDO = "F";
                _pedido.TP_MOVIMENTO = vPedido.TP_MOVIMENTO;
            }


            var pedido = new SYS.QUERYS.Lancamentos.Gourmet.QPedido();
            pedido.Gravar(_parcial ? _pedido : vPedido, ref transacao, null, false);
            ID_Pedido = pedido.id_pedido;
            ID_Nota = pedido.id_nota;

            if(_parcial)
            {
                    var _item = new MPedidoItem();

                    var selecionados = gvItens.SelectedRowsCount;

                    //for (int i = 0; i < selecionados; i++)
                    //{
                    //    _item.ID_ITEM = ((int?)gvItens.GetRowCellValue(i, colID_ITEM)).Padrao();
                    // _item.ID_PEDIDO = _pedido.ID_PEDIDO;
                    // new SYS.QUERYS.Lancamentos.Gourmet.QPedido().AtualizaPedidoItem(_pedido, _item, vPedido.ID_PEDIDO, ref transacao);                  
                    //}

                    foreach (var posicaoRow in gvItens.GetSelectedRows())
                    {
                        _item.ID_ITEM = ((int?)gvItens.GetRowCellValue(posicaoRow, colID_ITEM)).Padrao();
                        _item.ID_PEDIDO = _pedido.ID_PEDIDO;
                        new SYS.QUERYS.Lancamentos.Gourmet.QPedido().AtualizaPedidoItem(_pedido, _item, vPedido.ID_PEDIDO, ref transacao);  
                    }

                    if (_parcial)
                        BuscarItens();
            }

            GravaPedido = false;
            


            if (vPedido.ID_PEDIDO == 0 && ID_Pedido > 0)
                vPedido.ID_PEDIDO = ID_Pedido;

            GravaPedido = false;

            CalculaSubtotal();

            //if (MessageBox.Show("Deseja imprimir demonstrativo de consumo?", "Pergunta!", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            //    new SYS.QUERYS.Lancamentos.Comercial.QPedido().ImprimirConsumacao(ID_Pedido);

            ImprimirComanda();

            if (seApagar.Value <= 0)
            {
                Imprimir();
                this.ConfirmarOP = true;
                this.Close();
            }

            lb_troco.Text = "Troco: " + vl_troco.ToString("n2");
            lb_troco.Visible = false;
        }

        private void BuscarItens()
        {
            var itens = new SYS.QUERYS.Lancamentos.Gourmet.QPedido().BuscarItens(vPedido.ID_PEDIDO.ToString(), "", "").AsParallel().ToList();

            for (int i = 0; i < itens.Count; i++)
            {
                var complementos = new SYS.QUERYS.Lancamentos.Gourmet.QPedido().BuscarComplementos(itens[i].ID_PEDIDO, itens[i].ID_ITEM).AsParallel().ToList();
                for (int y = 0; y < complementos.Count; y++)
                    itens[i].COMPLEMENTOS.Add(complementos[y]);
            }

            for (int i = 0; i < itens.Count; i++)
            {
                var adicionais = new SYS.QUERYS.Lancamentos.Gourmet.QPedido().BuscarAdicionais(itens[i].ID_PEDIDO, itens[i].ID_ITEM).AsParallel().ToList();
                for (int y = 0; y < adicionais.Count; y++)
                    itens[i].Adicionais.Add(adicionais[y]);
            }

            if (itens.Count > 0)
                bsItens.DataSource = itens;
            else
                bsItens.DataSource = null;
        }

        private void ImprimirComanda()
        {
            //Report Relatorio = new Report();
            //var db = new DALDataContext();
            //Relatorio.Load(Utils.Arquivos.CaminhoPath() + "\\Report\\Comanda.frx");

            //var Dados = (from i in db.TB_GOU_ITEMPEDIDOs
            //             join y in db.TB_EST_PRODUTOs on i.CD_PRODUTO equals y.CD_PRODUTO
            //             where i.CD_PEDIDO == Nr_Pedido
            //             select y.CD_IMPRESSORA).Distinct().ToList();

            //for (int i = 0; i < Dados.Count; i++)
            //    if (Dados[i] == null)
            //        return;

            //for (int i = 0; i < Dados.Count; i++)
            //{
            //    var vItens = (from v in db.TB_GOU_PEDIDOs
            //                  join w in db.TB_GOU_ITEMPEDIDOs on v.CD_PEDIDO equals w.CD_PEDIDO
            //                  join y in db.TB_EST_PRODUTOs on w.CD_PRODUTO equals y.CD_PRODUTO
            //                  join x in db.TB_CFG_IMPRESSORAs on y.CD_IMPRESSORA equals x.CD_IMPRESSORA
            //                  where w.ST_IMPRESSO != "S"
            //                  && v.CD_PEDIDO == Nr_Pedido
            //                  && y.CD_IMPRESSORA == Dados[i].Value
            //                  select v).Distinct();

            //    if (vItens.Count() <= 0)
            //        continue;

            //    var caminhoImpressora = db.TB_CFG_IMPRESSORAs.FirstOrDefault(p => p.CD_IMPRESSORA.Equals(Dados[i].Value)).ENDERECO;

            //    Relatorio.RegisterData(vItens, "Pedido");
            //    PrintSettings config = new PrintSettings();
            //    config.Printer = @"" + caminhoImpressora;
            //    config.ShowDialog = false;
            //    config.PrintMode = PrintMode.Split;
            //    Relatorio.PrintSettings.Assign(config);
            //    Relatorio.SetParameterValue("Delivery", false);
            //    Relatorio.Prepare();
            //    if (editarExtrato)
            //        Relatorio.Design();
            //    else
            //        Relatorio.Print();
            //}

            //int transacao = 0;
            //var itens = (from i in db.TB_GOU_ITEMPEDIDOs where i.ST_IMPRESSO != "S" && i.CD_PEDIDO == Nr_Pedido select i).ToList();
            //for (int i = 0; i < itens.Count; i++)
                //LanPedido.AtualizaSTimpresso(itens[i].CD_ITEMPEDIDO, ref transacao);
        }

        private void Imprimir()
        {
        //    Report Relatorio = new Report();
        //    var db = new DALDataContext();

        //    Relatorio.Load(Utils.Arquivos.CaminhoPath() + "\\Report\\ExtratoFechamento.frx");

        //    var Dados = (from i in db.TB_GOU_PEDIDOs
        //                 where i.CD_PEDIDO == Nr_Pedido
        //                 select i).AsParallel();

        //    Relatorio.RegisterData(Dados, "Pedido");
        //    PrintSettings config = new PrintSettings();
        //    //config.Printer = @"\\casa\cozinha";
        //    config.ShowDialog = false;
        //    config.PrintMode = PrintMode.Split;
        //    Relatorio.PrintSettings.Assign(config);
        //    Relatorio.Prepare();
        //    if (editarExtrato)
        //        Relatorio.Design();
        //    else
        //        Relatorio.Print();
        }

        #endregion

        #region Eventos

        private void FFecharPedido_Load(object sender, EventArgs e)
        {
            bbiGravar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            BuscarItens();
            CalculaSubtotal();
            beCliente.Text = "1";
            beCliente_Leave(null, null);            
            seDesconto.Select();
        }

        private void vl_desconto_Leave(object sender, EventArgs e)
        {
            CalculaSubtotal();
        }

        private IQueryable Clifors(bool leave)
        {
            int? clifor = beCliente.Text.ToInt32(true);
            var consulta = new QClifor();
            var retorno = from a in consulta.Buscar(clifor ?? 0)
                          select new
                          {
                              ID = a.ID_CLIFOR,
                              NM = a.NM,
                          };

            if (leave)
                retorno = retorno.Take(1);
            else
                if (retorno.Count() > 0)
                {
                    beCliente.Text = retorno.ToList()[0].ID.ToString();
                    teNMCliente.Text = retorno.ToList()[0].NM.ToString();
                }
                else
                {
                    beCliente.Text = "";
                    teNMCliente.Text = "";
                }

            return retorno;
        }

        private void BuscaClifor()
        {
            try
            {
                using (var filtro = new SYS.FORMS.FFiltro()
                {
                    Consulta = Clifors(false),
                    Colunas = new List<SYS.FORMS.Coluna>()
                                {
                                    new SYS.FORMS.Coluna { Nome = "ID", Descricao = "Identificador do Produto", Tamanho = 100},
                                    new SYS.FORMS.Coluna { Nome = "NM",Descricao = "Nome do Produto", Tamanho = 350}
                                }
                })
                {
                    if (filtro.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        beCliente.Text = (filtro.Selecionados.FirstOrDefault().ID as int?).Padrao().ToString();
                        teNMCliente.Text = (filtro.Selecionados.FirstOrDefault().NM).Padrao().ToString();
                    }
                }
            }
            catch (Exception excessao)
            {
                excessao.Validar();
            }
        }

        private void bt_dinheiro_Click(object sender, EventArgs e)
        {
            try
            {
                afterGravar("01");
            }
            catch (Exception ex)
            {
                ex.Validar();
            }
        }      

        private void vl_pagar_Leave(object sender, EventArgs e)
        {

        }

        private void gc_itens_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.F2: sbPrazo_Click(null, null); return;
                case Keys.F3: sbCartão_Click(null, null); return;
                case Keys.F4: bt_dinheiro_Click(null, null); return;
                case Keys.P: editarExtrato = true; return;
            }
        }

        private void gcItens_Click(object sender, EventArgs e)
        {

        }

        private void sbPrazo_Click(object sender, EventArgs e)
        {
            try
            {
                //if (beCliente.Text.Trim() == "1")
                //    throw new Exception("Não é permitido venda a prazo para o clifor 1");

                afterGravar("05");
            }
            catch (Exception ex)
            {
                ex.Validar();
            }
        }

        private void sbCartão_Click(object sender, EventArgs e)
        {
            try
            {
                var frm = new FTp_Cartao();
                frm.ShowDialog();

                if(frm.DialogResult == System.Windows.Forms.DialogResult.OK)
                    afterGravar(frm.Tipo);
            }
            catch (Exception ex)
            {
                ex.Validar();
            }
        }

        private void sbDinheiro_Click(object sender, EventArgs e)
        {
            try
            {
                var frm = new FValor_Pago();
                frm.Valor = seApagar.Value;
                frm.ShowDialog();
                ValorPago = frm.Valor;

                if(frm.DialogResult == System.Windows.Forms.DialogResult.OK)
                    afterGravar("01");
            }
            catch (Exception ex)
            {
                ex.Validar();
            }
        }

        private void gvItens_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {

        }

        private void gvItens_SelectionChanged(object sender, DevExpress.Data.SelectionChangedEventArgs e)
        {
            CalculaSubtotal();
        }

        #endregion        

        private void beCliente_Leave(object sender, EventArgs e)
        {
            Clifors(false);            
        }

        private void sbCPF_Click(object sender, EventArgs e)
        {
            try
            {
                new FCPF().ShowDialog();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
            }
        }
    }
}
