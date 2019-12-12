using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SYS.UTILS;

namespace SYS.QUERYS.Lancamentos.Gourmet
{
    public class ListaPedidoItem : List<MPedidoItem>
    { }
 
    public class MPedidoItem
    {
        public bool Select { get; set; }
        public bool ST_PAGO { get; set; }
        public int ID_ITEM { get; set; }
        public int ID_PRODUTO { get; set; }
        public string NM_PRODUTO { get; set; }
        public string OBS { get; set; }
        public bool? ST_IMPRESSO { get; set; }
        public int ID_PEDIDO { get; set; }
        public decimal QUANTIDADE { get; set; }
        public decimal VL_UNITARIO { get; set; }
        public decimal TOTALCOMPLEMENTOS { get; set; }
        public decimal VL_DESCONTO { get; set; }
        public decimal VL_ACRESCIMO { get; set; }
        public decimal VL_SUBTOTAL
        {
            get
            {
                decimal desconto = VL_DESCONTO != null ? VL_DESCONTO : 0m;
                decimal acrescimo = VL_ACRESCIMO != null ? VL_ACRESCIMO : 0m;
                decimal complementos = TOTALCOMPLEMENTOS != null ? TOTALCOMPLEMENTOS : 0m;

                return Math.Round((QUANTIDADE * VL_UNITARIO) - desconto + acrescimo + complementos,2);
            }
            set { }
        }

        public decimal VL_PAGAR { get; set; }

        public bool? ST_ATIVO { get; set; }
        public string DS_CANCELAMENTO { get; set; }

        public ListaComplemento COMPLEMENTOS { get; set; }
        public ListaAdicionais Adicionais { get; set; }

        public MPedidoItem()
        {
            this.Select = true;
            this.ST_PAGO = false;
            this.ID_ITEM = 0;
            this.ID_PRODUTO = 0;
            this.NM_PRODUTO = "";
            this.ST_IMPRESSO = false;
            this.OBS = "";
            this.ID_PEDIDO = 0;
            this.QUANTIDADE = 0m;
            this.VL_UNITARIO = 0m;
            this.TOTALCOMPLEMENTOS = 0m;
            this.VL_DESCONTO = 0m;
            this.VL_ACRESCIMO = 0m;
            this.VL_SUBTOTAL = 0m;
            this.ST_ATIVO = false;
            this.DS_CANCELAMENTO = "";
            this.COMPLEMENTOS = new ListaComplemento();
            this.Adicionais = new ListaAdicionais();
        }
    }

    public class ListaPedido : List<MPedido>
    { }

    public class MPedido
    {
        public bool Select { get; set; }
        public int ID_PEDIDO { get; set; }
        public int? ID_PEDIDO_ORIGEM { get; set; }
        public string  CONDICAO_PAGAMENTO { get; set; }
        //01=Dinheiro
        //02=Cheque
        //03=Cartão de Crédito
        //04=Cartão de Débito
        //05=Crédito Loja
        //10=Vale Alimentação
        //11=Vale Refeição
        //12=Vale Presente
        //13=Vale Combustível
        //99=Outros
        public decimal VALO_DOCUMENTO { get; set; }
        public decimal VALOR_PEDIDO { get; set; }
        public int ID_EMPRESA { get; set; }
        public string ID_MESA { get; set; }
        public string ID_CARTAO { get; set; }
        public string ID_USUARIO { get; set; }
        public int ID_CLIFOR { get; set; }
        public string ST_PEDIDO { get; set; }
        public bool ST_ATIVO { get; set; }
        public string TP_MOVIMENTO { get; set; }
        public DateTime? DT_CADASTRO { get; set; }

        public string TempoAberto
        {
            get
            {
                return Conexao.DataHora.Subtract(DT_CADASTRO.Value).ToString();
            }
            set { }
        }

        public ListaPedidoItem Itens { get; set; }
        

