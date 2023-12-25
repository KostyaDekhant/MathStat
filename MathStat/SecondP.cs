using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using TheArtOfDevHtmlRenderer.Adapters;

namespace MathStat
{
    public partial class SecondP : Form
    {
        public SecondP()
        {
            InitializeComponent();
        }

        private void SecondPage_Load(object sender, EventArgs e)
        {
            InitTable();
            
        }

        private string path1 = @"C:\Users\Podor\Documents\GitHub\MathStat\MathStat\u_.png";
        private string path2 = @"C:\Users\Podor\Documents\GitHub\MathStat\MathStat\v_.png"; 
        private string path3 = @"C:\Users\Podor\Documents\GitHub\MathStat\MathStat\u2_.png";
        private string path4 = @"C:\Users\Podor\Documents\GitHub\MathStat\MathStat\v2_.png";
        private string path5 = @"C:\Users\Podor\Documents\GitHub\MathStat\MathStat\su2.png";
        private string path6 = @"C:\Users\Podor\Documents\GitHub\MathStat\MathStat\sv2.png";
        private string path7 = @"C:\Users\Podor\Documents\GitHub\MathStat\MathStat\susv.png";
        private void InitTable()
        {
            Form1 form = new Form1();
            //form.ThirdTable();
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
            label7.BackColor = clrBack;
            label1.ForeColor = clrFont;
            label2.ForeColor = clrFont;
            label3.ForeColor = clrFont;
            label4.ForeColor = clrFont;
            label5.ForeColor = clrFont;
            label6.ForeColor = clrFont;
            label7.ForeColor = clrFont;
            form.DrawDataGrid(Form1.ThirdHeaders, new Point(20, 40), new Size((int)((40 + 60 * 8) * Form1.multiplier), (int)((179 + 21) * Form1.multiplier)), dataGrid1, 9, 9);
            dataGrid1.ColumnHeadersHeight = (int)(25 * Form1.multiplier);
            form.FillTable3(dataGrid1);
            label1.Location = new Point(dataGrid1.Location.X, dataGrid1.Location.Y - (int)(20 * Form1.multiplier));
            label1.Text = "Таблица 4 – Группированные данные для условных вариант";
            label1.Font = new System.Drawing.Font("Arial", 11, System.Drawing.FontStyle.Bold);
            label2.Font = new System.Drawing.Font("Arial", 11 , System.Drawing.FontStyle.Bold);
            label3.Font = new System.Drawing.Font("Arial", 11, System.Drawing.FontStyle.Bold);
            label4.Font = new System.Drawing.Font("Arial", 11, System.Drawing.FontStyle.Bold);
            label5.Font = new System.Drawing.Font("Arial", 11, System.Drawing.FontStyle.Bold);
            label6.Font = new System.Drawing.Font("Arial", 11, System.Drawing.FontStyle.Bold);
            label7.Font = new System.Drawing.Font("Arial", 11, System.Drawing.FontStyle.Bold);
            label8.Font = new System.Drawing.Font("Arial", 11, System.Drawing.FontStyle.Bold);
            label9.Font = new System.Drawing.Font("Arial", 11, System.Drawing.FontStyle.Bold);
            pic1.Location = new Point(dataGrid1.Location.X, dataGrid1.Location.Y +  dataGrid1.Height + 30);
            pic2.Location = new Point(pic1.Location.X + pic1.Width+ 200, dataGrid1.Location.Y + dataGrid1.Height + 30);
            pic3.Location = new Point(dataGrid1.Location.X-5, pic1.Location.Y + pic1.Height + 30);
            pic4.Location = new Point(pic1.Location.X + pic1.Width + 200-5, pic1.Location.Y + pic1.Height + 30);
            pic5.Location = new Point(dataGrid1.Location.X, pic3.Location.Y + pic3.Height + 30);
            pic6.Location = new Point(pic1.Location.X+ pic1.Width + 330, pic3.Location.Y + pic3.Height + 30);
            pic7.Location = new Point(pic2.Location.X+ pic2.Width + 200, pic1.Location.Y - 10 + 30);
            pic1.Size = new Size(163, 90);
            pic2.Size = new Size(180, 90);
            pic3.Size = new Size(180, 90);
            pic4.Size = new Size(180, 90);
            pic5.Size = new Size(220, 90);
            pic6.Size = new Size(230, 90);
            pic7.Size = new Size(55, 117);
            label2.Location = new Point(pic1.Location.X + pic1.Width, pic1.Location.Y  +15 + 20);
            label3.Location = new Point(pic2.Location.X + pic2.Width-25, pic1.Location.Y  + 15 + 25);
            label4.Location = new Point(pic1.Location.X + pic1.Width + 6, pic3.Location.Y + 10 + 25);
            label5.Location = new Point(pic2.Location.X + pic2.Width, pic3.Location.Y + 10 + 25);
            label6.Location = new Point(pic1.Location.X + pic1.Width+60, pic5.Location.Y + 10 + 20);
            label7.Location = new Point(pic6.Location.X + pic6.Width+10, pic5.Location.Y + 10 + 20);
            label8.Location = new Point(pic1.Location.X + pic1.Width + 335 + pic7.Width + 100, pic7.Location.Y+15) ;
            label9.Location = new Point(pic1.Location.X + pic1.Width + 330 + pic7.Width+ 100, pic7.Location.Y+80);

           
            pic1.Image = System.Drawing.Image.FromFile(path1);
            pic2.Image = System.Drawing.Image.FromFile(path2);
            pic3.Image = System.Drawing.Image.FromFile(path3);
            pic4.Image = System.Drawing.Image.FromFile(path4);
            pic5.Image = System.Drawing.Image.FromFile(path5);
            pic6.Image = System.Drawing.Image.FromFile(path6);
            pic7.Image = System.Drawing.Image.FromFile(path7);
            label2.Text = Form1.sum[2] + "/" + Form1.N + " = " + Math.Round(Form1.sum[2]/Form1.N,2);
            label3.Text = Form1.sum[6] + "/" + Form1.N + " = " + Math.Round(Form1.sum[6] / Form1.N, 2); ;
            label4.Text = "= " +Form1.sum[3] + "/" + Form1.N + " = " + Math.Round(Form1.sum[3] / Form1.N, 2) ;
            label5.Text = Form1.sum[7] + "/" + Form1.N + " = " + Math.Round(Form1.sum[7] / Form1.N, 2); ;
            label6.Text = Form1.N + "/"+(Form1.N-1)+"(" + Math.Round(Form1.sum[3] / Form1.N, 2)+ " - (" +
                Math.Round(Form1.sum[2] / Form1.N, 2) + ")^2) = "+ (Form1.su2);
            label7.Text = Form1.N + "/" + (Form1.N - 1) + "(" + Math.Round(Form1.sum[7] / Form1.N, 2) + " - (" +
                Math.Round(Form1.sum[6] / Form1.N, 2) + ")^2) = " + (Form1.sv2);
            label8.Text = (Form1.su) + "";
            label9.Text = (Form1.sv) + "";
            label2.BringToFront();
            label3.BringToFront();
            label4.BringToFront();
            label5.BringToFront();
            label6.BringToFront();
            label7.BringToFront();
            label8.BringToFront();
            label9.BringToFront();

        }
    }
}
