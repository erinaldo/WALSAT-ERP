using DevExpress.XtraEditors;
using SYS.QUERYS;
using SYS.QUERYS.Cadastros.Estoque;
using SYS.QUERYS.Lancamentos.Estoque;
using SYS.QUERYS.Lancamentos.Financeiro;
using SYS.QUERYS.Lancamentos.Gourmet;
using SYS.UTILS;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using ACBrFramework.BAL;


namespace SYS.FORMS.Lancamentos.Gourmet
{
    public partial class FLancamento_Balanca : SYS.FORMS.FBase_Cadastro
    {
        public FLancamento_Balanca()
        {
            InitializeComponent();

            PreencheTela();

            beIDProduto.ButtonClick += delegate
            {
                try
                {
                    using (var filtro = new SYS.FORMS.FFiltro()
                    {
                        Consulta = Produtos(false),
                        Colunas = new List<SYS.FORMS.Coluna>()
                                {
                                    new SYS.FORMS.Coluna { Nome = "ID", Descricao = "Identificador do Produto", Tamanho = 100},
                                    new SYS.FORMS.Coluna { Nome = "NM",Descricao = "Nome do Produto", Tamanho = 350}
                                }
                    })
                    {
                        if (filtro.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                        {
                            beIDProduto.Text = (filtro.Selecionados.FirstOrDefault().ID as int?).Padrao().ToString();
                            teNMProduto.Text = (filtro.Selecionados.FirstOrDefault().NM as string).Validar();
                        }
                        if (filtro.Selecionados.FirstOrDefault().ID as int? >= 0)
                            BuscarDadosProduto(Convert.ToInt16(filtro.Selecionados.FirstOrDefault().ID));
                    }
                }
                catch (Exception excessao)
                {
                    excessao.Validar();
                }
            };
            Action ProdutoLeave = delegate
            {
                try
                {
                    var produto = Produtos(true).FirstOrDefaultDynamic();

                    if (produto != null)
                    {
                        beIDProduto.Text = produto != null ? (produto.ID as int?).Padrao().ToString() : "";
                        teNMProduto.Text = produto != null ? (produto.NM as string).Validar() : "";

                        if (produto.ID as int? >= 0)
                            BuscarDadosProduto(Convert.ToInt32(produto.ID));
                    }
                    else
                        limparCampos();                        
                }
                catch (Exception excessao)
                {
                    excessao.Validar();
                }
            };
            beIDProduto.Leave += delegate { ProdutoLeave(); };
        }

        public string NrMesa = "0";
        public string NrCartao = "0";
        public string NrAmbiente = "0";
        private bool editarExtrato = false;
        public int NR_pedido = 0;
        public int ID_NOTA = 0;
        private int Cd_Clifor = 0;
        public decimal Vl_Entrega = 0m;
        public decimal Vl_Troco = 0m;
        private bool St_Delivery = false;
        private int GrupoAtual = 0;
        public decimal vSubTotal = 0;
        public decimal vlPrazo = 0;
        public bool VendaDireta = false;
        private bool vPediuExtrato = false;
        private bool vComandaCancelada = false;
        private string OBS_ITEM = "";

        MPedido vpedido = new MPedido();
        public List<MComplemento> listaComplemento = new List<MComplemento>();
        public List<TB_EST_GRUPO_ADICIONAI> listaAdicional = new List<TB_EST_GRUPO_ADICIONAI>();

        #region Metodos

        private void PreencheTela(bool vSoItens = false, int vCd_Grupo = 0)
        {
            try
            {
                var Grupos = BuscaGrupos();

                if (!vSoItens)
                {

                    for (int i = 0; i < Grupos.Count; i++)
                    {
                        GrupoAtual = Grupos[0].ID_GRUPO;
                        DevExpress.XtraEditors.TileItem vGrupos = new TileItem();
                        vGrupos.Text = Grupos[i].NM;
                        vGrupos.Name = Grupos[i].ID_GRUPO.ToString();
                        vGrupos.TextAlignment = TileItemContentAlignment.MiddleCenter;
                        var vGruposElement = new TileItemElement();
                        vGruposElement.TextAlignment = DevExpress.XtraEditors.TileItemContentAlignment.MiddleCenter;
                        vGrupos.BorderVisibility = DevExpress.XtraEditors.TileItemBorderVisibility.Never;
                        vGrupos.Elements.Add(vGruposElement);
                        vGrupos.Id = 0;
                        vGrupos.ItemSize = DevExpress.XtraEditors.TileItemSize.Wide;
                        vGrupos.Padding = new System.Windows.Forms.Padding(-5);
                        //vGrupos.TextAlignment = TileItemContentAlignment.MiddleLeft;
                        //vGrupos.AppearanceItem.Normal.FontSizeDelta = 0;
                        //vGrupos.ItemSize = TileItemSize.Medium;
                        //vGrupos.AppearanceItem.Normal.ForeColor = Color.Black;
                        vGrupos.AppearanceItem.Normal.BackColor = Color.LightGreen;
                        vGrupos.AppearanceItem.Normal.BorderColor = Color.LightGreen;
                        vGrupos.AppearanceItem.Selected.BorderColor = Color.Green;
                        vGrupos.AppearanceItem.Selected.BackColor = Color.GreenYellow;
                        vGrupos.ItemClick += (s, e) => { tileGrupo_Click(vGrupos.Name, e); };
                        Grupo_Grupos.Items.Add(vGrupos);
                    }
                }

                if (Grupos.Count > 0)
                {
                    var itens = BuscaProdutos(vCd_Grupo > 0 ? vCd_Grupo : Grupos[0].ID_GRUPO);
                    if (Grupo_Itens.Items.Count > 0)
                        Grupo_Itens.Items.Clear();

                    for (int i = 0; i < itens.Count; i++)
                    {
                        DevExpress.XtraEditors.TileItem vItens = new TileItem();
                        vItens.Text = itens[i].NM;
                        vItens.Name = itens[i].ID_PRODUTO.ToString();
                        vItens.TextAlignment = TileItemContentAlignment.MiddleCenter;
                        vItens.BorderVisibility = TileItemBorderVisibility.Never;

                        vItens.Id = itens[i].ID_PRODUTO;
                        vItens.ItemSize = DevExpress.XtraEditors.TileItemSize.Wide;
                        vItens.Padding = new System.Windows.Forms.Padding(-5);

                        vItens.Elements.Add(new TileItemElement
                        {
                            TextAlignment = DevExpress.XtraEditors.TileItemContentAlignment.MiddleCenter
                        });
                        //vItens.TextAlignment = TileItemContentAlignment.MiddleCenter;
                        //vItens.AppearanceItem.Normal.FontSizeDelta = 0;
                        //vItens.ItemSize = TileItemSize.Default;
                        //vItens.AppearanceItem.Normal.ForeColor = Color.Black;
                        vItens.AppearanceItem.Normal.BackColor = Color.LightSalmon;
                        vItens.AppearanceItem.Normal.BorderColor = Color.LightSalmon;
                        vItens.AppearanceItem.Selected.BackColor = Color.Salmon;
                        vItens.AppearanceItem.Selected.BorderColor = Color.Salmon;
                        
                        vItens.ItemClick += (s, e) => { tileItens_Click(vItens.Name, e); };
                        
                        Grupo_Itens.Items.Add(vItens);
                    }
                }
                
            }
            catch (Exception ex)
            {
                ex.Validar();
            }
        }

        private List<TB_EST_GRUPO> BuscaGrupos()
        {
            var db = new ModelDataContext(Parametros.StringConexao);

            return (from i in db.TB_EST_GRUPOs
                    where i.ST_COMPLEMENTO != true
                    select i).AsParallel().ToList();
        }

        private List<TB_EST_PRODUTO> BuscaProdutos(int vCD_Grupo = 0)
        {
            var db = new ModelDataContext(Parametros.StringConexao);

            return (from i in db.TB_EST_PRODUTOs
                   where i.ID_GRUPO == vCD_Grupo
                   && i.ST_ATIVO != false
                   select i).AsParallel().ToList();
        }

        private void BuscarDadosProduto(int id_produto)
        {
            seQtd.BackColor = Color.White;

            var db = new ModelDataContext(Parametros.StringConexao);

            if (!new QPreco().Buscar(id_produto).Take(1).Any() && id_produto.TemValor())
            {
                limparCampos();
                throw new Exception(Mensagens.Necessario("o preço do produto", "lançar"));
            }

            var lresult = (from i in db.TB_EST_PRODUTOs
                           join y in db.TB_EST_PRECOs on i.ID_PRODUTO equals y.ID_PRODUTO
                           where i.ID_PRODUTO == id_produto
                           && (i.ST_ATIVO ?? false)
                           && (y.ST_ATIVO ?? false)
                           && y.TP_PRECO == "V"
                           select new
                           {
                               i.ID_PRODUTO,
                               NM_PRODUTO = i.NM,
                               VL_UNITARIO = y.VL_PRECO,
                               ST_BALANCA = i.TB_GOU_PRODUTO.ST_BALANCA ?? false
                           }).ToList();

            if (lresult.Any())
            {
                beIDProduto.Text = lresult[0].ID_PRODUTO.ToString();
                teNMProduto.Text = lresult[0].NM_PRODUTO;
                seVl_unitario.Value = lresult[0].VL_UNITARIO ?? 0m;
                seQtd.Value = 1;
                seVl_subtotal.Value = lresult[0].VL_UNITARIO ?? 0m;
                
                if(lresult[0].ST_BALANCA)
                {
                    decimal peso = 0m;

                    for (int i = 0; i < 7; i++)
                    {
                        peso = BuscaValorBalanca();
                        if (peso > 0)
                            i = 7;
                    }
                    if (peso < 0)
                    {
                        peso = 0m;
                        seQtd.BackColor = Color.LightSalmon;
                        beIDProduto.Focus();
                    }

                    seQtd.Value = peso;
                    

                }

                seQtd.Focus();
            }
            else
                limparCampos();
        }

        private decimal BuscaValorBalanca()
        {
            ACBrBAL bal = new ACBrBAL();
            bal.Modelo = ModeloBal.Toledo;
            bal.Porta = "COM3";
            bal.Ativar();
            bal.LePeso();
            var peso = bal.UltimoPesoLido;
            bal.Desativar();
            return peso;
        }

        private void afterGravar(string vStatus)
        {
            if (gvItens.DataSource == null)
                return;

            if (NR_pedido > 0)
                vpedido.ID_PEDIDO = NR_pedido;

            vpedido.ID_EMPRESA = 1;
            vpedido.ID_CLIFOR = St_Delivery ? Cd_Clifor : 1;
            vpedido.ID_MESA = NrCartao != "0" && NrCartao != "000" ? "" : NrMesa;
            vpedido.ID_CARTAO = NrCartao;
            vpedido.ST_ATIVO = gvItens.DataRowCount > 0;
            vpedido.ST_PEDIDO = vPediuExtrato ? "E" : vStatus;
            vpedido.TP_MOVIMENTO = "S";

            for (int i = 0; i < bsMPedidoItem.Count; i++)
                vpedido.Itens.Add((bsMPedidoItem[i] as MPedidoItem));

            var qPedido = new QPedido();
            var posicaoTransacao = 0;
            qPedido.Gravar(vpedido, ref posicaoTransacao);

            NR_pedido = qPedido.id_pedido;

            if (St_Delivery)
            {
                var vRegistro = new TB_GOU_DELIVERY();

                vRegistro.ID_PEDIDO = NR_pedido;
                vRegistro.ST = "A";
                vRegistro.VL = Vl_Entrega;

               
                new QDelivery().Gravar(vRegistro, ref posicaoTransacao);
            }
            vComandaCancelada = false;
            if ((NrMesa.Length > 0 && gvItens.DataRowCount == 0) || St_Delivery)
                this.Close();


        }

        private void CalculaSubtotal()
        {
            try
            {
                decimal subtotal = 0;
                for (int i = 0; i < bsMPedidoItem.Count; i++)
                    subtotal += (bsMPedidoItem[i] as MPedidoItem).VL_SUBTOTAL;

                decimal liquidado = 0m;
                decimal desconto = 0m;

                if (ID_NOTA != 0)
                {
                    liquidado = Math.Round(new QDuplicata().BuscaTotalLiquidado(ID_NOTA), 2);
                    desconto = Math.Round(new QDuplicata().BuscaTotalDesconto(ID_NOTA), 2);
                    vlPrazo = new QDuplicata().BuscaTotalAprazo(ID_NOTA);
                }

                vSubTotal = subtotal - liquidado - vlPrazo - desconto;
                seSubTotal.Value = vSubTotal;
            }
            catch (Exception)
            {
            }
        }

        private void limparCampos()
        {
            beIDProduto.Text = "";
            teNMProduto.Text = "";
            seVl_unitario.Value = 0m;
            seQtd.Value = 1m;
            seVl_subtotal.Value = 0m;
            beIDProduto.Focus();
        }

        private void buscaItens()
        {
            var itens = new QPedido().BuscarItens(NR_pedido.ToString(), "", "").AsParallel().ToList();

            for (int i = 0; i < itens.Count; i++)
            {
                var complementos = new QPedido().BuscarComplementos(itens[i].ID_PEDIDO, itens[i].ID_ITEM).AsParallel().ToList();
                for (int y = 0; y < complementos.Count; y++)
                    itens[i].COMPLEMENTOS.Add(complementos[y]);
            }

            for (int i = 0; i < itens.Count; i++)
            {
                var adicionais = new QPedido().BuscarAdicionais(itens[i].ID_PEDIDO, itens[i].ID_ITEM).AsParallel().ToList();
                for (int y = 0; y < adicionais.Count; y++)
                    itens[i].Adicionais.Add(adicionais[y]);
            }

            if (itens.Count > 0)
                bsMPedidoItem.DataSource = itens;
            else
                bsMPedidoItem.DataSource = null;

            CalculaSubtotal();
        }

        #endregion

        #region Eventos

        private void PedidoGourmet_Load(object sender, EventArgs e)
        {
            try
            {
                if (NrMesa != "0")
                {
                    this.Text += "Mesa " + NrMesa.ToString();

                    if (NrMesa != "0")
                        teConta.Text = "Mesa " + NrMesa.ToString();
                    else
                        teConta.Text = "Pedido " + NR_pedido.ToString();
                }
                else
                    if (NrCartao != "0")
                    {
                        this.Text += "Cartão " + NrCartao.ToString();

                        if (NrCartao != "0")
                            teConta.Text = "Cartão " + NrCartao.ToString();
                        else
                            teConta.Text = "Pedido " + NR_pedido.ToString();
                    }

                if (NR_pedido > 0)
                {
                    buscaItens();
                }
                beIDProduto.Select();
            }
            catch (Exception ex)
            {
                ex.Validar();
            }
        }

        private void tileGrupo_Click(object sender, EventArgs e)
        {
            try
            {
                PreencheTela(true, Convert.ToInt32(sender));
                GrupoAtual = Convert.ToInt32(sender);
            }
            catch (Exception ex)
            {
                ex.Validar();
            }
        }

        private void tileItens_Click(object sender, EventArgs e)
        {
            try
            {
                BuscarDadosProduto(Convert.ToInt32(sender));
            }
            catch (Exception ex)
            {
                ex.Validar();
            }
        }

        private void txt_qtd_Leave(object sender, EventArgs e)
        {
            try
            {
                seVl_subtotal.Value = Math.Round((Convert.ToDecimal(seQtd.Value) * Convert.ToDecimal(seVl_unitario.Value)),2);
            }
            catch (Exception) { }
        }

        private void txt_CD_Produto_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && e.KeyChar != (char)8)
            {
                e.Handled = true;
            }
        }

