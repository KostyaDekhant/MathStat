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

        private void InitTable()
        {
            Form1 form = new Form1();
            //form.ThirdTable();
            form.DrawDataGrid(Form1.ThirdHeaders, new Point(20, 40), new Size((int)((40 + 60 * 8) * Form1.multiplier), (int)((179 + 21) * Form1.multiplier)), dataGrid1, 9, 9);
            dataGrid1.ColumnHeadersHeight = (int)(25 * Form1.multiplier);
            form.FillTable3(dataGrid1);
            label1.Location = new Point(dataGrid1.Location.X, dataGrid1.Location.Y - (int)(20 * Form1.multiplier));
            label1.Text = "Таблица 4 – Группированные данные для условных вариант";
            label1.Font = new System.Drawing.Font("Arial", (int)(9 * Form1.multiplier), System.Drawing.FontStyle.Bold);
        }
    }
}
