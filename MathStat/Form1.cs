using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace MathStat
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        Queue <Double> weight = new Queue <Double> ();
        Queue <Int32> height = new Queue <Int32> ();
        string path = @"C:\Users\Podor\Documents\GitHub\MathStat\MathStat\data.txt";
        Chart[] chart = new Chart[4];

        double minW, maxW, RxW;
        int minH, maxH, RyH, hyH, hxW;
        int rangeH;
        double rangeW;
        int[] a = new int[8];
        int[] b = new int[8];
        double extensionW;
        double extensionH;

        double[] ui = new double[7];
        double[] vi = new double[7];

        string[] FirstHeaders = { "[ai-1, ai)", "xi*", "ni", "ni/n", "ni/n*hx" };
        string[] SecondHeaders = { "[bi-1, bi)", "yi*", "mi", "mi/n", "mi/n*hy" };
        string[] ThirdHeaders = { "ui", "ni", "uini", "ui^2", "vi", "mi", "vimi", "vi^2mu"};
        string[] FourHeaders = { "[ai-1, ai)", "ni", "zi=(ai-x_)/sx", "Ф(zi)", "pi", "npi", "(ni-npi)^2/npi"};

        DataGridView[] dataGrid = new DataGridView[4];

        Label[] nameTables = new Label[5];
        Label testLb = new Label();

        private void Form1_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < 4; i++)
            {
                chart[i] = new Chart();
                chart[i].Series.Add("");
                chart[i].ChartAreas.Add("");
            }
            for (int i = 0; i < 5; i++)
            {
                nameTables[i] = new Label();
                Controls.Add(nameTables[i]);
            }
            chart[0].Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            chart[0].Series[0].BorderWidth = 5;
            chart[2].Series[0].BorderWidth = 5;
            chart[2].Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            chart[1].Series[0]["PointWidth"] = "1";
            chart[3].Series[0]["PointWidth"] = "1";

            testLb.Location = new Point(980, 10);
            testLb.Size = new Size(100, 40);
            testLb.Text = "Сделал\nКостя Дехант ПИ11";
            Controls.Add(testLb);

            nameTables[0].Location = new Point(20, 20); //Текст над таблицей 2
            nameTables[1].Location = new Point(360+ 20, 20); //Текст над таблицей 3
            nameTables[2].Location = new Point(360+ 20+360, 20); //Текст над таблицей 4
            
            
            
            //chart[1].Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            this.Size = new Size(1440,720);
            dataGrid[0] = new DataGridView();
            dataGrid[1] = new DataGridView();
            dataGrid[2] = new DataGridView();
            dataGrid[3] = new DataGridView();
            ReadFile(path);
            
            string[] strX = { "x", "ni/n" };
            string[] strY = { "y", "mi/n" };
            
            FirstTable();
            FillTableX(dataGrid[0]);
            DrawGisto(a, strX, chart[0], false, new Point(20, 179 + 70), false);
            strX[1] = "n1/n*hx";
            DrawGisto(a, strX, chart[1], true, new Point(20, 179 + 70 + 179 + 20), false);
            
            SecondTable();
            FillTableY(dataGrid[1]);
            DrawGisto(b, strY, chart[2], false, new Point(340+40, 179 + 70),true);
            strY[1] = "m1/n*hy";
            DrawGisto(b, strY, chart[3], true, new Point(340 + 40, 179 + 70 + 179 + 20), true);

            ThirdTable();
            FillTable3(dataGrid[2]);


            TableFour();

        }


    //Таблица 2, но первая для подсчётов (группированный ряд для X)
    private void FirstTable()
        {
            nameTables[0].Text = "Таблица 2";
            minW = weight.Min(); 
            maxW = weight.Max(); 
            RxW = maxW - minW;
            hxW = Convert.ToInt32(Math.Floor(RxW/ 7)+1);
            extensionW = Math.Round((hxW - (RxW / 7))*7, 2);
            rangeW = Math.Round(minW - Math.Floor(minW),1);
            a[0] = Convert.ToInt32(minW - rangeW);
            int downshift;
            downshift = Convert.ToInt32(Math.Floor(extensionW/2)); 
            //label1.Text = extensionW.ToString();
            if (extensionW >= 2)
                a[0] -= downshift;
            for (int i = 1; i <= 7; i++)
            {
                a[i] = a[i - 1] + hxW;
            }
            DrawDataGrid(FirstHeaders, new Point(20, 50), new Size(40 + 60 * 5, 179), dataGrid[0], 8, 6);
        }

        private void SecondTable()
        {
            nameTables[1].Text = "Таблица 3";
            minH = height.Min();
            maxH = height.Max();
            RyH = maxH - minH;
            hyH = Convert.ToInt32(Math.Ceiling((double)RyH / 7));
            extensionH = (hyH - ((double)RyH / 7))*7;
            b[0] = minH;
            int downshift;
            downshift = Convert.ToInt32(Math.Floor(extensionH / 2));
            if (extensionH >= 2)
                b[0] -= downshift;
            for (int i = 1; i <= 7; i++)
            {
                b[i] = b[i - 1] + hyH;
            }
            DrawDataGrid(SecondHeaders, new Point(20+20+340, 50), new Size(40 + 60 * 5, 179), dataGrid[1], 8,6);
        }

        private void ThirdTable()
        {
            nameTables[2].Text = "Таблица 4";
            DrawDataGrid(ThirdHeaders, new Point(20 + 20 + 340 + 340 + 20, 50), new Size(40 + 60 * 8, 179+21), dataGrid[2], 9, 9);
        }

        private void TableFour()
        {
            DrawDataGrid(FourHeaders, new Point(20 + 20 + 340 + 340 + 20, 50 + 240), 
                new Size(40 + 60 * 8-32, 179+20), dataGrid[3], 9, 8);
            dataGrid[3].Columns[3].Width += 10;
            dataGrid[3].Columns[7].Width += 15;
            FillTable4(dataGrid[3]);
        }

        //ni подсчёт
        private int FindNumsW(double left, double right)
        {
            double[] arr = weight.ToArray();
            int arrlen = arr.Length;
            int count = 0;
            for (int i = 0; i < arrlen; i++)
            {
                if (arr[i] >= left && arr[i] < right)
                    count++;
            }
            return count;
        }

        private int FindNumsH(int left, int right)
        {
            int[] arr = height.ToArray();
            int arrlen = arr.Length;
            int count = 0;
            for (int i = 0; i < arrlen; i++)
            {
                if (arr[i] >= left && arr[i] < right)
                    count++;
            }
            return count;
        }

        private void DrawDataGrid(string[] str, Point point, Size size, DataGridView data, int rows, int columns)
        {
            //data = new DataGridView();
            data.Location = point;
            data.Size = size;
            data.RowCount = rows;
            data.ColumnCount = columns;
            
            data.Columns[0].HeaderText = "i";
            for (int i = 1; i <= str.Length; i++)
            {
                data.Columns[i].HeaderText = str[i-1];
            }
            data.RowHeadersVisible = false;
            data.Columns[0].Width = 40;
            for (int i = 1; i <= str.Length; i++)
            {
                data.Columns[i].Width = 60;
            }
            data.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            data.AllowUserToAddRows = false;
            data.AllowUserToDeleteRows = false;
            data.ReadOnly = true;
            data.AllowUserToResizeRows = false;
            data.AllowUserToResizeColumns = false;
            data.ScrollBars = ScrollBars.None;
            data.Enabled = false;
            Controls.Add(data);
            data.ClearSelection();
        }

        private void FillTable4(DataGridView dataGrid1)
        {
            for (int i = 0; i < 8; i++)
            {
                dataGrid[3].Rows[i].Cells[0].Value = Convert.ToString(i + 1);
            }
        }
        private void FillTableX(DataGridView dataGrid1)
        {
            for (int i = 0; i < 7; i++)
            {
                int ni = FindNumsW(a[i], a[i + 1]);
                dataGrid1.Rows[i].Cells[0].Value = Convert.ToString(i + 1);
                dataGrid1.Rows[i].Cells[1].Value = "[" + Convert.ToString(a[i]) + ", " + Convert.ToString(a[i + 1]);
                if (i == 6)
                    dataGrid1.Rows[i].Cells[1].Value += "]";
                else
                    dataGrid1.Rows[i].Cells[1].Value += ")";
                dataGrid1.Rows[i].Cells[2].Value = Convert.ToString((a[i] + hxW + a[i]) / 2);
                dataGrid1.Rows[i].Cells[3].Value = Convert.ToString(ni);
                dataGrid1.Rows[i].Cells[4].Value = string.Format("{0: 0.00}", (double)(ni) / 50);
                dataGrid1.Rows[i].Cells[5].Value = string.Format("{0: 0.000}", (double)(ni) / 50 / hxW);
            }
        }

        private void FillTableY(DataGridView dataGrid1)
        {
            for (int i = 0; i < 7; i++)
            {
                int mi = FindNumsH(b[i], b[i + 1]);
                dataGrid1.Rows[i].Cells[0].Value = Convert.ToString(i + 1);
                dataGrid1.Rows[i].Cells[1].Value = "[" + Convert.ToString(b[i]) + ", " + Convert.ToString(b[i + 1]);
                if (i == 6)
                    dataGrid1.Rows[i].Cells[1].Value += "]";
                else
                    dataGrid1.Rows[i].Cells[1].Value += ")";
                dataGrid1.Rows[i].Cells[2].Value = Convert.ToString((b[i] + hyH + b[i]) / 2);
                dataGrid1.Rows[i].Cells[3].Value = Convert.ToString(mi);
                dataGrid1.Rows[i].Cells[4].Value = string.Format("{0: 0.00}", (double)(mi) / 50);
                dataGrid1.Rows[i].Cells[5].Value = string.Format("{0: 0.000}", (double)(mi) / 50 / hyH);
            }
        }


        double u_, v_, u2_, v2_, su2, sv2, x_, y_, s2x, s2y, su, sv, sx, sy;
        private void FillTable3(DataGridView dataGrid1)
        {
            double xavr = (double)(a[3] + a[3 + 1])/2;
            double yavr = (double)(b[3] + b[3 + 1])/2;
            double[] sum = new double[8];
            for (int i = 0; i < 8; i++)
            {
                sum[i] = 0;
            }
            for (int i = 0; i < 7; i++)
            {
                int ni = FindNumsW(a[i], a[i + 1]);
                int mi = FindNumsH(b[i], b[i + 1]);
                sum[1] += ni;
                sum[5] += mi;
                dataGrid1.Rows[i].Cells[0].Value = Convert.ToString(i + 1);
                double ui = ((double)(a[i] + a[i + 1]) / 2 - xavr) / hxW;
                double vi = ((double)(b[i] + b[i + 1]) / 2 - yavr) / hyH;
                sum[0] += ui;
                sum[4] += vi;
                dataGrid1.Rows[i].Cells[1].Value = ui;
                dataGrid1.Rows[i].Cells[2].Value = Convert.ToString(ni);
                dataGrid1.Rows[i].Cells[3].Value = Convert.ToString(ui*ni);
                dataGrid1.Rows[i].Cells[4].Value = Convert.ToString(ui*ui*ni);
                dataGrid1.Rows[i].Cells[5].Value = vi;
                dataGrid1.Rows[i].Cells[6].Value = Convert.ToString(mi);
                dataGrid1.Rows[i].Cells[7].Value = Convert.ToString(vi * mi);
                dataGrid1.Rows[i].Cells[8].Value = Convert.ToString(vi * vi * mi);

                sum[2] += ui*ni;
                sum[3] += ui*ui*ni;
                sum[6] += vi*mi;
                sum[7] += vi*vi*mi;
            }
            dataGrid1.Rows[7].Cells[0].Value = Convert.ToString("Σ");
            for (int i = 0; i < 8; i++)
            {
                if(sum[i] == 0)
                    dataGrid1.Rows[7].Cells[i + 1].Value = "-";
                else
                    dataGrid1.Rows[7].Cells[i+1].Value = Convert.ToString(sum[i]);
            }
            u_ = sum[2] / 50;
            v_ = sum[6] / 50;
            u2_ = sum[3] / 50;
            v2_ = sum[7] / 50;
            su2 = Math.Round(50 * (u2_ - u_ * u_) / 49, 2);
            sv2 = Math.Round(50 * (v2_ - v_ * v_) / 49, 2);
            su = Math.Round(Math.Sqrt(su2),2);
            sv = Math.Round(Math.Sqrt(sv2),2 );
            x_ = hxW * u_ + xavr;
            y_ = hyH * v_ + yavr;
            s2x = hxW * hxW * su2;
            s2y = hyH * hyH * sv2;
            sx = Math.Round(Math.Sqrt(s2x), 2);
            sy = Math.Round(Math.Sqrt(s2y), 2);
            //testLb.Text = v_ + " "+ yavr +" " + x_ + " " + y_ + " " + s2x + " " + s2y + " " + sx + " " + sy;

        }



        private void DrawGisto(int[] arr, string[] str, Chart chart1, bool flag, Point loc, bool Hflag)
        {
            chart1.Location = loc;
            chart1.Size = new Size(340, 179);
            Axis ax = new Axis();
            ax.Title = str[0];
            Axis ay = new Axis();
            ay.Title = str[1];
            chart1.Series[0].LegendText = "";
            chart1.ChartAreas[0].AxisX = ax;
            chart1.ChartAreas[0].AxisY = ay;
            Controls.Add(chart1);
            for (int i = 0; i < 7; i++)
            {
                double Y;
                if(Hflag)
                    Y = (double)FindNumsH(arr[i], arr[i + 1]) / 50;
                else
                    Y = (double)FindNumsW(arr[i], arr[i + 1]) / 50;
                //label1.Text = (arr[i], arr[i + 1]).ToString() + " " + Y;
                if (flag)
                {
                    if (Hflag)
                        Y /= hyH;
                    else
                        Y /= hxW;
                    chart1.Series[0].Points.AddXY((arr[i], arr[i + 1]).ToString(), Y);
                }
                else 
                    chart1.Series[0].Points.AddXY(((double)(arr[i] + arr[i + 1]) / 2).ToString(),  Y);
                //label1.Text = (arr[i], arr[i + 1]).ToString() + " " + Y;

            }
        }
        private void ReadFile(string path)
        {
            string[] fileText = File.ReadAllLines(path);
            string[][] Cells = new string[fileText.Length][];
            for (int i = 0; i < fileText.Length; i++)
            {
                Cells[i] = fileText[i].Split(' ');
            }
            for (int j = 0; j < fileText.Length; j++)
            {
                for (int i = 0; i < 5; i++)
                {
                    weight.Enqueue(Convert.ToDouble(Cells[j][i * 2]));
                    height.Enqueue(Convert.ToInt32(Cells[j][i * 2+1]));
                }
            }
            
        }

    }
}
