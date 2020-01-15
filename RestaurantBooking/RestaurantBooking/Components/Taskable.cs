using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantBooking.Components
{
    public interface Taskable
    {
        void OnRecieve(string id);
    }
}
