using System;
using Prism.Mvvm;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;

namespace uniqlo_project.Views
{
    public class BaseContentPage : ContentPage
    {
        public BaseContentPage()
        {
            ViewModelLocator.SetAutowireViewModel(this, true);
            
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

        #region -- Overrides --

        protected override void OnAppearing()
        {
            base.OnAppearing();
            //double bottomPadding = safeArea.Bottom;
            //safeArea.Bottom = 0;
            //Padding = safeArea;

            //if ((Layout)Content != null)
            //{
            //    //HACK for iPhoneX bottom padding
            //    Thickness contentPad = ((Layout)Content).Padding;
            //    contentPad.Bottom = bottomPadding;

            //    ((Layout)Content).Padding = contentPad;
            //}




            //var actionsHandler = BindingContext as IViewActionsHandler;

            //if (actionsHandler != null)
            //{
            //	actionsHandler.OnAppearing();
            //}
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            //var actionsHandler = BindingContext as IViewActionsHandler;

            //if (actionsHandler != null)
            //{
            //	actionsHandler.OnDisappearing();
            //}
        }

        #endregion
    }
}
