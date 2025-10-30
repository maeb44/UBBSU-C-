using ServiceCenterApp.Models;
using System;
using System.Linq;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;

namespace Service
{
    public partial class OrdersForm : Form
    {
        public OrdersForm()
        {
            InitializeComponent();
            LoadClients();
            LoadDevices();
            LoadOrders();
        }

        private void LoadClients()
        {
            using (var db = new ServiceCenterContext())
            {
                comboBoxClient.DataSource = db.Clients.ToList();
                comboBoxClient.DisplayMember = "FullName";
                comboBoxClient.ValueMember = "ClientID";
            }
        }

        private void LoadDevices()
        {
            using (var db = new ServiceCenterContext())
            {
                comboBoxDevice.DataSource = db.Devices.ToList();
                comboBoxDevice.DisplayMember = "Model";
                comboBoxDevice.ValueMember = "DeviceID";
            }
        }

        private void LoadOrders()
        {
            using (var db = new ServiceCenterContext())
            {
                var orders = db.Orders
                               .Select(o => new
                               {
                                   o.OrderID,
                                   o.ClientID,
                                   ClientName = o.Client.FullName,
                                   o.DeviceID,
                                   DeviceModel = o.Device.Model,
                                   o.DateReceived,
                                   o.DateCompleted,
                                   o.Status
                               })
                               .ToList();
                dataGridViewOrders.DataSource = orders;
            }
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            using (var db = new ServiceCenterContext())
            {
                var order = new Order
                {
                    ClientID = (int)comboBoxClient.SelectedValue,
                    DeviceID = (int)comboBoxDevice.SelectedValue,
                    DateReceived = dateTimePickerReceived.Value,
                    DateCompleted = dateTimePickerCompleted.Checked
                        ? (DateTime?)dateTimePickerCompleted.Value
                        : null,
                    Status = textBoxStatus.Text
                };
                db.Orders.Add(order);
                db.SaveChanges();
            }
            LoadOrders();
        }

        private void buttonEdit_Click(object sender, EventArgs e)
        {
            if (dataGridViewOrders.CurrentRow == null) return;

            int orderId = (int)dataGridViewOrders.CurrentRow.Cells["OrderID"].Value;

            using (var db = new ServiceCenterContext())
            {
                var order = db.Orders.Find(orderId);
                if (order != null)
                {
                    order.ClientID = (int)comboBoxClient.SelectedValue;
                    order.DeviceID = (int)comboBoxDevice.SelectedValue;
                    order.DateReceived = dateTimePickerReceived.Value;
                    order.DateCompleted = dateTimePickerCompleted.Checked
                        ? (DateTime?)dateTimePickerCompleted.Value
                        : null;
                    order.Status = textBoxStatus.Text;

                    db.SaveChanges();
                }
            }
            LoadOrders();
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (dataGridViewOrders.CurrentRow == null) return;

            int orderId = (int)dataGridViewOrders.CurrentRow.Cells["OrderID"].Value;

            using (var db = new ServiceCenterContext())
            {
                var order = db.Orders.Find(orderId);
                if (order != null)
                {
                    db.Orders.Remove(order);
                    db.SaveChanges();
                }
            }
            LoadOrders();
        }

        private void buttonRefresh_Click(object sender, EventArgs e)
        {
            LoadOrders();
        }

        private void dataGridViewOrders_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridViewOrders.CurrentRow == null) return;

            int clientId = (int)dataGridViewOrders.CurrentRow.Cells["ClientID"].Value;
            int deviceId = (int)dataGridViewOrders.CurrentRow.Cells["DeviceID"].Value;

            comboBoxClient.SelectedValue = clientId;
            comboBoxDevice.SelectedValue = deviceId;

            dateTimePickerReceived.Value = (DateTime)dataGridViewOrders.CurrentRow.Cells["DateReceived"].Value;

            var completed = dataGridViewOrders.CurrentRow.Cells["DateCompleted"].Value;
            if (completed != DBNull.Value)
            {
                dateTimePickerCompleted.Value = (DateTime)completed;
                dateTimePickerCompleted.Checked = true;
            }
            else
            {
                dateTimePickerCompleted.Checked = false;
            }

            textBoxStatus.Text = dataGridViewOrders.CurrentRow.Cells["Status"].Value.ToString();
        }

        // ===== Экспорт в Excel =====
        private void buttonExport_Click(object sender, EventArgs e)
        {
            if (dataGridViewOrders.Rows.Count == 0)
            {
                MessageBox.Show("Нет данных для экспорта.");
                return;
            }

            using (SaveFileDialog sfd = new SaveFileDialog() { Filter = "Excel Workbook|*.xlsx" })
            {
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    var excelApp = new Excel.Application();
                    excelApp.Workbooks.Add();
                    Excel._Worksheet workSheet = excelApp.ActiveSheet;

                    // Заголовки
                    for (int i = 0; i < dataGridViewOrders.Columns.Count; i++)
                    {
                        workSheet.Cells[1, i + 1] = dataGridViewOrders.Columns[i].HeaderText;
                    }

                    // Данные
                    for (int i = 0; i < dataGridViewOrders.Rows.Count; i++)
                    {
                        for (int j = 0; j < dataGridViewOrders.Columns.Count; j++)
                        {
                            workSheet.Cells[i + 2, j + 1] =
                                dataGridViewOrders.Rows[i].Cells[j].Value?.ToString();
                        }
                    }

                    // Форматируем Excel
                    workSheet.Rows[1].Font.Bold = true;
                    workSheet.Columns.AutoFit();

                    // Сохраняем файл
                    workSheet.SaveAs(sfd.FileName);
                    excelApp.Quit();

                    MessageBox.Show("Экспорт выполнен успешно!");
                }
            }
        }
    }
}
