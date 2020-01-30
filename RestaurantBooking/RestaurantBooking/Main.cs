using Newtonsoft.Json;
using RestaurantBooking.Components;
using RestaurantBooking.Database;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace RestaurantBooking
{
    public class Main
    {
        private const string LINK = "http://windows.u7979705.fsdata.se/api/";
        private const string RM = "RBMenus";
        private const string RRS = "RBRestaurants";
        private const string RU = "RBUsers";
        private const string RI = "RBImages";
        private const string RRV = "RBReservations";
        private static char[] IM = { 'a', 'b', 'c', 'd', 'e', 'f', 'g'};
        public static List<IMenu> menu = new List<IMenu>();
        public static List<Reservation> resv = new List<Reservation>();
        public static List<Restaurant> rest = new List<Restaurant>();
        static Random random = new Random();
        public static List<User> users = new List<User>();


        public static async Task Load()
        {
            await LoadUsers();
            await LoadRest();
        }

        public static async Task LoadAdmin(Restaurant rest)
        {
            await LoadMenus(rest);
            await LoadResv(rest);
        }

        public static async Task<HttpResponseMessage> AddUser(User user)
        {
            users.Add(user);
            var json = JsonConvert.SerializeObject(user);
            var c = new StringContent(json, Encoding.UTF8, "application/json");
            HttpClient client = new HttpClient();
            return await client.PostAsync(LINK + RU, c);
        }

        public static async Task<HttpResponseMessage> AddRest(Restaurant res)
        {
            rest.Add(res);
            var json = JsonConvert.SerializeObject(res);
            var c = new StringContent(json, Encoding.UTF8, "application/json");
            HttpClient client = new HttpClient();
            return await client.PostAsync(LINK + RRS, c);
        }

        public static async Task<HttpResponseMessage> AddMenu(IMenu m)
        {
            menu.Add(m);
            var json = JsonConvert.SerializeObject(m);
            var c = new StringContent(json, Encoding.UTF8, "application/json");
            HttpClient client = new HttpClient();
            return await client.PostAsync(LINK + RM, c);
        }

        public static async Task<HttpResponseMessage> AddResv(Reservation r)
        {
            //resv.Add(r);
            var json = JsonConvert.SerializeObject(r);
            var c = new StringContent(json, Encoding.UTF8, "application/json");
            HttpClient client = new HttpClient();
            return await client.PostAsync(LINK + RRV, c);
        }

        public static async Task<HttpResponseMessage> AddImage(IImage i)
        {
            var json = JsonConvert.SerializeObject(i);
            var c = new StringContent(json, Encoding.UTF8, "application/json");
            HttpClient client = new HttpClient();
            return await client.PostAsync(LINK + RI, c);
        }

        public static async Task<HttpResponseMessage> RemoveUser(User user)
        {
            HttpClient client = new HttpClient();
            return await client.DeleteAsync(LINK + RU + "/" + user.RestID);
        }

        public static async Task<HttpResponseMessage> RemoveMenu(IMenu m)
        {
            for (int x = 0; x < menu.Count; x++)
            {
                IMenu me = menu[x];
                if (me.ID == m.ID)
                {
                    menu.Remove(me);
                    await RemoveImage(me.MenuImage);
                    x = -1;
                }
            }
            HttpClient client = new HttpClient();
            return await client.DeleteAsync(LINK + RM + "/" + m.ID);
        }

        public static async Task<HttpResponseMessage> RemoveResv(Reservation resv)
        {
            for (int x = 0; x < Main.resv.Count; x++)
            {
                Reservation r = Main.resv[x];
                if (r.ID == resv.ID)
                {
                    Main.resv.Remove(r);
                    x = -1;
                }
            }
            HttpClient client = new HttpClient();
            return await client.DeleteAsync(LINK + RRV + "/" + resv.ID);
        }

        public static async Task<HttpResponseMessage> RemoveImage(IImage img)
        {
            return await RemoveImage(img.ID);
        }

        public static async Task<HttpResponseMessage> RemoveImage(string id)
        {
            HttpClient client = new HttpClient();
            return await client.DeleteAsync(LINK + RI + "/" + id);
        }

        public static async Task<HttpResponseMessage> EditUser(User user)
        {
            var json = JsonConvert.SerializeObject(user);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpClient client = new HttpClient();
            var responce = await client.PutAsync(LINK + RU + "/" + user.RestID, content);
            return responce;
        }

        public static async Task<HttpResponseMessage> EditReservation(Reservation r)
        {
            var json = JsonConvert.SerializeObject(r);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpClient client = new HttpClient();
            var responce = await client.PutAsync(LINK + RRV + "/" + r.ID, content);
            return responce;
        }

        public static async Task LoadUsers()
        {
            users.Clear();
            HttpClient client = new HttpClient();
            var responce = await client.GetStringAsync(LINK + RU);
            users = JsonConvert.DeserializeObject<List<User>>(responce);
        }

        public static async Task LoadRest()
        {
            rest.Clear();
            HttpClient client = new HttpClient();
            var responce = await client.GetStringAsync(LINK + RRS);
            rest = JsonConvert.DeserializeObject<List<Restaurant>>(responce);
        }

        public static async Task LoadResv(Restaurant rest)
        {
            resv.Clear();
            HttpClient client = new HttpClient();
            var responce = await client.GetStringAsync(LINK + RRV);
            resv = JsonConvert.DeserializeObject<List<Reservation>>(responce);
            if(rest != null)
            {
                List<Reservation> R = new List<Reservation>();
                for(int x = 0; x < resv.Count; x++)
                {
                    if(resv[x].RestID == rest.RestID)
                    {
                        R.Add(resv[x]);
                    }
                }
                resv = R;
            }
        }

        public static async Task LoadMenus(Restaurant rest)
        {
            menu.Clear();
            HttpClient client = new HttpClient();
            var responce = await client.GetStringAsync(LINK + RM);
            menu = JsonConvert.DeserializeObject<List<IMenu>>(responce);
            if (rest != null)
            {
                List<IMenu> R = new List<IMenu>();
                for (int x = 0; x < menu.Count; x++)
                {
                    if (menu[x].RestID == rest.RestID)
                    {
                        R.Add(menu[x]);
                    }
                }
                menu = R;
            }
        }

        public static async Task<IImage> LoadImage(string id)
        {
            HttpClient client = new HttpClient();
            try
            {
                var responce = await client.GetStringAsync(LINK + RI + "/" + id);
                return JsonConvert.DeserializeObject<IImage>(responce);
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public static async Task<Reservation> GetResv(string id)
        {
            HttpClient client = new HttpClient();
            try
            {
                var responce = await client.GetStringAsync(LINK + RRV + "/" + id);
                return JsonConvert.DeserializeObject<Reservation>(responce);
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public static async Task<Reservation> ChangeStatus(string id, string status)
        {
            for(int x = 0; x < resv.Count; x++)
            {
                if(resv[x].ID == id)
                {
                    resv[x].Status = status;
                    await EditReservation(resv[x]);
                    return resv[x];
                }
            }
            return null;
        }

        public static User GetUserByName(string name)
        {
            for (int x = 0; x < users.Count; x++)
            {
                if (users[x].Username == name)
                {
                    return users[x];
                }
            }
            return null;
        }

        static void _ChangeStatus(Reservation res, string status)
        {
            if(HasStatus(GetRestaurantById(res.RestID), status))
            {
                res.Status = status;
            }
        }

        public static string[] GetStatus(Restaurant rest)
        {
            return rest.StatusList.Split(';');
        }

        public static bool HasStatus(Restaurant rest, string status)
        {
            var S = GetStatus(rest);
            for(int x = 0; x < S.Length; x++)
            {
                if(S[x] == status)
                {
                    return true;
                }
            }
            return false;
        }

        public static string Encrypt(string msg)
        {
            string N = "";
            char L;
            for(int x = 0; x < msg.Length; x++)
            {
                var C = msg.ToCharArray()[x];
                int index = C % 32;

                L = C;
            }
            return N;
        }

        public static string GenerateId()
        {
            string id = "RES";
            id += random.Next(1000, 9999);
            return id;
        }

        public static string GenerateRandomImageId()
        {
            string id = "";
            for(int x = 0; x < 7; x++)
            {
                id += IM[random.Next(IM.Length)];
            }
            return id;
        }

        public static Restaurant GetRestaurantByUser(User user)
        {
            for(int x = 0; x < rest.Count; x++)
            {
                if(rest[x].RestID == user.RestID)
                {
                    return rest[x];
                }
            }
            return null;
        }

        public static IMenu GetMenuById(string id)
        {
            for(int x = 0; x < menu.Count; x++)
            {
                if(menu[x].ID == id)
                {
                    return menu[x];
                }
            }
            return null;
        }

        public static Restaurant GetRestaurantById(string id)
        {
            for(int x = 0; x < rest.Count; x++)
            {
                if(rest[x].RestID == id)
                {
                    return rest[x];
                }
            }
            return null;
        }

        public static void AnimateGrowth(Taskable taskable, double factor, string id)
        {
            Animate(taskable, factor, id, "growth");
        }

        public static void AnimateColorTo(Taskable taskable, int delay, string id, Color toColor)
        {
            int c = 4;
            Device.StartTimer(TimeSpan.FromMilliseconds(delay), () => {
                if (taskable is VisualElement)
                {
                    Color bkg = ((VisualElement)taskable).BackgroundColor;
                    int[] rgb = { (int)(bkg.R*255), (int)(bkg.G*255), (int)(255*bkg.B)};
                    int[] rgb1 = { (int)(toColor.R * 255), (int)(toColor.G * 255), (int)(255 * toColor.B) };
                    if(rgb[0] == rgb1[0] && rgb[1] == rgb1[1] && rgb[2] == rgb1[2])
                    {
                        return false;
                    }
                    rgb = Inc(rgb, rgb1, 0, c);
                    ((VisualElement)taskable).BackgroundColor = Color.FromRgb(rgb[0], rgb[1], rgb[2]);
                } else
                {
                    return false;
                }
                return true;
            });
        }

        private static int[] Inc(int[] rgb, int[] rgb1, int place, int inc)
        {
            if (rgb[place] + inc - 1 < rgb1[place])
            {
                rgb[place] += inc;
            }
            else if (rgb[place] < rgb1[place] || rgb[place] > rgb1[place])
            {
                rgb[place] = rgb1[place];
            }
            else if (rgb[place] + inc - 1 > rgb1[place])
            {
                rgb[place] -= inc;
            }
            if(place == rgb.Length - 1)
            {
                return rgb;
            } else
            {
                return Inc(rgb, rgb1, place + 1, inc);
            }
        }

        private static void Animate(Taskable taskable, double factor, string id
            , string AnimationType, params object[] obj)
        {
            double a = 0;
            bool f = false;
            bool b = false;
            Device.StartTimer(TimeSpan.FromMilliseconds(10), () =>
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
                    if(AnimationType == "growth")
                    {
                        view.Scale += a / factor;
                    }
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
