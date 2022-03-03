using System;
using uniqlo_project.Controls;
using uniqlo_project.iOS.Renderers;
using CoreGraphics;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(ShadowFrame), typeof(ShadowFrameRenderer))]
namespace uniqlo_project.iOS.Renderers
{
    public class ShadowFrameRenderer : FrameRenderer
    {
        #region -- Overrides --

        protected override void OnElementChanged(ElementChangedEventArgs<Frame> e)
        {
            base.OnElementChanged(e);
            UpdateShadowSettings();
        }

        public override void Draw(CGRect rect)
        {
            base.Draw(rect);
            UpdateShadowSettings();
        }

        #endregion

        #region -- Private helpers --

        private void UpdateShadowSettings()
        {
            Layer.ShadowRadius = 2.0f;
            Layer.ShadowColor = UIColor.Black.CGColor;
            Layer.ShadowOffset = new CGSize(0.5, 2);
            Layer.ShadowOpacity = 0.20f;
            Layer.ShadowPath = UIBezierPath.FromRect(Layer.Bounds).CGPath;
            Layer.MasksToBounds = false;
        }

        #endregion
    }
}
