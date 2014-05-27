/*
 * Created by SharpDevelop.
 * User: guanxiang
 * Date: 2013/12/6
 * Time: 9:45
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace WCFPublishService
{
	partial class MainForm
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		
		/// <summary>
		/// Disposes resources used by the form.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing) {
				if (components != null) {
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}
		
		/// <summary>
		/// This method is required for Windows Forms designer support.
		/// Do not change the method contents inside the source code editor. The Forms designer might
		/// not be able to load this method if it was changed manually.
		/// </summary>
		private void InitializeComponent()
		{
            this.lsbSubscribers = new System.Windows.Forms.ListBox();
            this.btnNotify = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lsbSubscribers
            // 
            this.lsbSubscribers.FormattingEnabled = true;
            this.lsbSubscribers.ItemHeight = 12;
            this.lsbSubscribers.Location = new System.Drawing.Point(42, 12);
            this.lsbSubscribers.Name = "lsbSubscribers";
            this.lsbSubscribers.Size = new System.Drawing.Size(363, 244);
            this.lsbSubscribers.TabIndex = 0;
            // 
            // btnNotify
            // 
            this.btnNotify.Location = new System.Drawing.Point(178, 291);
            this.btnNotify.Name = "btnNotify";
            this.btnNotify.Size = new System.Drawing.Size(75, 23);
            this.btnNotify.TabIndex = 1;
            this.btnNotify.Text = "Notify";
            this.btnNotify.UseVisualStyleBackColor = true;
            this.btnNotify.Click += new System.EventHandler(this.btnNotify_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(488, 345);
            this.Controls.Add(this.btnNotify);
            this.Controls.Add(this.lsbSubscribers);
            this.Name = "MainForm";
            this.Text = "WCFPublisher";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResumeLayout(false);

		}

        private System.Windows.Forms.ListBox lsbSubscribers;
        private System.Windows.Forms.Button btnNotify;
	}
}
