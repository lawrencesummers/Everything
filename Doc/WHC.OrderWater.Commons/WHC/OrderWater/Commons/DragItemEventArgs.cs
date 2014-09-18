namespace WHC.OrderWater.Commons
{
    using System;
    using System.Windows.Forms;

    public class DragItemEventArgs : EventArgs
    {
        private TreeNode treeNode_0;

        public TreeNode Node
        {
            get
            {
                return this.treeNode_0;
            }
            set
            {
                this.treeNode_0 = value;
            }
        }
    }
}

