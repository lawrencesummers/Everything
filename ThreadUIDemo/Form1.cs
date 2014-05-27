using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ThreadUIDemo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            //File

               // FileInfo

            int level = PersonLevel.One;

            GetAge();
        }
        private void GetAge()
        {
            return;
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }

    public static class PersonLevel
    {
        public static int One = 1;
        public static int GetLevel(int a)
        { 
            return a++; 
        }
    }
}
