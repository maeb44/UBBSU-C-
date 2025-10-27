using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SalesDs
{
    public partial class ProductForm : Form
    {

        public ProductForm(SalesDataSet data)
        {
            InitializeComponent();
            salesDataSet = data;
        }
        private void ProductForm_Load(object sender, EventArgs e)
        {
            manufacturerBindingSource.DataSource = salesDataSet;
            productBindingSource.DataSource = salesDataSet;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                productBindingSource.RemoveFilter();
            }
            else
            {
                productBindingSource.Filter = "ProductName LIKE '*" + textBox1.Text
               + "*'";
            }
        }
    }
}