        public MPedido()
        {
            this.ID_PEDIDO = 0;
            this.ID_PEDIDO_ORIGEM = 0;
            this.ID_EMPRESA = 0;
            this.ID_MESA = "";
            this.ID_CARTAO = "";
            this.CONDICAO_PAGAMENTO = "";
            this.VALO_DOCUMENTO = 0m;
            this.VALOR_PEDIDO = 0M;
            this.ID_USUARIO = Parametros.ID_Usuario;
            this.ID_CLIFOR = 0;
            this.ID_PEDIDO = 0;
            this.ST_PEDIDO = "";
            this.ST_ATIVO = false;
            this.TP_MOVIMENTO = "";
            this.Itens = new ListaPedidoItem();
        }
    }

    public class QPedido 
    {
        public int id_pedido = 0;
        public int id_nota = 0;

        public IQueryable<TB_GOU_PEDIDO> Buscar(int id_pedido = 0)
        {
            var consulta = from a in Conexao.BancoDados.TB_GOU_PEDIDOs
                           select a;

            if (id_pedido.TemValor())
                consulta = consulta.Where(a => a.ID_PEDIDO == id_pedido);

            return consulta;
        }

        public IQueryable<MPedido> BuscarView(int id_pedido = 0)
        {
            var consulta = from a in Conexao.BancoDados.TB_GOU_PEDIDOs
                           join b in Conexao.BancoDados.TB_COM_PEDIDOs on a.ID_PEDIDO equals b.ID_PEDIDO
                           select new MPedido
                           {
                               ID_PEDIDO = a.ID_PEDIDO,
                               ID_PEDIDO_ORIGEM = b.ID_PEDIDO_ORIGEM,
                               ID_CLIFOR = b.ID_CLIFOR,
                               ID_EMPRESA = b.ID_EMPRESA,
                               ID_MESA = a.ID_MESA,
                               ID_CARTAO = a.ID_CARTAO,
                               ST_ATIVO = b.ST_ATIVO ?? false,
                               ST_PEDIDO = b.ST_PEDIDO,
                               TP_MOVIMENTO = b.TP_MOVIMENTO,
                               DT_CADASTRO = b.DT_CADASTRO
                           };

            if (id_pedido.TemValor())
                consulta = consulta.Where(a => a.ID_PEDIDO == id_pedido);

            return consulta;
        }

        public IQueryable<MPedidoItem> BuscarItens(string vNrPedido = "", string vProduto = "", string vCdItem = "", bool vST_ATIVO = true)
        {
            var lresult = from a in Conexao.BancoDados.TB_COM_PEDIDOs
                          join i in Conexao.BancoDados.TB_GOU_PEDIDO_ITEMs on a.ID_PEDIDO equals i.ID_PEDIDO
                          join x in Conexao.BancoDados.TB_COM_PEDIDO_ITEMs on new { i.ID_PEDIDO, i.ID_ITEM } equals new { x.ID_PEDIDO, x.ID_ITEM }
                          join y in Conexao.BancoDados.TB_EST_PRODUTOs on x.ID_PRODUTO equals y.ID_PRODUTO
                          where x.ST_ATIVO == vST_ATIVO
                          select new MPedidoItem
                          {
                              QUANTIDADE = x.QT ?? 0m,
                              ID_ITEM = x.ID_ITEM,
                              ID_PEDIDO = i.ID_PEDIDO,
                              ID_PRODUTO = x.ID_PRODUTO ?? 0,
                              NM_PRODUTO = y.NM,
                              VL_UNITARIO = x.VL_UNITARIO ?? 0m,
                              VL_DESCONTO = x.VL_DESCONTO ?? 0m,
                              VL_ACRESCIMO = x.VL_ACRESCIMO ?? 0m,
                              VL_SUBTOTAL = x.VL_SUBTOTAL ?? 0m,
                              ST_IMPRESSO = i.ST_IMPRESSO,
                              ST_ATIVO = x.ST_ATIVO,    
                              OBS = i.DS_OBSERVACAO,
                              TOTALCOMPLEMENTOS = i.TB_GOU_PEDIDO_ITEM_COMPLEMENTOs.Sum(p => p.VL) ?? 0m
                          };


            if (vNrPedido.Length > 0)
                lresult = from i in lresult where i.ID_PEDIDO == Convert.ToInt32(vNrPedido) select i;

            if (vProduto.Length > 0)
                lresult = from i in lresult where i.ID_PRODUTO == Convert.ToInt32(vProduto) select i;

            if (vCdItem.Length > 0)
                lresult = from i in lresult where i.ID_ITEM == Convert.ToInt32(vCdItem) select i;

            return lresult;
        }

