using System.Linq;
using SYS.UTILS;
using SYS.QUERYS.Lancamentos.Financeiro;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SYS.QUERYS;

namespace SYS.FORMS.Lancamentos.Financeiro
{
    public partial class FCaixa : SYS.FORMS.FBase_CadastroBusca
    {
        #region Declarações

        #endregion
        public FCaixa()
        {
            InitializeComponent();           
        }

        private void FCaixa_Load(object sender, EventArgs e)
        {
            bbiAdicionar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            bbiAlterar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            bbiDeletar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            bbiFicha.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
        }

        #region Métodos

        public override void Buscar()
        {
            var db = Conexao.BancoDados;

            var consulta = (from a in new QCaixaDiario().BuscarTotais("", ceFechados.Checked ? "" : deCaixa.Text.Trim(), ceFechados.Checked ? deCaixa.Text.Trim() : "")
                            select new
                            {
                                USUARIO = a.ID_USUARIO,
                                VALOR = (ceFechados.Checked ?
                                        ((from x in db.TB_COM_PEDIDOs
                                          join y in db.TB_COM_PEDIDO_ITEMs on x.ID_PEDIDO equals y.ID_PEDIDO
                                          where y.ST_ATIVO == true
                                          && x.ID_USUARIO == a.ID_USUARIO
                                          && x.DT_CADASTRO >= a.DT_INICIAL
                                          && x.DT_CADASTRO <= a.DT_FINAL
                                          select y.VL_SUBTOTAL).Sum() ?? 0m) :
                                        ((from x in db.TB_COM_PEDIDOs
                                          join y in db.TB_COM_PEDIDO_ITEMs on x.ID_PEDIDO equals y.ID_PEDIDO
                                          where y.ST_ATIVO == true
                                          && x.ID_USUARIO == a.ID_USUARIO
                                          && x.DT_CADASTRO >= a.DT_INICIAL
                                          && a.DT_FINAL == null
                                          select y.VL_SUBTOTAL).Sum() ?? 0m))
                                        + a.VALOR,
                                ABERTURA = a.DT_INICIAL,
                                FECHAMENTO = a.DT_FINAL
                            }).Where(x => x.VALOR > 0);

            teUsuario.Text.Validar(true);
            if (teUsuario.Text.TemValor())
                consulta = consulta.Where(a => a.USUARIO.Contains(teUsuario.Text.Trim()));

            gcCaixa.DataSource = consulta;
            gvCaixa.BestFitColumns(true);
        }

        #endregion
    }
}
