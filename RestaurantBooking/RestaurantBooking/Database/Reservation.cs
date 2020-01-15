using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantBooking.Database
{
    public class Reservation
    {
        public string ID { get; set; }
        public string Menues { get; set; }
        public int TableNr { get; set; }
        public string DisplayName { get; set; }

    }
}
