namespace LearnThread
{
    partial class Form2
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lblBWMessage = new System.Windows.Forms.Label();
            this.pbBW = new System.Windows.Forms.ProgressBar();
            this.btnStartBW = new System.Windows.Forms.Button();
            this.btnCancelBW = new System.Windows.Forms.Button();
            this.btnAbortThread = new System.Windows.Forms.Button();
            this.btnStartThread = new System.Windows.Forms.Button();
            this.pbThread = new System.Windows.Forms.ProgressBar();
            this.lblThreadMessage = new System.Windows.Forms.Label();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnCancelBW);
            this.groupBox1.Controls.Add(this.btnStartBW);
            this.groupBox1.Controls.Add(this.pbBW);
            this.groupBox1.Controls.Add(this.lblBWMessage);
            this.groupBox1.Location = new System.Drawing.Point(12, 53);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(373, 139);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "BackgroundWorker组件";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnAbortThread);
            this.groupBox2.Controls.Add(this.btnStartThread);
            this.groupBox2.Controls.Add(this.pbThread);
            this.groupBox2.Controls.Add(this.lblThreadMessage);
            this.groupBox2.Location = new System.Drawing.Point(12, 198);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(373, 142);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Thread线程";
            // 
            // lblBWMessage
            // 
            this.lblBWMessage.AutoSize = true;
            this.lblBWMessage.Location = new System.Drawing.Point(19, 28);
            this.lblBWMessage.Name = "lblBWMessage";
            this.lblBWMessage.Size = new System.Drawing.Size(77, 12);
            this.lblBWMessage.TabIndex = 0;
            this.lblBWMessage.Text = "lblBWMessage";
            // 
            // pbBW
            // 
            this.pbBW.Location = new System.Drawing.Point(6, 60);
            this.pbBW.Name = "pbBW";
            this.pbBW.Size = new System.Drawing.Size(357, 23);
            this.pbBW.TabIndex = 1;
            // 
            // btnStartBW
            // 
            this.btnStartBW.Location = new System.Drawing.Point(21, 104);
            this.btnStartBW.Name = "btnStartBW";
            this.btnStartBW.Size = new System.Drawing.Size(75, 23);
            this.btnStartBW.TabIndex = 2;
            this.btnStartBW.Text = "StartUp";
            this.btnStartBW.UseVisualStyleBackColor = true;
            this.btnStartBW.Click += new System.EventHandler(this.btnStartBW_Click);
            // 
            // btnCancelBW
            // 
            this.btnCancelBW.Location = new System.Drawing.Point(120, 104);
            this.btnCancelBW.Name = "btnCancelBW";
            this.btnCancelBW.Size = new System.Drawing.Size(75, 23);
            this.btnCancelBW.TabIndex = 3;
            this.btnCancelBW.Text = "Cancel";
            this.btnCancelBW.UseVisualStyleBackColor = true;
            this.btnCancelBW.Click += new System.EventHandler(this.btnCancelBW_Click);
            // 
            // btnAbortThread
            // 
            this.btnAbortThread.Location = new System.Drawing.Point(120, 107);
            this.btnAbortThread.Name = "btnAbortThread";
            this.btnAbortThread.Size = new System.Drawing.Size(75, 23);
            this.btnAbortThread.TabIndex = 7;
            this.btnAbortThread.Text = "Cancel";
            this.btnAbortThread.UseVisualStyleBackColor = true;
            this.btnAbortThread.Click += new System.EventHandler(this.btnAbortThread_Click);
            // 
            // btnStartThread
            // 
            this.btnStartThread.Location = new System.Drawing.Point(21, 107);
            this.btnStartThread.Name = "btnStartThread";
            this.btnStartThread.Size = new System.Drawing.Size(75, 23);
            this.btnStartThread.TabIndex = 6;
            this.btnStartThread.Text = "StartUp";
            this.btnStartThread.UseVisualStyleBackColor = true;
            this.btnStartThread.Click += new System.EventHandler(this.btnStartThread_Click);
            // 
            // pbThread
            // 
            this.pbThread.Location = new System.Drawing.Point(6, 63);
            this.pbThread.Name = "pbThread";
            this.pbThread.Size = new System.Drawing.Size(357, 23);
            this.pbThread.TabIndex = 5;
            // 
            // lblThreadMessage
            // 
            this.lblThreadMessage.AutoSize = true;
            this.lblThreadMessage.Location = new System.Drawing.Point(19, 31);
            this.lblThreadMessage.Name = "lblThreadMessage";
            this.lblThreadMessage.Size = new System.Drawing.Size(101, 12);
            this.lblThreadMessage.TabIndex = 4;
            this.lblThreadMessage.Text = "lblThreadMessage";
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(18, 12);
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(120, 21);
            this.numericUpDown1.TabIndex = 1;
            this.numericUpDown1.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(403, 352);
            this.Controls.Add(this.numericUpDown1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "Form1";
            this.Text = "Update UI";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label lblBWMessage;
        private System.Windows.Forms.Button btnCancelBW;
        private System.Windows.Forms.Button btnStartBW;
        private System.Windows.Forms.ProgressBar pbBW;
        private System.Windows.Forms.Button btnAbortThread;
        private System.Windows.Forms.Button btnStartThread;
        private System.Windows.Forms.ProgressBar pbThread;
        private System.Windows.Forms.Label lblThreadMessage;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
    }
}

