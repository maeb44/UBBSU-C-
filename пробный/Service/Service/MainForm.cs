using ServiceCenterApp;
using ServiceCenterApp.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Service
{
    public partial class MainForm : Form
    {
        private Client _client;
        public MainForm()
        {
            InitializeComponent();
            LoadClients();
        }
    
        private void LoadClients()
        {
            using (var db = new ServiceCenterContext())
            {
                var clients = db.Clients.ToList();
                dataGridViewClients.DataSource = clients;
            }
        }
        private void buttonAdd_Click(object sender, EventArgs e)
        {
            using (var db = new ServiceCenterContext())
            {
                var newClient = new Client
                {
                    FullName = "Новый клиент",
                    Phone = "0000000000"
                };
                db.Clients.Add(newClient);
                db.SaveChanges();
            }
            LoadClients();
        }
        private void buttonEdit_Click(object sender, EventArgs e)
        {
            if (dataGridViewClients.CurrentRow != null)
            {
                int clientId = (int)dataGridViewClients.CurrentRow.Cells["ClientID"].Value;

                using (var db = new ServiceCenterContext())
                {
                    Client client = db.Clients.Find(clientId);
                    if (client != null)
                    {
                        EditClientForm editForm = new EditClientForm(client);
                        if (editForm.ShowDialog() == DialogResult.OK)
                        {
                            LoadClients(); // обновляем DataGridView
                        }
                    }
                }
            }
        }
        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (dataGridViewClients.CurrentRow == null) return;

            int clientId = (int)dataGridViewClients.CurrentRow.Cells["ClientID"].Value;

            using (var db = new ServiceCenterContext())
            {
                var client = db.Clients.Find(clientId);
                if (client != null)
                {
                    db.Clients.Remove(client);
                    db.SaveChanges();
                }
            }
            LoadClients();
        }
        private void buttonDevices_Click(object sender, EventArgs e)
        {
            DeviceForm form = new DeviceForm();
            form.ShowDialog();
        }
        private void buttonOrders_Click(object sender, EventArgs e)
        {
            OrdersForm form = new OrdersForm();
            form.ShowDialog();
        }
        private void buttonAbout_Click(object sender, EventArgs e)
        {
            About form = new About();
            form.ShowDialog();
        }
    }


 }