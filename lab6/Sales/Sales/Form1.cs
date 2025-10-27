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
            FileStream fs;
            try
            {
                fs = new FileStream(fileName, FileMode.Open);
            }
            catch (IOException e)
            {
                MessageBox.Show("Не удалось открыть файл " + fileName);
                return;
            }
            StreamReader salesFile = new StreamReader(fs);
            try
            {
                saleBindingSource.Clear();//очищаем все хранящиеся данные
                while (!salesFile.EndOfStream)
                {
                    Sale s = new Sale();
                    s.Read(salesFile);
                    saleBindingSource.Add(s);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Ошибка чтения файла " + fileName);
                return;
            }
            finally
            {
                salesFile.Close();
            }
        }

        void WriteSales()
        {
            FileStream fs;
            try
            {
                fs = new FileStream(fileName, FileMode.Create);
            }
            catch (IOException e)
            {
                MessageBox.Show("Не удалось открыть файл " + fileName);
                return;
            }
            StreamWriter salesFile = new StreamWriter(fs);
            try
            {
                foreach (Sale s in saleBindingSource.List)
                {
                    s.Write(salesFile);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Ошибка чтения файла " + fileName);
                return;
            }
            finally
            {
                salesFile.Close();
            }
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

        private void Main_Load(object sender, EventArgs e)
        {
            DateTime d = DateTime.Today;
            dateTimePicker2.Value = d;
            dateTimePicker1.Value = new DateTime(d.Year, d.Month, 1);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            List<Sale> sd = salesData.FindAll(
            delegate (Sale sale)
            {
                if (dateTimePicker1.Checked && sale.SaleDate < dateTimePicker1.Value)
                    return false;
                if (dateTimePicker2.Checked && sale.SaleDate > dateTimePicker2.Value)
                    return false;
                if (textBoxName.Text != "" && sale.Name != textBoxName.Text)
                    return false;
                return true;
            });

            if (sd.Count == 0)
            {
                MessageBox.Show("Нет данных, соответствующих критериям фильтра", "Информация",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (checkBoxSort.Checked) sd.Sort(
            delegate (Sale s1, Sale s2)
            {
                // Проверяем на null перед сортировкой
                if (s1 == null || s2 == null) return 0;

                int res = s1.SaleDate.CompareTo(s2.SaleDate);
                if (res == 0)
                {
                    // Безопасное сравнение с учетом null
                    return string.Compare(s1.Name, s2.Name, StringComparison.Ordinal);
                }
                else
                {
                    return res;
                }
            });

            Form f = new Result(sd);
            f.Show();
        }


        private void dataGridView1_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            MessageBox.Show("Ошибка при вводе данных");
        }

    }

}
