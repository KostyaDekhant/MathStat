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
    public partial class FourthP : Form
    {
        public FourthP()
        {
            InitializeComponent();
        }
        private string path = @"C:\Users\Podor\Documents\GitHub\MathStat\MathStat\first.png";
        private string path1 = @"C:\Users\Podor\Documents\GitHub\MathStat\MathStat\second.png";
        private void FourthP_Load(object sender, EventArgs e)
        {
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
            label5.BackColor = clrBack;
            label6.BackColor = clrBack;
            label1.ForeColor = clrFont;
            label2.ForeColor = clrFont;
            label3.ForeColor = clrFont;
            label4.ForeColor = clrFont;
            label5.ForeColor = clrFont;
            label6.ForeColor = clrFont;
            form.tInit();
            form.t((1 - (1 - Form1.y) / 2), Form1.N - 1);
            label6.Font = new System.Drawing.Font("Arial", (int)(9 * Form1.multiplier), System.Drawing.FontStyle.Bold);
            label5.Font = new System.Drawing.Font("Arial", (int)(9 * Form1.multiplier), System.Drawing.FontStyle.Bold);
            label4.Font = new System.Drawing.Font("Arial", (int)(9 * Form1.multiplier), System.Drawing.FontStyle.Bold);
            label3.Font = new System.Drawing.Font("Arial", (int)(9 * Form1.multiplier), System.Drawing.FontStyle.Bold);
            label2.Font = new System.Drawing.Font("Arial", (int)(9 * Form1.multiplier), System.Drawing.FontStyle.Bold);
            label1.Font = new System.Drawing.Font("Arial", (int)(9 * Form1.multiplier), System.Drawing.FontStyle.Bold);
            label3.Text = "    Доверительный интервал для математического ожидания\r\nM(X) при неизвестном σ имеет вид (см. пункт 3.2.2):";
            pic1.Image = Image.FromFile(path);
            pic2.Image = Image.FromFile(path1);
            label1.Text = "    Учитывая, что x_ = " + Form1.x_+ ", sx = "+ Form1.sx + ", n = " + Form1.N + ", α = 1 - "+ Form1.y+" = " 
                + (1-Form1.y).ToString() + ", t1-α/2(n-1) = t" + (1- (1 - Form1.y)/2).ToString()+"(" + (Form1.N-1).ToString()+
                ") = " + Form1.CalcT.ToString() + ", получим с надёжностью y = " + Form1.y + ":\n" + form.CalcMX();
            label2.Text = "    Аналогично получим доверительный интервал для математического ожидания M(Y) (учитывая, что y_ = " + Form1.y_ +
                ", sy = "+ Form1.sy + ":\n" + form.CalcMY();
            label3.Location = new Point(20, 40);
            pic1.Location = new Point(label3.Location.X, label3.Location.Y + (int)(30 * Form1.multiplier));
            pic2.Size = new Size(pic2.Width, pic2.Height+35);
            label1.Location = new Point(pic1.Location.X, pic1.Location.Y + pic1.Height + (int)(20 * Form1.multiplier));
            label2.Location = new Point(pic1.Location.X, label1.Location.Y + label1.Height + (int)(20 * Form1.multiplier));
            label4.Location = new Point(pic1.Location.X, label2.Location.Y + label2.Height + (int)(20 * Form1.multiplier));
            
            label4.Text = "Доверительный интервал для дисперсии D(X), как было ранее показано, имеет вид:";
            label5.Text = "    Так как sx^2 = " + Form1.s2x + "; χ(1-α/2)^2(n-1) = χ(" + (1-(1- Form1.y)/2)+ ")^2(" + 
                (Form1.N-1) + ") = " + form.X(1-(1-Form1.y)/2, Form1.N-16) + ";\n" + "χ(α/2)^2(n-1) = χ("+ (1 - Form1.y) / 2 
                + ")^2(" + (Form1.N - 1) + ") = " + form.X((1 - Form1.y) / 2, Form1.N - 16)+", то с надёжностью y = 0,95:\n"+
                Math.Round((Form1.N - 1)*Form1.s2x/ form.X(1 - (1 - Form1.y) / 2, Form1.N - 16),2) +  " < D(X) < " + 
                Math.Round((Form1.N - 1) * Form1.s2x / form.X((1 - Form1.y) / 2, Form1.N - 16), 2);

            label6.Text = "    Аналогично определяется доверительный интервал для дисперсии D(Y) с учётом, что sy^2 = " + Form1.s2y + ":\n" +
                Math.Round((Form1.N - 1) * Form1.s2y / form.X(1 - (1 - Form1.y) / 2, Form1.N - 16), 2) + " < D(Y) < " +
                Math.Round((Form1.N - 1) * Form1.s2y / form.X((1 - Form1.y) / 2, Form1.N - 16), 2);
            pic2.Location = new Point(label4.Location.X, label4.Location.Y + (int)(30 * Form1.multiplier));
            label5.Location = new Point(pic1.Location.X, pic2.Location.Y + pic2.Height + (int)(20 * Form1.multiplier));
            label6.Location = new Point(pic1.Location.X, pic2.Location.Y + pic2.Height + 50 + (int)(20 * Form1.multiplier));
        }
    }
}
