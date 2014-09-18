namespace WHC.OrderWater.Commons
{
    using System;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Drawing;
    using System.Drawing.Design;
    using System.Runtime.InteropServices;
    using System.Threading;
    using System.Windows.Forms;

    [ToolboxBitmap(typeof(TreeViewDragDrop)), Description("内置拖拉的TreeView控件，可定义ICON/Cursor")]
    public class TreeViewDragDrop : TreeView
    {
        private Color color_0 = SystemColors.HighlightText;
        private Color color_1 = SystemColors.Highlight;
        private Cursor cursor_0 = null;
        private DragCompleteEventHandler dragCompleteEventHandler_0;
        private WHC.OrderWater.Commons.DragCursorType dragCursorType_0;
        private DragDropEffects dragDropEffects_0 = DragDropEffects.Move;
        private DragItemEventHandler dragItemEventHandler_0;
        private DragItemEventHandler dragItemEventHandler_1;
        private Form0 form0_0 = new Form0();
        private int int_0;
        private TreeNode treeNode_0;
        private TreeNode treeNode_1;

        [Description("Occurs when an item is dragged, and the drag is cancelled.")]
        public event DragItemEventHandler DragCancel
        {
            add
            {
                DragItemEventHandler handler2;
                DragItemEventHandler handler = this.dragItemEventHandler_1;
                do
                {
                    handler2 = handler;
                    DragItemEventHandler handler3 = (DragItemEventHandler) Delegate.Combine(handler2, value);
                    handler = Interlocked.CompareExchange<DragItemEventHandler>(ref this.dragItemEventHandler_1, handler3, handler2);
                }
                while (handler != handler2);
            }
            remove
            {
                DragItemEventHandler handler2;
                DragItemEventHandler handler = this.dragItemEventHandler_1;
                do
                {
                    handler2 = handler;
                    DragItemEventHandler handler3 = (DragItemEventHandler) Delegate.Remove(handler2, value);
                    handler = Interlocked.CompareExchange<DragItemEventHandler>(ref this.dragItemEventHandler_1, handler3, handler2);
                }
                while (handler != handler2);
            }
        }

        [Description("Occurs when an item is dragged and dropped onto another.")]
        public event DragCompleteEventHandler DragComplete
        {
            add
            {
                DragCompleteEventHandler handler2;
                DragCompleteEventHandler handler = this.dragCompleteEventHandler_0;
                do
                {
                    handler2 = handler;
                    DragCompleteEventHandler handler3 = (DragCompleteEventHandler) Delegate.Combine(handler2, value);
                    handler = Interlocked.CompareExchange<DragCompleteEventHandler>(ref this.dragCompleteEventHandler_0, handler3, handler2);
                }
                while (handler != handler2);
            }
            remove
            {
                DragCompleteEventHandler handler2;
                DragCompleteEventHandler handler = this.dragCompleteEventHandler_0;
                do
                {
                    handler2 = handler;
                    DragCompleteEventHandler handler3 = (DragCompleteEventHandler) Delegate.Remove(handler2, value);
                    handler = Interlocked.CompareExchange<DragCompleteEventHandler>(ref this.dragCompleteEventHandler_0, handler3, handler2);
                }
                while (handler != handler2);
            }
        }

        [Description("Occurs when an item is starting to be dragged. This event can be used to cancel dragging of particular items.")]
        public event DragItemEventHandler DragStart
        {
            add
            {
                DragItemEventHandler handler2;
                DragItemEventHandler handler = this.dragItemEventHandler_0;
                do
                {
                    handler2 = handler;
                    DragItemEventHandler handler3 = (DragItemEventHandler) Delegate.Combine(handler2, value);
                    handler = Interlocked.CompareExchange<DragItemEventHandler>(ref this.dragItemEventHandler_0, handler3, handler2);
                }
                while (handler != handler2);
            }
            remove
            {
                DragItemEventHandler handler2;
                DragItemEventHandler handler = this.dragItemEventHandler_0;
                do
                {
                    handler2 = handler;
                    DragItemEventHandler handler3 = (DragItemEventHandler) Delegate.Remove(handler2, value);
                    handler = Interlocked.CompareExchange<DragItemEventHandler>(ref this.dragItemEventHandler_0, handler3, handler2);
                }
                while (handler != handler2);
            }
        }

        public TreeViewDragDrop()
        {
            base.SetStyle(ControlStyles.DoubleBuffer, true);
            this.AllowDrop = true;
            this.form0_0.labelText.Font = this.Font;
            this.form0_0.BackColor = this.BackColor;
            if ((this.dragCursorType_0 == WHC.OrderWater.Commons.DragCursorType.Custom) && (this.cursor_0 != null))
            {
                this.DragCursor = this.cursor_0;
            }
            this.form0_0.Show();
            this.form0_0.Visible = false;
        }

        protected override void OnDragDrop(DragEventArgs e)
        {
            if (this.dragCursorType_0 == WHC.OrderWater.Commons.DragCursorType.DragIcon)
            {
                this.Cursor = Cursors.Default;
            }
            this.form0_0.Visible = false;
            if (e.Data.GetDataPresent("System.Windows.Forms.TreeNode", false))
            {
                TreeNode data = (TreeNode) e.Data.GetData("System.Windows.Forms.TreeNode");
                Point pt = base.PointToClient(new Point(e.X, e.Y));
                TreeNode nodeAt = base.GetNodeAt(pt);
                if (nodeAt != null)
                {
                    nodeAt.BackColor = SystemColors.HighlightText;
                    nodeAt.ForeColor = SystemColors.ControlText;
                    if (((nodeAt != data) && !nodeAt.FullPath.StartsWith(data.FullPath)) && (data.Parent != nodeAt))
                    {
                        data.Remove();
                        nodeAt.Nodes.Insert(0, data);
                        nodeAt.Expand();
                        base.SelectedNode = data;
                        this.Cursor = Cursors.Default;
                        if (this.dragCompleteEventHandler_0 != null)
                        {
                            DragCompleteEventArgs args = new DragCompleteEventArgs {
                                SourceNode = data,
                                TargetNode = nodeAt
                            };
                            this.dragCompleteEventHandler_0(this, args);
                        }
                    }
                }
            }
        }

        protected override void OnDragEnter(DragEventArgs e)
        {
            e.Effect = this.dragDropEffects_0;
            this.treeNode_0 = null;
            this.treeNode_1 = null;
            Debug.WriteLine(this.form0_0.labelText.Size);
        }

        protected override void OnDragLeave(EventArgs e)
        {
            if (this.treeNode_1 != null)
            {
                base.SelectedNode = this.treeNode_1;
            }
            if (this.treeNode_0 != null)
            {
                this.treeNode_0.BackColor = this.color_1;
                this.treeNode_0.ForeColor = this.color_0;
            }
            this.form0_0.Visible = false;
            this.Cursor = Cursors.Default;
            if (this.dragItemEventHandler_1 != null)
            {
                DragItemEventArgs args = new DragItemEventArgs {
                    Node = this.treeNode_1
                };
                this.dragItemEventHandler_1(this, args);
            }
        }

        protected override void OnDragOver(DragEventArgs e)
        {
            if (this.treeNode_0 != null)
            {
                this.treeNode_0.BackColor = SystemColors.HighlightText;
                this.treeNode_0.ForeColor = SystemColors.ControlText;
            }
            Point pt = base.PointToClient(new Point(e.X, e.Y));
            TreeNode nodeAt = base.GetNodeAt(pt);
            if (nodeAt != null)
            {
                nodeAt.BackColor = this.color_1;
                nodeAt.ForeColor = this.color_0;
                if (this.dragCursorType_0 == WHC.OrderWater.Commons.DragCursorType.DragIcon)
                {
                    this.form0_0.Location = new Point(e.X + 5, e.Y - 5);
                    this.form0_0.Visible = true;
                }
                if ((pt.Y + 10) > base.ClientSize.Height)
                {
                    SendMessage(base.Handle, 0x115, (IntPtr) 1, 0);
                }
                else if (pt.Y < (base.Top + 10))
                {
                    SendMessage(base.Handle, 0x115, IntPtr.Zero, 0);
                }
                this.treeNode_0 = nodeAt;
            }
        }

        protected override void OnGiveFeedback(GiveFeedbackEventArgs e)
        {
            if (e.Effect == this.dragDropEffects_0)
            {
                e.UseDefaultCursors = false;
                if ((this.dragCursorType_0 == WHC.OrderWater.Commons.DragCursorType.Custom) && (this.cursor_0 != null))
                {
                    this.Cursor = this.cursor_0;
                }
                else if (this.dragCursorType_0 == WHC.OrderWater.Commons.DragCursorType.DragIcon)
                {
                    this.Cursor = Cursors.Default;
                }
                else
                {
                    e.UseDefaultCursors = true;
                }
            }
            else
            {
                e.UseDefaultCursors = true;
                this.Cursor = Cursors.Default;
            }
        }

        protected override void OnItemDrag(ItemDragEventArgs e)
        {
            this.treeNode_1 = (TreeNode) e.Item;
            if (this.dragItemEventHandler_0 != null)
            {
                DragItemEventArgs args = new DragItemEventArgs {
                    Node = this.treeNode_1
                };
                this.dragItemEventHandler_0(this, args);
            }
            if (this.treeNode_0 != null)
            {
                this.treeNode_0.BackColor = SystemColors.HighlightText;
                this.treeNode_0.ForeColor = SystemColors.ControlText;
            }
            int width = (int) ((this.treeNode_1.Text.Length * ((int) this.form0_0.labelText.Font.Size)) * 1.5f);
            if (this.treeNode_1.Text.Length < 5)
            {
                width += 20;
            }
            this.form0_0.Size = new Size(width, this.form0_0.Height);
            this.form0_0.labelText.Size = new Size(width, this.form0_0.labelText.Size.Height);
            this.form0_0.labelText.Text = this.treeNode_1.Text;
            base.DoDragDrop(e.Item, this.dragDropEffects_0);
        }

        protected override void OnKeyUp(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                if (this.treeNode_1 != null)
                {
                    base.SelectedNode = this.treeNode_1;
                }
                if (this.treeNode_0 != null)
                {
                    this.treeNode_0.BackColor = SystemColors.HighlightText;
                    this.treeNode_0.ForeColor = SystemColors.ControlText;
                }
                this.Cursor = Cursors.Default;
                this.form0_0.Visible = false;
                if (this.dragItemEventHandler_1 != null)
                {
                    DragItemEventArgs args = new DragItemEventArgs {
                        Node = this.treeNode_1
                    };
                    this.dragItemEventHandler_1(this, args);
                }
            }
        }

        [DllImport("User32.dll")]
        private static extern int SendMessage(IntPtr intptr_0, int int_1, IntPtr intptr_1, int int_2);
        protected override void WndProc(ref Message m)
        {
            if (m.Msg == 20)
            {
                m.Msg = 0;
            }
            base.WndProc(ref m);
        }

        [Description("The custom cursor to use when dragging an item, if DragCursor is set to Custom."), Category("Drag and drop")]
        public Cursor DragCursor
        {
            get
            {
                return this.cursor_0;
            }
            set
            {
                if (!(value == this.cursor_0))
                {
                    this.cursor_0 = value;
                    if (!base.IsHandleCreated)
                    {
                    }
                }
            }
        }

        [Category("Drag and drop"), Description("The cursor type to use when dragging - None uses the default drag and drop cursor, DragIcon uses an icon and label, Custom uses a custom cursor.")]
        public WHC.OrderWater.Commons.DragCursorType DragCursorType
        {
            get
            {
                return this.dragCursorType_0;
            }
            set
            {
                this.dragCursorType_0 = value;
            }
        }

        [TypeConverter(typeof(ImageIndexConverter)), Description("The default image index for the DragImage icon."), Editor("System.Windows.Forms.Design.ImageIndexEditor", typeof(UITypeEditor)), Category("Drag and drop")]
        public int DragImageIndex
        {
            get
            {
                if (this.form0_0.imageList_0 == null)
                {
                    return -1;
                }
                if (this.int_0 >= this.form0_0.imageList_0.Images.Count)
                {
                    return Math.Max(0, this.form0_0.imageList_0.Images.Count - 1);
                }
                return this.int_0;
            }
            set
            {
                if ((this.form0_0.imageList_0.Images.Count > 0) && (this.form0_0.imageList_0.Images[value] != null))
                {
                    this.form0_0.pictureBox1.Image = this.form0_0.imageList_0.Images[value];
                    this.form0_0.Size = new Size(this.form0_0.Width, this.form0_0.pictureBox1.Image.Height);
                    this.form0_0.labelText.Size = new Size(this.form0_0.labelText.Width, this.form0_0.pictureBox1.Image.Height);
                }
                this.int_0 = value;
            }
        }

        [Category("Drag and drop"), Description("The imagelist control from which DragImage icons are taken.")]
        public ImageList DragImageList
        {
            get
            {
                return this.form0_0.imageList_0;
            }
            set
            {
                if (value != this.form0_0.imageList_0)
                {
                    this.form0_0.imageList_0 = value;
                    if ((this.form0_0.imageList_0.Images.Count > 0) && (this.form0_0.imageList_0.Images[this.int_0] != null))
                    {
                        this.form0_0.pictureBox1.Image = this.form0_0.imageList_0.Images[this.int_0];
                        this.form0_0.Height = this.form0_0.pictureBox1.Image.Height;
                    }
                    if (base.IsHandleCreated)
                    {
                        SendMessage((IntPtr) 0x1109, 0, (value == null) ? IntPtr.Zero : value.Handle, 0);
                    }
                }
            }
        }

        [Description("The drag mode (move,copy etc.)"), Category("Drag and drop")]
        public DragDropEffects DragMode
        {
            get
            {
                return this.dragDropEffects_0;
            }
            set
            {
                this.dragDropEffects_0 = value;
            }
        }

        [Description("Sets the font for the dragged node (shown as ghosted text/icon)."), Category("Drag and drop")]
        public Font DragNodeFont
        {
            get
            {
                return this.form0_0.labelText.Font;
            }
            set
            {
                this.form0_0.labelText.Font = value;
                this.form0_0.Size = new Size(this.form0_0.Width, (int) this.form0_0.labelText.Font.GetHeight());
                this.form0_0.labelText.Size = new Size(this.form0_0.labelText.Width, (int) this.form0_0.labelText.Font.GetHeight());
            }
        }

        [Category("Drag and drop"), Description("Sets the opacity for the dragged node (shown as ghosted text/icon)."), TypeConverter(typeof(OpacityConverter))]
        public double DragNodeOpacity
        {
            get
            {
                return this.form0_0.Opacity;
            }
            set
            {
                this.form0_0.Opacity = value;
            }
        }

        [Description("The background colour of the node being dragged over."), Category("Drag and drop")]
        public Color DragOverNodeBackColor
        {
            get
            {
                return this.color_1;
            }
            set
            {
                this.color_1 = value;
            }
        }

        [Description("The foreground colour of the node being dragged over."), Category("Drag and drop")]
        public Color DragOverNodeForeColor
        {
            get
            {
                return this.color_0;
            }
            set
            {
                this.color_0 = value;
            }
        }

        internal class Form0 : Form
        {
            private Container container_0 = null;
            public ImageList imageList_0;
            public Label labelText;
            public PictureBox pictureBox1;

            public Form0()
            {
                this.method_0();
            }

            protected override void Dispose(bool disposing)
            {
                if (disposing && (this.container_0 != null))
                {
                    this.container_0.Dispose();
                }
                base.Dispose(disposing);
            }

            private void method_0()
            {
                this.container_0 = new Container();
                this.labelText = new Label();
                this.pictureBox1 = new PictureBox();
                this.imageList_0 = new ImageList(this.container_0);
                base.SuspendLayout();
                this.labelText.BackColor = Color.Transparent;
                this.labelText.Location = new Point(0x10, 2);
                this.labelText.Name = "labelText";
                this.labelText.Size = new Size(100, 0x16);
                this.labelText.TabIndex = 0;
                this.pictureBox1.Location = new Point(0, 0);
                this.pictureBox1.Name = "pictureBox1";
                this.pictureBox1.Size = new Size(0x10, 0x10);
                this.pictureBox1.TabIndex = 1;
                this.pictureBox1.TabStop = false;
                this.AutoScaleBaseSize = new Size(5, 13);
                this.BackColor = SystemColors.Control;
                base.ClientSize = new Size(100, 0x16);
                base.Controls.Add(this.pictureBox1);
                base.Controls.Add(this.labelText);
                base.Size = new Size(300, 500);
                base.FormBorderStyle = FormBorderStyle.None;
                base.Opacity = 0.3;
                base.ShowInTaskbar = false;
                base.ResumeLayout(false);
            }
        }
    }
}

