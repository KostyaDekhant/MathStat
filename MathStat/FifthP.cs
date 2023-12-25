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
    public partial class FifthP : Form
    {
        public FifthP()
        {
            InitializeComponent();
        }

        private void FoutrhP_Load(object sender, EventArgs e)
        {
            InitTable();
            nameTable.Text = "Таблица 7 – Корреляционная таблица";
        }

        private void InitTable() 
        {
            Form1 form = new Form1();
            
            string[] SixHeaders = { };
            form.DrawDataGrid(SixHeaders, new Point(20, 40),
                new Size((int)((40 + 50 * 9+70) * Form1.multiplier), (int)((179 + 20 + 15 + 30 * 7 + 10 ) * Form1.multiplier)), dataGrid1, 9, 9);
            form.FillTable6(dataGrid1);
            if(Form1.multiplier == 1.5)
                dataGrid1.Size = new Size(dataGrid1.Width, dataGrid1.Height-110);
            for (int i = 0; i < 9; i++)
            {
                dataGrid1.Columns[i].Width = (int)(65 *Form1.multiplier);
            }
            dataGrid1.Columns[0].HeaderText = "X/Y";

            for (int i = 0; i < 7; i++)
            {
                dataGrid1.Rows[i].Cells[8].Value = form.FindNumsW(Form1.a[i], Form1.a[i + 1]).ToString();
                dataGrid1.Rows[7].Cells[i + 1].Value = form.FindNumsH(Form1.b[i], Form1.b[i + 1]).ToString();
            }
            dataGrid1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            dataGrid1.ColumnHeadersHeight = (int)(47 * Form1.multiplier);
            nameTable.Location = new Point(dataGrid1.Location.X, dataGrid1.Location.Y - (int)(20 * Form1.multiplier));
            nameTable.Font = new System.Drawing.Font("Arial", (int)(9 * Form1.multiplier), System.Drawing.FontStyle.Bold);
            nameTable.Text = "Таблица 7 – Корреляционная таблица";
        }
    }
}
