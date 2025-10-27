using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sales
{
    public partial class Main : Form
    {
        private string fileName;
        private Button Delete = new Button();
        List<Sale> salesData = new List<Sale>();
        public Main()
        {
            InitializeComponent();
            saleBindingSource.DataSource = salesData;
        }


        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            dataGridView1.EndEdit();

            // Получаем все строки для удаления
            var rowsToDelete = dataGridView1.SelectedRows
                .Cast<DataGridViewRow>()
                .Where(r => !r.IsNewRow)
                .ToList();

            // Если нет выделенных строк, но есть текущая строка - добавляем её
            if (rowsToDelete.Count == 0 &&
                dataGridView1.CurrentRow != null &&
                !dataGridView1.CurrentRow.IsNewRow)
            {
                rowsToDelete.Add(dataGridView1.CurrentRow);
            }

            if (rowsToDelete.Count == 0)
            {
                MessageBox.Show("Нет строк для удаления", "Информация",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Подтверждение удаления
            if (MessageBox.Show($"Вы действительно хотите удалить {rowsToDelete.Count} строк(у)?",
                                "Подтверждение удаления",
                                MessageBoxButtons.YesNo,
                                MessageBoxIcon.Question) != DialogResult.Yes)
            {
                return;
            }

            // Удаляем строки
            foreach (var row in rowsToDelete)
            {
                dataGridView1.Rows.Remove(row);
            }
        }
        void ReadSales()
        {
            FileStream fs = new FileStream(fileName, FileMode.Open);
            StreamReader salesFile = new StreamReader(fs);
            saleBindingSource.Clear(); //очищаем все хранящиеся данные
            while (!salesFile.EndOfStream)
            {
                Sale s = new Sale();
                s.Read(salesFile);
                saleBindingSource.Add(s);
            }
            salesFile.Close();
        }
        void WriteSales()
        {
            FileStream fs = new FileStream(fileName, FileMode.Create);
            StreamWriter salesFile = new StreamWriter(fs);
            foreach (Sale s in saleBindingSource.List)
            {
                s.Write(salesFile);
            }
            salesFile.Close();
        }


        private void новыйToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saleBindingSource.Clear(); //очищаем все хранящиеся данные
            fileName = "";
        }
        private void открытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                fileName = openFileDialog1.FileName;
                ReadSales();
            }
        }
        private void сохранитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (fileName == "")
            {
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    fileName = saveFileDialog1.FileName;
                }
                else
                {
                    return;
                }
            }
            dataGridView1.EndEdit();
            WriteSales();
        }
        private void сохранитьКакToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                fileName = saveFileDialog1.FileName;
                dataGridView1.EndEdit();
                WriteSales();
            }
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }


    }
}
