namespace WHC.OrderWater.Commons
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Drawing.Printing;
    using System.Windows.Forms;

    public class CoolPrintPreviewDialog : Form
    {
        private ToolStripButton _btnCancel;
        private ToolStripButton _btnFirst;
        private ToolStripButton _btnLast;
        private ToolStripButton _btnNext;
        private ToolStripButton _btnPageSetup;
        private ToolStripButton _btnPrev;
        private ToolStripButton _btnPrint;
        private ToolStripSplitButton _btnZoom;
        private ToolStripMenuItem _item10;
        private ToolStripMenuItem _item100;
        private ToolStripMenuItem _item150;
        private ToolStripMenuItem _item200;
        private ToolStripMenuItem _item25;
        private ToolStripMenuItem _item50;
        private ToolStripMenuItem _item500;
        private ToolStripMenuItem _item75;
        private ToolStripMenuItem _itemActualSize;
        private ToolStripMenuItem _itemFullPage;
        private ToolStripMenuItem _itemPageWidth;
        private ToolStripMenuItem _itemTwoPages;
        private ToolStripLabel _lblPageCount;
        private Control0 _preview;
        private ToolStripSeparator _separator;
        private ToolStrip _toolStrip;
        private ToolStripTextBox _txtStartPage;
        private IContainer icontainer_0;
        private PrintDocument printDocument_0;
        private ToolStripSeparator toolStripMenuItem1;
        private ToolStripSeparator toolStripSeparator2;

        public CoolPrintPreviewDialog() : this(null)
        {
        }

        public CoolPrintPreviewDialog(Control parentForm)
        {
            this.icontainer_0 = null;
            this.InitializeComponent();
            if (parentForm != null)
            {
                base.Size = parentForm.Size;
            }
        }

        private void _btnCancel_Click(object sender, EventArgs e)
        {
            if (this._preview.Boolean_0)
            {
                this._preview.method_2();
            }
            else
            {
                base.Close();
            }
        }

        private void _btnFirst_Click(object sender, EventArgs e)
        {
            this._preview.Int32_0 = 0;
        }

        private void _btnLast_Click(object sender, EventArgs e)
        {
            this._preview.Int32_0 = this._preview.Int32_1 - 1;
        }

        private void _btnNext_Click(object sender, EventArgs e)
        {
            this._preview.Int32_0++;
        }

        private void _btnPageSetup_Click(object sender, EventArgs e)
        {
            using (PageSetupDialog dialog = new PageSetupDialog())
            {
                dialog.Document = this.Document;
                if (dialog.ShowDialog(this) == DialogResult.OK)
                {
                    this._preview.method_1();
                }
            }
        }

        private void _btnPrint_Click(object sender, EventArgs e)
        {
            using (PrintDialog dialog = new PrintDialog())
            {
                dialog.AllowSomePages = true;
                dialog.AllowSelection = true;
                dialog.UseEXDialog = true;
                dialog.Document = this.Document;
                PrinterSettings printerSettings = dialog.PrinterSettings;
                printerSettings.FromPage = 1;
                printerSettings.MinimumPage = 1;
                printerSettings.MaximumPage = printerSettings.ToPage = this._preview.Int32_1;
                if (dialog.ShowDialog(this) == DialogResult.OK)
                {
                    this._preview.method_3();
                }
            }
        }

        private void _btnZoom_ButtonClick(object sender, EventArgs e)
        {
            this._preview.Enum0_0 = (this._preview.Enum0_0 == ((Enum0) 0)) ? ((Enum0) 1) : ((Enum0) 0);
        }

        private void _btnZoom_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem == this._itemActualSize)
            {
                this._preview.Enum0_0 = (Enum0) 0;
            }
            else if (e.ClickedItem == this._itemFullPage)
            {
                this._preview.Enum0_0 = (Enum0) 1;
            }
            else if (e.ClickedItem == this._itemPageWidth)
            {
                this._preview.Enum0_0 = (Enum0) 2;
            }
            else if (e.ClickedItem == this._itemTwoPages)
            {
                this._preview.Enum0_0 = (Enum0) 3;
            }
            if (e.ClickedItem == this._item10)
            {
                this._preview.Double_0 = 0.1;
            }
            else if (e.ClickedItem == this._item100)
            {
                this._preview.Double_0 = 1.0;
            }
            else if (e.ClickedItem == this._item150)
            {
                this._preview.Double_0 = 1.5;
            }
            else if (e.ClickedItem == this._item200)
            {
                this._preview.Double_0 = 2.0;
            }
            else if (e.ClickedItem == this._item25)
            {
                this._preview.Double_0 = 0.25;
            }
            else if (e.ClickedItem == this._item50)
            {
                this._preview.Double_0 = 0.5;
            }
            else if (e.ClickedItem == this._item500)
            {
                this._preview.Double_0 = 5.0;
            }
            else if (e.ClickedItem == this._item75)
            {
                this._preview.Double_0 = 0.75;
            }
        }

        private void _txtStartPage_Enter(object sender, EventArgs e)
        {
            this._txtStartPage.SelectAll();
        }

        private void _txtStartPage_KeyPress(object sender, KeyPressEventArgs e)
        {
            char keyChar = e.KeyChar;
            if (keyChar == '\r')
            {
                this.method_0();
                e.Handled = true;
            }
            else if (!((keyChar <= ' ') || char.IsDigit(keyChar)))
            {
                e.Handled = true;
            }
        }

        private void _txtStartPage_Validating(object sender, CancelEventArgs e)
        {
            this.method_0();
        }

        private void cthxhGhxiB(object sender, EventArgs e)
        {
            this._preview.Int32_0--;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.icontainer_0 != null))
            {
                this.icontainer_0.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            ComponentResourceManager manager = new ComponentResourceManager(typeof(CoolPrintPreviewDialog));
            this._toolStrip = new ToolStrip();
            this._btnPrint = new ToolStripButton();
            this._btnPageSetup = new ToolStripButton();
            this.toolStripSeparator2 = new ToolStripSeparator();
            this._btnZoom = new ToolStripSplitButton();
            this._itemActualSize = new ToolStripMenuItem();
            this._itemFullPage = new ToolStripMenuItem();
            this._itemPageWidth = new ToolStripMenuItem();
            this._itemTwoPages = new ToolStripMenuItem();
            this.toolStripMenuItem1 = new ToolStripSeparator();
            this._item500 = new ToolStripMenuItem();
            this._item200 = new ToolStripMenuItem();
            this._item150 = new ToolStripMenuItem();
            this._item100 = new ToolStripMenuItem();
            this._item75 = new ToolStripMenuItem();
            this._item50 = new ToolStripMenuItem();
            this._item25 = new ToolStripMenuItem();
            this._item10 = new ToolStripMenuItem();
            this._btnFirst = new ToolStripButton();
            this._btnPrev = new ToolStripButton();
            this._txtStartPage = new ToolStripTextBox();
            this._lblPageCount = new ToolStripLabel();
            this._btnNext = new ToolStripButton();
            this._btnLast = new ToolStripButton();
            this._separator = new ToolStripSeparator();
            this._btnCancel = new ToolStripButton();
            this._preview = new Control0();
            this._toolStrip.SuspendLayout();
            base.SuspendLayout();
            this._toolStrip.AutoSize = false;
            this._toolStrip.GripStyle = ToolStripGripStyle.Hidden;
            this._toolStrip.Items.AddRange(new ToolStripItem[] { this._btnPrint, this._btnPageSetup, this.toolStripSeparator2, this._btnZoom, this._btnFirst, this._btnPrev, this._txtStartPage, this._lblPageCount, this._btnNext, this._btnLast, this._separator, this._btnCancel });
            this._toolStrip.Location = new Point(0, 0);
            this._toolStrip.Name = "_toolStrip";
            this._toolStrip.Size = new Size(0x2ec, 30);
            this._toolStrip.TabIndex = 0;
            this._toolStrip.Text = "toolStrip1";
            this._btnPrint.DisplayStyle = ToolStripItemDisplayStyle.Image;
            this._btnPrint.Image = (Image) manager.GetObject("_btnPrint.Image");
            this._btnPrint.ImageTransparentColor = Color.Magenta;
            this._btnPrint.Name = "_btnPrint";
            this._btnPrint.Size = new Size(0x17, 0x1b);
            this._btnPrint.Text = "打印文档";
            this._btnPrint.Click += new EventHandler(this._btnPrint_Click);
            this._btnPageSetup.DisplayStyle = ToolStripItemDisplayStyle.Image;
            this._btnPageSetup.Image = (Image) manager.GetObject("_btnPageSetup.Image");
            this._btnPageSetup.ImageTransparentColor = Color.Magenta;
            this._btnPageSetup.Name = "_btnPageSetup";
            this._btnPageSetup.Size = new Size(0x17, 0x1b);
            this._btnPageSetup.Text = "页面设置";
            this._btnPageSetup.Click += new EventHandler(this._btnPageSetup_Click);
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new Size(6, 30);
            this._btnZoom.AutoToolTip = false;
            this._btnZoom.DropDownItems.AddRange(new ToolStripItem[] { this._itemActualSize, this._itemFullPage, this._itemTwoPages, this._itemPageWidth, this.toolStripMenuItem1, this._item500, this._item200, this._item150, this._item100, this._item75, this._item50, this._item25, this._item10 });
            this._btnZoom.Image = (Image) manager.GetObject("_btnZoom.Image");
            this._btnZoom.ImageTransparentColor = Color.Magenta;
            this._btnZoom.Name = "_btnZoom";
            this._btnZoom.Size = new Size(0x4f, 0x1b);
            this._btnZoom.Text = "缩放(&Z)";
            this._btnZoom.ButtonClick += new EventHandler(this._btnZoom_ButtonClick);
            this._btnZoom.DropDownItemClicked += new ToolStripItemClickedEventHandler(this._btnZoom_DropDownItemClicked);
            this._itemActualSize.Image = (Image) manager.GetObject("_itemActualSize.Image");
            this._itemActualSize.Name = "_itemActualSize";
            this._itemActualSize.Size = new Size(0x76, 0x16);
            this._itemActualSize.Text = "实际尺寸";
            this._itemFullPage.Image = (Image) manager.GetObject("_itemFullPage.Image");
            this._itemFullPage.Name = "_itemFullPage";
            this._itemFullPage.Size = new Size(0x76, 0x16);
            this._itemFullPage.Text = "单页";
            this._itemPageWidth.Image = (Image) manager.GetObject("_itemPageWidth.Image");
            this._itemPageWidth.Name = "_itemPageWidth";
            this._itemPageWidth.Size = new Size(0x76, 0x16);
            this._itemPageWidth.Text = "页宽";
            this._itemTwoPages.Image = (Image) manager.GetObject("_itemTwoPages.Image");
            this._itemTwoPages.Name = "_itemTwoPages";
            this._itemTwoPages.Size = new Size(0x76, 0x16);
            this._itemTwoPages.Text = "双页";
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new Size(0x73, 6);
            this._item500.Name = "_item500";
            this._item500.Size = new Size(0x76, 0x16);
            this._item500.Text = "500%";
            this._item200.Name = "_item200";
            this._item200.Size = new Size(0x76, 0x16);
            this._item200.Text = "200%";
            this._item150.Name = "_item150";
            this._item150.Size = new Size(0x76, 0x16);
            this._item150.Text = "150%";
            this._item100.Name = "_item100";
            this._item100.Size = new Size(0x76, 0x16);
            this._item100.Text = "100%";
            this._item75.Name = "_item75";
            this._item75.Size = new Size(0x76, 0x16);
            this._item75.Text = "75%";
            this._item50.Name = "_item50";
            this._item50.Size = new Size(0x76, 0x16);
            this._item50.Text = "50%";
            this._item25.Name = "_item25";
            this._item25.Size = new Size(0x76, 0x16);
            this._item25.Text = "25%";
            this._item10.Name = "_item10";
            this._item10.Size = new Size(0x76, 0x16);
            this._item10.Text = "10%";
            this._btnFirst.DisplayStyle = ToolStripItemDisplayStyle.Image;
            this._btnFirst.Image = (Image) manager.GetObject("_btnFirst.Image");
            this._btnFirst.ImageTransparentColor = Color.Red;
            this._btnFirst.Name = "_btnFirst";
            this._btnFirst.Size = new Size(0x17, 0x1b);
            this._btnFirst.Text = "首页";
            this._btnFirst.Click += new EventHandler(this._btnFirst_Click);
            this._btnPrev.DisplayStyle = ToolStripItemDisplayStyle.Image;
            this._btnPrev.Image = (Image) manager.GetObject("_btnPrev.Image");
            this._btnPrev.ImageTransparentColor = Color.Red;
            this._btnPrev.Name = "_btnPrev";
            this._btnPrev.Size = new Size(0x17, 0x1b);
            this._btnPrev.Text = "上一页";
            this._btnPrev.Click += new EventHandler(this.cthxhGhxiB);
            this._txtStartPage.AutoSize = false;
            this._txtStartPage.Name = "_txtStartPage";
            this._txtStartPage.Size = new Size(0x20, 0x15);
            this._txtStartPage.TextBoxTextAlign = HorizontalAlignment.Center;
            this._txtStartPage.Validating += new CancelEventHandler(this._txtStartPage_Validating);
            this._txtStartPage.Enter += new EventHandler(this._txtStartPage_Enter);
            this._txtStartPage.KeyPress += new KeyPressEventHandler(this._txtStartPage_KeyPress);
            this._lblPageCount.Name = "_lblPageCount";
            this._lblPageCount.Size = new Size(11, 0x1b);
            this._lblPageCount.Text = " ";
            this._btnNext.DisplayStyle = ToolStripItemDisplayStyle.Image;
            this._btnNext.Image = (Image) manager.GetObject("_btnNext.Image");
            this._btnNext.ImageTransparentColor = Color.Red;
            this._btnNext.Name = "_btnNext";
            this._btnNext.Size = new Size(0x17, 0x1b);
            this._btnNext.Text = "下一页";
            this._btnNext.Click += new EventHandler(this._btnNext_Click);
            this._btnLast.DisplayStyle = ToolStripItemDisplayStyle.Image;
            this._btnLast.Image = (Image) manager.GetObject("_btnLast.Image");
            this._btnLast.ImageTransparentColor = Color.Red;
            this._btnLast.Name = "_btnLast";
            this._btnLast.Size = new Size(0x17, 0x1b);
            this._btnLast.Text = "最后一页";
            this._btnLast.Click += new EventHandler(this._btnLast_Click);
            this._separator.Name = "_separator";
            this._separator.Size = new Size(6, 30);
            this._separator.Visible = false;
            this._btnCancel.AutoToolTip = false;
            this._btnCancel.Image = (Image) manager.GetObject("_btnCancel.Image");
            this._btnCancel.ImageTransparentColor = Color.Magenta;
            this._btnCancel.Name = "_btnCancel";
            this._btnCancel.Size = new Size(0x31, 0x1b);
            this._btnCancel.Text = "取消";
            this._btnCancel.Click += new EventHandler(this._btnCancel_Click);
            this._preview.AutoScroll = true;
            this._preview.Dock = DockStyle.Fill;
            this._preview.DyJkoTjhuC(null);
            this._preview.Location = new Point(0, 30);
            this._preview.Name = "_preview";
            this._preview.Size = new Size(0x2ec, 0x2a5);
            this._preview.TabIndex = 1;
            this._preview.Event_1 += new EventHandler(this.method_2);
            this._preview.Event_0 += new EventHandler(this.method_1);
            base.AutoScaleDimensions = new SizeF(96f, 96f);
            base.AutoScaleMode = AutoScaleMode.Dpi;
            base.ClientSize = new Size(0x2ec, 0x2c3);
            base.Controls.Add(this._preview);
            base.Controls.Add(this._toolStrip);
            base.Name = "CoolPrintPreviewDialog";
            base.ShowIcon = false;
            base.ShowInTaskbar = false;
            base.StartPosition = FormStartPosition.CenterParent;
            this.Text = "打印预览";
            this._toolStrip.ResumeLayout(false);
            this._toolStrip.PerformLayout();
            base.ResumeLayout(false);
        }

        private void method_0()
        {
            int num;
            if (int.TryParse(this._txtStartPage.Text, out num))
            {
                this._preview.Int32_0 = num - 1;
            }
        }

        private void method_1(object sender, EventArgs e)
        {
            this._txtStartPage.Text = (this._preview.Int32_0 + 1).ToString();
        }

        private void method_2(object sender, EventArgs e)
        {
            base.Update();
            Application.DoEvents();
            this._lblPageCount.Text = string.Format("/ {0}", this._preview.Int32_1);
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            if (!(!this._preview.Boolean_0 || e.Cancel))
            {
                this._preview.method_2();
            }
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            this._preview.DyJkoTjhuC(this.Document);
        }

        private void printDocument_0_BeginPrint(object sender, PrintEventArgs e)
        {
            this._btnCancel.Text = "取消(&C)";
            this._btnPageSetup.Enabled = false;
            this._btnPrint.Enabled = false;
        }

        private void printDocument_0_EndPrint(object sender, PrintEventArgs e)
        {
            this._btnCancel.Text = "关闭(&C)";
            this._btnPageSetup.Enabled = true;
            this._btnPrint.Enabled = true;
        }

        public PrintDocument Document
        {
            get
            {
                return this.printDocument_0;
            }
            set
            {
                if (this.printDocument_0 != null)
                {
                    this.printDocument_0.BeginPrint -= new PrintEventHandler(this.printDocument_0_BeginPrint);
                    this.printDocument_0.EndPrint -= new PrintEventHandler(this.printDocument_0_EndPrint);
                }
                this.printDocument_0 = value;
                if (this.printDocument_0 != null)
                {
                    this.printDocument_0.BeginPrint += new PrintEventHandler(this.printDocument_0_BeginPrint);
                    this.printDocument_0.EndPrint += new PrintEventHandler(this.printDocument_0_EndPrint);
                }
                if (base.Visible)
                {
                    this._preview.DyJkoTjhuC(this.Document);
                }
            }
        }
    }
}