        private void bt_adicionar_Click(object sender, EventArgs e)
        {
            try
            {
                if (teNMProduto.Text.Trim() != "")
                {
                    if (seQtd.Value <= 0)
                    {
                        XtraMessageBox.Show("Quantidade dever ser maior que zero!","Atenção");
                        seQtd.Value = 0m;
                        return;
                    }

                    MPedidoItem item = new MPedidoItem();
                    item.ID_PRODUTO = Convert.ToInt32(beIDProduto.Text.Trim());
                    item.NM_PRODUTO = teNMProduto.Text.Trim();
                    item.QUANTIDADE = Convert.ToDecimal(seQtd.Value);
                    item.VL_UNITARIO = Convert.ToDecimal(seVl_unitario.Value);
                    item.TOTALCOMPLEMENTOS = listaComplemento.Sum(p => p.VALOR);
                    item.OBS = OBS_ITEM.Trim();
                    item.ST_ATIVO = true;

                    for (int i = 0; i < listaComplemento.Count; i++)
                        item.COMPLEMENTOS.Add(new MComplemento()
                        {
                            ID_PRODUTO_PEDIDO = listaComplemento[i].ID_PRODUTO_PEDIDO,
                            ID_PRODUTO = listaComplemento[i].ID_PRODUTO,
                            NM_PRODUTO = listaComplemento[i].NM_PRODUTO,
                            QUANTIDADE = listaComplemento[i].QUANTIDADE,
                            VALOR = listaComplemento[i].VALOR,
                        });

                    for (int i = 0; i < listaAdicional.Count; i++)
                        item.Adicionais.Add(new MAdicionais()
                        {
                            ID_ADICIONAL = listaAdicional[i].ID_ADICIONAL,
                            NM_ADICIONAL = listaAdicional[i].DS,
                            ID_GRUPO = listaAdicional[i].ID_GRUPO
                        });

                    bsMPedidoItem.Add(item);

                    //ajustes
                    vPediuExtrato = false;
                    BuscarDadosProduto(0);
                    listaComplemento.Clear();
                    CalculaSubtotal();
                    OBS_ITEM = "";
                }
                beIDProduto.Select();
            }
            catch (Exception ex)
            {
                ex.Validar();
            }
        }

