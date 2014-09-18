namespace WHC.OrderWater.Commons
{
    using System;
    using System.Collections;
    using System.Drawing;
    using System.Windows.Forms;

    public class SortableListView : ListView
    {
        private Bitmap bitmap_0;
        private Bitmap bitmap_1;
        private int int_0 = 1;
        private int int_1 = 0;

        public SortableListView()
        {
            if (this.bitmap_0 != null)
            {
                this.bitmap_0.MakeTransparent(Color.Magenta);
            }
            if (this.bitmap_1 != null)
            {
                this.bitmap_1.MakeTransparent(Color.Magenta);
            }
            base.BorderStyle = BorderStyle.None;
            this.Dock = DockStyle.Fill;
            base.FullRowSelect = true;
            base.HideSelection = false;
            base.LabelEdit = true;
            base.LabelWrap = false;
            base.View = View.Details;
            base.Sorting = SortOrder.None;
            base.AllowColumnReorder = true;
            base.OwnerDraw = true;
            base.DrawColumnHeader += new DrawListViewColumnHeaderEventHandler(this.SortableListView_DrawColumnHeader);
            base.DrawItem += new DrawListViewItemEventHandler(this.NtEoaeaMno);
            base.DrawSubItem += new DrawListViewSubItemEventHandler(this.SortableListView_DrawSubItem);
            base.ColumnClick += new ColumnClickEventHandler(this.SortableListView_ColumnClick);
            base.ColumnReordered += new ColumnReorderedEventHandler(this.SortableListView_ColumnReordered);
        }

        public void AddColumns(params string[] columns)
        {
            base.Columns.Clear();
            foreach (string str in columns)
            {
                base.Columns.Add(str, 120);
            }
            base.ListViewItemSorter = new Class23(this.int_1, this.int_0);
        }

        private void NtEoaeaMno(object sender, DrawListViewItemEventArgs e)
        {
            e.DrawDefault = true;
        }

        private void SortableListView_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            if (this.int_1 != e.Column)
            {
                this.int_1 = e.Column;
                this.int_0 = 1;
            }
            else
            {
                this.int_0 *= -1;
            }
            base.ListViewItemSorter = new Class23(this.int_1, this.int_0);
        }

        private void SortableListView_ColumnReordered(object sender, ColumnReorderedEventArgs e)
        {
            if (e.OldDisplayIndex == this.int_1)
            {
                this.int_1 = e.NewDisplayIndex;
                base.ListViewItemSorter = new Class23(this.int_1, this.int_0);
            }
        }

        private void SortableListView_DrawColumnHeader(object sender, DrawListViewColumnHeaderEventArgs e)
        {
            bool flag;
            if (flag = this.int_1 == e.ColumnIndex)
            {
                e.DrawBackground();
                e.DrawText(TextFormatFlags.EndEllipsis | TextFormatFlags.VerticalCenter);
                if ((flag && (this.bitmap_0 != null)) && (this.bitmap_1 != null))
                {
                    Point point = new Point(e.Bounds.Left + ((int) e.Graphics.MeasureString(e.Header.Text + "XY", e.Font).Width), ((e.Bounds.Top + e.Bounds.Bottom) - this.bitmap_0.Height) / 2);
                    e.Graphics.DrawImage((this.int_0 > 0) ? this.bitmap_0 : this.bitmap_1, point);
                }
                this.Refresh();
            }
            else
            {
                e.DrawDefault = true;
            }
        }

        private void SortableListView_DrawSubItem(object sender, DrawListViewSubItemEventArgs e)
        {
            e.DrawDefault = true;
        }

        public Bitmap ImageAscending
        {
            get
            {
                return this.bitmap_0;
            }
            set
            {
                this.bitmap_0 = value;
            }
        }

        public Bitmap ImageDescending
        {
            get
            {
                return this.bitmap_1;
            }
            set
            {
                this.bitmap_1 = value;
            }
        }

        private class Class23 : IComparer
        {
            private int int_0;
            private int int_1;

            public Class23(int int_2, int int_3)
            {
                this.int_1 = int_3;
                this.int_0 = int_2;
            }

            public int Compare(object x, object y)
            {
                return (Math.Sign(this.int_1) * string.Compare(((ListViewItem) x).SubItems[this.int_0].Text, ((ListViewItem) y).SubItems[this.int_0].Text));
            }
        }
    }
}

