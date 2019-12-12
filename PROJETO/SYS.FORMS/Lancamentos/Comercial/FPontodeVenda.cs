using SYS.QUERYS;
using SYS.UTILS;
using System.Linq;
using SYS.QUERYS.Cadastros.Estoque;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SYS.QUERYS.Lancamentos.Gourmet;
using SYS.QUERYS.Lancamentos.Comercial;
using SYS.QUERYS.Lancamentos.Financeiro;
using SYS.QUERYS.Cadastros.Relacionamento;
using DevExpress.XtraEditors;
using SYS.FORMS.Lancamentos.Fiscal;

namespace SYS.FORMS.Lancamentos.Comercial
{
    public partial class FPontodeVenda : SYS.FORMS.FBase
    {

        decimal DescontoPercentual = 0m;
        decimal DescontoValor = 0m;
        decimal Quantidade = 1m;
        bool ST_Cancelar = false;
        decimal NR_pedido = 0m;
        int ID_NOTA = 0;
        decimal valorTroco = 0m;
        int ID_Clifor = 1;

        private void afterGravar(string condicao,decimal valor)
        {
            if (gvItens.DataSource == null)
                return;

            if (valor < seTotalPagar.Value)
                valor = seTotalPagar.Value;


            if (condicao == "01")
                if (XtraMessageBox.Show("Confirmar fechamento em dinheiro?", "Pergunta", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    return;

            if (condicao == "03")
                if (XtraMessageBox.Show("Confirmar fechamento em cartão de crédito?", "Pergunta", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    return;

            if (condicao == "04")
                if (XtraMessageBox.Show("Confirmar fechamento em cartão de débito?", "Pergunta", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    return;
                    
            if (condicao == "05")
                BuscaClifor();

            decimal valorDoc = 0m;

            decimal liquidado = new QDuplicata().BuscaTotalLiquidado(ID_NOTA);
            decimal descontado = new QDuplicata().BuscaTotalDesconto(ID_NOTA);

            valorDoc = valor > seTotalPagar.Value - liquidado ? seTotalPagar.Value - liquidado : valor;
            valorTroco = condicao == "01" && valor > seTotalPagar.Value - liquidado ? valor - (seTotalPagar.Value - liquidado) : 0;

            var vpedido = new MPedido();

            vpedido.ID_EMPRESA = 1;
            vpedido.ID_CLIFOR = ID_Clifor;
            vpedido.ST_ATIVO = gvItens.DataRowCount > 0;
            vpedido.ST_PEDIDO = "F";
            vpedido.TP_MOVIMENTO = "S";
            vpedido.CONDICAO_PAGAMENTO = condicao;
            vpedido.VALO_DOCUMENTO = valorDoc;

            for (int i = 0; i < bsItens.Count; i++)
                vpedido.Itens.Add((bsItens[i] as MPedidoItem));

            var pedido = new SYS.QUERYS.Lancamentos.Comercial.QPedido();
            var posicaoTransacao = 0;


            pedido.Gravar(vpedido,false, ref posicaoTransacao);

            NR_pedido = pedido.ID_PEDIDO;
            ID_NOTA = pedido.ID_NOTA;

            calculaSubTotal(true);

            if (seTotalPagar.Value > 0)
            {
                bsItens.Clear();
                zeracampos();
                if(valorTroco > 0)
                    teNM.Text = "Troco: " + valorTroco.ToString("N2");
            }
            
            //if(Mensagens.Pergunta("Deseja enviar e imprimir NFC-e?") == DialogResult.Yes)
            //{
            //    var processador = new FComunicadorFiscal_Processamento();

            //    var documento = QQuery.BancoDados.TB_FAT_NOTA_X_DOCUMENTOs.FirstOrDefault(a => a.ID_NOTA == pedido.ID_NOTA && a.ID_EMPRESA == vpedido.ID_EMPRESA);
            //    processador.Enviar(documento.ID_DOCUMENTO, documento.ID_EMPRESA, Modelo.NFCe, Ambiente.Producao, Emissao.ContigenciaOFFLINE, ImpressaoDANFE.NFCe);
            //}
        }
        
        public FPontodeVenda()
        {
            InitializeComponent();
            teCodigo.BackColor = this.BackColor;
            teNM.BackColor = this.BackColor;            
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
                        ID_Clifor = (filtro.Selecionados.FirstOrDefault().ID as int?).Padrao();
                    }
                }
            }
            catch (Exception excessao)
            {
                excessao.Validar();
            }
        }

        private void teCodigo_Leave(object sender, EventArgs e)
        {
            teCodigo.Focus();
        }        

        private void teCodigo_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.L)
                    teCodigo.Text = "";

                //fechamento
                if (e.KeyCode == Keys.F2 || e.KeyCode == Keys.F3 || e.KeyCode == Keys.F4 || e.KeyCode == Keys.F5)
                {
                    if (seSubTotal.Value <= 0 && bsItens.Count <= 0)
                        teNM.Text = "Nenhum produto adicionado";

                    decimal valorPagar = teCodigo.Text.Trim().Length > 0 ? Convert.ToDecimal(teCodigo.Text.Trim()) : seTotalPagar.Value;
                    
                    if (e.KeyCode == Keys.F2 && bsItens.Count > 0)
                        afterGravar("01",valorPagar);
                    if (e.KeyCode == Keys.F3 && bsItens.Count > 0)
                        afterGravar("03", valorPagar);
                    if (e.KeyCode == Keys.F4 && bsItens.Count > 0)
                        afterGravar("04", valorPagar);
                    if (e.KeyCode == Keys.F5 && bsItens.Count > 0)
                        afterGravar("05", valorPagar);

                    teCodigo.Text = "";
                }

                if (e.KeyCode == Keys.P)
                    Busca();

                if (e.KeyCode == Keys.C && bsItens.Count > 0)
                {
                    ST_Cancelar = true;
                    teNM.Text = "Selecione o item para cancelar";
                    e.SuppressKeyPress = true;
                    return;
                }

                if (e.KeyCode == Keys.Escape)
                    if (seTotalPagar.Value > 0)
                    {
                        if (XtraMessageBox.Show("Deseja sair e cancelar a compra?", "Atenção", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            this.Close();
                    }
                    else
                        this.Close();

                if (teCodigo.Text.Trim().Length == 0)
                    if ((e.KeyCode != Keys.NumPad0 && e.KeyCode != Keys.D0) &&
                        (e.KeyCode != Keys.NumPad1 && e.KeyCode != Keys.D1) &&
                        (e.KeyCode != Keys.NumPad2 && e.KeyCode != Keys.D2) &&
                        (e.KeyCode != Keys.NumPad3 && e.KeyCode != Keys.D3) &&
                        (e.KeyCode != Keys.NumPad4 && e.KeyCode != Keys.D4) &&
                        (e.KeyCode != Keys.NumPad5 && e.KeyCode != Keys.D5) &&
                        (e.KeyCode != Keys.NumPad6 && e.KeyCode != Keys.D6) &&
                        (e.KeyCode != Keys.NumPad7 && e.KeyCode != Keys.D7) &&
                        (e.KeyCode != Keys.NumPad8 && e.KeyCode != Keys.D8) &&
                        (e.KeyCode != Keys.NumPad9 && e.KeyCode != Keys.D9))
                    {
                        e.SuppressKeyPress = true;
                        teCodigo.Text = "";
                        return;
                    }

                if (teCodigo.Text.Trim() != "")
                    switch (e.KeyCode)
                    {
                        case Keys.D:
                            {
                                e.SuppressKeyPress = true;
                                DescontoPercentual = Convert.ToDecimal(teCodigo.Text.Trim());
                                teCodigo.Text = "";
                            } break;
                        case Keys.V:
                            {
                                e.SuppressKeyPress = true;
                                DescontoValor = Convert.ToDecimal(teCodigo.Text.Trim());
                                teCodigo.Text = "";
                            } break;
                        case Keys.X:
                            {
                                e.SuppressKeyPress = true;
                                Quantidade = Convert.ToDecimal(teCodigo.Text.Trim());
                                teCodigo.Text = "";
                            } break;
                        case Keys.Enter:
                            {
                                if (teCodigo.Text.Trim() != "")
                                    BuscarItens(teCodigo.Text.Trim());
                                teCodigo.Text = "";
                            }
                            break;
                    }
            }
            catch (Exception ex)
            {
                ex.Validar();
            }
        }

        private void calculaSubTotal(bool subGrid = false)
        {
            decimal liquidado = 0m;
            decimal desconto = 0m;
            decimal vlPrazo = 0m;

            if (NR_pedido != 0)
            {
                liquidado = Math.Round(new QDuplicata().BuscaTotalLiquidado(ID_NOTA), 2);
                desconto = Math.Round(new QDuplicata().BuscaTotalDesconto(ID_NOTA), 2);
                vlPrazo = Math.Round(new QDuplicata().BuscaTotalAprazo(ID_NOTA),2);
            }

            if (subGrid)
            {
                for (int i = 0; i < bsItens.Count; i++)
                    seTotalPagar.Value += (bsItens[i] as MPedidoItem).VL_SUBTOTAL - liquidado - desconto - vlPrazo;
            }
            else
            {
                var valorDesconto = DescontoValor > 0 ? DescontoValor : DescontoPercentual > 0 ? ((teUnitario.Value * Quantidade) * DescontoPercentual) / 100 : 0m;

                if (valorDesconto >= (teUnitario.Value * Quantidade))
                    throw new Exception("O desconto deve ser menor que o subtotal do item!");

                teQuantidade.Value = Math.Round(Quantidade,2);
                teDesconto.Value = Math.Round(valorDesconto,2);
                seSubTotal.Value = Math.Round((teUnitario.Value * Quantidade) - valorDesconto - liquidado - desconto - vlPrazo,2);
                seTotalPagar.Value += Math.Round((teUnitario.Value * Quantidade) - valorDesconto - liquidado - desconto - vlPrazo, 2);
            }
        }

        private void BuscarItens(string valor)
        {
            try
            {
                var db = Conexao.BancoDados;

                var lresult = (from i in new QProduto().Buscar()
                               join y in Conexao.BancoDados.TB_EST_PRODUTO_BARRAs on i.ID_PRODUTO equals y.ID_PRODUTO
                              where y.ID_BARRA_REFERENCIA == valor.PadLeft(14,'0')
                              select i).ToList();

                if (lresult.Count <= 0)
                {
                    lresult = null;
                    lresult = (from i in new QProduto().Buscar()
                               where i.ID_PRODUTO == Convert.ToInt32(valor)
                               select i).ToList();
                }

                if (lresult.Count > 0)
                {
                    var VL_VENDA = (from i in Conexao.BancoDados.TB_EST_PRECOs
                                   where i.ID_PRODUTO == lresult[0].ID_PRODUTO
                                   && (i.ST_ATIVO ?? false)
                                   && i.TP_PRECO == "V"
                                   select i).ToList();

                    if (VL_VENDA.Count <= 0)
                    {
                        teNM.Text = "Produto sem preço!";
                        return;
                    }
                        

                    teUnitario.Value = VL_VENDA[0].VL_PRECO ?? 0m;
                    teNM.Text = lresult[0].NM;

                    //subtotal
                    calculaSubTotal();
                    //------------

                    //adicionar itens

                    var itens = new MPedidoItem();
                    itens.ID_ITEM = bsItens.Count + 1;
                    itens.ID_PRODUTO = lresult[0].ID_PRODUTO;
                    itens.NM_PRODUTO = lresult[0].NM;
                    itens.VL_UNITARIO = VL_VENDA[0].VL_PRECO ?? 0m;
                    itens.QUANTIDADE = Quantidade;
                    itens.VL_DESCONTO = teDesconto.Value;
                    itens.VL_SUBTOTAL = (teUnitario.Value * Quantidade) - teDesconto.Value;
                    itens.ST_ATIVO = true;
                    bsItens.Add(itens);

                    DescontoValor = 0m;
                    DescontoPercentual = 0m;
                    Quantidade = 1m;

                }
                else
                    teNM.Text = "PRODUTO NÃO CADASTRADO";
            }
            catch (Exception ex)
            {
                ex.Validar();
                zeracampos();
                DescontoValor = 0m;
                DescontoPercentual = 0m;
                Quantidade = 1m;
            }
        }

        private void zeracampos()
        {
            teNM.Text = "Informe um produto";
            teDesconto.Value = 0m;
            teQuantidade.Value = 0m;
            teUnitario.Value = 0m;
            seTotalPagar.Value = 0m;
            seSubTotal.Value = 0m;
        }

        private void Busca()
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
                        teCodigo.Text = (filtro.Selecionados.FirstOrDefault().ID as int?).Padrao().ToString();
                    }
                    else
                        teCodigo.Text = "";
                }
            }
            catch (Exception excessao)
            {
                excessao.Validar();
            }
        }

        private IQueryable Produtos(bool leave)
        {
            var produto = teCodigo.Text.ToInt32(true).Padrao();

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

        private void FPontodeVenda_Load(object sender, EventArgs e)
        {
            teCodigo.Select();
        }

        private void gcItens_Click(object sender, EventArgs e)
        {
            try
            {
                if (ST_Cancelar)
                    if (XtraMessageBox.Show("Confirmar cancelamento do produto " + (bsItens.Current as MPedidoItem).NM_PRODUTO + "?", "Atenção", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        seTotalPagar.Value -= (bsItens.Current as MPedidoItem).VL_SUBTOTAL;
                        bsItens.Remove(bsItens.Current);
                        ST_Cancelar = false;
                        zeracampos();
                        calculaSubTotal(true);
                    }
            }
            catch (Exception ex)
            {
                ex.Validar();
            }

        }

        private IQueryable Clifors(bool leave)
        {
            var consulta = new QClifor();
            var retorno = from a in consulta.Buscar(0)
                          select new
                          {
                              ID = a.ID_CLIFOR,
                              NM = a.NM,
                          };

            if (leave)
                retorno = retorno.Take(1);

            return retorno;
        }

        private void teCodigo_KeyPress(object sender, KeyPressEventArgs e)
        {
            string caracteresPermitidos = "1234567890,";

            if (!(caracteresPermitidos.Contains(e.KeyChar.ToString().ToUpper())))
            {
                e.Handled = true;
            } 
        }

    }
}
