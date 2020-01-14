using RestaurantBooking.Database;
using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantBooking
{
    public class Main
    {
        private static List<Menu> menu = new List<Menu>();
        private static List<Reservation> resv = new List<Reservation>();
        private static List<Restaurant> rest = new List<Restaurant>();
        static Random random = new Random();

        public static string GenerateId()
        {
            string id = "RES";
            id += random.Next(1000, 9999);
            return id;
        }


    }
}
