using ServiceCenterApp.Models;
using System;
using System.Linq;
using System.Windows.Forms;

namespace Service
{
    public partial class DeviceForm : Form
    {
        public DeviceForm()
        {
            InitializeComponent();
            LoadDevices();
        }

        private void LoadDevices()
        {
            using (var db = new ServiceCenterContext())
            {
                var devices = db.Devices.ToList();
                dataGridViewDevices.DataSource = devices;
            }
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            using (var db = new ServiceCenterContext())
            {
                var newDevice = new Device
                {
                    DeviceType = "Новый тип",
                    Model = "Новая модель",
                    SerialNumber = "0000",
                    FaultDescription = ""
                };
                db.Devices.Add(newDevice);
                db.SaveChanges();
            }
            LoadDevices();
        }

        private void buttonEdit_Click(object sender, EventArgs e)
        {
            if (dataGridViewDevices.CurrentRow != null)
            {
                int deviceId = (int)dataGridViewDevices.CurrentRow.Cells["DeviceID"].Value;
                using (var db = new ServiceCenterContext())
                {
                    var device = db.Devices.Find(deviceId);
                    if (device != null)
                    {
                        EditDeviceForm editForm = new EditDeviceForm(device);
                        if (editForm.ShowDialog() == DialogResult.OK)
                        {
                            LoadDevices();
                        }
                    }
                }
            }
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (dataGridViewDevices.CurrentRow == null) return;

            int deviceId = (int)dataGridViewDevices.CurrentRow.Cells["DeviceID"].Value;
            using (var db = new ServiceCenterContext())
            {
                var device = db.Devices.Find(deviceId);
                if (device != null)
                {
                    db.Devices.Remove(device);
                    db.SaveChanges();
                }
            }
            LoadDevices();
        }

        private void buttonRefresh_Click(object sender, EventArgs e)
        {
            LoadDevices();
        }
    }
}