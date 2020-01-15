using RestaurantBooking.Components;
using RestaurantBooking.Database;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace RestaurantBooking
{
    public class Main
    {
        private static List<IMenu> menu = new List<IMenu>();
        private static List<Reservation> resv = new List<Reservation>();
        private static List<Restaurant> rest = new List<Restaurant>();
        static Random random = new Random();

        public static string GenerateId()
        {
            string id = "RES";
            id += random.Next(1000, 9999);
            return id;
        }

        public static void AnimateGrowth(Taskable taskable, int factor, int speed,
            string id)
        {
            Animate(taskable, factor, speed, id, "growth");
        }

        private static void Animate(Taskable taskable, int factor, int speed, string id
            , string AnimationType)
        {
            double a = 0;
            bool f = false;
            bool b = false;
            Device.StartTimer(TimeSpan.FromMilliseconds(20), () =>
            {
                if (!f)
                {
                    a++;
                }
                else
                {
                    a--;
                }
                if (a >= 5)
                {
                    f = true;
                }
                if (a <= -5)
                {
                    f = false;
                }
                if(taskable is View)
                {
                    View view = (View)taskable;
                    view.Scale += a / 220;
                    if (view.Scale == 1 && b)
                    {
                        taskable.OnRecieve(id);
                        return false;
                    }
                }
                b = true;
                return true;
            });
        }

    }
}
