namespace WHC.OrderWater.Commons
{
    using System;
    using System.Windows.Forms;

    public class DragCompleteEventArgs : EventArgs
    {
        private TreeNode treeNode_0;
        private TreeNode treeNode_1;

        public TreeNode SourceNode
        {
            get
            {
                return this.treeNode_1;
            }
            set
            {
                this.treeNode_1 = value;
            }
        }

        public TreeNode TargetNode
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

