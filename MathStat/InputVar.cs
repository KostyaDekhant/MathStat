using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows.Forms;

namespace MathStat
{
    public partial class InputVar : Form
    {
        public InputVar()
        {
            InitializeComponent();
        }

        private void InputVar_Load(object sender, EventArgs e)
        {
            PictureBox[] pic = new PictureBox[4];
            DataGridView dataGridView1 = new DataGridView();
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
            label1.ForeColor = clrFont;
            string[] str = { "xi", "yi", "xi", "yi", "xi", "yi", "xi", "yi", "xi", "yi" };
            string[] str2 = { };
            form.DrawDataGrid(str2, new Point(20, 40), new Size((int)(400 * Form1.multiplier), (int)(420 * Form1.multiplier)), dataGridView1, 11, 10); 
            //form.DrawDataGrid(str2, new Point(20, 40), new Size(1, 1), dataGridView1, 11, 10); 
            double[] W = Form1.weight.ToArray();
            int[] H = Form1.height.ToArray(); 

            label1.Font = new System.Drawing.Font("Arial", (int)(9 * Form1.multiplier), System.Drawing.FontStyle.Bold);
            label1.Text = "Исходные данные";
            label1.Location = new Point(dataGridView1.Location.X, dataGridView1.Location.Y - (int)(20 * Form1.multiplier));

            for (int i = 0; i < 10; i++)
            {
                dataGridView1.Columns[i].HeaderText = str[i];
                dataGridView1.Columns[i].Width = (int)(40 * Form1.multiplier);
                dataGridView1.Rows[i].Height = (int)(40 * Form1.multiplier);
                dataGridView1.Rows[i].Cells[0].Value = W[5*i];
                dataGridView1.Rows[i].Cells[2].Value = W[5*i+1];
                dataGridView1.Rows[i].Cells[4].Value = W[5*i+2];
                dataGridView1.Rows[i].Cells[6].Value = W[5*i+3];
                dataGridView1.Rows[i].Cells[8].Value = W[5*i+4];
                dataGridView1.Rows[i].Cells[0 + 1].Value = H[5 * i];
                dataGridView1.Rows[i].Cells[2 + 1].Value = H[5 * i + 1];
                dataGridView1.Rows[i].Cells[4 + 1].Value = H[5 * i + 2];
                dataGridView1.Rows[i].Cells[6 + 1].Value = H[5 * i + 3];
                dataGridView1.Rows[i].Cells[8 + 1].Value = H[5 * i + 4];
            }
            for (int i = 0; i < 4; i++)
            {
                pic[i] = new PictureBox();
                pic[i].Size = new Size((int)(1 * Form1.multiplier), (int)(420 * Form1.multiplier));
                pic[i].Location = new Point((int)((20 + 40 * 2 * (i + 1) * Form1.multiplier)), 40);
                pic[i].BackColor = Color.Black;
                Controls.Add(pic[i]);
            }
            if (Form1.multiplier == 1.5)
            {
                for (int i = 0; i < 4; i++)
                {
                    pic[i].Height -= 6;
                }
                dataGridView1.Height -= 6;
            }
            Controls.Add(dataGridView1);
            dataGridView1.ClearSelection();
        }
    }
}
