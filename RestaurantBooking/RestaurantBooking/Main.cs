using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantBooking
{
    public class Main
    {
        static Random random = new Random();

        public static string GenerateId()
        {
            string id = "RES";
            id += random.Next(1000, 9999);
            return id;
        }


    }
}
