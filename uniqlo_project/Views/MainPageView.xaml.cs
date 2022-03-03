using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;

namespace uniqlo_project.Views
{
    public partial class MainPageView : Xamarin.Forms.TabbedPage
    {
        public MainPageView()
        {
            InitializeComponent();

            Xamarin.Forms.NavigationPage.SetHasNavigationBar(this, false);

            

            if (Device.RuntimePlatform == Device.Android) Padding = new Thickness(0, -30, 0, 0);
            if (Device.Idiom == TargetIdiom.Phone)
            {
                if (Xamarin.Forms.Application.Current.MainPage?.Height > 811)
                {
                    Padding = new Thickness(0, 0, 0, 10);
                    On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(false);
                }
            }
        }

        protected override bool OnBackButtonPressed()
        {
            return true;
        }
    }
}
