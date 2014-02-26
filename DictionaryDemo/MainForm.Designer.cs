namespace DictionaryDemo
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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.字典号 = new System.Windows.Forms.Label();
            this.txtDictionaryNO = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtDictionaryName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtValue = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtDisplay = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtRemark = new System.Windows.Forms.TextBox();
            this.btnAdd = new System.Windows.Forms.Button();
            this.cboGroup = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtDicNO = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtDicName = new System.Windows.Forms.TextBox();
            this.btnAddDic = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.colDictionaryNO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDisplay = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colRemark = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colDictionaryNO,
            this.colName,
            this.colValue,
            this.colDisplay,
            this.colRemark});
            this.dataGridView1.Location = new System.Drawing.Point(209, 137);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(608, 316);
            this.dataGridView1.TabIndex = 0;
            // 
            // 字典号
            // 
            this.字典号.AutoSize = true;
            this.字典号.Location = new System.Drawing.Point(51, 146);
            this.字典号.Name = "字典号";
            this.字典号.Size = new System.Drawing.Size(41, 12);
            this.字典号.TabIndex = 1;
            this.字典号.Text = "字典号";
            this.字典号.Click += new System.EventHandler(this.字典号_Click);
            // 
            // txtDictionaryNO
            // 
            this.txtDictionaryNO.Location = new System.Drawing.Point(99, 143);
            this.txtDictionaryNO.Name = "txtDictionaryNO";
            this.txtDictionaryNO.Size = new System.Drawing.Size(100, 21);
            this.txtDictionaryNO.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(39, 173);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "字典名称";
            // 
            // txtDictionaryName
            // 
            this.txtDictionaryName.Location = new System.Drawing.Point(99, 170);
            this.txtDictionaryName.Name = "txtDictionaryName";
            this.txtDictionaryName.Size = new System.Drawing.Size(100, 21);
            this.txtDictionaryName.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(51, 200);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 1;
            this.label3.Text = "字典值";
            // 
            // txtValue
            // 
            this.txtValue.Location = new System.Drawing.Point(99, 197);
            this.txtValue.Name = "txtValue";
            this.txtValue.Size = new System.Drawing.Size(100, 21);
            this.txtValue.TabIndex = 2;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(15, 227);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(77, 12);
            this.label4.TabIndex = 1;
            this.label4.Text = "字典展示名称";
            // 
            // txtDisplay
            // 
            this.txtDisplay.Location = new System.Drawing.Point(99, 224);
            this.txtDisplay.Name = "txtDisplay";
            this.txtDisplay.Size = new System.Drawing.Size(100, 21);
            this.txtDisplay.TabIndex = 2;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(63, 254);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(29, 12);
            this.label5.TabIndex = 1;
            this.label5.Text = "备注";
            // 
            // txtRemark
            // 
            this.txtRemark.Location = new System.Drawing.Point(99, 251);
            this.txtRemark.Name = "txtRemark";
            this.txtRemark.Size = new System.Drawing.Size(100, 21);
            this.txtRemark.TabIndex = 2;
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(99, 278);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(100, 23);
            this.btnAdd.TabIndex = 3;
            this.btnAdd.Text = "新增字典项";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // cboGroup
            // 
            this.cboGroup.FormattingEnabled = true;
            this.cboGroup.Location = new System.Drawing.Point(99, 111);
            this.cboGroup.Name = "cboGroup";
            this.cboGroup.Size = new System.Drawing.Size(100, 20);
            this.cboGroup.TabIndex = 4;
            this.cboGroup.SelectedIndexChanged += new System.EventHandler(this.cboGroup_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(39, 323);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 12);
            this.label6.TabIndex = 1;
            this.label6.Text = "字典项号";
            // 
            // txtDicNO
            // 
            this.txtDicNO.Location = new System.Drawing.Point(99, 320);
            this.txtDicNO.Name = "txtDicNO";
            this.txtDicNO.Size = new System.Drawing.Size(100, 21);
            this.txtDicNO.TabIndex = 2;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(39, 350);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 12);
            this.label7.TabIndex = 1;
            this.label7.Text = "字典名称";
            // 
            // txtDicName
            // 
            this.txtDicName.Location = new System.Drawing.Point(99, 347);
            this.txtDicName.Name = "txtDicName";
            this.txtDicName.Size = new System.Drawing.Size(100, 21);
            this.txtDicName.TabIndex = 2;
            // 
            // btnAddDic
            // 
            this.btnAddDic.Location = new System.Drawing.Point(99, 384);
            this.btnAddDic.Name = "btnAddDic";
            this.btnAddDic.Size = new System.Drawing.Size(100, 23);
            this.btnAddDic.TabIndex = 3;
            this.btnAddDic.Text = "新增字典";
            this.btnAddDic.UseVisualStyleBackColor = true;
            this.btnAddDic.Click += new System.EventHandler(this.btnAddDic_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(39, 114);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "字典列表";
            // 
            // colDictionaryNO
            // 
            this.colDictionaryNO.DataPropertyName = "DictionaryNO";
            this.colDictionaryNO.HeaderText = "字典号";
            this.colDictionaryNO.Name = "colDictionaryNO";
            // 
            // colName
            // 
            this.colName.DataPropertyName = "Name";
            this.colName.HeaderText = "字典名称";
            this.colName.Name = "colName";
            // 
            // colValue
            // 
            this.colValue.DataPropertyName = "Value";
            this.colValue.HeaderText = "字典值";
            this.colValue.Name = "colValue";
            // 
            // colDisplay
            // 
            this.colDisplay.DataPropertyName = "Display";
            this.colDisplay.HeaderText = "字典展示名称";
            this.colDisplay.Name = "colDisplay";
            // 
            // colRemark
            // 
            this.colRemark.DataPropertyName = "Remark";
            this.colRemark.HeaderText = "备注";
            this.colRemark.Name = "colRemark";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(823, 465);
            this.Controls.Add(this.cboGroup);
            this.Controls.Add(this.btnAddDic);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.txtDicName);
            this.Controls.Add(this.txtRemark);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtDicNO);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtDisplay);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtValue);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtDictionaryName);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtDictionaryNO);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.字典号);
            this.Controls.Add(this.dataGridView1);
            this.Name = "MainForm";
            this.Text = "MainForm";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label 字典号;
        private System.Windows.Forms.TextBox txtDictionaryNO;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtDictionaryName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtValue;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtDisplay;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtRemark;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.ComboBox cboGroup;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtDicNO;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtDicName;
        private System.Windows.Forms.Button btnAddDic;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDictionaryNO;
        private System.Windows.Forms.DataGridViewTextBoxColumn colName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colValue;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDisplay;
        private System.Windows.Forms.DataGridViewTextBoxColumn colRemark;
    }
}

