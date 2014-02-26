namespace OutLookBarDemo
{
    partial class MainForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.outlookBar = new OutLookBar.OutlookBar();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // outlookBar
            // 
            this.outlookBar.ButtonHeight = 25;
            this.outlookBar.Location = new System.Drawing.Point(1, 0);
            this.outlookBar.Name = "outlookBar";
            this.outlookBar.SelectedBand = 0;
            this.outlookBar.Size = new System.Drawing.Size(139, 371);
            this.outlookBar.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(214, 77);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "label1";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(405, 370);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.outlookBar);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private OutLookBar.OutlookBar outlookBar;
        private System.Windows.Forms.Label label1;
    }
}

