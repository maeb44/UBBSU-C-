using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace SalesDs
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }
        private void MainForm_Load(object sender, EventArgs e)
        {
            salesDataSet.Sale.Columns["SaleDate"].DefaultValue =
            DateTime.Today;
            try
            {

                salesDataSet.ReadXml(Properties.Settings.Default.FileName);
            }
            catch
            {
                MessageBox.Show("Ошибка чтения данных");
            }
        }
        private void сохранитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Validate();
            saleBindingSource.EndEdit();
            try
            {
                salesDataSet.WriteXml(Properties.Settings.Default.FileName);
            }
            catch
            {
                MessageBox.Show("Ошибка сохранения данных");
            }
        }
        private void производителиToolStripMenuItem_Click(object sender,EventArgs e)
        {
            ManufacturerForm f = new ManufacturerForm(salesDataSet);
            f.Show();
        }
        private void товарыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ProductForm f = new ProductForm(salesDataSet);
            f.Show();
        }

        private void товарыПоПроизводителямToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ManufacturerProductForm f =
            new ManufacturerProductForm(salesDataSet);
            f.Show();
        }
        private void отчетToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Report f = new Report(salesDataSet);
            f.Show();
        }

    }
}
