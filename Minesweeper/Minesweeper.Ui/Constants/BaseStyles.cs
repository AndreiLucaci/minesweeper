using System;
using System.Windows.Media.Imaging;

namespace Minesweeper.Ui.Constants
{
    public class BaseStyles
    {
        public static BitmapImage SurprisedFace { get; } =
            LoadImage(@"/Minesweeper.Ui;component/Images/minesweeper surprised.png");

        public static BitmapImage WinFace { get; } = LoadImage(@"/Minesweeper.Ui;component/Images/minesweeper win.png");

        public static BitmapImage LooseFace { get; } =
            LoadImage(@"/Minesweeper.Ui;component/Images/minesweeper dead.png");

        public static BitmapImage Skin { get; } = LoadImage(@"/Minesweeper.Ui;component/Images/skin.png");

        protected static BitmapImage LoadImage(string path)
        {
            var bitmap = new BitmapImage();

            bitmap.BeginInit();
            bitmap.UriSource = new Uri(path, UriKind.Relative);
            bitmap.EndInit();
            bitmap.Freeze();

            return bitmap;
        }
    }
}