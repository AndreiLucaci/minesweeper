using System;
using System.Windows.Media.Imaging;

namespace Minesweeper.Ui.Constants
{
    public class FaceStyles : BaseStyles
    {
        public static BitmapImage FaceWin { get; set; }
        public static BitmapImage FaceDead { get; set; }
        public static BitmapImage FaceSmile { get; set; }

        public static void Invalidate()
        {
            FaceDead = null;
            FaceSmile = null;
            FaceWin = null;

            GC.Collect();
        }
    }
}