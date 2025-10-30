using System;

namespace ServiceCenterApp.Models
{
    public class Order
    {
        public int OrderID { get; set; }
        public int ClientID { get; set; }
        public int DeviceID { get; set; }
        public DateTime DateReceived { get; set; }
        public DateTime? DateCompleted { get; set; }
        public string Status { get; set; }

        public virtual Client Client { get; set; }
        public virtual Device Device { get; set; }
    }
}
