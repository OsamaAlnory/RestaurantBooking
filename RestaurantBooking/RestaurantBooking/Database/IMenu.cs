using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantBooking.Database
{
    public class IMenu
    {
        public string ID { get; set; }
        public string RestID { get; set; }
        public string MenuName { get; set; }
        public string MenuImage { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }

    }
}
