using System;
using Xamarin.Forms;

namespace uniqlo_project
{
    public class Fonts
    {
        public static string MonserratRegular = Device.RuntimePlatform == Device.iOS ? "Montserrat-Regular" : "Montserrat-Regular.ttf#Montserrat-Regular";
        public static string MonserratMedium = Device.RuntimePlatform == Device.iOS ? "Montserrat-Medium" : "Montserrat-Medium.ttf#Montserrat-Medium";
        public static string MonserratSemiBold = Device.RuntimePlatform == Device.iOS ? "Montserrat-SemiBold" : "Montserrat-SemiBold.ttf#Montserrat-SemiBold";
    }
}
