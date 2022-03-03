using System;
using Xamarin.Forms;

namespace uniqlo_project.Controls
{
    public class ShadowFrame : Frame
    {
        public ShadowFrame()
        {
            switch (Device.RuntimePlatform)
            {
                case Device.iOS:
                    BorderColor = Color.Transparent;
                    break;
                case Device.Android:
                default:
                    BorderColor = Color.White;
                    break;
            }
        }
    }
}
