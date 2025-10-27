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
    public partial class ManufacturerForm : Form
    {
        public ManufacturerForm(SalesDataSet data)
        {
            InitializeComponent();
            salesDataSet = data;
        }
        private void ManufacturerForm_Load(object sender, EventArgs e)
        {
            manufacturerBindingSource.DataSource = salesDataSet;
        }

    }
}
