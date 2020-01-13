using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantBooking.Database
{
    public class User
    {
        public static List<User> users = new List<User>();

        public string RestID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public int UType { get; set; }


        public static User GetUserByName(string name)
        {
            for(int x = 0; x < users.Count; x++)
            {
                if(users[x].Username == name)
                {
                    return users[x];
                }
            }
            return null;
        }

    }
}
