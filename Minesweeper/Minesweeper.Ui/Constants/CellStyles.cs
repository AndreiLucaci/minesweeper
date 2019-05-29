using System;
using System.Windows.Media.Imaging;

namespace Minesweeper.Ui.Constants
{
    public class CellStyles : BaseStyles
    {
        public static BitmapImage Cell0 { get; set; }
        public static BitmapImage Cell1 { get; set; }
        public static BitmapImage Cell2 { get; set; }
        public static BitmapImage Cell3 { get; set; }
        public static BitmapImage Cell4 { get; set; }
        public static BitmapImage Cell5 { get; set; }
        public static BitmapImage Cell6 { get; set; }
        public static BitmapImage Cell7 { get; set; }
        public static BitmapImage Cell8 { get; set; }
        public static BitmapImage MineImagePath { get; set; } 
        public static BitmapImage FlagImagePath { get; set; }
        public static BitmapImage MineExplodedImagePath { get; set; } 
        public static BitmapImage MineWrongFlagPath { get; set; }
        public static BitmapImage UntouchedImagePath { get; set; }

        public static void Invalidate()
        {
            Cell0 = null;
            Cell1 = null;
            Cell2 = null;
            Cell3 = null;
            Cell4 = null;
            Cell5 = null;
            Cell6 = null;
            Cell7 = null;
            Cell8 = null;
            MineImagePath = null;
            FlagImagePath = null;
            MineExplodedImagePath = null;
            MineWrongFlagPath = null;
            UntouchedImagePath = null;

            GC.Collect();
        }
    }
}