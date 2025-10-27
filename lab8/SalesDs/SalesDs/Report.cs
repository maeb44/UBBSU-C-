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
    public partial class Report : Form
    {
        private SalesDataSet salesDataSet;

        public Report(SalesDataSet data)
        {
            InitializeComponent();
            salesDataSet = data;
        }

        private void Report_Load(object sender, EventArgs e)
        {
            try
            {
                GenerateProductsReport();
                FormatDataGridViews();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при формировании отчета: {ex.Message}",
                                "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void GenerateProductsReport()
        {
            // LINQ запрос для объединения Manufacturer и Product
            var products = (
                from manufacturer in salesDataSet.Manufacturer
                join product in salesDataSet.Product
                    on manufacturer.Field<int?>("ManufacturerID") equals product.Field<int?>("ManufacturerID")
                select new
                {
                    ManufacturerName = manufacturer.Field<string>("ManufacturerName") ?? "Не указан",
                    ProductName = product.Field<string>("ProductName") ?? "Без названия",
                    Price = product.Field<decimal?>("Price") ?? 0m,
                    ManufacturerID = manufacturer.Field<int?>("ManufacturerID") ?? 0,
                    ProductID = product.Field<int?>("ProductID") ?? 0
                }).ToArray();

            // Привязка данных к первому DataGridView (все товары)
            dataGridView1.DataSource = products;

            // Группировка по производителям (статистика)
            var manufacturerStats = (
                from product in products
                group product by product.ManufacturerName into manufacturerGroup
                select new
                {
                    ManufacturerName = manufacturerGroup.Key,
                    ProductCount = manufacturerGroup.Count(),
                    AveragePrice = manufacturerGroup.Average(p => p.Price),
                    MaxPrice = manufacturerGroup.Max(p => p.Price),
                    MinPrice = manufacturerGroup.Min(p => p.Price),
                    TotalValue = manufacturerGroup.Sum(p => p.Price)
                }).ToArray();

            // Привязка итоговых данных ко второму DataGridView
            dataGridView2.DataSource = manufacturerStats;
        }

        private void FormatDataGridViews()
        {
            // Форматирование первого DataGridView (список товаров)
            if (dataGridView1.Columns.Count > 0)
            {
                dataGridView1.Columns["ManufacturerName"].HeaderText = "Производитель";
                dataGridView1.Columns["ProductName"].HeaderText = "Товар";
                dataGridView1.Columns["Price"].HeaderText = "Цена";
                dataGridView1.Columns["ManufacturerID"].HeaderText = "ID производителя";
                dataGridView1.Columns["ProductID"].HeaderText = "ID товара";

                // Скрываем ID столбцы если не нужны
                dataGridView1.Columns["ManufacturerID"].Visible = false;
                dataGridView1.Columns["ProductID"].Visible = false;

                // Форматирование цены
                dataGridView1.Columns["Price"].DefaultCellStyle.Format = "C2";
                dataGridView1.Columns["Price"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

                dataGridView1.AutoResizeColumns();
            }

            // Форматирование второго DataGridView (статистика по производителям)
            if (dataGridView2.Columns.Count > 0)
            {
                dataGridView2.Columns["ManufacturerName"].HeaderText = "Производитель";
                dataGridView2.Columns["ProductCount"].HeaderText = "Количество товаров";
                dataGridView2.Columns["AveragePrice"].HeaderText = "Средняя цена";
                dataGridView2.Columns["MaxPrice"].HeaderText = "Максимальная цена";
                dataGridView2.Columns["MinPrice"].HeaderText = "Минимальная цена";
                dataGridView2.Columns["TotalValue"].HeaderText = "Общая стоимость";

                // Форматирование числовых столбцов
                dataGridView2.Columns["AveragePrice"].DefaultCellStyle.Format = "C2";
                dataGridView2.Columns["MaxPrice"].DefaultCellStyle.Format = "C2";
                dataGridView2.Columns["MinPrice"].DefaultCellStyle.Format = "C2";
                dataGridView2.Columns["TotalValue"].DefaultCellStyle.Format = "C2";

                dataGridView2.Columns["ProductCount"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridView2.Columns["AveragePrice"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dataGridView2.Columns["MaxPrice"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dataGridView2.Columns["MinPrice"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dataGridView2.Columns["TotalValue"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

                dataGridView2.AutoResizeColumns();
            }
        }
    }
}