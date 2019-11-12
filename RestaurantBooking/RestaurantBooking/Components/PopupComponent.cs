
namespace RestaurantBooking.Components
{
    public interface PopupComponent
    {
        PopupType GetPopupType();
        void OnClosed();
    }
}
