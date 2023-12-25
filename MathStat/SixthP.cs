using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows.Forms;

namespace MathStat
{
    public partial class SixthP : Form
    {
        public SixthP()
        {
            InitializeComponent();
        }

        private string path = @"C:\Users\Podor\Documents\GitHub\MathStat\MathStat\yx.png";
        private string path1 = @"C:\Users\Podor\Documents\GitHub\MathStat\MathStat\xy.png";
        private void SixthP_Load(object sender, EventArgs e)
        {
            Form1 form = new Form1();
            FifthP fifthP = new FifthP();
            string[] SixHeaders = { };
            form.DrawDataGrid(SixHeaders, new Point(20, 40),
                new Size((int)((40 + 50 * 9 + 70) * Form1.multiplier), (int)((179 + 20 + 15 + 30 * 7 + 10) * Form1.multiplier)), fifthP.dataGrid1, 9, 9);
            form.FillTable6(fifthP.dataGrid1);
            Color clrFont = new Color();
            Color clrBack = new Color();
            chart1.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            chart1.Series[0].BorderWidth = 5;
            chart1.Series[1].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            chart1.Series[1].BorderWidth = 5;
            if (Form1.darkMode)
            {
                clrBack = Color.FromArgb(255, 33, 31, 45);
                clrFont = Color.White;
            }
            else
            {
                clrFont = Color.Black;
                clrBack = Color.White;
            }
            this.BackColor = clrBack;
            label1.BackColor = clrBack;
            label1.ForeColor = clrFont;
            label1.Font = new System.Drawing.Font("Arial", (int)(9 * Form1.multiplier), System.Drawing.FontStyle.Bold);
            chart1.Size = new Size((int)((40 + 60 * 5+350) * Form1.multiplier), (int)((179+220) * Form1.multiplier));
            //form.DrawGisto(Form1.a, Form1.strX, chart1, true, new Point(20, 60), false);
            //form.FillGisto(Form1.a, chart1, true, false);
            double[] at;
            int[] bt;
            at = Form1.weight.ToArray();
            bt = Form1.height.ToArray();
            for (int i = 0; i < 8; i++)
            {
                double tX, tY;
                tX = Form1.rв * (Form1.sx / Form1.sy) * (Form1.b[i] - Form1.y_) + Form1.x_;
                tY = Form1.rв * (Form1.sy / Form1.sx) * (Form1.a[i] - Form1.x_) + Form1.y_;
                chart1.Series[0].Points.AddXY(Form1.a[i], tY);
                chart1.Series[1].Points.AddXY(tX, Form1.b[i]);
            }
            int tempN = Form1.N;
            for (int i = 0; i < tempN; i++)
            {
                chart1.Series[2].Points.AddXY(at[i], bt[i]);
            }
            chart1.Series[3].Points.AddXY(Form1.x_, Form1.y_);
            chart1.ChartAreas[0].AxisX.Minimum = Form1.a[0]-2;
            chart1.ChartAreas[0].AxisX.Maximum = Form1.a[7]+4;
            chart1.ChartAreas[0].AxisY.Minimum = Form1.b[0]-3;
            chart1.ChartAreas[0].AxisY.Maximum = Form1.b[7];
            label1.Text = "Линии регресии и диаграмма рассеивания";
            pic1.Location = new Point(chart1.Location.X + chart1.Width-10, 40);
            pic2.Location = new Point(chart1.Location.X + chart1.Width-10, 40+200);
            pic1.Image = System.Drawing.Image.FromFile(path);
            pic2.Image = System.Drawing.Image.FromFile(path1);
            pic1.Size = new Size(615, 110);
            pic2.Size = new Size(615, 110);
        }
    }
}
