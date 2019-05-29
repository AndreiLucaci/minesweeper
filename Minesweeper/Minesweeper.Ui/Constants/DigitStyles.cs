using System;
using System.Windows.Media.Imaging;

namespace Minesweeper.Ui.Constants
{
    public class DigitStyles : BaseStyles
    {
        public static BitmapImage Tile0 { get; set; }
        public static BitmapImage Tile1 { get; set; }
        public static BitmapImage Tile2 { get; set; }
        public static BitmapImage Tile3 { get; set; }
        public static BitmapImage Tile4 { get; set; }
        public static BitmapImage Tile5 { get; set; }
        public static BitmapImage Tile6 { get; set; }
        public static BitmapImage Tile7 { get; set; }
        public static BitmapImage Tile8 { get; set; }
        public static BitmapImage Tile9 { get; set; }

        public static void Invalidate()
        {
            Tile0 = null;
            Tile1 = null;
            Tile2 = null;
            Tile3 = null;
            Tile4 = null;
            Tile5 = null;
            Tile6 = null;
            Tile7 = null;
            Tile8 = null;
            Tile9 = null;

            GC.Collect();
        }
    }
}