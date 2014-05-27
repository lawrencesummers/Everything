/*
 * Created by SharpDevelop.
 * User: guanxiang
 * Date: 2013/10/30
 * Time: 20:53
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
    using System;
    using System.Windows.Forms;
    using System.Collections.Generic;

namespace LawrenceUtils
{
	/// <summary>
	/// Description of CheckBoxListUtil.
	/// </summary>
	public class CheckBoxListUtil
	{
		public CheckBoxListUtil()
		{
		}
		
		 public static string GetCheckedItems(CheckedListBox cblItems)
        {
            string str = "";
            for (int i = 0; i < cblItems.Items.Count; i++)
            {
                if (cblItems.GetItemChecked(i))
                {
                    str = str + string.Format("{0},", cblItems.GetItemText(cblItems.Items[i]));
                }
            }
            return str.Trim(new char[] { ',' });
        }

        public static void DealWithCheckListUpdate(CheckedListBox chkList, string stringInput, char split)
        {
            if (string.IsNullOrEmpty(stringInput))
            {
                return;
            }


            System.Collections.Generic.List<string> sumItems = new List<string>();
            foreach (var item in chkList.Items)
            {
                sumItems.Add(item.ToString());
            }

            string[] myItemsArray = stringInput.Split(split);
            List<string> myItems = new List<string>();
            for (int i = 0; i < myItemsArray.Length; i++)
            {
                myItems.Add(myItemsArray[i]);
            }
            
            for (int i = 0; i < sumItems.Count; i++)
            {
                if (myItems.Contains(sumItems[i]))
                {
                    chkList.SetItemChecked(i, true);
                }

            }
        }

        public static string DealWithCheckListSave(CheckedListBox chkList)
        {
            string strCollected = string.Empty;

            for (int i = 0; i < chkList.Items.Count; i++)
            {

                if (chkList.GetItemChecked(i))
                {

                    if (strCollected == string.Empty)
                    {

                        strCollected = chkList.GetItemText(

         chkList.Items[i]);

                    }

                    else
                    {

                        strCollected = strCollected + "+" + chkList.

        GetItemText(chkList.Items[i]);

                    }

                }

            }
            return strCollected;
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

        public static void SetAllUnchecked(CheckedListBox cblItems)
        {
            for (int i = 0; i < cblItems.Items.Count; i++)
            {
                    cblItems.SetItemChecked(i, false);
            }
        }
	}
}
