using System;
using System.Windows.Media.Imaging;

namespace Minesweeper.Ui.Constants
{
    public class FaceStyles : BaseStyles
    {
        public static BitmapImage FaceWin { get; } = LoadImage($@"{AppDomain.CurrentDomain.BaseDirectory}\Images\fwin.gif");
        public static BitmapImage FaceDead { get; } = LoadImage($@"{AppDomain.CurrentDomain.BaseDirectory}\Images\fdead.gif");
        public static BitmapImage FaceSmile { get; } = LoadImage($@"{AppDomain.CurrentDomain.BaseDirectory}\Images\fsmile.gif");
    }
}
