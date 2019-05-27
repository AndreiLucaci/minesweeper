using System;
using System.Windows.Media.Imaging;

namespace Minesweeper.Ui.Constants
{
    public class DigitStyles : BaseStyles
    {
        public static BitmapImage Tile0 { get; } = LoadImage($@"{AppDomain.CurrentDomain.BaseDirectory}\Images\t0.gif");
        public static BitmapImage Tile1 { get; } = LoadImage($@"{AppDomain.CurrentDomain.BaseDirectory}\Images\t1.gif");
        public static BitmapImage Tile2 { get; } = LoadImage($@"{AppDomain.CurrentDomain.BaseDirectory}\Images\t2.gif");
        public static BitmapImage Tile3 { get; } = LoadImage($@"{AppDomain.CurrentDomain.BaseDirectory}\Images\t3.gif");
        public static BitmapImage Tile4 { get; } = LoadImage($@"{AppDomain.CurrentDomain.BaseDirectory}\Images\t4.gif");
        public static BitmapImage Tile5 { get; } = LoadImage($@"{AppDomain.CurrentDomain.BaseDirectory}\Images\t5.gif");
        public static BitmapImage Tile6 { get; } = LoadImage($@"{AppDomain.CurrentDomain.BaseDirectory}\Images\t6.gif");
        public static BitmapImage Tile7 { get; } = LoadImage($@"{AppDomain.CurrentDomain.BaseDirectory}\Images\t7.gif");
        public static BitmapImage Tile8 { get; } = LoadImage($@"{AppDomain.CurrentDomain.BaseDirectory}\Images\t8.gif");
        public static BitmapImage Tile9 { get; } = LoadImage($@"{AppDomain.CurrentDomain.BaseDirectory}\Images\t9.gif");
    }
}
