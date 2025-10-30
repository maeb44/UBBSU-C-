using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenterApp.Models
{
    public class Device
    {
        public int DeviceID { get; set; }
        public string DeviceType { get; set; }
        public string Model { get; set; }
        public string SerialNumber { get; set; }
        public string FaultDescription { get; set; }
    }
}
