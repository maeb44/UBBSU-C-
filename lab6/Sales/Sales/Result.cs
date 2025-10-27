using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sales
{
    public partial class Result : Form
    {
        private List<Sale> salesData;

        public Result(List<Sale> data)
        {
            InitializeComponent();
            salesData = data;
            dataGridView1.DataSource = salesData; // Привязываем данные к DataGridView
        }

        private void Result_Load(object sender, EventArgs e)
        {
            saleBindingSource.DataSource = salesData;
            decimal sum = 0;
            foreach (Sale s in salesData)
            {
                sum += s;
            }
            labelSum.Text =$"Общая сумма: {sum.ToString()} руб.";
        }
    }

}
