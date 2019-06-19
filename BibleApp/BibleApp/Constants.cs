using Xamarin.Forms;

namespace BibleApp
{
    public static class Constants
    {
        // The iOS simulator can connect to localhost. However, Android emulators must use the 10.0.2.2 special alias to your host loopback interface.
        public static string BaseAddress = Device.RuntimePlatform == Device.Android ? "https://getbible.net" : "https://getbible.net";
        public static string TodoItemsUrl = BaseAddress + "/index.php?option=com_getbible&view=json&getbible=jQuery1124049066415789860285_1560903873079&p={0}&v={1}";
    }
}