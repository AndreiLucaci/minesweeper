using System.Windows.Media.Imaging;

namespace Minesweeper.Ui.Constants
{
    public class CellStyles : BaseStyles
    {
        public static BitmapImage Mine0ImagePath { get; } = 
            LoadImage($@"/Minesweeper.Ui;component/Images/0.gif");

        public static BitmapImage Mine1ImagePath { get; } =
            LoadImage($@"/Minesweeper.Ui;component/Images/1.gif");

        public static BitmapImage Mine2ImagePath { get; } =
            LoadImage($@"/Minesweeper.Ui;component/Images/2.gif");

        public static BitmapImage Mine3ImagePath { get; } =
            LoadImage($@"/Minesweeper.Ui;component/Images/3.gif");

        public static BitmapImage Mine4ImagePath { get; } =
            LoadImage($@"/Minesweeper.Ui;component/Images/4.gif");

        public static BitmapImage Mine5ImagePath { get; } =
            LoadImage($@"/Minesweeper.Ui;component/Images/5.gif");

        public static BitmapImage Mine6ImagePath { get; } =
            LoadImage($@"/Minesweeper.Ui;component/Images/6.gif");

        public static BitmapImage Mine7ImagePath { get; } =
            LoadImage($@"/Minesweeper.Ui;component/Images/7.gif");

        public static BitmapImage Mine8ImagePath { get; } =
            LoadImage($@"/Minesweeper.Ui;component/Images/8.gif");

        public static BitmapImage MineImagePath { get; } =
            LoadImage($@"/Minesweeper.Ui;component/Images/mine.gif");

        public static BitmapImage FlagImagePath { get; } =
            LoadImage($@"/Minesweeper.Ui;component/Images/mineflagged.gif");

        public static BitmapImage MineExplodedImagePath { get; } =
            LoadImage($@"/Minesweeper.Ui;component/Images/mineexploded.gif");

        public static BitmapImage MineWrongFlagPath { get; }  =
            LoadImage($@"/Minesweeper.Ui;component/Images/minemissflag.gif");

        public static BitmapImage UntouchedImagePath { get; } =
            LoadImage($@"/Minesweeper.Ui;component/Images/untouched.gif");
    }
}