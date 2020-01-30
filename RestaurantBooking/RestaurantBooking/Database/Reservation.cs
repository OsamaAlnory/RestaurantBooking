using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantBooking.Database
{
    public class Reservation
    {
        public string RestID { get; set; }
        public string ID { get; set; }
        public string Menus { get; set; }
        public int TableNr { get; set; }
        public string DisplayName { get; set; }
        public string Status { get; set; }

    }
}
