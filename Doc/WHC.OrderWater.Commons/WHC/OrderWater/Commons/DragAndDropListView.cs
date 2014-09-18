namespace WHC.OrderWater.Commons
{
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Drawing;
    using System.Runtime.CompilerServices;
    using System.Threading;
    using System.Windows.Forms;

    public class DragAndDropListView : ListView
    {
        private bool bool_0 = true;
        private Color color_0 = Color.Red;
        private ListViewItem listViewItem_0;
        private ProcessDragItemEventHandler processDragItemEventHandler_0;

        public event ProcessDragItemEventHandler ProcessDragItem
        {
            add
            {
                ProcessDragItemEventHandler handler2;
                ProcessDragItemEventHandler handler = this.processDragItemEventHandler_0;
                do
                {
                    handler2 = handler;
                    ProcessDragItemEventHandler handler3 = (ProcessDragItemEventHandler) Delegate.Combine(handler2, value);
                    handler = Interlocked.CompareExchange<ProcessDragItemEventHandler>(ref this.processDragItemEventHandler_0, handler3, handler2);
                }
                while (handler != handler2);
            }
            remove
            {
                ProcessDragItemEventHandler handler2;
                ProcessDragItemEventHandler handler = this.processDragItemEventHandler_0;
                do
                {
                    handler2 = handler;
                    ProcessDragItemEventHandler handler3 = (ProcessDragItemEventHandler) Delegate.Remove(handler2, value);
                    handler = Interlocked.CompareExchange<ProcessDragItemEventHandler>(ref this.processDragItemEventHandler_0, handler3, handler2);
                }
                while (handler != handler2);
            }
        }

        private DragItemData method_0()
        {
            DragItemData data = new DragItemData(this);
            foreach (ListViewItem item in base.SelectedItems)
            {
                data.DragItems.Add(item.Clone());
            }
            return data;
        }

        private void method_1()
        {
            if (this.listViewItem_0 != null)
            {
                this.listViewItem_0 = null;
            }
        }

        private void method_2()
        {
            base.SuspendLayout();
            base.ResumeLayout(false);
        }

        protected override void OnDragDrop(DragEventArgs drgevent)
        {
            if (!this.bool_0)
            {
                base.OnDragDrop(drgevent);
            }
            else
            {
                Point point = base.PointToClient(new Point(drgevent.X, drgevent.Y));
                ListViewItem itemAt = base.GetItemAt(point.X, point.Y);
                if ((drgevent.Data.GetDataPresent(typeof(DragItemData).ToString()) && (((DragItemData) drgevent.Data.GetData(typeof(DragItemData).ToString())).ListView != null)) && (((DragItemData) drgevent.Data.GetData(typeof(DragItemData).ToString())).DragItems.Count != 0))
                {
                    DragItemData dragItemData = (DragItemData) drgevent.Data.GetData(typeof(DragItemData).ToString());
                    if ((this.processDragItemEventHandler_0 == null) || this.processDragItemEventHandler_0(dragItemData, itemAt))
                    {
                        int num;
                        ListViewItem item;
                        if (itemAt == null)
                        {
                            for (num = 0; num < dragItemData.DragItems.Count; num++)
                            {
                                item = (ListViewItem) dragItemData.DragItems[num];
                                base.Items.Add(item);
                            }
                        }
                        else
                        {
                            int index = itemAt.Index;
                            if ((this == dragItemData.ListView) && (index > base.SelectedItems[0].Index))
                            {
                                index++;
                            }
                            for (num = dragItemData.DragItems.Count - 1; num >= 0; num--)
                            {
                                item = (ListViewItem) dragItemData.DragItems[num];
                                base.Items.Insert(index, item);
                                int num3 = ((index + 1) > (base.Items.Count - 1)) ? (base.Items.Count - 1) : (index + 1);
                                base.Items[index].Position = base.Items[num3].Position;
                            }
                        }
                        if (dragItemData.ListView != null)
                        {
                            foreach (ListViewItem item3 in dragItemData.ListView.SelectedItems)
                            {
                                dragItemData.ListView.Items.Remove(item3);
                            }
                        }
                        this.Refresh();
                        if (this.listViewItem_0 != null)
                        {
                            this.listViewItem_0 = null;
                        }
                        base.Invalidate();
                        base.OnDragDrop(drgevent);
                    }
                }
            }
        }

        protected override void OnDragEnter(DragEventArgs drgevent)
        {
            if (!this.bool_0)
            {
                base.OnDragEnter(drgevent);
            }
            else if (!drgevent.Data.GetDataPresent(typeof(DragItemData).ToString()))
            {
                drgevent.Effect = DragDropEffects.None;
            }
            else
            {
                drgevent.Effect = DragDropEffects.Move;
                base.OnDragEnter(drgevent);
            }
        }

        protected override void OnDragLeave(EventArgs e)
        {
            this.method_1();
            base.Invalidate();
            base.OnDragLeave(e);
        }

        protected override void OnDragOver(DragEventArgs drgevent)
        {
            Point[] pointArray;
            if (!this.bool_0)
            {
                base.OnDragOver(drgevent);
                return;
            }
            if (!drgevent.Data.GetDataPresent(typeof(DragItemData).ToString()))
            {
                drgevent.Effect = DragDropEffects.None;
                return;
            }
            if (base.Items.Count <= 0)
            {
                goto Label_0A5D;
            }
            Point point = base.PointToClient(new Point(drgevent.X, drgevent.Y));
            ListViewItem itemAt = base.GetItemAt(point.X, point.Y);
            Graphics graphics = base.CreateGraphics();
            if (itemAt == null)
            {
                drgevent.Effect = DragDropEffects.Move;
                if (this.listViewItem_0 != null)
                {
                    this.listViewItem_0 = null;
                    base.Invalidate();
                }
                itemAt = base.Items[base.Items.Count - 1];
                if ((base.View == View.Details) || (base.View == View.List))
                {
                    graphics.DrawLine(new Pen(this.color_0, 2f), new Point(itemAt.Bounds.X, itemAt.Bounds.Y + itemAt.Bounds.Height), new Point(itemAt.Bounds.X + base.Bounds.Width, itemAt.Bounds.Y + itemAt.Bounds.Height));
                    pointArray = new Point[] { new Point(itemAt.Bounds.X, (itemAt.Bounds.Y + itemAt.Bounds.Height) - 5), new Point(itemAt.Bounds.X + 5, itemAt.Bounds.Y + itemAt.Bounds.Height), new Point(itemAt.Bounds.X, (itemAt.Bounds.Y + itemAt.Bounds.Height) + 5) };
                    graphics.FillPolygon(new SolidBrush(this.color_0), pointArray);
                    pointArray = new Point[] { new Point(base.Bounds.Width - 4, (itemAt.Bounds.Y + itemAt.Bounds.Height) - 5), new Point(base.Bounds.Width - 9, itemAt.Bounds.Y + itemAt.Bounds.Height), new Point(base.Bounds.Width - 4, (itemAt.Bounds.Y + itemAt.Bounds.Height) + 5) };
                    graphics.FillPolygon(new SolidBrush(this.color_0), pointArray);
                }
                else
                {
                    graphics.DrawLine(new Pen(this.color_0, 2f), new Point(itemAt.Bounds.X + itemAt.Bounds.Width, itemAt.Bounds.Y), new Point(itemAt.Bounds.X + itemAt.Bounds.Width, itemAt.Bounds.Y + itemAt.Bounds.Height));
                    pointArray = new Point[] { new Point((itemAt.Bounds.X + itemAt.Bounds.Width) - 5, itemAt.Bounds.Y), new Point((itemAt.Bounds.X + itemAt.Bounds.Width) + 5, itemAt.Bounds.Y), new Point(itemAt.Bounds.X + itemAt.Bounds.Width, itemAt.Bounds.Y + 5) };
                    graphics.FillPolygon(new SolidBrush(this.color_0), pointArray);
                    pointArray = new Point[] { new Point((itemAt.Bounds.X + itemAt.Bounds.Width) - 5, itemAt.Bounds.Y + itemAt.Bounds.Height), new Point((itemAt.Bounds.X + itemAt.Bounds.Width) + 5, itemAt.Bounds.Y + itemAt.Bounds.Height), new Point(itemAt.Bounds.X + itemAt.Bounds.Width, (itemAt.Bounds.Y + itemAt.Bounds.Height) - 5) };
                    graphics.FillPolygon(new SolidBrush(this.color_0), pointArray);
                }
                base.OnDragOver(drgevent);
                return;
            }
            if (!(((this.listViewItem_0 == null) || (this.listViewItem_0 == itemAt)) ? (this.listViewItem_0 != null) : false))
            {
                base.Invalidate();
            }
            this.listViewItem_0 = itemAt;
            if ((base.View == View.Details) || (base.View == View.List))
            {
                graphics.DrawLine(new Pen(this.color_0, 2f), new Point(itemAt.Bounds.X, itemAt.Bounds.Y), new Point(itemAt.Bounds.X + base.Bounds.Width, itemAt.Bounds.Y));
                pointArray = new Point[] { new Point(itemAt.Bounds.X, itemAt.Bounds.Y - 5), new Point(itemAt.Bounds.X + 5, itemAt.Bounds.Y), new Point(itemAt.Bounds.X, itemAt.Bounds.Y + 5) };
                graphics.FillPolygon(new SolidBrush(this.color_0), pointArray);
                pointArray = new Point[] { new Point(base.Bounds.Width - 4, itemAt.Bounds.Y - 5), new Point(base.Bounds.Width - 9, itemAt.Bounds.Y), new Point(base.Bounds.Width - 4, itemAt.Bounds.Y + 5) };
                graphics.FillPolygon(new SolidBrush(this.color_0), pointArray);
            }
            else
            {
                graphics.DrawLine(new Pen(this.color_0, 2f), new Point(itemAt.Bounds.X, itemAt.Bounds.Y), new Point(itemAt.Bounds.X, itemAt.Bounds.Y + itemAt.Bounds.Height));
                pointArray = new Point[] { new Point(itemAt.Bounds.X - 5, itemAt.Bounds.Y), new Point(itemAt.Bounds.X + 5, itemAt.Bounds.Y), new Point(itemAt.Bounds.X, itemAt.Bounds.Y + 5) };
                graphics.FillPolygon(new SolidBrush(this.color_0), pointArray);
                pointArray = new Point[] { new Point(itemAt.Bounds.X - 5, itemAt.Bounds.Y + itemAt.Bounds.Height), new Point(itemAt.Bounds.X + 5, itemAt.Bounds.Y + itemAt.Bounds.Height), new Point(itemAt.Bounds.X, (itemAt.Bounds.Y + itemAt.Bounds.Height) - 5) };
                graphics.FillPolygon(new SolidBrush(this.color_0), pointArray);
            }
            using (IEnumerator enumerator = base.SelectedItems.GetEnumerator())
            {
                while (enumerator.MoveNext())
                {
                    ListViewItem current = (ListViewItem) enumerator.Current;
                    if (current.Index == itemAt.Index)
                    {
                        goto Label_0A34;
                    }
                }
                goto Label_0A57;
            Label_0A34:
                drgevent.Effect = DragDropEffects.None;
                itemAt.EnsureVisible();
                return;
            }
        Label_0A57:
            itemAt.EnsureVisible();
        Label_0A5D:
            drgevent.Effect = DragDropEffects.Move;
            base.OnDragOver(drgevent);
        }

        protected override void OnItemDrag(ItemDragEventArgs e)
        {
            if (!this.bool_0)
            {
                base.OnItemDrag(e);
            }
            else
            {
                base.DoDragDrop(this.method_0(), DragDropEffects.Move);
                base.OnItemDrag(e);
            }
        }

        protected override void OnLostFocus(EventArgs e)
        {
            this.method_1();
            base.Invalidate();
            base.OnLostFocus(e);
        }

        [Category("Behavior"), Description("允许重新排序")]
        public bool AllowReorder
        {
            get
            {
                return this.bool_0;
            }
            set
            {
                this.bool_0 = value;
            }
        }

        [Description("拖拉的线条颜色显示"), Category("Appearance")]
        public Color LineColor
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

        public class DragItemData
        {
            private ArrayList arrayList_0;
            private DragAndDropListView dragAndDropListView_0;

            public DragItemData(DragAndDropListView listView)
            {
                this.dragAndDropListView_0 = listView;
                this.arrayList_0 = new ArrayList();
            }

            public ArrayList DragItems
            {
                get
                {
                    return this.arrayList_0;
                }
            }

            public DragAndDropListView ListView
            {
                get
                {
                    return this.dragAndDropListView_0;
                }
            }
        }

        public delegate bool ProcessDragItemEventHandler(DragAndDropListView.DragItemData dragItemData, ListViewItem hoveItem);
    }
}

