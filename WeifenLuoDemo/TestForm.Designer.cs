namespace WeifenLuoDemo
{
    partial class TestForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.monthCalendar1 = new System.Windows.Forms.MonthCalendar();
            this.xNumericUpDown2 = new Hundsun.UFT.Windows.Forms.XNumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.xNumericUpDown2)).BeginInit();
            this.SuspendLayout();
            // 
            // monthCalendar1
            // 
            this.monthCalendar1.Location = new System.Drawing.Point(48, 71);
            this.monthCalendar1.Name = "monthCalendar1";
            this.monthCalendar1.TabIndex = 0;
            // 
            // xNumericUpDown2
            // 
            this.xNumericUpDown2.AsString = "0";
            this.xNumericUpDown2.DisabledBackColor = System.Drawing.SystemColors.Control;
            this.xNumericUpDown2.DisabledForeColor = System.Drawing.SystemColors.GrayText;
            this.xNumericUpDown2.Location = new System.Drawing.Point(389, 224);
            this.xNumericUpDown2.Name = "xNumericUpDown2";
            this.xNumericUpDown2.Size = new System.Drawing.Size(120, 21);
            this.xNumericUpDown2.TabIndex = 2;
            // 
            // TestForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(638, 474);
            this.Controls.Add(this.xNumericUpDown2);
            this.Controls.Add(this.monthCalendar1);
            this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Name = "TestForm";
            this.Text = "TestForm";
            ((System.ComponentModel.ISupportInitialize)(this.xNumericUpDown2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.MonthCalendar monthCalendar1;
        private Hundsun.UFT.Windows.Forms.XNumericUpDown xNumericUpDown2;
    }
}