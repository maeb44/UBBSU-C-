using ServiceCenterApp.Models;
using System;
using System.Windows.Forms;


namespace Service
{
    public partial class EditDeviceForm : Form
    {
        private Device _device;

        public EditDeviceForm(Device device)
        {
            InitializeComponent();
            _device = device;

            // Заполняем поля данными
            textBoxDeviceType.Text = _device.DeviceType;
            textBoxModel.Text = _device.Model;
            textBoxSerialNumber.Text = _device.SerialNumber;
            textBoxFaultDescription.Text = _device.FaultDescription;
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            // Обновляем данные устройства
            _device.DeviceType = textBoxDeviceType.Text;
            _device.Model = textBoxModel.Text;
            _device.SerialNumber = textBoxSerialNumber.Text;
            _device.FaultDescription = textBoxFaultDescription.Text;

            using (var db = new ServiceCenterContext())
            {
                db.Entry(_device).State = System.Data.Entity.EntityState.Modified; // <-- вот ключ
                db.SaveChanges();
            }

            DialogResult = DialogResult.OK;
            Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