        private void bt_remover_Click(object sender, EventArgs e)
        {
            try
            {
                if (bsMPedidoItem.Current == null)
                    return;

                if ((bsMPedidoItem.Current as MPedidoItem).ID_ITEM == 0)
                    bsMPedidoItem.Remove(bsMPedidoItem.Current);
                else
                {
                    var mtvo = new FMotivoCancelamento();

                    if (Parametros.ID_Usuario == Parametros.BackdoorUsuario)
                        if (mtvo.ShowDialog() == DialogResult.OK)
                            CancelarItem(mtvo.DS_Motivo);
                        else
                        {
                            var fAutenticacao = new FAutenticacao();
                            fAutenticacao.ShowDialog();

                            if (fAutenticacao.DialogResult == System.Windows.Forms.DialogResult.OK)
                            {
                                mtvo.ShowDialog();
                                if (mtvo.DialogResult == DialogResult.OK)
                                {
                                    CancelarItem(mtvo.DS_Motivo);
                                }
                            }
                        }
                }
                CalculaSubtotal();
            }
            catch (Exception ex)
            {
                ex.Validar();
            }
        }

        private void CancelarItem(string Motivo)
        {
            (bsMPedidoItem.Current as MPedidoItem).ST_ATIVO = false;
            (bsMPedidoItem.Current as MPedidoItem).DS_CANCELAMENTO = Motivo;
            (bsMPedidoItem.Current as MPedidoItem).ST_IMPRESSO = false;

            vComandaCancelada = true;
            afterGravar("O");

            bsMPedidoItem.Remove(bsMPedidoItem.Current);

            buscaItens();

            if (bsMPedidoItem.Count == 0)
                afterGravar("O");
        }