        public IQueryable<MComplemento> BuscarComplementos(int vNrPedido = 0, int vItemPedido = 0)
        {

            var lresult = from i in Conexao.BancoDados.TB_GOU_PEDIDO_ITEM_COMPLEMENTOs
                          join y in Conexao.BancoDados.TB_EST_PRODUTOs on i.ID_PRODUTO equals y.ID_PRODUTO
                          where i.ID_ITEM == vItemPedido
                          && i.ID_PEDIDO == vNrPedido
                          select new MComplemento
                          {
                              ID_COMPLEMENTO = i.ID_COMPLEMENTO,
                              ID_ITEMPEDIDO = i.ID_ITEM,
                              ID_PRODUTO = i.ID_PRODUTO,
                              NM_PRODUTO = y.NM,
                              QUANTIDADE = i.QT ?? 0m,
                              VALOR = i.VL ?? 0m,

                          };

            return lresult;
        }

        public IQueryable<MAdicionais> BuscarAdicionais(int vNrPedido = 0, int vItemPedido = 0)
        {

            var lresult = from i in Conexao.BancoDados.TB_GOU_PEDIDO_ITEM_ADICIONALs
                          join y in Conexao.BancoDados.TB_EST_GRUPO_ADICIONAIs on i.ID_ADICIONAL equals y.ID_ADICIONAL 
                          where i.ID_ITEM == vItemPedido
                          && i.ID_PEDIDO == vNrPedido
                          select new MAdicionais
                          {
                              ID_ADICIONAL = i.ID_ADICIONAL,
                              ID_ITEMADICIONAL = i.ID_ITEM_ADICIONAL,
                              ID_GRUPO = i.ID_GRUPO,
                              NM_ADICIONAL = y.DS,
                              ST_TIPO = y.TP
                          };

            return lresult;
        }

        public void Gravar(MPedido pedido, ref int posicaoTransacao, List<MPedidoItem> transferencia = null, bool GeraNotaFiscal = false)
        {
            try
            {
                Conexao.Iniciar(ref posicaoTransacao);

                // Grava primeiramente o pedido no comercial
                var pedidoComercial = new Comercial.QPedido();
                pedidoComercial.Gravar(pedido, GeraNotaFiscal, ref posicaoTransacao, transferencia);
                id_nota = pedidoComercial.ID_NOTA;

                var existente = Conexao.BancoDados.TB_GOU_PEDIDOs.FirstOrDefault(a => a.ID_PEDIDO == pedido.ID_PEDIDO);

                #region Inserção

                if (existente == null)
                {
                    pedido.ID_PEDIDO = (Conexao.BancoDados.TB_GOU_PEDIDOs.Any() ? Conexao.BancoDados.TB_GOU_PEDIDOs.Max(a => a.ID_PEDIDO) : 0) + 1;

                    var vPedido = new TB_GOU_PEDIDO();
                    vPedido.ID_EMPRESA = pedido.ID_EMPRESA;
                    vPedido.ID_PEDIDO = pedido.ID_PEDIDO;
                    vPedido.ID_MESA = pedido.ID_MESA;
                    vPedido.ID_CARTAO = pedido.ID_CARTAO;
                    vPedido.ST = pedido.ST_PEDIDO;                    

                    Conexao.BancoDados.TB_GOU_PEDIDOs.InsertOnSubmit(vPedido);
                    Conexao.Enviar();

                    id_pedido = vPedido.ID_PEDIDO;                    
                }

                #endregion

                #region Atualização

                else
                {
                    existente.ID_PEDIDO = pedido.ID_PEDIDO;
                    existente.ID_EMPRESA = pedido.ID_EMPRESA;
                    existente.ID_MESA = pedido.ID_MESA;
                    existente.ID_CARTAO = pedido.ID_CARTAO;
                    existente.ST = pedido.ST_PEDIDO;
                    Conexao.Enviar();
                    id_pedido = pedido.ID_PEDIDO;
                }

                for (int i = 0; i < pedido.Itens.Count; i++)
                    GravaItemPedido(pedido,pedido.Itens[i]);

                #endregion              
                

                Conexao.Finalizar(ref posicaoTransacao);
            }
            catch (Exception excessao)
            {
                Conexao.Voltar(ref posicaoTransacao);
                throw excessao;
            }
        }

