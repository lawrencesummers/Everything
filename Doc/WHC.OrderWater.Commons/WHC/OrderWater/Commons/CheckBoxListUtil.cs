namespace WHC.OrderWater.Commons
{
    using System;
    using System.Windows.Forms;

    public class CheckBoxListUtil
    {
        public static string GetCheckedItems(CheckedListBox cblItems)
        {
            string str = "";
            for (int i = 0; i < cblItems.CheckedItems.Count; i++)
            {
                if (cblItems.GetItemChecked(i))
                {
                    str = str + string.Format("{0},", cblItems.GetItemText(cblItems.Items[i]));
                }
            }
            return str.Trim(new char[] { ',' });
        }

        public static string GetCheckedItems(GroupBox group)
        {
            string str = "";
            foreach (Control control in group.Controls)
            {
                CheckBox box = control as CheckBox;
                if ((box != null) && box.Checked)
                {
                    str = str + string.Format("{0},", box.Text);
                }
            }
            return str.Trim(new char[] { ',' });
        }

        public static void SetCheck(CheckedListBox cblItems, string valueList)
        {
            foreach (string str in valueList.Split(new char[] { ',' }))
            {
                for (int i = 0; i < cblItems.Items.Count; i++)
                {
                    if (cblItems.GetItemText(cblItems.Items[i]) == str)
                    {
                        cblItems.SetItemChecked(i, true);
                    }
                }
            }
        }

        public static void SetCheck(GroupBox group, string valueList)
        {
            foreach (string str in valueList.Split(new char[] { ',' }))
            {
                foreach (Control control in group.Controls)
                {
                    CheckBox box = control as CheckBox;
                    if ((box != null) && (box.Text == str))
                    {
                        box.Checked = true;
                    }
                }
            }
        }
    }
}

