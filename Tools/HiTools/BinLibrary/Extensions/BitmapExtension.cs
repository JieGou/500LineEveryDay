﻿using System;
using System.Drawing;
using System.Windows;
using System.Windows.Media.Imaging;

namespace CodeInTangsengjiewa3.BinLibrary.Extensions
{
    public static class BitmapExtension
    {
        public static BitmapSource ToBitmapSource(this Bitmap bitmap)
        {
            BitmapSource result = null;
            IntPtr handle = bitmap.GetHbitmap();
            result = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(handle, IntPtr.Zero,
                                                                                  System.Windows.Int32Rect.Empty,
                                                                                  BitmapSizeOptions.FromEmptyOptions());
            return result;
        }
    }
}