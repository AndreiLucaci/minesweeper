using System.Windows;

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
	}
}
