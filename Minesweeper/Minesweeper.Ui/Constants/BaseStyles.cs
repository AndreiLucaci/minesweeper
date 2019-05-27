using System;
using System.Windows.Media.Imaging;

namespace Minesweeper.Ui.Constants
{
    public class BaseStyles
    {
        protected static BitmapImage LoadImage(string path)
        {
            var bitmap = new BitmapImage();

            bitmap.BeginInit();
            bitmap.UriSource = new Uri(path);
            bitmap.EndInit();

            return bitmap;
        }
    }
}
