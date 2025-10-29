using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SalesDb
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void saleBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.saleBindingSource.EndEdit();

        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            salesDataSet.Sale.Columns["SaleDate"].DefaultValue =DateTime.Today;
            this.saleTableAdapter.Fill(this.salesDataSet.Sale);

        }
        private void товарыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ProductForm f = new ProductForm();
            f.Show();
        }
        private void отчетыToolStripMenuItem_Click(object sender,EventArgs e)
        {
            this.Validate();
            this.saleBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.salesDataSet);
            ReportForm f = new ReportForm();
            f.Show();
        }

        private void saleDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
