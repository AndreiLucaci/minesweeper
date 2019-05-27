using System;
using System.Windows.Media.Imaging;

namespace Minesweeper.Ui.Constants
{
    public class FaceStyles : BaseStyles
    {
        public static BitmapImage FaceWin { get; } =
            LoadImage($@"/Minesweeper.Ui;component/Images/fwin.gif");

        public static BitmapImage FaceDead { get; } =
            LoadImage($@"/Minesweeper.Ui;component/Images/fdead.gif");

        public static BitmapImage FaceSmile { get; } =
            LoadImage($@"/Minesweeper.Ui;component/Images/fsmile.gif");
    }
}