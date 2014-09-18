namespace RDIFramework.Utilities
{
    using System;
    using System.Windows.Forms;

    public abstract class DialogHelper
    {
        public static string DATA_ISNULL = "数据为空，请检查你的操作数据是否正确？";

        protected DialogHelper()
        {
        }

        public static DialogResult ShowDlgReturnResult(string strMsg)
        {
            if (MessageBox.Show(strMsg, "询问信息", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                return DialogResult.Yes;
            }
            return DialogResult.No;
        }

        public static void ShowErrorMsg(string strMsg)
        {
            MessageBox.Show(strMsg, "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Hand);
        }

        public static void ShowQuestionMsg(string strMsg)
        {
            MessageBox.Show(strMsg, "询问提示", MessageBoxButtons.OK, MessageBoxIcon.Question);
        }

        public static void ShowSuccessMsg(string strMsg)
        {
            MessageBox.Show(strMsg, "成功提示", MessageBoxButtons.OK, MessageBoxIcon.None);
        }

        public static void ShowWarningMsg(string strMsg)
        {
            MessageBox.Show(strMsg, "警告提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }
    }
}

