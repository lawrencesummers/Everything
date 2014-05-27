using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraCharts;

namespace DEVExpressChartDemo
{
    public partial class SimpleDemo : Form
    {
        public SimpleDemo()
        {
            InitializeComponent();
        }
        DataTable dataTb1 = null; //工资
        DataTable dataTb2 = null; //奖金


        protected override void OnLoad(EventArgs e)
        {
            SetTable(ref dataTb1);
            SetTable(ref dataTb2);


            this.chartControl1.Series.Clear();
            this.chartControl1.Text = "xxx";


            Series S1 = new Series("王总工资", ViewType.Bar);
            //S1.ArgumentScaleType = ScaleType.Numerical; //X轴的数据类型
            

            Series S2 = new Series("王总奖金", ViewType.Spline);
            //S2.ArgumentScaleType = ScaleType.Numerical; //X轴的数据类型


            S1.DataSource = dataTb1;
            S2.DataSource = dataTb2;


            //X轴的数据字段

            S1.ArgumentDataMember = "Month";
            S2.ArgumentDataMember = "Month";

            //Y轴的数据字段

            S1.ValueDataMembers[0] = "GongZi";
            S2.ValueDataMembers[0] = "GongZi";

            this.chartControl1.Series.Add(S1);
            this.chartControl1.Series.Add(S2);



            base.OnLoad(e);
        }

        private void SetTable(ref DataTable dataTb)
        {

            dataTb = new DataTable("UserInfo");

            DataColumn col = new DataColumn("UserID", typeof(int));
            dataTb.Columns.Add(col);

            DataColumn colName = new DataColumn("Month", typeof(string));
            dataTb.Columns.Add(colName);

            DataColumn colgongzi = new DataColumn("GongZi", typeof(int));//工资
            dataTb.Columns.Add(colgongzi);

            for (int i = 1; i < 9; i++)
            {
                Random random = new Random();

                DataRow dr = dataTb.NewRow();
                dr[0] = i;
                dr[1] = i.ToString() + "月";
                System.Threading.Thread.Sleep(100);
                dr[2] = random.Next(200, 1000);
                dataTb.Rows.Add(dr);
            }

        }


    }
}
