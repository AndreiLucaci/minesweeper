using System.Windows;
using System.Windows.Controls;
using Minesweeper.Ui.Views;

namespace Minesweeper.Ui.Constants
{
	public class CellStyles
	{
		public static Style UntouchedStyle { get; } = (Style) Application.Current.Resources["UntouchedStyle"];
		public static Style OpenStyle { get; } = (Style)Application.Current.Resources["OpenStyle"];
		public static Style FlaggedStyle { get; } = (Style)Application.Current.Resources["FlaggedStyle"];
		public static Style MineStyle { get; } = (Style)Application.Current.Resources["MineStyle"];
		public static Style OneMineStyle { get; } = (Style) Application.Current.Resources["OneMineStyle"];
		public static Style TwoMineStyle { get; } = (Style)Application.Current.Resources["TwoMineStyle"];
		public static Style ThreeMineStyle { get; } = (Style)Application.Current.Resources["ThreeMineStyle"];
		public static Style FourMineStyle { get; } = (Style) Application.Current.Resources["FourMineStyle"];
		public static Style FiveMineStyle { get; } = (Style)Application.Current.Resources["FiveMineStyle"];
		public static Style SixMineStyle { get; } = (Style)Application.Current.Resources["SixMineStyle"];
		public static Style SevenMineStyle { get; } = (Style)Application.Current.Resources["SevenMineStyle"];

	    public static Image Mine0Image { get; } = (Image) Application.Current.Resources["Mine0"];
	    public static Image Mine1Image { get; } = (Image) Application.Current.Resources["Mine1"];
	    public static Image Mine2Image { get; } = (Image) Application.Current.Resources["Mine2"];
	    public static Image Mine3Image { get; } = (Image) Application.Current.Resources["Mine3"];
	    public static Image Mine4Image { get; } = (Image) Application.Current.Resources["Mine4"];
	    public static Image Mine5Image { get; } = (Image) Application.Current.Resources["Mine5"];
	    public static Image Mine6Image { get; } = (Image) Application.Current.Resources["Mine6"];
	    public static Image Mine7Image { get; } = (Image) Application.Current.Resources["Mine7"];
	    public static Image Mine8Image { get; } = (Image) Application.Current.Resources["Mine8"];

        public static Image MineImage { get; }         = (Image)Application.Current.Resources["Mine"];
        public static Image FlagImage { get; }         = (Image)Application.Current.Resources["MineFlagged"];
        public static Image MineExplodedImage { get; } = (Image)Application.Current.Resources["MineExploded"];
        public static Image MineFlaggedImage { get; }  = (Image)Application.Current.Resources["MineMissFlag"];

	    public static Image UntouchedImage { get; } = (Image) Application.Current.Resources["Untouched"];

	    public static string Mine0ImagePath { get; } = @"~\..\Images\0.gif";
	    public static string Mine1ImagePath { get; } = @"~\..\Images\1.gif";
	    public static string Mine2ImagePath { get; } = @"~\..\Images\2.gif";
	    public static string Mine3ImagePath { get; } = @"~\..\Images\3.gif";
	    public static string Mine4ImagePath { get; } = @"~\..\Images\4.gif";
	    public static string Mine5ImagePath { get; } = @"~\..\Images\5.gif";
	    public static string Mine6ImagePath { get; } = @"~\..\Images\6.gif";
	    public static string Mine7ImagePath { get; } = @"~\..\Images\7.gif";
	    public static string Mine8ImagePath { get; } = @"~\..\Images\8.gif";

	    public static string MineImagePath { get; } = @"~\..\Images\mine.gif";
	    public static string FlagImagePath { get; } = @"~\..\Images\mineflagged.gif";
        public static string MineExplodedImagePath { get; } = @"~\..\Images\mineexploded.gif";
	    public static string MineFlaggedImagePath { get; }  = @"~\..\Images\8.gif";

        public static string UntouchedImagePath { get; } = @"~\..\Images\8.gif";


    }
}
