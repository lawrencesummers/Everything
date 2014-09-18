namespace WHC.OrderWater.Commons
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Drawing.Printing;
    using System.Windows.Forms;

    public class FrmPosPrintPreview : Form
    {
        private Button btnClose;
        private Button btnPreview;
        private Button btnPrint;
        private Button btnPrintSetup;
        private Font font_0 = new Font("宋体", 9f);
        private GroupBox groupBox1;
        private IContainer icontainer_0 = null;
        public bool Landscape = false;
        private MultipadPrintDocument multipadPrintDocument_0 = new MultipadPrintDocument();
        public int POSPageMargin = 2;
        public string PrinterName = "GP-5860III";
        public string PrintString = "";
        private TextBox txtContent;

        public FrmPosPrintPreview()
        {
            this.InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            base.Close();
        }

        private void btnPreview_Click(object sender, EventArgs e)
        {
            PrintPreviewDialog dialog = new PrintPreviewDialog();
            this.multipadPrintDocument_0.Text = this.txtContent.Text;
            this.multipadPrintDocument_0.Font = this.font_0;
            dialog.Document = this.multipadPrintDocument_0;
            dialog.ShowDialog();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            PrintDialog dialog = new PrintDialog();
            this.multipadPrintDocument_0.Text = this.txtContent.Text;
            this.multipadPrintDocument_0.Font = this.font_0;
            dialog.Document = this.multipadPrintDocument_0;
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                this.multipadPrintDocument_0.Print();
            }
        }

        private void btnPrintSetup_Click(object sender, EventArgs e)
        {
            PageSetupDialog dialog = new PageSetupDialog {
                Document = this.multipadPrintDocument_0
            };
            dialog.PageSettings.Margins = PrinterUnitConvert.Convert(dialog.PageSettings.Margins, PrinterUnit.ThousandthsOfAnInch, PrinterUnit.HundredthsOfAMillimeter);
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                this.multipadPrintDocument_0.Print();
            }
            else
            {
                dialog.PageSettings.Margins = PrinterUnitConvert.Convert(dialog.PageSettings.Margins, PrinterUnit.HundredthsOfAMillimeter, PrinterUnit.ThousandthsOfAnInch);
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.icontainer_0 != null))
            {
                this.icontainer_0.Dispose();
            }
            base.Dispose(disposing);
        }

        private void FrmPosPrintPreview_Load(object sender, EventArgs e)
        {
            this.txtContent.Text = this.PrintString;
            this.multipadPrintDocument_0.Text = this.txtContent.Text;
            this.multipadPrintDocument_0.Font = this.font_0;
            this.multipadPrintDocument_0.DefaultPageSettings.Landscape = this.Landscape;
            int pOSPageMargin = this.POSPageMargin;
            this.multipadPrintDocument_0.DefaultPageSettings.Margins = new Margins(pOSPageMargin, pOSPageMargin, pOSPageMargin, pOSPageMargin);
            this.multipadPrintDocument_0.PrinterSettings.PrinterName = this.PrinterName;
        }

        private void InitializeComponent()
        {
            this.txtContent = new TextBox();
            this.groupBox1 = new GroupBox();
            this.btnPrintSetup = new Button();
            this.btnPreview = new Button();
            this.btnPrint = new Button();
            this.btnClose = new Button();
            this.groupBox1.SuspendLayout();
            base.SuspendLayout();
            this.txtContent.Dock = DockStyle.Fill;
            this.txtContent.Location = new Point(3, 0x11);
            this.txtContent.Multiline = true;
            this.txtContent.Name = "txtContent";
            this.txtContent.Size = new Size(0x1c8, 0x18a);
            this.txtContent.TabIndex = 0;
            this.groupBox1.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
            this.groupBox1.Controls.Add(this.txtContent);
            this.groupBox1.Location = new Point(3, 0x36);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(0x1ce, 0x19e);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "打印内容";
            this.btnPrintSetup.Location = new Point(12, 12);
            this.btnPrintSetup.Name = "btnPrintSetup";
            this.btnPrintSetup.Size = new Size(0x62, 0x17);
            this.btnPrintSetup.TabIndex = 2;
            this.btnPrintSetup.Text = "打印设置";
            this.btnPrintSetup.Click += new EventHandler(this.btnPrintSetup_Click);
            this.btnPreview.Location = new Point(0x7e, 12);
            this.btnPreview.Name = "btnPreview";
            this.btnPreview.Size = new Size(0x62, 0x17);
            this.btnPreview.TabIndex = 2;
            this.btnPreview.Text = "打印预览";
            this.btnPreview.Click += new EventHandler(this.btnPreview_Click);
            this.btnPrint.Location = new Point(0xf1, 12);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new Size(0x62, 0x17);
            this.btnPrint.TabIndex = 2;
            this.btnPrint.Text = "打印";
            this.btnPrint.Click += new EventHandler(this.btnPrint_Click);
            this.btnClose.Location = new Point(0x182, 12);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new Size(0x4b, 0x17);
            this.btnClose.TabIndex = 3;
            this.btnClose.Text = "关闭";
            this.btnClose.Click += new EventHandler(this.btnClose_Click);
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x1d5, 0x1d8);
            base.Controls.Add(this.btnClose);
            base.Controls.Add(this.btnPrint);
            base.Controls.Add(this.btnPreview);
            base.Controls.Add(this.btnPrintSetup);
            base.Controls.Add(this.groupBox1);
            base.Name = "FrmPosPrintPreview";
            base.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "USB口打印管理界面";
            base.Load += new EventHandler(this.FrmPosPrintPreview_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            base.ResumeLayout(false);
        }
    }
}