        private void txt_CD_Produto_Leave(object sender, EventArgs e)
        {
            try
            {
                if (beIDProduto.Text.Trim() != "")
                    BuscarDadosProduto(Convert.ToInt32(beIDProduto.Text.Trim()));
                
            }
            catch (Exception ex)
            {
                ex.Validar();
                limparCampos();
            }
        }

        private void gc_itens_Click(object sender, EventArgs e)
        {
            if (gvItens.DataRowCount == 0)
                return;
        }        

        private void gc_itens_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.F2: afterGravar("O"); this.Close(); return;
                case Keys.Escape: this.Close(); return;
                case Keys.F1: bt_adicionar_Click(null, null); return;
                case Keys.Enter: 
                    if (sender.GetType().ToString() == "DevExpress.XtraEditors.ButtonEdit")
                        seQtd.Focus();
                    if (sender.GetType().ToString() == "DevExpress.XtraEditors.SpinEdit")
                        sbAdicionar.Focus();                  
                    ; return;
            }
        }

        private void bt_limpar_Click(object sender, EventArgs e)
        {
            limparCampos();
        }

        public override void Gravar()
        {
            try
            {
                afterGravar("O");
                this.Close();
            }
            catch (Exception ex)
            {
                ex.Validar();
            }
        }

        public override void Cancelar()
        {
            this.Close();
        }

        private IQueryable Produtos(bool leave)
        {
            var produto = beIDProduto.Text.ToInt32(true).Padrao();

            if (leave && produto <= 0)
                return null;

            var consulta = new QProduto();
            var retorno = from a in consulta.Buscar((leave ? produto : 0))
                          join b in Conexao.BancoDados.TB_EST_GRUPOs on a.ID_GRUPO equals b.ID_GRUPO
                          where !b.ST_COMPLEMENTO ?? false
                          select new
                          {
                              ID = a.ID_PRODUTO,
                              NM = a.NM,
                          };

            if (leave)
                retorno = retorno.Take(1);

            return retorno;
        }

        #endregion         
    }
}
