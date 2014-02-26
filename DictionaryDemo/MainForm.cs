using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DictionaryDemo
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            using (var context = new EverythingContext())
            {
                var sysDictionays = from db in context.SysDictionaries select db;
                int count = sysDictionays.Count();
                List<SysDictionary> infoList = sysDictionays.ToList();
                this.dataGridView1.DataSource = infoList;
                var cboSource = (from db in sysDictionays select new { db.DictionaryNO, db.Name }).Distinct().ToList();
                this.cboGroup.DataSource = cboSource;
                this.cboGroup.ValueMember = "DictionaryNO";
                this.cboGroup.DisplayMember = "Name";
            }
            base.OnLoad(e);
        }

        private void btnAddDic_Click(object sender, EventArgs e)
        {
            string dicNO = txtDicNO.Text;
            string dicName = txtDicName.Text;
            using (var context = new EverythingContext())
            {
                var exit = context.SysDictionaries.Any(x => x.DictionaryNO == dicNO);
                if (exit)
                {
                    MessageBox.Show("ExitDicNO");
                }
                else
                {
                    var dictionary = new SysDictionary
                    {
                        ID = Guid.NewGuid().ToString(),
                        DictionaryNO = dicNO,
                        Name = dicName,
                        Value = -1,
                        Display = dicName
                    };
                    context.SysDictionaries.Add(dictionary);
                    if (context.SaveChanges() == 1)
                    {
                        MessageBox.Show("success");
                    }
                }
            }
        }

        private void 字典号_Click(object sender, EventArgs e)
        {

        }

        private void cboGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboGroup.SelectedIndex != -1)
            {
                txtDictionaryNO.Text = cboGroup.SelectedValue.ToString();
                txtDictionaryName.Text = cboGroup.Text.ToString();
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            using (var context = new EverythingContext())
            {
               var value = Int32.Parse(txtValue.Text);
               if (context.SysDictionaries.Any(x => x.DictionaryNO == txtDictionaryNO.Text && x.Value == value))
                {
                    MessageBox.Show("the dictionay"+txtDictionaryName.Text+"already has this value");
                    return;
                }
                var item = new SysDictionary
                {
                    ID = Guid.NewGuid().ToString(),
                    DictionaryNO = txtDictionaryNO.Text,
                    Name = txtDictionaryName.Text,
                    Value = Int32.Parse(txtValue.Text),
                    Display = txtDisplay.Text,
                    Remark = txtRemark.Text
                };
                context.SysDictionaries.Add(item);
                if (context.SaveChanges() == 1)
                {
                    MessageBox.Show("success");
                }
            }
        }
    }
}
