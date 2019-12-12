using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraLayout;
using DevExpress.XtraReports.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Linq;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SYS.UTILS
{
    public static class Extensoes
    {
        public static Int32? ToInt32(this string valor, bool identificadorValido = false, string nomeIdentificador = "", bool mostrarExcessao = false)
        {
            valor = valor.RemoverEspacos();
            var retorno = (Int32?)null;

            try
            {
                retorno = Convert.ToInt32(valor);
            }
            catch
            {
                try
                {
                    retorno = System.Int32.Parse(valor);
                }
                catch
                {
                    try
                    {
                        var retornoValorizado = 0;
                        System.Int32.TryParse(valor, out retornoValorizado);
                        retorno = retornoValorizado;
                    }
                    catch { }
                }
            }

            if (identificadorValido && retorno <= 0)
            {
                retorno = null;

                if (mostrarExcessao)
                    throw new SYSException("É necessário informar o identificador" + (nomeIdentificador.TemValor() ? (" do " + nomeIdentificador) : "") + "!");
            }

            return retorno;
        }

        public static Decimal? ToDecimal(this string valor, bool identificadorValido = false, string nomeIdentificador = "", bool mostrarExcessao = false)
        {
            valor = valor.RemoverEspacos();
            var retorno = (Decimal?)null;

            try
            {
                retorno = Convert.ToDecimal(valor);
            }
            catch
            {
                try
                {
                    retorno = System.Decimal.Parse(valor);
                }
                catch
                {
                    try
                    {
                        var retornoValorizado = 0m;
                        System.Decimal.TryParse(valor, out retornoValorizado);
                        retorno = retornoValorizado;
                    }
                    catch { }
                }
            }

            if (identificadorValido && retorno <= 0m)
            {
                retorno = null;

                if (mostrarExcessao)
                    throw new SYSException("É necessário informar o identificador" + (nomeIdentificador.TemValor() ? (" do " + nomeIdentificador) : "") + "!");
            }

            return retorno;
        }

        public static string Padrao(this string valor)
        {
            valor = (valor ?? "").ToUpper();

            return valor;
        }
        public static Int32 Padrao(this Int32? valor)
        {
            return valor ?? 0;
        }
        public static Decimal Padrao(this Decimal? valor)
        {
            return valor ?? 0m;
        }

        public static bool Padrao(this bool? valor)
        {
            return valor ?? false;
        }
        public static DateTime Padrao(this DateTime? valor)
        {
            return valor ?? new DateTime();
        }


        /// <summary>
        /// Compute hash for string encoded as UTF8
        /// </summary>
        /// <param name="s">String to be hashed</param>
        /// <returns>40-character hex string</returns>
        public static string SHA1HashStringForUTF8String(this string s)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(s);

            var sha1 = SHA1.Create();
            byte[] hashBytes = sha1.ComputeHash(bytes);

            return HexStringFromBytes(hashBytes);
        }

        /// <summary>
        /// Convert an array of bytes to a string of hex digits
        /// </summary>
        /// <param name="bytes">array of bytes</param>
        /// <returns>String of hex digits</returns>
        public static string HexStringFromBytes(byte[] bytes)
        {
            var sb = new StringBuilder();
            foreach (byte b in bytes)
            {
                var hex = b.ToString("x2");
                sb.Append(hex);
            }
            return sb.ToString();
        }


        public static void Padronizar(this XtraForm forma)
        {
            if (forma == null)
                return;

            try
            {
                PadronizarForma(forma.Controls.Cast<Control>().ToList());
            }
            catch (Exception excessao)
            {
                excessao.Validar();
            }
        }
        private static void PadronizarForma(this List<Control> controles)
        {
            if (controles == null)
                return;

            try
            {
                foreach(var controle in controles)
                {
                    if (controle.Controls != null)
                        PadronizarForma(controle.Controls.Cast<Control>().ToList());

                    if(controle is LayoutControl)
                        (controle as LayoutControl).AllowCustomization = false;
                }
            }
            catch (Exception excessao)
            {
                excessao.Validar();
            }
        }


        public static void Padronizar(this GridControl grade, Single tamanhoFonte = 8, string nomeFonte = "Tahoma", bool mostrarFiltro = false)
        {
            if (grade == null)
                return;

            try
            {
                var fonte = new System.Drawing.Font(nomeFonte, tamanhoFonte);

                foreach (GridView view in grade.Views)
                {
                    view.OptionsView.ColumnAutoWidth = false;
                    view.OptionsView.EnableAppearanceEvenRow = true;
                    //view.OptionsView.ShowAutoFilterRow = true;
                    view.OptionsView.ShowIndicator = false;

                    view.OptionsFind.AlwaysVisible = mostrarFiltro;
                    view.OptionsFind.ShowClearButton = false;
                    view.OptionsFind.ShowCloseButton = false;
                    view.OptionsFind.ShowFindButton = false;

                    view.Appearance.ColumnFilterButton.Font = fonte;
                    view.Appearance.ColumnFilterButtonActive.Font = fonte;
                    view.Appearance.CustomizationFormHint.Font = fonte;
                    view.Appearance.DetailTip.Font = fonte;
                    view.Appearance.Empty.Font = fonte;
                    view.Appearance.EvenRow.Font = fonte;
                    view.Appearance.FilterCloseButton.Font = fonte;
                    view.Appearance.FilterPanel.Font = fonte;
                    view.Appearance.FixedLine.Font = fonte;
                    view.Appearance.FocusedCell.Font = fonte;
                    view.Appearance.FocusedRow.Font = fonte;
                    view.Appearance.FooterPanel.Font = fonte;
                    view.Appearance.GroupButton.Font = fonte;
                    view.Appearance.GroupFooter.Font = fonte;
                    view.Appearance.GroupPanel.Font = fonte;
                    view.Appearance.GroupRow.Font = fonte;
                    view.Appearance.HeaderPanel.Font = fonte;
                    view.Appearance.HideSelectionRow.Font = fonte;
                    view.Appearance.HorzLine.Font = fonte;
                    view.Appearance.OddRow.Font = fonte;
                    view.Appearance.Preview.Font = fonte;
                    view.Appearance.Row.Font = fonte;
                    view.Appearance.RowSeparator.Font = fonte;
                    view.Appearance.SelectedRow.Font = fonte;
                    view.Appearance.TopNewRow.Font = fonte;
                    view.Appearance.VertLine.Font = fonte;
                    view.Appearance.ViewCaption.Font = fonte;
                }
            }
            catch (Exception excessao)
            {
                excessao.Validar();
            }
        }

        public static void Padronizar(this ButtonEdit campo, bool somenteNumerosInteiros)
        {
            if (campo == null)
                return;

            campo.Properties.Mask.EditMask = @"\p{Lu}+";

            if (somenteNumerosInteiros)
            {
                campo.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
                campo.Properties.Mask.UseMaskAsDisplayFormat = true;
                campo.Properties.Mask.EditMask = @"[0-9]+";
            }
        }

        public static void Padronizar(this TextEdit campo, bool somenteNumerosInteiros)
        {
            if (campo == null)
                return;

            campo.Properties.Mask.EditMask = @"\p{Lu}+";

            if (somenteNumerosInteiros)
            {
                campo.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
                campo.Properties.Mask.UseMaskAsDisplayFormat = true;
                campo.Properties.Mask.EditMask = @"[0-9]+";
            }
        }
        
        public static void Padronizar(this DateEdit campo, bool dataInteira = false)
        {
            if (campo == null)
                return;

            campo.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            campo.Properties.Mask.UseMaskAsDisplayFormat = true;
            campo.Properties.Mask.EditMask = "([012]?[1-9]|[123]0|31)/(0?[1-9]|1[012])/([123][0-9])?[0-9][0-9]" + (dataInteira ? @"(0\d|1\d|2[0-3])\:(0?[1-9]|1[019]|2[029]|3[039]|4[049]|5[059])" : "");
        }

        public static string GetID(this ComboBoxEdit componente)
        {
            if (componente == null || componente.Properties.Items.Count == 0)
                return null;

            var selecionado = componente.SelectedItem.ToString();

            if (selecionado == null)
                return null;

            return selecionado.Split('-')[0].Trim();
        }

        public static void SetID(this ComboBoxEdit componente, IdentificadorDescricao identificador)
        {
            if (componente == null || componente.Properties.Items.Count == 0)
                return;

            componente.SelectedItem = identificador.ID + " - " + identificador.DESCRICAO.Validar(true);
        }

        public static void SetList(this ComboBoxEdit componente, List<IdentificadorDescricao> lista)
        {
            if (componente == null || componente.Properties.Items.Count == 0 || lista == null || lista.Count == 0)
                return;

            componente.Properties.Items.Clear();
            componente.Properties.Items.Add(""); // Para cadastros novos
            lista.ForEach(a => componente.Properties.Items.Add(a.ID + " - " + a.DESCRICAO.Validar(true)));
        }

        public static string PTBR(this DateTime valor, bool somenteHorasSegundos = false, bool comHorasSegundos = false)
        {
            return valor.ToString(somenteHorasSegundos ? "HH:mm:ss" : (comHorasSegundos ? "dd/MM/yyyy HH:mm:ss" : "dd/MM/yyyy"));
        }

        public static string RemoverEspacos(this string valor)
        {
            return (valor ?? "").Replace(" ", "");
        }

        public static string Validar(this string valor, bool sobreescreverOriginal = false, int casasLimite = -1)
        {
            var retorno = (valor ?? "").Trim().ToUpper();

            if (sobreescreverOriginal)
                valor = retorno;

            if (casasLimite >= 0)
                valor = valor.Substring(0, casasLimite > valor.Length ? valor.Length : casasLimite);

            return retorno;
        }

        public static decimal Validar(this decimal? valor)
        {
            valor = valor ?? 0m;

            return (decimal)valor;
        }

        public static bool TemValor(this string valor)
        {
            return (valor ?? "").Trim().Length > 0;
        }

        public static bool TemValor(this int valor, bool diferenteZero = true, bool menorZero = false, bool maiorZero = true)
        {
            return (diferenteZero && valor != 0) || (menorZero && valor < 0) || (maiorZero && valor > 0);
        }
        
        public static bool TemValor(this int? valor, bool diferenteZero = true, bool menorZero = false, bool maiorZero = true)
        {
            return valor.Padrao().TemValor(diferenteZero, menorZero, maiorZero);
        }

        public static bool TemValor (this DateTime? valor)
        {
            return valor == null || valor < valor.Padrao();
        }

        public static bool TemValor(this decimal valor, bool diferenteZero = true, bool menorZero = false, bool maiorZero = true)
        {
            return (diferenteZero && valor != 0m) || (menorZero && valor < 0m) || (maiorZero && valor > 0m);
        }

        public static bool TemValor(this decimal? valor, bool diferenteZero = true, bool menorZero = false, bool maiorZero = true)
        {
            return valor.Padrao().TemValor(diferenteZero, menorZero, maiorZero);
        }

        public static string ENUS(this decimal valor, int casas)
        {
            return valor.ToString("F" + casas, CultureInfo.CreateSpecificCulture("en-US"));
        }

        public static string ENUS(this int valor, int casas)
        {
            return ENUS(Convert.ToDecimal(valor), casas);
        }

        public static DateTime DataMinima
        {
            get
            {
                return new DateTime(2000, 1, 1);
            }
        }

        public static bool DataValida(this DateTime valor)
        {
            return valor < DataMinima;
        }

        public static bool DataValida(this DateTime? valor)
        {
            return valor.Padrao() < DataMinima;
        }
        

        public static dynamic GetSelectedRow(this GridView view)
        {
            if (view==null || view.FocusedRowHandle < 0)
                return null;

            return view.GetRow(view.FocusedRowHandle) as dynamic;
        }

        public static T GetSelectedRow<T>(this GridView view) where T : class
        {
            if (view == null || view.FocusedRowHandle < 0)
                return null;

            return view.GetRow(view.FocusedRowHandle) as T;
        }

        public static Type ToCSharpType(this object sqlType, bool nullable)
        {
            switch (sqlType == null ? "" : sqlType.ToString())
            {
                case "bigint":
                    return nullable ? typeof(long?) : typeof(long);

                case "binary":
                case "image":
                case "timestamp":
                case "varbinary":
                    return typeof(byte[]);

                case "bit":
                    return nullable ? typeof(bool?) : typeof(bool);

                case "char":
                case "nChar":
                case "ntext":
                case "nvarchar":
                case "text":
                case "varchar":
                case "xml":
                    return typeof(string);

                case "datetime":
                case "smalldatetime":
                case "date":
                case "time":
                case "datetime2":
                    return nullable ? typeof(DateTime?) : typeof(DateTime);

                case "decimal":
                case "money":
                case "smallmoney":
                    return nullable ? typeof(decimal?) : typeof(decimal);

                case "float":
                    return nullable ? typeof(double?) : typeof(double);

                case "int":
                    return nullable ? typeof(int?) : typeof(int);

                case "real":
                    return nullable ? typeof(float?) : typeof(float);

                case "uniqueidentifier":
                    return nullable ? typeof(Guid?) : typeof(Guid);

                case "smallint":
                    return nullable ? typeof(short?) : typeof(short);

                case "tinyint":
                    return nullable ? typeof(byte?) : typeof(byte);

                case "variant":
                case "udt":
                    return typeof(object);

                case "structured":
                    return typeof(DataTable);

                case "datetimeoffset":
                    return nullable ? typeof(DateTimeOffset?) : typeof(DateTimeOffset);

                default:
                    return typeof(string);
            }
        }

        public static dynamic FirstOrDefaultDynamic (this IQueryable lista)
        {
            if (lista == null)
                return null;

            return lista.Cast<dynamic>().FirstOrDefault();
        }

        public static EntitySet<T> ToEntitySet<T>(this IEnumerable<T> lista) where T : class
        {
            var retorno = new EntitySet<T>();

            if (lista == null)
                return retorno;

            foreach (T registro in lista)
                retorno.Add(registro);

            return retorno;
        }

        public static BindingList<T> ToBindingList<T>(this EntitySet<T> lista) where T : class
        {
            var retorno = new BindingList<T>();

            if (lista == null)
                return retorno;

            foreach (T registro in lista)
                retorno.Add(registro);

            return retorno;
        }

        public static BindingList<dynamic> ToBindingListDynamic(this IEnumerable lista)
        {
            return (lista.Cast<dynamic>() as BindingList<dynamic>);
        }

        public static List<T> ToList<T>(this EntitySet<T> lista) where T : class
        {
            var retorno = new List<T>();

            if (lista == null)
                return retorno;

            foreach (T registro in lista)
                retorno.Add(registro);

            return retorno;
        }

        public static IQueryable ToIQueryable<T>(this GridView gridView)
        {
            return gridView.ToGridList<T>().AsQueryable();
        }

        public static List<T> ToGridList<T>(this GridView gridView)
        {
            if (gridView == null)
                return null;

            var data = new List<T>();

            for (int a = 0; a < gridView.DataRowCount; a++)
                data.Add((T)gridView.GetRow(a));

            return data;
        }

        public static DataTable ToDataTable<T>(this GridView gridView)
        {
            return ToDataTable<T>(gridView.ToIQueryable<T>().Cast<T>());
        }

        public static DataTable ToDataTable<T>(this IQueryable<T> varlist)
        {
            DataTable dtReturn = new DataTable();

            // column names 
            PropertyInfo[] oProps = null;

            if (varlist == null) return dtReturn;

            foreach (T rec in varlist)
            {
                // Use reflection to get property names, to create table, Only first time, others  will follow 
                if (oProps == null)
                {
                    oProps = ((Type)rec.GetType()).GetProperties();
                    foreach (PropertyInfo pi in oProps)
                    {
                        Type colType = pi.PropertyType;

                        if ((colType.IsGenericType) && (colType.GetGenericTypeDefinition() == typeof(Nullable<>)))
                        {
                            colType = colType.GetGenericArguments()[0];
                        }

                        dtReturn.Columns.Add(new DataColumn(pi.Name, colType));
                    }
                }

                DataRow dr = dtReturn.NewRow();

                foreach (PropertyInfo pi in oProps)
                {
                    var ret = pi.GetValue(rec, null);
                    dr[pi.Name] = ret == null ? DBNull.Value : ret;
                }

                dtReturn.Rows.Add(dr);
            }
            return dtReturn;
        }

        public static DataSet ToDataSet<T>(this IList<T> list)
        {
            Type elementType = typeof(T);
            DataSet ds = new DataSet();
            DataTable t = new DataTable();
            ds.Tables.Add(t);

            //add a column to table for each public property on T
            foreach (var propInfo in elementType.GetProperties())
            {
                Type ColType = Nullable.GetUnderlyingType(propInfo.PropertyType) ?? propInfo.PropertyType;

                t.Columns.Add(propInfo.Name, ColType);
            }

            //go through each property on T and add each value to the table
            foreach (T item in list)
            {
                DataRow row = t.NewRow();

                foreach (var propInfo in elementType.GetProperties())
                {
                    row[propInfo.Name] = propInfo.GetValue(item, null) ?? DBNull.Value;
                }

                t.Rows.Add(row);
            }

            return ds;
        }


        public static Stream ToStream(this XtraReport report)
        {
            var stream = new MemoryStream();
            report.SaveLayout(stream);

            return stream;
        }

        public static Stream ToStream(this string param)
        {
            return new MemoryStream(Encoding.UTF8.GetBytes(param));
        }

        public static XtraReport ToXtraReport(this Stream report)
        {
            return XtraReport.FromStream((MemoryStream)report, true);
        }

        public static XtraReport ToXtraReport(this string report)
        {
            return ToXtraReport(ToStream(report));
        }

        public static string Save(this XtraReport report)
        {
            var stream = new MemoryStream();
            report.SaveLayoutToXml(stream);

            stream.Position = 0;
            return new StreamReader(stream).ReadToEnd();
        }
    }
}