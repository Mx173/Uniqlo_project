using System;
using uniqlo_project.Controls;
using uniqlo_project.iOS.Renderers;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using ExtendedEntry = uniqlo_project.Controls.ExtendedEntry;

[assembly: ExportRenderer(typeof(ExtendedEntry), typeof(ExtendedEntryRenderer))]
namespace uniqlo_project.iOS.Renderers
{
    public class ExtendedEntryRenderer : EntryRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);
            var view = (ExtendedEntry)Element;

            if (view != null)
            {
                Control.BorderStyle = view.HasBorder ? UITextBorderStyle.RoundedRect : UITextBorderStyle.None;
                Control.TintColor = Color.FromHex("#51D9B9").ToUIColor();
            }
            else
            {
                return;
            }
        }
    }
}
