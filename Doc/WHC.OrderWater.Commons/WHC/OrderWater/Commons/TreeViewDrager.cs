namespace WHC.OrderWater.Commons
{
    using System;
    using System.Drawing;
    using System.Threading;
    using System.Windows.Forms;

    public class TreeViewDrager
    {
        private Image image_0;
        private ImageList imageList_0;
        private ImageList imageList_1;
        private ProcessDragNodeEventHandler processDragNodeEventHandler_0;
        private Timer timer_0;
        private TreeNode treeNode_0;
        private TreeNode treeNode_1;
        private TreeView treeView_0;

        public event ProcessDragNodeEventHandler ProcessDragNode
        {
            add
            {
                ProcessDragNodeEventHandler handler2;
                ProcessDragNodeEventHandler handler = this.processDragNodeEventHandler_0;
                do
                {
                    handler2 = handler;
                    ProcessDragNodeEventHandler handler3 = (ProcessDragNodeEventHandler) Delegate.Combine(handler2, value);
                    handler = Interlocked.CompareExchange<ProcessDragNodeEventHandler>(ref this.processDragNodeEventHandler_0, handler3, handler2);
                }
                while (handler != handler2);
            }
            remove
            {
                ProcessDragNodeEventHandler handler2;
                ProcessDragNodeEventHandler handler = this.processDragNodeEventHandler_0;
                do
                {
                    handler2 = handler;
                    ProcessDragNodeEventHandler handler3 = (ProcessDragNodeEventHandler) Delegate.Remove(handler2, value);
                    handler = Interlocked.CompareExchange<ProcessDragNodeEventHandler>(ref this.processDragNodeEventHandler_0, handler3, handler2);
                }
                while (handler != handler2);
            }
        }

        public TreeViewDrager()
        {
            this.timer_0 = new Timer();
            this.treeNode_0 = null;
            this.treeNode_1 = null;
            this.imageList_0 = new ImageList();
        }

        public TreeViewDrager(TreeView treeView)
        {
            this.timer_0 = new Timer();
            this.treeNode_0 = null;
            this.treeNode_1 = null;
            this.imageList_0 = new ImageList();
            this.treeView_0 = treeView;
            this.method_0();
        }

        private void method_0()
        {
            this.timer_0.Interval = 200;
            this.timer_0.Tick += new EventHandler(this.timer_0_Tick);
            this.treeView_0.AllowDrop = true;
            this.treeView_0.DragDrop += new DragEventHandler(this.treeView_0_DragDrop);
            this.treeView_0.DragOver += new DragEventHandler(this.treeView_0_DragOver);
            this.treeView_0.DragLeave += new EventHandler(this.treeView_0_DragLeave);
            this.treeView_0.GiveFeedback += new GiveFeedbackEventHandler(this.treeView_0_GiveFeedback);
            this.treeView_0.DragEnter += new DragEventHandler(this.treeView_0_DragEnter);
            this.treeView_0.ItemDrag += new ItemDragEventHandler(this.treeView_0_ItemDrag);
        }

        private void timer_0_Tick(object sender, EventArgs e)
        {
            Point pt = this.treeView_0.PointToClient(Control.MousePosition);
            TreeNode nodeAt = this.treeView_0.GetNodeAt(pt);
            if (nodeAt != null)
            {
                if (pt.Y < 30)
                {
                    if (nodeAt.PrevVisibleNode != null)
                    {
                        nodeAt = nodeAt.PrevVisibleNode;
                        Class27.ImageList_DragShowNolock(false);
                        nodeAt.EnsureVisible();
                        this.treeView_0.Refresh();
                        Class27.ImageList_DragShowNolock(true);
                    }
                }
                else if ((pt.Y > (this.treeView_0.Size.Height - 30)) && (nodeAt.NextVisibleNode != null))
                {
                    nodeAt = nodeAt.NextVisibleNode;
                    Class27.ImageList_DragShowNolock(false);
                    nodeAt.EnsureVisible();
                    this.treeView_0.Refresh();
                    Class27.ImageList_DragShowNolock(true);
                }
            }
        }

        private void treeView_0_DragDrop(object sender, DragEventArgs e)
        {
            Class27.ImageList_DragLeave(this.treeView_0.Handle);
            TreeNode nodeAt = this.treeView_0.GetNodeAt(this.treeView_0.PointToClient(new Point(e.X, e.Y)));
            if (this.treeNode_0 != nodeAt)
            {
                if ((this.processDragNodeEventHandler_0 != null) && this.processDragNodeEventHandler_0(this.treeNode_0, nodeAt))
                {
                    if (this.treeNode_0.Parent == null)
                    {
                        this.treeView_0.Nodes.Remove(this.treeNode_0);
                    }
                    else
                    {
                        this.treeNode_0.Parent.Nodes.Remove(this.treeNode_0);
                    }
                    nodeAt.Nodes.Add(this.treeNode_0);
                    nodeAt.ExpandAll();
                    this.treeNode_0 = null;
                    this.timer_0.Enabled = false;
                }
                else
                {
                    MessageBox.Show("持久化失败，不能移动节点！");
                }
            }
        }

        private void treeView_0_DragEnter(object sender, DragEventArgs e)
        {
            Class27.ImageList_DragEnter(this.treeView_0.Handle, e.X - this.treeView_0.Left, e.Y - this.treeView_0.Top);
            this.timer_0.Enabled = true;
        }

        private void treeView_0_DragLeave(object sender, EventArgs e)
        {
            Class27.ImageList_DragLeave(this.treeView_0.Handle);
            this.timer_0.Enabled = false;
        }

        private void treeView_0_DragOver(object sender, DragEventArgs e)
        {
            Point point = this.treeView_0.PointToClient(new Point(e.X, e.Y));
            Class27.ImageList_DragMove(point.X - this.treeView_0.Left, point.Y - this.treeView_0.Top);
            TreeNode nodeAt = this.treeView_0.GetNodeAt(this.treeView_0.PointToClient(new Point(e.X, e.Y)));
            if (nodeAt == null)
            {
                e.Effect = DragDropEffects.None;
            }
            else
            {
                e.Effect = DragDropEffects.Move;
                if (this.treeNode_1 != nodeAt)
                {
                    Class27.ImageList_DragShowNolock(false);
                    this.treeView_0.SelectedNode = nodeAt;
                    Class27.ImageList_DragShowNolock(true);
                    this.treeNode_1 = nodeAt;
                }
                for (TreeNode node2 = nodeAt; node2.Parent != null; node2 = node2.Parent)
                {
                    if (node2.Parent == this.treeNode_0)
                    {
                        e.Effect = DragDropEffects.None;
                    }
                }
            }
        }

        private void treeView_0_GiveFeedback(object sender, GiveFeedbackEventArgs e)
        {
            if (e.Effect == DragDropEffects.Move)
            {
                e.UseDefaultCursors = false;
                this.treeView_0.Cursor = Cursors.Default;
            }
            else
            {
                e.UseDefaultCursors = true;
            }
        }

        private void treeView_0_ItemDrag(object sender, ItemDragEventArgs e)
        {
            this.treeNode_0 = (TreeNode) e.Item;
            this.treeView_0.SelectedNode = this.treeNode_0;
            this.imageList_0.Images.Clear();
            this.imageList_0.ImageSize = new Size(this.treeNode_0.Bounds.Size.Width + this.treeView_0.Indent, this.treeNode_0.Bounds.Height);
            Bitmap image = new Bitmap(this.treeNode_0.Bounds.Width + this.treeView_0.Indent, this.treeNode_0.Bounds.Height);
            Graphics graphics = Graphics.FromImage(image);
            if (this.imageList_1 != null)
            {
                graphics.DrawImage(this.imageList_1.Images[0], 0, 0);
            }
            graphics.DrawString(this.treeNode_0.Text, this.treeView_0.Font, new SolidBrush(this.treeView_0.ForeColor), (float) this.treeView_0.Indent, 1f);
            this.imageList_0.Images.Add(image);
            Point point = this.treeView_0.PointToClient(Control.MousePosition);
            int num = (point.X + this.treeView_0.Indent) - this.treeNode_0.Bounds.Left;
            int num2 = point.Y - this.treeNode_0.Bounds.Top;
            if (Class27.ImageList_BeginDrag(this.imageList_0.Handle, 0, num, num2))
            {
                this.treeView_0.DoDragDrop(image, DragDropEffects.Move);
                Class27.ImageList_EndDrag();
            }
        }

        public ImageList TreeImageList
        {
            get
            {
                return this.imageList_1;
            }
            set
            {
                this.imageList_1 = value;
            }
        }
    }
}

