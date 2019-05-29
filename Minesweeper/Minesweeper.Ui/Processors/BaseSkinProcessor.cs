using System;
using System.Drawing;
using System.Windows.Media.Imaging;
using Minesweeper.Ui.Models;

namespace Minesweeper.Ui.Processors
{
    public abstract class BaseSkinProcessor : ISkinProcessor
    {
        protected ISkinProcessor InnerSkinProcessor;

        public abstract void Process(SkinType skinType);

        //protected static BitmapImage LoadImage(string path)
        //{
        //    var bitmap = new BitmapImage();

        //    bitmap.BeginInit();
        //    bitmap.UriSource = new Uri(path, UriKind.Relative);
        //    bitmap.EndInit();

        //    return bitmap;
        //}

        protected static Image LoadImage(string path)
        {
            return Image.FromFile(path);
        }
    }
}