        private void GravaItemPedido(MPedido pedido, MPedidoItem Itens)
        {
            var existenteItem = Conexao.BancoDados.TB_GOU_PEDIDO_ITEMs.FirstOrDefault(a => a.ID_PEDIDO == pedido.ID_PEDIDO && a.ID_ITEM == Itens.ID_ITEM);

            if (existenteItem == null)
            {
                Itens.ID_ITEM = (Conexao.BancoDados.TB_GOU_PEDIDO_ITEMs.Any() ? Conexao.BancoDados.TB_GOU_PEDIDO_ITEMs.Max(a => a.ID_ITEM) : 0) + 1;

                var vItens = new TB_GOU_PEDIDO_ITEM();
                vItens.ID_PEDIDO = pedido.ID_PEDIDO;
                vItens.ID_EMPRESA = pedido.ID_EMPRESA;
                vItens.ID_ITEM = Itens.ID_ITEM;
                vItens.DS_OBSERVACAO = Itens.OBS;
                vItens.ST_IMPRESSO = Itens.ST_IMPRESSO;

                Conexao.BancoDados.TB_GOU_PEDIDO_ITEMs.InsertOnSubmit(vItens);
                Conexao.Enviar();
            }
            else
            {
                existenteItem.ID_EMPRESA = pedido.ID_EMPRESA;
                existenteItem.DS_OBSERVACAO = Itens.OBS;
                existenteItem.ST_IMPRESSO = Itens.ST_IMPRESSO;

                Conexao.Enviar();
            }

            for (int y = 0; y < Itens.COMPLEMENTOS.Count; y++)
            {
                var existenteItemComplemento = Conexao.BancoDados.TB_GOU_PEDIDO_ITEM_COMPLEMENTOs.FirstOrDefault(a => a.ID_PEDIDO == pedido.ID_PEDIDO && a.ID_ITEM == Itens.ID_ITEM && a.ID_COMPLEMENTO == Itens.COMPLEMENTOS[y].ID_COMPLEMENTO);

                if (existenteItemComplemento == null)
                {
                    Itens.COMPLEMENTOS[y].ID_COMPLEMENTO = (Conexao.BancoDados.TB_GOU_PEDIDO_ITEM_COMPLEMENTOs.Any() ? Conexao.BancoDados.TB_GOU_PEDIDO_ITEM_COMPLEMENTOs.Max(a => a.ID_COMPLEMENTO) : 0) + 1;

                    var vComplementos = new TB_GOU_PEDIDO_ITEM_COMPLEMENTO();
                    vComplementos.ID_COMPLEMENTO = Itens.COMPLEMENTOS[y].ID_COMPLEMENTO;
                    vComplementos.ID_EMPRESA = pedido.ID_EMPRESA;
                    vComplementos.ID_PRODUTO = Itens.COMPLEMENTOS[y].ID_PRODUTO;
                    vComplementos.ID_PEDIDO = pedido.ID_PEDIDO;
                    vComplementos.ID_ITEM = Itens.ID_ITEM;
                    vComplementos.QT = Itens.COMPLEMENTOS[y].QUANTIDADE;
                    vComplementos.VL = Itens.COMPLEMENTOS[y].VALOR;

                    Conexao.BancoDados.TB_GOU_PEDIDO_ITEM_COMPLEMENTOs.InsertOnSubmit(vComplementos);
                    Conexao.Enviar();
                }
                else
                {
                    existenteItemComplemento.ID_EMPRESA = pedido.ID_EMPRESA;
                    existenteItemComplemento.ID_PRODUTO = Itens.COMPLEMENTOS[y].ID_PRODUTO;
                    existenteItemComplemento.QT = Itens.COMPLEMENTOS[y].QUANTIDADE;
                    existenteItemComplemento.VL = Itens.COMPLEMENTOS[y].VALOR;
                    Conexao.Enviar();
                }
            }

            for (int y = 0; y < Itens.Adicionais.Count; y++)
            {

                var existenteItemAdicional = Conexao.BancoDados.TB_GOU_PEDIDO_ITEM_ADICIONALs.FirstOrDefault(a => a.ID_PEDIDO == pedido.ID_PEDIDO && a.ID_ITEM == Itens.ID_ITEM && a.ID_ADICIONAL == Itens.Adicionais[y].ID_ADICIONAL);

                if (existenteItemAdicional == null)
                {
                    Itens.Adicionais[y].ID_ITEMADICIONAL = (Conexao.BancoDados.TB_GOU_PEDIDO_ITEM_ADICIONALs.Any() ? Conexao.BancoDados.TB_GOU_PEDIDO_ITEM_ADICIONALs.Max(a => a.ID_ITEM_ADICIONAL) : 0) + 1;

                    var vAdicional = new TB_GOU_PEDIDO_ITEM_ADICIONAL();
                    vAdicional.ID_ITEM_ADICIONAL = Itens.Adicionais[y].ID_ITEMADICIONAL;
                    vAdicional.ID_PEDIDO = pedido.ID_PEDIDO;
                    vAdicional.ID_EMPRESA = pedido.ID_EMPRESA;
                    vAdicional.ID_ITEM = Itens.ID_ITEM;
                    vAdicional.ID_ADICIONAL = Itens.Adicionais[y].ID_ADICIONAL;
                    vAdicional.ID_GRUPO = Itens.Adicionais[y].ID_GRUPO;

                    Conexao.BancoDados.TB_GOU_PEDIDO_ITEM_ADICIONALs.InsertOnSubmit(vAdicional);
                    Conexao.Enviar();
                }
                else
                {
                    existenteItemAdicional.ID_EMPRESA = pedido.ID_EMPRESA;
                    existenteItemAdicional.ID_ADICIONAL = Itens.Adicionais[y].ID_ADICIONAL;
                    existenteItemAdicional.ID_GRUPO = Itens.Adicionais[y].ID_GRUPO;
                    Conexao.Enviar();
                }
            }
        }

