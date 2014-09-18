namespace WHC.OrderWater.Commons
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class PrintOptions : Form
    {
        protected Button btnCancel;
        protected Button btnOK;
        internal CheckBox chkFitToPageWidth;
        internal CheckedListBox chklst;
        internal GroupBox gboxRowsToPrint;
        private IContainer icontainer_0;
        internal Label lblColumnsToPrint;
        internal Label lblTitle;
        internal RadioButton rdoAllRows;
        internal RadioButton rdoSelectedRows;
        internal TextBox txtTitle;

        public PrintOptions()
        {
            this.icontainer_0 = null;
            this.InitializeComponent();
        }

        public PrintOptions(List<string> availableFields)
        {
            this.icontainer_0 = null;
            this.InitializeComponent();
            foreach (string str in availableFields)
            {
                this.chklst.Items.Add(str, true);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            base.DialogResult = DialogResult.Cancel;
            base.Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            base.DialogResult = DialogResult.OK;
            base.Close();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.icontainer_0 != null))
            {
                this.icontainer_0.Dispose();
            }
            base.Dispose(disposing);
        }

        public List<string> GetSelectedColumns()
        {
            List<string> list = new List<string>();
            foreach (object obj2 in this.chklst.CheckedItems)
            {
                list.Add(obj2.ToString());
            }
            return list;
        }

        private void InitializeComponent()
        {
            ComponentResourceManager manager = new ComponentResourceManager(typeof(PrintOptions));
            this.rdoSelectedRows = new RadioButton();
            this.rdoAllRows = new RadioButton();
            this.chkFitToPageWidth = new CheckBox();
            this.lblTitle = new Label();
            this.txtTitle = new TextBox();
            this.gboxRowsToPrint = new GroupBox();
            this.lblColumnsToPrint = new Label();
            this.btnOK = new Button();
            this.btnCancel = new Button();
            this.chklst = new CheckedListBox();
            this.gboxRowsToPrint.SuspendLayout();
            base.SuspendLayout();
            this.rdoSelectedRows.AutoSize = true;
            this.rdoSelectedRows.Font = new Font("新宋体", 9f);
            this.rdoSelectedRows.Location = new Point(0x5b, 0x12);
            this.rdoSelectedRows.Name = "rdoSelectedRows";
            this.rdoSelectedRows.Size = new Size(0x3b, 0x10);
            this.rdoSelectedRows.TabIndex = 1;
            this.rdoSelectedRows.TabStop = true;
            this.rdoSelectedRows.Text = "选中行";
            this.rdoSelectedRows.UseVisualStyleBackColor = true;
            this.rdoAllRows.AutoSize = true;
            this.rdoAllRows.Font = new Font("新宋体", 9f);
            this.rdoAllRows.Location = new Point(9, 0x12);
            this.rdoAllRows.Name = "rdoAllRows";
            this.rdoAllRows.Size = new Size(0x3b, 0x10);
            this.rdoAllRows.TabIndex = 0;
            this.rdoAllRows.TabStop = true;
            this.rdoAllRows.Text = "全部行";
            this.rdoAllRows.UseVisualStyleBackColor = true;
            this.chkFitToPageWidth.AutoSize = true;
            this.chkFitToPageWidth.CheckAlign = ContentAlignment.MiddleRight;
            this.chkFitToPageWidth.FlatStyle = FlatStyle.System;
            this.chkFitToPageWidth.Font = new Font("新宋体", 9f);
            this.chkFitToPageWidth.Location = new Point(0xbb, 0x48);
            this.chkFitToPageWidth.Name = "chkFitToPageWidth";
            this.chkFitToPageWidth.Size = new Size(0x66, 0x11);
            this.chkFitToPageWidth.TabIndex = 0x15;
            this.chkFitToPageWidth.Text = "适应页面宽度";
            this.chkFitToPageWidth.UseVisualStyleBackColor = true;
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new Font("新宋体", 9f);
            this.lblTitle.Location = new Point(0xb8, 0x63);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new Size(0x35, 12);
            this.lblTitle.TabIndex = 20;
            this.lblTitle.Text = "页面抬头";
            this.txtTitle.AcceptsReturn = true;
            this.txtTitle.Font = new Font("新宋体", 9f);
            this.txtTitle.Location = new Point(0xb8, 0x72);
            this.txtTitle.Multiline = true;
            this.txtTitle.Name = "txtTitle";
            this.txtTitle.ScrollBars = ScrollBars.Vertical;
            this.txtTitle.Size = new Size(0xb0, 0x47);
            this.txtTitle.TabIndex = 0x13;
            this.gboxRowsToPrint.Controls.Add(this.rdoSelectedRows);
            this.gboxRowsToPrint.Controls.Add(this.rdoAllRows);
            this.gboxRowsToPrint.Font = new Font("新宋体", 9f);
            this.gboxRowsToPrint.Location = new Point(0xb8, 20);
            this.gboxRowsToPrint.Name = "gboxRowsToPrint";
            this.gboxRowsToPrint.Size = new Size(0xad, 0x27);
            this.gboxRowsToPrint.TabIndex = 0x12;
            this.gboxRowsToPrint.TabStop = false;
            this.gboxRowsToPrint.Text = "打印方式";
            this.lblColumnsToPrint.AutoSize = true;
            this.lblColumnsToPrint.Font = new Font("新宋体", 9f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.lblColumnsToPrint.Location = new Point(8, 8);
            this.lblColumnsToPrint.Name = "lblColumnsToPrint";
            this.lblColumnsToPrint.Size = new Size(0x41, 12);
            this.lblColumnsToPrint.TabIndex = 0x11;
            this.lblColumnsToPrint.Text = "待打印的列";
            this.btnOK.BackColor = SystemColors.Control;
            this.btnOK.Cursor = Cursors.Default;
            this.btnOK.FlatStyle = FlatStyle.System;
            this.btnOK.Font = new Font("新宋体", 9f);
            this.btnOK.ForeColor = SystemColors.ControlText;
            this.btnOK.Image = (Image) manager.GetObject("btnOK.Image");
            this.btnOK.ImageAlign = ContentAlignment.MiddleRight;
            this.btnOK.Location = new Point(0xe1, 0xe7);
            this.btnOK.Name = "btnOK";
            this.btnOK.RightToLeft = RightToLeft.No;
            this.btnOK.Size = new Size(0x3f, 0x17);
            this.btnOK.TabIndex = 14;
            this.btnOK.Text = "确定(&O)";
            this.btnOK.UseVisualStyleBackColor = false;
            this.btnOK.Click += new EventHandler(this.btnOK_Click);
            this.btnCancel.BackColor = SystemColors.Control;
            this.btnCancel.Cursor = Cursors.Default;
            this.btnCancel.DialogResult = DialogResult.Cancel;
            this.btnCancel.FlatStyle = FlatStyle.System;
            this.btnCancel.Font = new Font("新宋体", 9f);
            this.btnCancel.ForeColor = SystemColors.ControlText;
            this.btnCancel.Image = (Image) manager.GetObject("btnCancel.Image");
            this.btnCancel.Location = new Point(0x129, 0xe7);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.RightToLeft = RightToLeft.No;
            this.btnCancel.Size = new Size(0x3f, 0x17);
            this.btnCancel.TabIndex = 15;
            this.btnCancel.Text = "取消(&C)";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new EventHandler(this.btnCancel_Click);
            this.chklst.CheckOnClick = true;
            this.chklst.Font = new Font("新宋体", 9f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.chklst.FormattingEnabled = true;
            this.chklst.Location = new Point(8, 0x1a);
            this.chklst.Name = "chklst";
            this.chklst.Size = new Size(170, 0xe4);
            this.chklst.TabIndex = 13;
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x171, 0x10b);
            base.Controls.Add(this.chkFitToPageWidth);
            base.Controls.Add(this.lblTitle);
            base.Controls.Add(this.txtTitle);
            base.Controls.Add(this.gboxRowsToPrint);
            base.Controls.Add(this.lblColumnsToPrint);
            base.Controls.Add(this.btnOK);
            base.Controls.Add(this.btnCancel);
            base.Controls.Add(this.chklst);
            base.FormBorderStyle = FormBorderStyle.FixedToolWindow;
            base.Name = "PrintOptions";
            base.SizeGripStyle = SizeGripStyle.Show;
            base.StartPosition = FormStartPosition.CenterParent;
            this.Text = "打印选项";
            base.Load += new EventHandler(this.PrintOptions_Load);
            this.gboxRowsToPrint.ResumeLayout(false);
            this.gboxRowsToPrint.PerformLayout();
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        private void PrintOptions_Load(object sender, EventArgs e)
        {
            this.rdoAllRows.Checked = true;
            this.chkFitToPageWidth.Checked = true;
            this.txtTitle.Text = this.PrintTitle;
        }

        public bool FitToPageWidth
        {
            get
            {
                return this.chkFitToPageWidth.Checked;
            }
        }

        public bool PrintAllRows
        {
            get
            {
                return this.rdoAllRows.Checked;
            }
        }

        public string PrintTitle
        {
            get
            {
                return this.txtTitle.Text;
            }
            set
            {
                this.txtTitle.Text = value;
            }
        }
    }
}

