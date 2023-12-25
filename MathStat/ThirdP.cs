using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MathStat
{
    public partial class ThirdP : Form
    {
        public ThirdP()
        {
            InitializeComponent();
        }

        private void ThirdP_Load(object sender, EventArgs e)
        {
            InitTable();
        }
        
        private void InitTable()
        {
            
            label1.Text = "Таблица 5 – Расчёт Xв^2 для признака X";
            
            label2.Text = "Таблица 6 – Расчёт Xв^2 для признака Y";
            Form1 form = new Form1();
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
            label1.ForeColor = clrFont;
            label2.ForeColor = clrFont;
            label3.ForeColor = clrFont;
            label4.ForeColor = clrFont;
            form.DrawDataGrid(Form1.FourHeaders, new Point(20, 40),
                                new Size((int)((40 + 60 * 8 -35) * Form1.multiplier), (int)((179 + 20) * Form1.multiplier)), dataGrid1, 9, 8);
            //form.TableFour();
            form.FillTable4(dataGrid1);
            dataGrid1.ColumnHeadersHeight = (int)(25 * Form1.multiplier);
            dataGrid1.Columns[3].Width = (int)(70 * Form1.multiplier);
            dataGrid1.Columns[7].Width = (int)(75 * Form1.multiplier);
            label1.Location = new Point(dataGrid1.Location.X, dataGrid1.Location.Y - (int)(20 * Form1.multiplier));
            label1.Font = new System.Drawing.Font("Arial", (int)(9 * Form1.multiplier), System.Drawing.FontStyle.Bold);
            form.DrawDataGrid(Form1.FiveHeaders, new Point(dataGrid1.Location.X + dataGrid1.Width + 20, dataGrid1.Location.Y),
               new Size((int)((40 + 60 * 8 - 35) * Form1.multiplier), (int)((179 + 20) * Form1.multiplier)), dataGrid2, 9, 8);
            //form.TableFive();
            form.FillTable5(dataGrid2);
            dataGrid2.ColumnHeadersHeight = (int)(25 * Form1.multiplier);
            dataGrid2.Columns[3].Width = (int)(70 * Form1.multiplier);
            dataGrid2.Columns[7].Width = (int)(75 * Form1.multiplier);
            label2.Location = new Point(dataGrid2.Location.X, dataGrid2.Location.Y - (int)(20 * Form1.multiplier));
            label2.Font = new System.Drawing.Font("Arial", (int)(9 * Form1.multiplier), System.Drawing.FontStyle.Bold);

            label3.Location = new Point(20, dataGrid2.Location.Y + dataGrid2.Height + (int)(20 * Form1.multiplier));
            label4.Location = new Point(dataGrid2.Location.X, dataGrid2.Location.Y + dataGrid2.Height + (int)(20 * Form1.multiplier));
            label3.Font = new System.Drawing.Font("Arial", (int)(9 * Form1.multiplier), System.Drawing.FontStyle.Bold);
            label4.Font = new System.Drawing.Font("Arial", (int)(9 * Form1.multiplier), System.Drawing.FontStyle.Bold);
            label3.Text = "Число интервалов m = "+ Form1.m1 + ", поэтому число степеней свободы для χ^2 - \nраспределения равно m – k – 1 = " +
                Form1.m1 + " - 2 - 1 = " + (Form1.m1-2-1) + ". По таблице находим \nквантиль χ(0,95)^2(" + (Form1.m1 - 2 - 1) + ") = "
                + form.X(0.95, Form1.m1 - 2 - 1) + "\nВывод: так как χв^2 = " + Form1.tempsum7 + " < X(0.95)^2(" 
                + (Form1.m1 - 2 - 1) + ") = " + form.X(0.95, Form1.m1 - 2 - 1) + ", то гипотеза H0 о нормальном \nраспределении " +
                "величины X не противоречит выборочным данным.";
            label4.Text = "Число интервалов m = " + Form1.m2 + ", поэтому число степеней свободы для χ^2 - \nраспределения равно m – k – 1 = " +
                Form1.m2 + " - 2 - 1 = " + (Form1.m2 - 2 - 1) + ". По таблице находим \nквантиль χ(0,95)^2(" + (Form1.m2 - 2 - 1) + ") = "
                + form.X(0.95, Form1.m2 - 2 - 1) + "\nВывод: так как χв^2 = " + Form1.tempsum7_2 + " < X(0.95)^2("
                + (Form1.m2 - 2 - 1) + ") = " + form.X(0.95, Form1.m2 - 2 - 1) + ", то гипотеза H0 о нормальном \nраспределении " +
                "величины X не противоречит выборочным данным.";


        }
    }
}
