using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
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

        private Form actualF;
        public static Queue<Double> weight = new Queue <Double> (); //Вес
        public static Queue<Int32> height = new Queue <Int32> ();   //Рост
        public static Queue<Tuple<int, Queue<Double>>> tValues = new Queue <Tuple<int, Queue<Double>>> ();   //Рост
        public static string path = @"C:\Users\Podor\Documents\GitHub\MathStat\MathStat\4.txt"; //Путь к исходникам
        public static string pathall = @"C:\Users\Podor\Documents\GitHub\MathStat\MathStat\all.txt"; //Путь к исходникам
        public static string patht = @"C:\Users\Podor\Documents\GitHub\MathStat\MathStat\t.txt"; //Путь к распр. стьюд.
        public static Chart[] chart = new Chart[4]; //Графики

        //Для Ф(x)
        public static Microsoft.Office.Interop.Excel.Application exApp = new Microsoft.Office.Interop.Excel.Application();

        public static int N = 50;
        public static double y = 0.95;
        public static double minW, maxW, RxW;
        public static int minH, maxH, RyH, hyH, hxW;
        public static int rangeH;
        public static double rangeW;
        public static int[] a = new int[8]; //Вес
        public static int[] b = new int[8]; //Рост
        public static double[] Ui = new double[7]; //Для веса
        public static double[] Vi = new double[7]; //Для роста
        public static double rв, uv_; //Выборочный коэффициент, и uv_ для пункта 5
        public static double extensionW;
        public static double extensionH;
        public static double multiplier = 1;

        //List<Tuple<double, double, double>> F = new List<Tuple<double, double, double>>();

        public static string[] FirstHeaders = { "[ai–1, ai)", "xi*", "ni", "ni/n", "ni/n*hx" };
        public static string[] SecondHeaders = { "[bi-1, bi)", "yi*", "mi", "mi/n", "mi/n*hy" };
        public static string[] ThirdHeaders = { "ui", "ni", "uini", "ui^2", "vi", "mi", "vimi", "vi^2mu"};
        public static string[] FourHeaders = { "[ai-1, ai)", "ni", "zi=(ai-x_)/sx", "Ф(zi)", "pi", "npi", "(ni-npi)^2/npi"};
        public static string[] FiveHeaders = { "[bi-1, bi)", "mi", "zi=(bi-y_)/sy", "Ф(zi)", "pi", "npi", "(mi-npi)^2/npi"};
        public static string[] strX = { "x", "ni/n" };
        public static string[] strY = { "y", "mi/n" };

        //Variebles for Table3
        static public double u_, v_, u2_, v2_, su2, sv2, x_, y_, s2x, s2y, su, sv, sx, sy;

        //Таблицы
        static public DataGridView[] dataGrid = new DataGridView[6];

        static public Label[] nameTables = new Label[15];
        static public Label testLb = new Label();

        //Нужно для цветов строк
        static public DataGridViewCellStyle row = new DataGridViewCellStyle(); //Серый цвет (закрашенный)
        static public DataGridViewCellStyle row1 = new DataGridViewCellStyle(); //Стандартный цвет

        private void Form1_Load(object sender, EventArgs e)
        {
            //CreateFs();
            for (int i = 0; i < 4; i++)
            {
                chart[i] = new Chart();
                chart[i].Series.Add("");
                chart[i].ChartAreas.Add("");
            }
            for (int i = 0; i < 15; i++)
            {
                nameTables[i] = new Label();
                nameTables[i].Size = new Size(300, 20);
                //Controls.Add(nameTables[i]);
            }
            chart[0].Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            chart[0].Series[0].BorderWidth = 5;
            chart[2].Series[0].BorderWidth = 5;
            chart[2].Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            chart[1].Series[0]["PointWidth"] = "1";
            chart[3].Series[0]["PointWidth"] = "1";


            //Watermark
            testLb.Location = new Point(980, 9);
            testLb.Size = new Size(100, 40);
            testLb.Text = "Сделал\nКостя Дехант ПИ11";
            //Controls.Add(testLb);

            //nameTables[0].Location = new Point(20, 20); //Текст над таблицей 2
            //nameTables[1].Location = new Point(360+ 20, 20); //Текст над таблицей 3
            //nameTables[2].Location = new Point(360+ 20+360, 20); //Текст над таблицей 4
            
            
            //Размер окна
            this.Size = new Size(1440,720);
            //Цвет окна
            //this.BackColor = Color.FromArgb(255, 15, 15, 15);
            //Цвет страницы


            //Инициализация таблиц
            for (int i = 0; i < 6; i++)
            {
                dataGrid[i] = new DataGridView();
            }
            ReadFile(pathall, 4); //Чтение исходников

            row.BackColor = Color.LightGray;

            //InitTables();
            //InitTabCtrl();
            dataGrid[0].ClearSelection();
            dataGrid[1].ClearSelection();


            //Создание кнопки для перезагрузки исходных данных
            Button button = new Button();
            button.Location = new Point(1360, 0);
            button.Size = new Size(20, 20);
            button.Visible = true;
            button.Click += new EventHandler(this.Refrech_Click);
            //Controls.Add(button);
            InitTables();
            //-!
            OpenChildForm(new InputVar(), sender);
            tInit();

            for (int i = 0; i < 26; i++)
            {
                guna2ComboBox1.Items.Add("Вариант "+ (i+1).ToString());
            }
            guna2ComboBox1.SelectedIndex = 25;
        }

        public void FullScreen()
        {
            guna2Panel1.Size = new Size(220, this.Height);
            multiplier = 1.5;
        }

        public void InitTables()
        {
            //Всё необходимое для первого пункта
            FirstTable();
            //DrawDataGrid(FirstHeaders, new Point(20, 40), new Size(40 + 60 * 5, 179), dataGrid[0], 8, 6);
            //FillTableX(dataGrid[0]);
            //DrawGisto(a, strX, chart[0], false, new Point(dataGrid[0].Location.X + 20 + dataGrid[0].Width, dataGrid[0].Location.Y), false);

            //FillGisto(a, chart[0], false, false);

            //strX[1] = "n1/n*hx";
           // DrawGisto(a, strX, chart[1], true, new Point(chart[0].Location.X + 20 + chart[0].Width, chart[0].Location.Y), false);

            //FillGisto(a, chart[1], true, false);

            //nameTables[0].Location = new Point(dataGrid[0].Location.X, dataGrid[0].Location.Y - 20);
            //nameTables[0].Text = "Таблица 2 – Группированный ряд для X";
            //nameTables[1].Location = new Point(chart[0].Location.X, chart[0].Location.Y - 20);
            //nameTables[1].Text = "Полигон относительных частот";
            //nameTables[2].Location = new Point(chart[1].Location.X, chart[1].Location.Y - 20);
            //nameTables[2].Text = "Гистрограмма относительных частот";


            SecondTable();
            //DrawDataGrid(SecondHeaders, new Point(20, dataGrid[0].Location.Y+40 + dataGrid[0].Height), new Size(40 + 60 * 5, 179), dataGrid[1], 8, 6);
            //FillTableY(dataGrid[1]);
            //DrawGisto(b, strY, chart[2], false, new Point(dataGrid[1].Location.X + 20 + dataGrid[1].Width, dataGrid[1].Location.Y), true);

            //FillGisto(b, chart[2], false, true);
            //strY[1] = "m1/n*hy";
            //DrawGisto(b, strY, chart[3], true, new Point(chart[2].Location.X + 20 + chart[2].Width, chart[2].Location.Y), true);

            //FillGisto(b, chart[3], true, true);
            //nameTables[3].Location = new Point(dataGrid[1].Location.X, dataGrid[1].Location.Y - 20);
            //nameTables[3].Text = "Таблица 3 – Группированный ряд для Y";
            //nameTables[4].Location = new Point(chart[2].Location.X, chart[2].Location.Y - 20);
            //nameTables[4].Text = "Полигон относительных частот";
            //nameTables[5].Location = new Point(chart[3].Location.X, chart[3].Location.Y - 20);
            //nameTables[5].Text = "Гистрограмма относительных частот";
            //-!



            //Пункт 2
            ThirdTable();
            DrawDataGrid(ThirdHeaders, new Point(20, 40), new Size(40 + 60 * 8, 179 + 21), dataGrid[2], 9, 9);
            FillTable3(dataGrid[2]);
            //nameTables[6].Location = new Point(dataGrid[2].Location.X, dataGrid[2].Location.Y - 20);
            //nameTables[6].Text = "Таблица 4 – Группированные данные для условных вариант";
            //-!

            //Пунтк 3

            //DrawDataGrid(FourHeaders, new Point(20,40),
            //                    new Size(40 + 60 * 8 - 32, 179 + 20), dataGrid[3], 9, 8);
            //TableFour();
            //FillTable4(dataGrid[3]);
            //nameTables[7].Location = new Point(dataGrid[3].Location.X, dataGrid[3].Location.Y - 20);
            //nameTables[7].Text = "Таблица 5 – Расчёт Xв^2 для признака X";


            //DrawDataGrid(FiveHeaders, new Point(dataGrid[3].Location.X + dataGrid[3].Width + 20, dataGrid[3].Location.Y),
            //   new Size(40 + 60 * 8 - 32, 179 + 20), dataGrid[4], 9, 8);
            //TableFive();
            //FillTable5(dataGrid[4]);
            //nameTables[8].Location = new Point(dataGrid[4].Location.X, dataGrid[4].Location.Y - 20);
            //nameTables[8].Text = "Таблица 6 – Расчёт Xв^2 для признака Y";
            //-!

            //Пунтк 4

            //Реализовать подсчёты

            //-!

            //Пункт 5
            //Заголовки и поправить в целом
            //string[] SixHeaders = { };
            //DrawDataGrid(SixHeaders, new Point(20, 40),
            //    new Size(40 + 60 * 8 - 80 + 8*15, 179 + 20+15+30*7+10), dataGrid[5], 9, 9);
            //FillTable6(dataGrid[5]);
            TableSix();
            //for (int i = 0; i < 8; i++)
            //{
            //    dataGrid[5].Columns[i].Width += 15;
                
            //}
            //dataGrid[5].Columns[0].Width += 15;
            //dataGrid[5].ColumnHeadersHeight += 25;
            //nameTables[9].Location = new Point(dataGrid[5].Location.X, dataGrid[5].Location.Y - 20);
            //nameTables[9].Text = "Таблица 7 – Корреляционная таблица";
            //-!


        }
        private void InitTabCtrl()
        {
            //TabPage []tabPage = new TabPage[7];

            //for (int i = 0; i < 7; i++)
            //{
            //    tabPage[i] = new TabPage();

               
            //    tabControl1.Controls.Add(tabPage[i]);
            //    tabControl1.TabPages[i].Text = "Пункт " + (i + 1).ToString();
            //}
            //tabPage[0].Controls.Add(dataGrid[0]);
            //tabPage[0].Controls.Add(nameTables[0]);
            //tabPage[0].Controls.Add(dataGrid[1]);
            //tabPage[0].Controls.Add(nameTables[1]);
            //tabPage[0].Controls.Add(chart[0]); 
            //tabPage[0].Controls.Add(nameTables[2]);
            //tabPage[0].Controls.Add(chart[1]);
            //tabPage[0].Controls.Add(nameTables[3]);
            //tabPage[0].Controls.Add(chart[2]); 
            //tabPage[0].Controls.Add(nameTables[4]);
            //tabPage[0].Controls.Add(chart[3]); 
            //tabPage[0].Controls.Add(nameTables[5]);


            //tabPage[1].Controls.Add(dataGrid[2]);
            //tabPage[1].Controls.Add(nameTables[6]);

            //tabPage[2].Controls.Add(dataGrid[3]);
            //tabPage[2].Controls.Add(nameTables[7]);

            //tabPage[2].Controls.Add(dataGrid[4]);
            //tabPage[2].Controls.Add(nameTables[8]);

            ////tabPage[4].Controls.Add();
            //tabPage[4].Controls.Add(dataGrid[5]);
            //tabPage[4].Controls.Add(nameTables[9]);



            //tabControl1.Size = new Size(4 * 20 + dataGrid[0].Width + chart[0].Width + chart[1].Width + dataGrid[0].Location.X, 
            //    dataGrid[0].Location.Y + dataGrid[0].Height + dataGrid[1].Height+20*4);



        }

        private Button currentButton;
        private Form activeForm = new Form();
        private void ActivateButton(object btnSender)
        {
            //if(btnSender != null)
            //{
            //    if(currentButton != (Button)btnSender)
            //}
        }

        private void OpenChildForm(Form childForm, object btnSender)
        {
            if(activeForm != null)
            {
                activeForm.Close();
            }
            ActivateButton(btnSender);
            activeForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            this.Back_Panel.Controls.Add(childForm);
            this.Back_Panel.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
        }

        int actualeFormNum = 0;
        private void gBttn1_Click(object sender, EventArgs e)
        {
            actualeFormNum = 1;
            OpenChildForm(new FirstP(), sender);
        }

        private void gBttn2_Click(object sender, EventArgs e)
        {
            actualeFormNum = 2;
            OpenChildForm(new SecondP(), sender);
        }

        private void gBttn3_Click(object sender, EventArgs e)
        {
            actualeFormNum = 3;
            OpenChildForm(new ThirdP(), sender);
        }

        private void gBttn4_Click(object sender, EventArgs e)
        {
            actualeFormNum = 4;
            OpenChildForm(new FourthP(), sender);
        }

        private void gBttn5_Click(object sender, EventArgs e)
        {
            actualeFormNum = 5;
            OpenChildForm(new FifthP(), sender);
        }

        private void gBttn6_Click(object sender, EventArgs e)
        {
            actualeFormNum = 6;
            //OpenChildForm(new SixthP(), sender);
        }





        //private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (tabControl1.SelectedIndex == 0)
        //    {
        //        //tabControl1.Size = new Size(5 * 20 + dataGrid[0].Width + chart[0].Width + chart[1].Width, 4 * 20 + dataGrid[0].Height + dataGrid[1].Height);
        //        dataGrid[0].ClearSelection();
        //        dataGrid[1].ClearSelection();
        //    }
        //    else if (tabControl1.SelectedIndex == 1)
        //    {
        //        dataGrid[2].ClearSelection();

        //    }
        //    else if (tabControl1.SelectedIndex == 2)
        //    {
        //        dataGrid[3].ClearSelection();
        //        dataGrid[4].ClearSelection();
        //    }
        //    else if (tabControl1.SelectedIndex == 3)
        //    {

        //    }
        //    else if (tabControl1.SelectedIndex == 4)
        //    {
        //        dataGrid[5].ClearSelection();
        //        tabControl1.Height = dataGrid[5].Location.Y + dataGrid[5].Height + 30;
        //    }
        //}


        //Исправить ещё в одном месте значение, чтобы не слетало.
        void Refrech_Click(Object sender,
                               EventArgs e)
        {

        }


        //Таблица 2, но первая для подсчётов (группированный ряд для X)
        public void FirstTable()
        {
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
        }

        public void SecondTable()
        {
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
        }

        public void ThirdTable()
        {
            //DrawDataGrid(ThirdHeaders, new Point(20 + 20 + 340 + 340 + 20, 50), new Size(40 + 60 * 8, 179+21), dataGrid[2], 9, 9);
        }

        public void TableFour()
        {
            //исправить ширину
            //dataGrid[3].Columns[3].Width = 70;
            //dataGrid[3].Columns[7].Width = 75;
        }

        public void TableFive()
        {
            //dataGrid[4].Columns[3].Width = 70;
            //dataGrid[4].Columns[7].Width = 75;
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            OpenChildForm(new InputVar(), sender);
        }

        private void guna2ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Refresh_data();
            ReadFile(pathall, guna2ComboBox1.SelectedIndex + 1);
            
            InitTables();
            PerformClicks();
        }

        private void Refresh_data()
        {
            minW = maxW= RxW = rangeW = rв= uv_= extensionW= extensionH= u_= v_=
                u2_= v2_=  su2=  sv2= x_= y_= s2x= s2y= su= sv= sx= sy = 0;
            minH =maxH= RyH =hyH = hxW = rangeH = 0;
            for (int i = 0; i < 7; i++)
            {
                a[i] = 0;
                b[i] = 0;
                Ui[i] = 0;
                Vi[i] = 0;
            }
            height.Clear(); 
            weight.Clear(); 
        }

        private void PerformClicks()
        {
            switch(actualeFormNum)
            {
                case 0:
                    guna2Button1.PerformClick();
                    break;
                case 1:
                    gBttn1.PerformClick();
                    break;
                case 2:
                    gBttn2.PerformClick();
                    break;
                case 3:
                    gBttn3.PerformClick();
                    break;
                case 4:
                    gBttn4.PerformClick();
                    break;
                case 5:
                    gBttn5.PerformClick();
                    break;
                case 6:
                    gBttn6.PerformClick();
                    break;

            }
        }

        private void Form1_SizeChanged(object sender, EventArgs e)
        {

            if (this.WindowState == System.Windows.Forms.FormWindowState.Maximized)
            {
                FullScreen();
            }
            else
                multiplier = 1;
            PerformClicks();
            label3.Location = new Point(this.Width - 340, this.Height - 85);
            guna2ComboBox1.Location = new Point(this.Width - 180, this.Height - 90);
            Back_Panel.Size = new Size(this.Width - 220, this.Height - 50 - 95);
            labelCreator.Location = new Point(labelCreator.Location.X, this.Height - 100);
        }

        public void TableSix()
        {
            //for (int i = 1; i < 9; i++)
            //{
            //    dataGrid[5].Columns[i].Width = 50;
            //}
            //dataGrid[5].Columns[0].HeaderText = "X/Y";
            
            //for (int i = 0; i < 7; i++)
            //{
            //    dataGrid[5].Rows[i].Cells[8].Value = FindNumsW(a[i], a[i + 1]).ToString(); 
            //    dataGrid[5].Rows[7].Cells[i+1].Value = FindNumsH(b[i], b[i + 1]).ToString(); 
            //}
        }

        public string CalcMX()
        {
            double leftV, rightV;
            //tInit();
            
            double a = 1 - y;
            CalcT = Math.Round(t(1 - (a / 2), N - 1), 2); //
            leftV = Math.Round(x_ - (sx / Math.Sqrt(N)) * CalcT, 2); //
            rightV = Math.Round(x_ + (sx / Math.Sqrt(N)) * CalcT, 2); //
            return leftV.ToString() + " < M(X) < " +rightV;
        }
        public string CalcMY()
        {
            double leftV, rightV;
            //tInit();
            
            double a = 1 - y;
            CalcT = Math.Round(t(1 - (a / 2), N - 1), 2); //
            leftV = Math.Round(y_ - (sy / Math.Sqrt(N)) * CalcT, 2); //
            rightV = Math.Round(y_ + (sy / Math.Sqrt(N)) * CalcT, 2); //
            return string.Format("{0: 0.00}", leftV) + " < M(Y) < " + string.Format("{0: 0.00}", rightV);
        }
        public static double CalcT = 0;
        public double t(double a, int n)
        {
            int tempI = 1;
            
            for (int i = 1; i < 11; i++)
            {
                //MessageBox.Show(a.ToString() +  " " + ArrT[0, i]);
                if (ArrT[0, i] == a)
                {
                    tempI = i;
                    break;
                }
            }
            CalcT = Math.Round(ArrT[n, tempI], 2);
            return ArrT[n, tempI];
        }
        double[,] ArrT = new double[100,52];
        public void tInit()
        {
            //double num = 0;
            string[] str = File.ReadAllLines(patht);
            int strlen = str.Length;
            for (int i = 0; i < strlen; i++)
            {
                string[] str1 = str[i].Split();
                //int str1len = str1.Length;
                //Queue<Double> temp = new Queue<Double>();
                //for (int j = 1; j < str1len; j++)
                //{
                //    temp.Enqueue(Convert.ToDouble(str1[j]));
                //}
                //tValues.Enqueue(new Tuple<Int32, Queue<Double>>(Convert.ToInt32(str1[0]), temp));
                for (int j = 1; j < 11; j++)
                {
                    ArrT[i, j] = Convert.ToDouble(str1[j]);
                }
            }
            
            //tValues.Dequeue().Item2.ToArray()

        }


        //ni подсчёт
        public int FindNumsW(double left, double right)
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

        public int FindNumsH(int left, int right)
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

        public void DrawDataGrid(string[] str, Point point, Size size, DataGridView data, int rows, int columns)
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
            data.Columns[0].Width = (int)(40 * multiplier);
            for (int i = 1; i < columns; i++)
            {
                data.Columns[i].Width = (int)(60 *multiplier);
            }
            for (int i = 0; i < rows; i++)
            {
                data.Rows[i].Height = (int)(data.Rows[i].Height * multiplier);
            }
            data.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            data.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            data.AllowUserToAddRows = false;
            data.AllowUserToDeleteRows = false;
            data.ReadOnly = true;
            data.AllowUserToResizeRows = false;
            data.AllowUserToResizeColumns = false;
            data.ScrollBars = ScrollBars.None;
            data.Enabled = false;
            //Controls.Add(data);
            data.ClearSelection();
        }

        public double Ffuncion(double zi)
        {
            double Fnum = 0;
            Fnum = exApp.WorksheetFunction.Erf(0, zi / Math.Sqrt(2));
            Fnum *= Math.Sqrt(Math.PI) / Math.Sqrt(2);
            Fnum /= Math.Sqrt(Math.PI * 2);
            return Fnum;
        }




        public void FillTableX(DataGridView dataGrid1)
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

        public void FillTableY(DataGridView dataGrid1)
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

        public void FillTable3(DataGridView dataGrid1)
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
                Ui[i] = ui;
                double vi = ((double)(b[i] + b[i + 1]) / 2 - yavr) / hyH;
                Vi[i] = vi;
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
            dataGrid1.Rows[7].Cells[0].Value = "Σ";
            for (int i = 0; i < 8; i++)
            {
                if(sum[i] == 0)
                    dataGrid1.Rows[7].Cells[i + 1].Value = "-";
                else
                    dataGrid1.Rows[7].Cells[i+1].Value = Convert.ToString(sum[i]);
            }
            u_ = (double)sum[2] / 50;
            v_ = (double)sum[6] / 50;
            u2_ = sum[3] / 50;
            v2_ = sum[7] / 50;
            su2 = Math.Round(50 * (u2_ - u_ * u_) / 49, 2);
            sv2 = Math.Round(50 * (v2_ - v_ * v_) / 49, 2);
            su = Math.Round(Math.Sqrt(su2),2);
            sv = Math.Round(Math.Sqrt(sv2),2 );
            x_ = hxW * u_ + xavr;
            y_ = Math.Round(hyH * v_ + yavr, 1);
            s2x = hxW * hxW * su2;
            s2y = hyH * hyH * sv2;
            sx = Math.Round(Math.Sqrt(s2x), 2);
            sy = Math.Round(Math.Sqrt(s2y), 2);
        }

        public void FillTable4(DataGridView dataGrid1)
        {
            double[] zi = new double[8];
            zi[0] = -7.0;
            zi[7] = 7.0;
            double[] npi = new double[7];
            int nisum = 0;
            double npisum = 0;
            double pisum = 0;
            int[] ni = new int[7];
            for (int i = 0; i < 7; i++)
            {
                dataGrid1.Rows[i].Cells[0].Value = Convert.ToString(i + 1);
                if (i == 0)
                    dataGrid1.Rows[i].Cells[1].Value = "(-oo, " + Convert.ToString(a[i + 1]);
                else if (i == 6)
                    dataGrid1.Rows[i].Cells[1].Value = "[" + Convert.ToString(a[i]) + ", +oo";
                else
                    dataGrid1.Rows[i].Cells[1].Value = "[" + Convert.ToString(a[i]) + ", " + Convert.ToString(a[i + 1]);
                dataGrid1.Rows[i].Cells[1].Value += ")";
                ni[i] = FindNumsW(a[i], a[i + 1]);
                nisum += ni[i];
                dataGrid1.Rows[i].Cells[2].Value = Convert.ToString(ni[i]);
                if (i + 1 != 7)
                    zi[i + 1] = Math.Round((a[i + 1] - x_) / sx, 2);

                dataGrid1.Rows[i].Cells[3].Value = Convert.ToString(string.Format("{0: 0.00}", zi[i + 1]));
                if (i == 6)
                    dataGrid1.Rows[i].Cells[3].Value = "+oo";
                dataGrid1.Rows[i].Cells[4].Value = Convert.ToString(string.Format("{0: 0.000}", Math.Round(Ffuncion(zi[i + 1]) + 0.0001, 3)));
                double pi = Math.Round(Ffuncion(zi[i + 1]) + 0.0001, 3) - Math.Round(Ffuncion(zi[i]) + 0.0001, 3);
                dataGrid1.Rows[i].Cells[5].Value = Convert.ToString(string.Format("{0: 0.000}", Math.Round(pi, 3)));
                npi[i] = Math.Round(pi, 4) * 50;
                pisum += Math.Round(pi, 4);
                dataGrid1.Rows[i].Cells[6].Value = string.Format("{0: 0.00}", npi[i]);
                npisum += npi[i];
            }


            bool AddQ = false;

            DataGridViewCellStyle row = new DataGridViewCellStyle();
            row.BackColor = Color.LightGray;

            double temp_sum = 0;
            for (int i = 0; i < 7; i++)
            {
                int num = i + 1;
                temp_sum += npi[i];
                //MessageBox.Show(temp_sum.ToString());
                if (temp_sum < 5)//(npi[i] < 5)
                {
                    dataGrid1.Rows[i].DefaultCellStyle = row;
                    AddQ = true;
                    if (AddQ && i == 6)
                    {
                        for (int j = i - 2; j >= 0; j--)
                        {
                            temp_sum += npi[j];
                            if (temp_sum  >= 5)//(npi[j] >= 5)//
                            {
                                int num1 = j + 1;
                                dataGrid1.Rows[j].DefaultCellStyle = row;
                                AddQ = false;
                                temp_sum = 0;
                                break;
                            }
                        }
                    }
                }
                else if (AddQ)
                {
                    dataGrid1.Rows[i].DefaultCellStyle = row;
                    temp_sum = 0;
                    AddQ = false;
                }
                else
                {
                    temp_sum = 0;
                }
            }
            int m = 0;
            for (int i = 0; i < 7; i++)
            {
                if (dataGrid1.Rows[i].DefaultCellStyle != row)
                    m++;
                else if (dataGrid1.Rows[i].DefaultCellStyle == row && dataGrid1.Rows[i + 1].DefaultCellStyle != row)
                    m++;
            }
            double npitemp = 0;
            int nitemp = 0;
            double tempsum7 = 0;
            for (int i = 0; i < 7; i++)
            {
                if (dataGrid1.Rows[i].DefaultCellStyle != row)
                {
                    double tempnum = Math.Round((ni[i] - npi[i]) * (ni[i] - npi[i]) / (npi[i]), 2);
                    dataGrid1.Rows[i].Cells[7].Value = tempnum;
                    tempsum7 += tempnum;
                }
                else if (dataGrid1.Rows[i].DefaultCellStyle == row && dataGrid1.Rows[i + 1].DefaultCellStyle != row)
                {
                    nitemp += ni[i];
                    npitemp += npi[i];
                    double tempnum = Math.Round((nitemp - npitemp) * (nitemp - npitemp) / (npitemp), 2);
                    dataGrid1.Rows[i].Cells[7].Value = tempnum;
                    tempsum7 += tempnum;
                    npitemp = 0;
                    nitemp = 0;
                }
                else
                {
                    nitemp += ni[i];
                    npitemp += npi[i];
                }
            }



            dataGrid1.Rows[7].Cells[0].Value = "Σ";
            dataGrid1.Rows[7].Cells[1].Value = "-";
            dataGrid1.Rows[7].Cells[2].Value = Convert.ToString(nisum);
            dataGrid1.Rows[7].Cells[3].Value = "-";
            dataGrid1.Rows[7].Cells[4].Value = "-";
            dataGrid1.Rows[7].Cells[5].Value = pisum;
            dataGrid1.Rows[7].Cells[6].Value = Convert.ToString(npisum);
            dataGrid1.Rows[7].Cells[7].Value = tempsum7;
        }

        public void FillTable5(DataGridView dataGrid1)
        {


            double[] zi = new double[8];
            zi[0] = -7.0;
            zi[7] = 7.0;
            double[] npi = new double[7];
            int misum = 0;
            double npisum = 0;
            double pisum = 0;
            int[] mi = new int[7];
            for (int i = 0; i < 7; i++)
            {
                dataGrid1.Rows[i].Cells[0].Value = Convert.ToString(i + 1);
                if (i == 0)
                    dataGrid1.Rows[i].Cells[1].Value = "(-oo, " + Convert.ToString(b[i + 1]);
                else if (i == 6)
                    dataGrid1.Rows[i].Cells[1].Value = "[" + Convert.ToString(b[i]) + ", +oo";
                else
                    dataGrid1.Rows[i].Cells[1].Value = "[" + Convert.ToString(b[i]) + ", " + Convert.ToString(b[i + 1]);
                dataGrid1.Rows[i].Cells[1].Value += ")";
                mi[i] = FindNumsH(b[i], b[i + 1]);
                misum += mi[i];
                dataGrid1.Rows[i].Cells[2].Value = Convert.ToString(mi[i]);
                if (i + 1 != 7)
                    zi[i + 1] = Math.Round((b[i + 1] - y_) / sy, 2);

                dataGrid1.Rows[i].Cells[3].Value = Convert.ToString(string.Format("{0: 0.00}", zi[i + 1]));
                if (i == 6)
                    dataGrid1.Rows[i].Cells[3].Value = "+oo";
                dataGrid1.Rows[i].Cells[4].Value = Convert.ToString(string.Format("{0: 0.000}", Math.Round(Ffuncion(zi[i + 1]) + 0.0001, 3)));
                double pi = Math.Round(Ffuncion(zi[i + 1]) + 0.0001, 3) - Math.Round(Ffuncion(zi[i]) + 0.0001, 3);
                dataGrid1.Rows[i].Cells[5].Value = Convert.ToString(string.Format("{0: 0.000}", Math.Round(pi, 3)));
                npi[i] = Math.Round(pi, 4) * 50;
                pisum += Math.Round(pi, 4);
                dataGrid1.Rows[i].Cells[6].Value = string.Format("{0: 0.00}", npi[i]);
                npisum += npi[i];
                //dataGrid1.Rows[i].Cells[7].Value = Math.Round((ni[i] - npi[i]) * (ni[i] - npi[i]) /(npi[i]),2);
            }

            //str.Location = new Point(1440 - 100, 20);
            //str.Size = new Size(300, 300);
            //str.Text = "";
            bool AddQ = false;


            for (int i = 0; i < 7; i++)
            {
                int num = i + 1;
                if (npi[i] < 5)
                {
                    dataGrid1.Rows[i].DefaultCellStyle = row;
                    //str.Text += num + " ";
                    AddQ = true;
                    if (AddQ && i == 6)
                    {
                        for (int j = i - 1; j >= 0; j--)
                        {
                            if (npi[j] > 5)
                            {
                                int num1 = j + 1;
                                dataGrid1.Rows[j].DefaultCellStyle = row;
                                AddQ = false;
                                break;
                            }
                        }
                    }
                }
                else if (AddQ)
                {
                    dataGrid1.Rows[i].DefaultCellStyle = row;
                    //str.Text += num + "\n";
                    AddQ = false;
                }
                //else
                //    str.Text += "\n" + num;
            }
            int m = 0;
            for (int i = 0; i < 7; i++)
            {
                if (dataGrid1.Rows[i].DefaultCellStyle != row)
                    m++;
                else if (dataGrid1.Rows[i].DefaultCellStyle == row && dataGrid1.Rows[i + 1].DefaultCellStyle != row)
                    m++;
            }
            double npitemp = 0;
            int mitemp = 0;
            double tempsum7 = 0;
            for (int i = 0; i < 7; i++)
            {
                if (dataGrid1.Rows[i].DefaultCellStyle != row)
                {
                    double tempnum = Math.Round((mi[i] - npi[i]) * (mi[i] - npi[i]) / (npi[i]), 2);
                    dataGrid1.Rows[i].Cells[7].Value = tempnum;
                    tempsum7 += tempnum;
                }
                else if (dataGrid1.Rows[i].DefaultCellStyle == row && dataGrid1.Rows[i + 1].DefaultCellStyle != row)
                {
                    mitemp += mi[i];
                    npitemp += npi[i];
                    double tempnum = Math.Round((mitemp - npitemp) * (mitemp - npitemp) / (npitemp), 2);
                    dataGrid1.Rows[i].Cells[7].Value = tempnum;
                    tempsum7 += tempnum;
                    //str.Text = nitemp + " " + npitemp;
                    npitemp = 0;
                    mitemp = 0;
                }
                else
                {
                    mitemp += mi[i];
                    npitemp += npi[i];
                }
            }

            dataGrid1.Rows[7].Cells[0].Value = "Σ";
            dataGrid1.Rows[7].Cells[1].Value = "-";
            dataGrid1.Rows[7].Cells[2].Value = Convert.ToString(misum);
            dataGrid1.Rows[7].Cells[3].Value = "-";
            dataGrid1.Rows[7].Cells[4].Value = "-";
            dataGrid1.Rows[7].Cells[5].Value = pisum;
            dataGrid1.Rows[7].Cells[6].Value = Convert.ToString(npisum);
            dataGrid1.Rows[7].Cells[7].Value = tempsum7;
            //str.Text = m + "";
            //Controls.Add(str);

        }

        public void FillTable6(DataGridView dataGrid1)
        {
            int[] H = height.ToArray<Int32>();
            double[] W = weight.ToArray<Double>();
            int hlen;//, wlen;
            hlen = H.Length;
            //wlen = W.Length;
            int[,] map = new int[7, 7];
            int sum = 0;
            dataGrid1.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            for (int i = 0; i < hlen; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    for (int k = 0; k < 7; k++)
                    {
                        if (a[j] <= W[i] && W[i] < a[j + 1] && b[k] <= H[i] && H[i] < b[k + 1])
                        {
                            sum++;
                            map[j, k]++;
                        }
                    }

                }
            }
            for (int i = 1; i < 8; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    if (map[j, i - 1] != 0)
                        dataGrid1[i, j].Value = map[j, i - 1].ToString();
                }
            }

            for (int i = 0; i < 7; i++)
            {
                //int ni = FindNumsW(a[i], a[i + 1]);
                dataGrid1.Rows[i].Cells[0].Value = "[" + Convert.ToString(a[i]) + ", " + Convert.ToString(a[i + 1]);
                if (i == 6)
                    dataGrid1.Rows[i].Cells[0].Value += "]";
                else
                    dataGrid1.Rows[i].Cells[0].Value += ")";

                dataGrid1.Columns[i+1].HeaderText = "[" + Convert.ToString(b[i]) + ", " + Convert.ToString(b[i + 1]);
                if (i == 6)
                    dataGrid1.Columns[i + 1].HeaderText += "]";
                else
                    dataGrid1.Columns[i + 1].HeaderText += ")";
                dataGrid1.Columns[i + 1].HeaderText += "\ny"+(i+1).ToString()+"*="+((b[i + 1] + b[i])/2).ToString() + "\n<" + Vi[i].ToString() + ">";
                dataGrid1.Rows[i].Cells[0].Value += "\nx"+(i+1).ToString()+"*="+((double)(a[i + 1] + a[i]) / 2).ToString() + "\n<" + Ui[i].ToString()+">";
                dataGrid1.Rows[i].Height += 30;
            }
           
           
            //Добавить ещё строку с <x>

            dataGrid1.Columns[8].HeaderText = "ni*";
            dataGrid1.Rows[7].Cells[0].Value = "n*j";
            dataGrid1.Rows[7].Cells[8].Value = "Σ=" + sum;

            uv_ = 0;
            for (int i = 0; i < 7; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    uv_ += Ui[i] * Vi[j] * map[j,i];
                   
                }
            }
            uv_ /= 50;
            rв = 50 *(uv_-u_*v_)/(su*sv*49);
            //MessageBox.Show(sx.ToString() + "  " + sy.ToString());
        }

        public void DrawGisto(int[] arr, string[] str, Chart chart1, bool flag, Point loc, bool Hflag)
        {
            chart1.Location = loc;
            chart1.Size = new Size((int)(340 *multiplier), (int)(179 * multiplier));
            Axis ax = new Axis();
            ax.Title = str[0];
            Axis ay = new Axis();
            ay.Title = str[1];
            chart1.Series[0].LegendText = "";
            chart1.ChartAreas[0].AxisX = ax;
            chart1.ChartAreas[0].AxisY = ay;
            //Controls.Add(chart1);
            //for (int i = 0; i < 7; i++)
            //{
            //    double Y;
            //    if(Hflag)
            //        Y = (double)FindNumsH(arr[i], arr[i + 1]) / 50;
            //    else
            //        Y = (double)FindNumsW(arr[i], arr[i + 1]) / 50;
            //    if (flag)
            //    {
            //        if (Hflag)
            //            Y /= hyH;
            //        else
            //            Y /= hxW;
            //        chart1.Series[0].Points.AddXY((arr[i], arr[i + 1]).ToString(), Y);
            //    }
            //    else 
            //        chart1.Series[0].Points.AddXY(((double)(arr[i] + arr[i + 1]) / 2).ToString(),  Y);

            //}
        }

        public void FillGisto(int[] arr, Chart chart1, bool flag, bool Hflag)
        {
            for (int i = 0; i < 7; i++)
            {
                double Y;
                if (Hflag)
                    Y = (double)FindNumsH(arr[i], arr[i + 1]) / 50;
                else
                    Y = (double)FindNumsW(arr[i], arr[i + 1]) / 50;
                if (flag)
                {
                    if (Hflag)
                        Y /= hyH;
                    else
                        Y /= hxW;
                    chart1.Series[0].Points.AddXY((arr[i], arr[i + 1]).ToString(), Y);
                }
                else
                    chart1.Series[0].Points.AddXY(((double)(arr[i] + arr[i + 1]) / 2).ToString(), Y);
            }
        }

        public void ReadFile(string path, int num)
        {
            int count = N / 5;
            int shift = (num - 1) * 10;
            string[] fileText = File.ReadAllLines(path);
            string[][] Cells = new string[count][];
            int c = 0;
            for (int i = shift; i < shift+count; i++)
            {
                Cells[c++] = fileText[i].Split(' ');
            }
            for (int j = 0; j < count; j++)
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
