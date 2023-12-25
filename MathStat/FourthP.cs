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
        private void FourthP_Load(object sender, EventArgs e)
        {
            Form1 form = new Form1();
            form.tInit();
            form.t((1 - (1 - Form1.y) / 2), Form1.N - 1);
            label3.Font = new System.Drawing.Font("Arial", (int)(9 * Form1.multiplier), System.Drawing.FontStyle.Bold);
            label2.Font = new System.Drawing.Font("Arial", (int)(9 * Form1.multiplier), System.Drawing.FontStyle.Bold);
            label1.Font = new System.Drawing.Font("Arial", (int)(9 * Form1.multiplier), System.Drawing.FontStyle.Bold);
            label3.Text = "    Доверительный интервал для математического ожидания\r\nM(X) при неизвестном σ имеет вид (см. пункт 3.2.2):";
            pic1.Image = Image.FromFile(path);
            label1.Text = "    Учитывая, что x_ = " + Form1.x_+ ", sx = "+ Form1.sx + ", n = " + Form1.N + ", α = 1 - "+ Form1.y+" = " 
                + (1-Form1.y).ToString() + ", t1-α/2(n-1) = t" + (1- (1 - Form1.y)/2).ToString()+"(" + (Form1.N-1).ToString()+
                ") = " + Form1.CalcT.ToString() + ", получим с надёжностью y = " + Form1.y + ":\n" + form.CalcMX();
            label2.Text = "    Аналогично получим доверительный интервал для математического ожидания M(Y) (учитывая, что y_ = " + Form1.y_ +
                ", sy = "+ Form1.sy + ":\n" + form.CalcMY();
        }
    }
}
