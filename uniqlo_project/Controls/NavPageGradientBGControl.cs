using System;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using Xamarin.Forms;

namespace uniqlo_project.Controls
{
    public class NavPageGradientBGControl : ContentView
    {
        public NavPageGradientBGControl()
        {
            SKCanvasView canvasView = new SKCanvasView();
            canvasView.PaintSurface += OnCanvasViewPaintSurface;
            Content = canvasView;
        }

        private void OnCanvasViewPaintSurface(object sender, SKPaintSurfaceEventArgs args)
        {
            SKImageInfo info = args.Info;
            SKSurface surface = args.Surface;
            SKCanvas canvas = surface.Canvas;

            canvas.Clear();

            var colors = new SKColor[] { new SKColor(253, 113, 101), new SKColor(244, 112, 72) };
            var shader = SKShader.CreateLinearGradient(new SKPoint(0, info.Height), new SKPoint(info.Width, 0), colors, null, SKShaderTileMode.Clamp);
            var paint = new SKPaint() { Shader = shader };
            canvas.DrawPaint(paint);
        }
    }
}
