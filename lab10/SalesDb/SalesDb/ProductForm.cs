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
    public partial class ProductForm : Form
    {
        public ProductForm()
        {
            InitializeComponent();
        }

        private void manufacturerBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.manufacturerBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.salesDataSet);

        }

        private void ProductForm_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "salesDataSet.Product". При необходимости она может быть перемещена или удалена.
            this.productTableAdapter.Fill(this.salesDataSet.Product);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "salesDataSet.Manufacturer". При необходимости она может быть перемещена или удалена.
            this.manufacturerTableAdapter.Fill(this.salesDataSet.Manufacturer);

        }

        private void manufacturerBindingSource_DataError(object sender,BindingManagerDataErrorEventArgs e)
        {
            MessageBox.Show("Ошибка в данных");
        }
        private void productBindingSource_DataError(object sender,BindingManagerDataErrorEventArgs e)
        {
            MessageBox.Show("Ошибка в данных");
        }
        private void manufacturerDataGridView_DataError(object sender,DataGridViewDataErrorEventArgs e)
        {
            MessageBox.Show("Ошибка в данных");
        }
        private void productDataGridView_DataError(object sender,DataGridViewDataErrorEventArgs e)
        {
            MessageBox.Show("Ошибка в данных");
        }
        private void productDataGridView_Enter(object sender, EventArgs e)
        {
            if (salesDataSet.HasChanges(DataRowState.Added))
            {
                DataRowView dr =
                (DataRowView)manufacturerBindingSource.Current;
                if (dr != null)
                {
                    string st = dr["ManufacturerName"] as string;
                    if (st != null)
                    {
                        Validate();
                        manufacturerBindingSource.EndEdit();
                        tableAdapterManager.UpdateAll(this.salesDataSet);

                        manufacturerTableAdapter.Fill(this.salesDataSet.Manufacturer);
                        int pos =
                       this.manufacturerBindingSource.Find("ManufacturerName", st);
                        manufacturerBindingSource.Position = pos;
                    }
                }
            }
        }

    }
}
