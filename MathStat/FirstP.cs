﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows.Forms;

namespace MathStat
{
    public partial class FirstP : Form
    {
        public FirstP()
        {
            InitializeComponent();
        }

        private void FirstP_Load(object sender, EventArgs e)
        {
            InitTable();
            chart1.Series[0].IsVisibleInLegend = false;
            chart2.Series[0].IsVisibleInLegend = false;
            chart3.Series[0].IsVisibleInLegend = false;
            chart4.Series[0].IsVisibleInLegend = false;

            chart1.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            chart1.Series[0].BorderWidth = 5;
            chart3.Series[0].BorderWidth = 5;
            chart3.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            chart2.Series[0]["PointWidth"] = "1";
            chart4.Series[0]["PointWidth"] = "1";

        }

        private void InitTable()
        {
            Form1 form = new Form1();
            //form.FirstTable();
            Color clrFont = new Color();
            Color clrBack = new Color();
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
            label2.BackColor = clrBack;
            label3.BackColor = clrBack;
            label4.BackColor = clrBack;
            label5.BackColor = clrBack;
            label6.BackColor = clrBack;
            labelinfo.BackColor = clrBack;
            labelinfo1.BackColor = clrBack;
            label1.ForeColor = clrFont;
            label2.ForeColor = clrFont;
            label3.ForeColor = clrFont;
            label4.ForeColor = clrFont;
            label5.ForeColor = clrFont;
            labelinfo.ForeColor = clrFont;
            labelinfo1.ForeColor = clrFont;
            form.DrawDataGrid(Form1.FirstHeaders, new Point(20, 40), new Size((int)((40 + 60 * 5) * Form1.multiplier), (int)(179 * Form1.multiplier)), dataGrid1, 8, 6);
            form.FillTableX(dataGrid1);
            form.DrawGisto(Form1.a, Form1.strX, chart1, false, new Point(dataGrid1.Location.X + 20 + dataGrid1.Width, dataGrid1.Location.Y), false);
            form.FillGisto(Form1.a, chart1, false, false);
            dataGrid1.ColumnHeadersHeight = (int)(25 * Form1.multiplier);
            Form1.strX[1] = "n1/n*hx";
            form.DrawGisto(Form1.a, Form1.strX, chart2, true, new Point(chart1.Location.X + 20 + chart1.Width, chart1.Location.Y), false);
            form.FillGisto(Form1.a, chart2, true, false);

            label1.Location = new Point(dataGrid1.Location.X, dataGrid1.Location.Y - (int)(20 *Form1.multiplier));
            label1.Text = "Таблица 2 – Группированный ряд для X";
            label2.Location = new Point(chart1.Location.X, chart1.Location.Y - (int)(20 * Form1.multiplier));
            label2.Text = "Полигон относительных частот";
            label3.Location = new Point(chart2.Location.X, chart2.Location.Y - (int)(20 * Form1.multiplier));
            label3.Text = "Гистрограмма относительных частот";
            label1.Font = new System.Drawing.Font("Arial", (int)(9* Form1.multiplier), System.Drawing.FontStyle.Bold);
            label2.Font = new System.Drawing.Font("Arial", (int)(9* Form1.multiplier), System.Drawing.FontStyle.Bold);
            label3.Font = new System.Drawing.Font("Arial", (int)(9* Form1.multiplier), System.Drawing.FontStyle.Bold);
            label4.Font = new System.Drawing.Font("Arial", (int)(9* Form1.multiplier), System.Drawing.FontStyle.Bold);
            label5.Font = new System.Drawing.Font("Arial", (int)(9* Form1.multiplier), System.Drawing.FontStyle.Bold);
            label6.Font = new System.Drawing.Font("Arial", (int)(9* Form1.multiplier), System.Drawing.FontStyle.Bold);
            labelinfo.Font = new System.Drawing.Font("Arial", (int)(9* Form1.multiplier), System.Drawing.FontStyle.Bold);
            labelinfo1.Font = new System.Drawing.Font("Arial", (int)(9* Form1.multiplier), System.Drawing.FontStyle.Bold);

            //form.SecondTable();
            form.DrawDataGrid(Form1.SecondHeaders, new Point(20, dataGrid1.Location.Y + 40 + dataGrid1.Height), new Size((int)((40 + 60 * 5) * Form1.multiplier), (int)(179 * Form1.multiplier)), dataGrid2, 8, 6);
            form.FillTableY(dataGrid2);
            form.DrawGisto(Form1.b, Form1.strY, chart3, false, new Point(dataGrid2.Location.X + 20 + dataGrid2.Width, dataGrid2.Location.Y), true);
            dataGrid2.ColumnHeadersHeight = (int)(25 * Form1.multiplier);

            form.FillGisto(Form1.b, chart3, false, true);
            Form1.strY[1] = "m1/n*hy";
            form.DrawGisto(Form1.b, Form1.strY, chart4, true, new Point(chart3.Location.X + 20 + chart3.Width, chart3.Location.Y), true);

            form.FillGisto(Form1.b, chart4, true, true);
            label4.Location = new Point(dataGrid2.Location.X, dataGrid2.Location.Y - (int)(20 * Form1.multiplier));
            label4.Text = "Таблица 3 – Группированный ряд для Y";
            label5.Location = new Point(chart3.Location.X, chart3.Location.Y - (int)(20 * Form1.multiplier));
            label5.Text = "Полигон относительных частот";
            label6.Location = new Point(chart4.Location.X, chart4.Location.Y - (int)(20 * Form1.multiplier));
            label6.Text = "Гистрограмма относительных частот";
            labelinfo.Text = "xmax = " + Form1.maxW + ", xmin = " + Form1.minW + "\nRx = xmax - xmin = " 
                + Form1.maxW + " - " + Form1.minW + " = " + Form1.RxW + "\n" + 
                "r = 7, hx = Rx / r = " + Form1.RxW + "/7=" + Math.Round(Form1.RxW/7,3).ToString() + "\n"
                +"Для удобства возьмём hx = "+ Form1.hxW + "\nТогда расширение промежутка разбиения составит ("
                + Form1.hxW+ " - " + Math.Round(Form1.RxW / 7, 3).ToString() + ")*7 = " + Form1.extensionW+
                "\nДля определения границ интервалов  [ai-1, i) можно для удобства (чтобы\nграницы " +
                "интервалов стали целыми числами) сдвинуть начало 1-го интервала,\n например, в точку a0 = xmin - "+
                Math.Round((Form1.minW - Form1.a[0]), 1)  + " = "+ Form1.minW + " - " + Math.Round((Form1.minW - Form1.a[0]), 1) + " = " + Form1.a[0];
            labelinfo.Location = new Point(20, dataGrid2.Location.Y + dataGrid2.Height + 15);

            labelinfo1.Text = "ymax = " + Form1.maxH + ", ymin = " + Form1.minH + "\nRy = ymax - ymin = "
                + Form1.maxH + " - " + Form1.minH + " = " + Form1.RyH + "\n" +
                "r = 7, hy = Ry / r = " + Form1.RyH + "/7=" + Math.Round(((double)Form1.RyH / 7), 3).ToString() + "\n"
                + "Для удобства возьмём hy = " + Form1.hyH + "\nТогда расширение промежутка разбиения составит ("
                + Form1.hyH + " - " + Math.Round(((double)Form1.RyH / 7), 3).ToString() + ")*7 = " + Form1.extensionH +
                "\nДля определения границ интервалов  [bi-1, i) можно для удобства (чтобы\nграницы " +
                "интервалов стали целыми числами) сдвинуть начало 1-го интервала,\n например, в точку b0 = ymin - " +
                Math.Floor(Form1.extensionH / 2) + " = " + Form1.minH + " - " + Math.Floor(Form1.extensionH / 2) + " = " + Form1.b[0];
            labelinfo1.Location = new Point(chart2.Location.X-150, dataGrid2.Location.Y + dataGrid2.Height + 15);
        }
    }
}
