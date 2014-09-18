namespace WHC.OrderWater.Commons
{
    using System;
    using System.Drawing;
    using System.Windows.Forms;

    public class DGVCellDrawingEventArgs : EventArgs
    {
        public DataGridViewCellStyle CellStyle;
        public int column;
        public RectangleF DrawingBounds;
        public Graphics g;
        public bool Handled;
        public int row;

        public DGVCellDrawingEventArgs(Graphics g, RectangleF bounds, DataGridViewCellStyle style, int row, int column)
        {
            this.g = g;
            this.DrawingBounds = bounds;
            this.CellStyle = style;
            this.row = row;
            this.column = column;
            this.Handled = false;
        }
    }
}

