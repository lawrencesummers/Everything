namespace WHC.OrderWater.Commons.Web
{
    using System;
    using System.Collections;
    using System.IO;
    using System.Text;
    using System.Web;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    public class ExcelHelper
    {
        public static void ExportExcel(DataGrid dataGrid)
        {
            string str = DateTime.Now.ToFileTime() + ".xls";
            HttpResponse response = HttpContext.Current.Response;
            response.Charset = "GB2312";
            response.ContentEncoding = Encoding.GetEncoding("GB2312");
            response.ContentType = "application/ms-excel/msword";
            response.AppendHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode(str));
            StringWriter writer = new StringWriter();
            HtmlTextWriter writer2 = new HtmlTextWriter(writer);
            writer2.WriteLine("<meta http-equiv=\"Content-Type\" content=\"text/html;charset=GB2312\">");
            foreach (DataGridColumn column in dataGrid.Columns)
            {
                if (((column is ButtonColumn) || (column is EditCommandColumn)) || (column is HyperLinkColumn))
                {
                    column.Visible = false;
                }
            }
            if (dataGrid.Items.Count > 0)
            {
                TableCellCollection cells = dataGrid.Items[0].Cells;
                for (int i = 0; i < cells.Count; i++)
                {
                    using (IEnumerator enumerator = cells[i].Controls.GetEnumerator())
                    {
                        while (enumerator.MoveNext())
                        {
                            Control current = (Control) enumerator.Current;
                            if (!((((current is Label) || (current is LiteralControl)) || (current is DataBoundLiteralControl)) || (current is HyperLink)))
                            {
                                goto Label_01D1;
                            }
                            HyperLink link = current as HyperLink;
                            if ((link != null) && ((link.Text == "查看") || (link.Text == "编辑")))
                            {
                                dataGrid.Columns[i].Visible = false;
                            }
                        }
                        goto Label_01FE;
                    Label_01D1:
                        dataGrid.Columns[i].Visible = false;
                    }
                Label_01FE:;
                }
            }
            writer2.WriteLine(RenderDataGrid(dataGrid));
            response.Write(writer.ToString());
            response.End();
        }

        public static void ExportExcel(string htmlString, string fileName)
        {
            if (fileName.IndexOf(".xls", StringComparison.OrdinalIgnoreCase) < 0)
            {
                fileName = fileName + ".xls";
            }
            HttpResponse response = HttpContext.Current.Response;
            response.Charset = "GB2312";
            response.ContentEncoding = Encoding.GetEncoding("GB2312");
            response.ContentType = "application/ms-excel/msword";
            response.AppendHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode(fileName));
            StringWriter writer = new StringWriter();
            HtmlTextWriter writer2 = new HtmlTextWriter(writer);
            writer2.WriteLine("<meta http-equiv=\"Content-Type\" content=\"text/html;charset=GB2312\">");
            writer2.WriteLine(htmlString);
            response.Write(writer.ToString());
            response.Flush();
            response.End();
        }

        public static void ExportWord(string htmlString, string fileName)
        {
            if (fileName.IndexOf(".doc", StringComparison.OrdinalIgnoreCase) < 0)
            {
                fileName = fileName + ".doc";
            }
            HttpResponse response = HttpContext.Current.Response;
            response.Charset = "GB2312";
            response.ContentEncoding = Encoding.GetEncoding("GB2312");
            response.ContentType = "application/ms-word";
            response.AppendHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode(fileName));
            StringWriter writer = new StringWriter();
            HtmlTextWriter writer2 = new HtmlTextWriter(writer);
            writer2.WriteLine("<meta http-equiv=\"Content-Type\" content=\"text/html;charset=GB2312\">");
            writer2.WriteLine(htmlString);
            response.Write(writer.ToString());
            response.Flush();
            response.End();
        }

        public static string RenderControl(Control control)
        {
            StringWriter writer = new StringWriter();
            HtmlTextWriter writer2 = new HtmlTextWriter(writer);
            control.RenderControl(writer2);
            return writer.ToString();
        }

        public static string RenderDataGrid(DataGrid dataGrid)
        {
            string format = "<td>{0}</td>";
            StringBuilder builder = new StringBuilder();
            string str2 = "";
            int num = 0;
            while (num < dataGrid.Columns.Count)
            {
                if (dataGrid.Columns[num].Visible)
                {
                    str2 = str2 + string.Format(format, dataGrid.Columns[num].HeaderText) + "\r\n";
                }
                num++;
            }
            builder.Append(string.Format("<tr height=40 bgcolor='#C0C0C0'>{0}</tr> \r\n", str2));
            for (int i = 0; i < dataGrid.Items.Count; i++)
            {
                TableCellCollection cells = dataGrid.Items[i].Cells;
                ITextControl control = null;
                string str3 = "";
                for (num = 0; num < cells.Count; num++)
                {
                    if (dataGrid.Columns[num].Visible)
                    {
                        string str4 = "";
                        foreach (Control control2 in cells[num].Controls)
                        {
                            control = control2 as ITextControl;
                            if (control != null)
                            {
                                str4 = str4 + control.Text + "  ";
                            }
                            else
                            {
                                HyperLink link = control2 as HyperLink;
                                if (link != null)
                                {
                                    str4 = str4 + link.Text + "  ";
                                }
                            }
                        }
                        str3 = str3 + string.Format(format, str4) + "\r\n";
                    }
                }
                if (!string.IsNullOrEmpty(str3))
                {
                    builder.Append(string.Format("<tr>{0}</tr> \r\n", str3));
                }
            }
            return string.Format("<Table border=1>{0}</Table> \r\n", builder.ToString());
        }
    }
}