        public void AtualizaPedidoItem(MPedido pedido, MPedidoItem item,int _pedidoOrigem , ref int posicaoTransacao)//muda o pedido original do item
        {
            Conexao.Iniciar(ref posicaoTransacao);

            var _item = Conexao.BancoDados.TB_COM_PEDIDO_ITEMs.FirstOrDefault(a => a.ID_PEDIDO == _pedidoOrigem && a.ID_ITEM == item.ID_ITEM);
                        
            var novoItem = new MPedidoItem();
            novoItem.ID_PRODUTO = _item.ID_PRODUTO ?? 0;
            novoItem.QUANTIDADE = _item.QT ?? 0m;
            novoItem.VL_UNITARIO = _item.VL_UNITARIO ?? 0m;
            novoItem.VL_ACRESCIMO = _item.VL_ACRESCIMO ?? 0m;
            novoItem.VL_DESCONTO = _item.VL_DESCONTO ?? 0m;
            novoItem.VL_SUBTOTAL = _item.VL_SUBTOTAL ?? 0m;
            novoItem.ST_ATIVO = _item.ST_ATIVO;
            novoItem.ST_IMPRESSO = _item.TB_GOU_PEDIDO_ITEM.ST_IMPRESSO;
            novoItem.OBS = _item.DS_OBSERVACAO;

            var _adicionais = Conexao.BancoDados.TB_GOU_PEDIDO_ITEM_ADICIONALs.Where(p=> p.ID_ITEM == _item.ID_ITEM && p.ID_PEDIDO == _item.ID_PEDIDO && p.ID_EMPRESA == _item.ID_EMPRESA).ToList();
            
            for (int i = 0; i < _adicionais.Count; i++)
            {
                var novoAdicionais = new MAdicionais();
                novoAdicionais.ID_GRUPO = _adicionais[i].ID_GRUPO;
                novoAdicionais.ID_ITEMPEDIDO = _adicionais[i].ID_ITEM;
                novoAdicionais.ID_ADICIONAL = _adicionais[i].ID_ADICIONAL;
                novoItem.Adicionais.Add(novoAdicionais);
            }

            var _complementos = Conexao.BancoDados.TB_GOU_PEDIDO_ITEM_COMPLEMENTOs.Where(p => p.ID_ITEM == _item.ID_ITEM && p.ID_PEDIDO == _item.ID_PEDIDO && p.ID_EMPRESA == _item.ID_EMPRESA).ToList();

            for (int i = 0; i < _complementos.Count; i++)
            {
                var novoComplementos = new MComplemento();
                novoComplementos.ID_ITEMPEDIDO = _complementos[i].ID_ITEM;
                novoComplementos.ID_PRODUTO = _complementos[i].ID_PRODUTO;
                novoComplementos.QUANTIDADE = _complementos[i].QT ?? 0m;
                novoComplementos.VALOR = _complementos[i].VL ?? 0m;
                novoItem.COMPLEMENTOS.Add(novoComplementos);
            }
            new Comercial.QPedido().GravaPedidoItem(pedido, novoItem);

            GravaItemPedido(pedido, novoItem);

            var delItem = new MPedidoItem();
            delItem.ID_ITEM = _item.ID_ITEM;
            delItem.ID_PEDIDO = _item.ID_PEDIDO;

            new Comercial.QPedido().DeletarItemParaTransferir(delItem , ref posicaoTransacao);

            Conexao.Finalizar(ref posicaoTransacao);
        }

