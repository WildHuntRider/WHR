﻿using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media.Imaging;

namespace WHR.Utility.Media
{
    internal class Utility
    {
        internal static HsbaColor RgbaToHsba(RgbaColor rgba)
        {
            int[] rgb = new int[] { rgba.R, rgba.G, rgba.B };
            Array.Sort(rgb);
            int max = rgb[2];
            int min = rgb[0];
            double hsbB = max / 255.0;
            double hsbS = max == 0 ? 0 : (max - min) / (double)max;
            double hsbH = 0;
            if (rgba.R == rgba.G && rgba.R == rgba.B)
            {
            }
            else
            {
                if (max == rgba.R && rgba.G >= rgba.B) hsbH = (rgba.G - rgba.B) * 60.0 / (max - min) + 0.0;
                else if (max == rgba.R && rgba.G < rgba.B) hsbH = (rgba.G - rgba.B) * 60.0 / (max - min) + 360.0;
                else if (max == rgba.G) hsbH = (rgba.B - rgba.R) * 60.0 / (max - min) + 120.0;
                else if (max == rgba.B) hsbH = (rgba.R - rgba.G) * 60.0 / (max - min) + 240.0;
            }
            return new HsbaColor(hsbH, hsbS, hsbB, rgba.A / 255.0);
        }
        internal static RgbaColor HsbaToRgba(HsbaColor hsba)
        {
            double r = 0, g = 0, b = 0;
            int i = (int)((hsba.H / 60) % 6);
            double f = (hsba.H / 60) - i;
            double p = hsba.B * (1 - hsba.S);
            double q = hsba.B * (1 - f * hsba.S);
            double t = hsba.B * (1 - (1 - f) * hsba.S);
            switch (i)
            {
                case 0:
                    r = hsba.B;
                    g = t;
                    b = p;
                    break;
                case 1:
                    r = q;
                    g = hsba.B;
                    b = p;
                    break;
                case 2:
                    r = p;
                    g = hsba.B;
                    b = t;
                    break;
                case 3:
                    r = p;
                    g = q;
                    b = hsba.B;
                    break;
                case 4:
                    r = t;
                    g = p;
                    b = hsba.B;
                    break;
                case 5:
                    r = hsba.B;
                    g = p;
                    b = q;
                    break;
                default:
                    break;
            }
            return new RgbaColor((int)(255.0 * r), (int)(255.0 * g), (int)(255.0 * b), (int)(255.0 * hsba.A));
        }
        internal static BitmapSource LoadImg(string path)
        {
            try
            {
                BitmapImage bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.StreamSource = new System.IO.MemoryStream(System.IO.File.ReadAllBytes(path));
                bitmapImage.EndInit();

                return bitmapImage;
            }
            catch
            {
                return null;
            }
        }
        internal static Bitmap BitmapImageToBitmap(BitmapImage bitmapImage)
        {
            PixelFormat pp = PixelFormat.Format32bppArgb;
            Bitmap bmp = new Bitmap(bitmapImage.PixelWidth, bitmapImage.PixelHeight, pp);
            BitmapData data = bmp.LockBits(new Rectangle(System.Drawing.Point.Empty, bmp.Size), ImageLockMode.WriteOnly, pp);
            bitmapImage.CopyPixels(Int32Rect.Empty, data.Scan0, data.Height * data.Stride, data.Stride);
            bmp.UnlockBits(data);
            return bmp;
        }
        internal static BitmapSource BitmapToBitmapSource(Bitmap bitmap)
        {
            return Imaging.CreateBitmapSourceFromHBitmap(bitmap.GetHbitmap(), IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
        }
        internal static int GetBrightness(int r, int g, int b)
        {
            return (int)((0.2126 * r + 0.7152 * g + 0.0722 * b) / 2.55);
        }
    }
}