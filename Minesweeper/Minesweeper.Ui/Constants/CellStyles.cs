using System;
using System.Windows.Media.Imaging;

namespace Minesweeper.Ui.Constants
{
	public class CellStyles
	{
	    public static BitmapImage Mine0ImagePath { get; } = LoadImage($@"{AppDomain.CurrentDomain.BaseDirectory}\Images\0.gif");
	    public static BitmapImage Mine1ImagePath { get; } = LoadImage($@"{AppDomain.CurrentDomain.BaseDirectory}\Images\1.gif");
	    public static BitmapImage Mine2ImagePath { get; } = LoadImage($@"{AppDomain.CurrentDomain.BaseDirectory}\Images\2.gif");
	    public static BitmapImage Mine3ImagePath { get; } = LoadImage($@"{AppDomain.CurrentDomain.BaseDirectory}\Images\3.gif");
	    public static BitmapImage Mine4ImagePath { get; } = LoadImage($@"{AppDomain.CurrentDomain.BaseDirectory}\Images\4.gif");
	    public static BitmapImage Mine5ImagePath { get; } = LoadImage($@"{AppDomain.CurrentDomain.BaseDirectory}\Images\5.gif");
	    public static BitmapImage Mine6ImagePath { get; } = LoadImage($@"{AppDomain.CurrentDomain.BaseDirectory}\Images\6.gif");
	    public static BitmapImage Mine7ImagePath { get; } = LoadImage($@"{AppDomain.CurrentDomain.BaseDirectory}\Images\7.gif");
	    public static BitmapImage Mine8ImagePath { get; } = LoadImage($@"{AppDomain.CurrentDomain.BaseDirectory}\Images\8.gif");

	    public static BitmapImage MineImagePath { get; }         = LoadImage($@"{AppDomain.CurrentDomain.BaseDirectory}\Images\mine.gif");
	    public static BitmapImage FlagImagePath { get; }         = LoadImage($@"{AppDomain.CurrentDomain.BaseDirectory}\Images\mineflagged.gif");
        public static BitmapImage MineExplodedImagePath { get; } = LoadImage($@"{AppDomain.CurrentDomain.BaseDirectory}\Images\mineexploded.gif");
	    public static BitmapImage MineFlaggedImagePath { get; }  = LoadImage($@"{AppDomain.CurrentDomain.BaseDirectory}\Images\8.gif");

        public static BitmapImage UntouchedImagePath { get; }    = LoadImage($@"{AppDomain.CurrentDomain.BaseDirectory}\Images\untouched.gif");

	    private static BitmapImage LoadImage(string path)
	    {
	        var bitmap = new BitmapImage();

	        bitmap.BeginInit();
	        bitmap.UriSource = new Uri(path);
	        bitmap.EndInit();

	        return bitmap;
	    }
    }
}