        public void Deletar(MPedido pedido, ref int posicaoTransacao)
        {
            try
            {
                Conexao.Iniciar(ref posicaoTransacao);

                var existente = Conexao.BancoDados.TB_COM_PEDIDOs.FirstOrDefault(a => a.ID_PEDIDO == pedido.ID_PEDIDO);
                if (existente != null)
                    existente.ST_ATIVO = false;

                Conexao.Enviar();

                Conexao.Finalizar(ref posicaoTransacao);
            }
            catch (Exception excessao)
            {
                Conexao.Voltar(ref posicaoTransacao);
                throw excessao;
            }
        }

        public void VerificaMesaSemItem()
        {
            var db = Conexao.BancoDados;

            var Pedido = BuscarView().Where(p => p.ST_PEDIDO == "O");

            for (int i = 0; i < Pedido.Count(); i++)
            {
                var item = (from a in db.TB_COM_PEDIDO_ITEMs
                            where a.ID_PEDIDO == Pedido.ToList()[i].ID_PEDIDO
                            && (a.ST_ATIVO ?? false) == true
                            select a).ToList();


                if (item.Count == 0 || (Pedido.ToList()[i].ID_MESA == "0" && Pedido.ToList()[i].ID_CARTAO == "0"))
                {
                    int posicao_transacao = 0;
                    Pedido.ToList()[i].ST_ATIVO = false;
                    Pedido.ToList()[i].ST_PEDIDO = "F";
                    MPedido vPedido = Pedido.ToList()[i];

                    vPedido.ST_PEDIDO = "F";
                    vPedido.ST_ATIVO = false;

                    Gravar(vPedido, ref posicao_transacao);
                }
            }

        }
    }
}