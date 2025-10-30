using ServiceCenterApp.Models;
using System;
using System.Windows.Forms;

namespace ServiceCenterApp
{
    public partial class EditClientForm : Form
    {
        private Client _client;

        public EditClientForm(Client client)
        {
            InitializeComponent();
            _client = client;

            // Заполняем поля текущими данными
            txtFullName.Text = _client.FullName;
            txtPhone.Text = _client.Phone;
            txtEmail.Text = _client.Email;
            txtAddress.Text = _client.Address;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            // Обновляем данные клиента
            _client.FullName = txtFullName.Text;
            _client.Phone = txtPhone.Text;
            _client.Email = txtEmail.Text;
            _client.Address = txtAddress.Text;

            using (var db = new ServiceCenterContext())
            {
                db.Entry(_client).State = System.Data.Entity.EntityState.Modified; // <-- вот ключ
                db.SaveChanges();
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}