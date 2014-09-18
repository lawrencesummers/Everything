namespace WHC.OrderWater.Commons.Web
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Data;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    public class GridViewHelper
    {
        public static string GetDatagridItems(DataGrid dg)
        {
            return GetDatagridItems(dg, false);
        }

        public static string GetDatagridItems(DataGrid dg, bool UseSemicolon)
        {
            string str = string.Empty;
            foreach (DataGridItem item in dg.Items)
            {
                string str2 = dg.DataKeys[item.ItemIndex].ToString();
                if (((CheckBox) item.FindControl("cbxDelete")).Checked)
                {
                    if (UseSemicolon)
                    {
                        str = str + "'" + str2 + "',";
                    }
                    else
                    {
                        str = str + str2 + ",";
                    }
                }
            }
            return str.Trim(new char[] { ',' });
        }

        public static DataTable GridView2DataTable(GridView gv)
        {
            DataTable table = new DataTable();
            int num = 0;
            List<string> list = new List<string>();
            if (gv.ShowHeader || (gv.Columns.Count != 0))
            {
                string str2;
                GridViewRow headerRow = gv.HeaderRow;
                int count = headerRow.Cells.Count;
                int num2 = 0;
                while (num2 < count)
                {
                    str2 = smethod_1(headerRow.Cells[num2]);
                    list.Add(str2);
                    num2++;
                }
                foreach (GridViewRow row in gv.Rows)
                {
                    if (row.RowType == DataControlRowType.DataRow)
                    {
                        DataRow row2 = table.NewRow();
                        int num4 = 0;
                        for (num2 = 0; num2 < count; num2++)
                        {
                            str2 = smethod_1(row.Cells[num2]);
                            if (!string.IsNullOrEmpty(str2))
                            {
                                if (num == 0)
                                {
                                    string str = list[num2];
                                    if (string.IsNullOrEmpty(str) || table.Columns.Contains(str))
                                    {
                                        goto Label_0140;
                                    }
                                    DataColumn column = table.Columns.Add();
                                    column.ColumnName = str;
                                    column.DataType = typeof(string);
                                }
                                row2[num4] = str2;
                                num4++;
                            Label_0140:;
                            }
                        }
                        num++;
                        table.Rows.Add(row2);
                    }
                }
            }
            return table;
        }

        public static void SetDropDownListItem(DropDownList control, string strValue)
        {
            if (!string.IsNullOrEmpty(strValue))
            {
                control.ClearSelection();
                ListItem item = control.Items.FindByValue(strValue);
                if (item != null)
                {
                    control.SelectedValue = item.Value;
                }
            }
        }

        private static string smethod_0(string string_0, int int_0)
        {
            if ((int_0 != 0) && (string_0.Length > int_0))
            {
                return (string_0.Substring(0, int_0) + "..");
            }
            return string_0;
        }

        private static string smethod_1(object object_0)
        {
            string text = object_0.Text;
            if (!string.IsNullOrEmpty(text))
            {
                return text;
            }
            using (IEnumerator enumerator = object_0.Controls.GetEnumerator())
            {
                Control current;
                ITextControl control2;
                IButtonControl control4;
                while (enumerator.MoveNext())
                {
                    current = (Control) enumerator.Current;
                    if ((current != null) && (current is IButtonControl))
                    {
                        goto Label_0072;
                    }
                    if ((current != null) && (current is ITextControl))
                    {
                        LiteralControl control3 = current as LiteralControl;
                        if (control3 == null)
                        {
                            goto Label_0098;
                        }
                    }
                }
                return text;
            Label_0072:
                control4 = current as IButtonControl;
                return control4.Text.Replace("\r\n", "").Trim();
            Label_0098:
                control2 = current as ITextControl;
                return control2.Text.Replace("\r\n", "").Trim();
            }
        }

        private static void smethod_2(object object_0, int int_0)
        {
            string text = object_0.Text;
            if (!string.IsNullOrEmpty(text))
            {
                object_0.Text = smethod_0(text, int_0);
            }
            using (IEnumerator enumerator = object_0.Controls.GetEnumerator())
            {
                Control current;
                IButtonControl control2;
                ITextControl control3;
                while (enumerator.MoveNext())
                {
                    current = (Control) enumerator.Current;
                    if ((current != null) && (current is IButtonControl))
                    {
                        goto Label_007B;
                    }
                    if ((current != null) && (current is ITextControl))
                    {
                        LiteralControl control4 = current as LiteralControl;
                        if (control4 == null)
                        {
                            goto Label_00AC;
                        }
                    }
                }
                return;
            Label_007B:
                control2 = current as IButtonControl;
                text = control2.Text.Replace("\r\n", "").Trim();
                control2.Text = smethod_0(text, int_0);
                return;
            Label_00AC:
                control3 = current as ITextControl;
                text = control3.Text.Replace("\r\n", "").Trim();
                if (control3 is DataBoundLiteralControl)
                {
                    object_0.Text = smethod_0(text, int_0);
                }
                else
                {
                    control3.Text = smethod_0(text, int_0);
                }
            }
        }
    }
}

