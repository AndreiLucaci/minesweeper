using System.Windows;
using System.Windows.Controls;

namespace Minesweeper.Ui.Constants
{
	public class CellStyles
	{
		public CellStyles()
		{
			SetUntouchedStyle();
			SetOpenStyle();
			SetFlaggedStyle();
			SetMineStyle();
			SetOneMineStyle();
			SetTwoMineStyle();
			SetThreeMineStyle();
			SetFourMineStyle();
			SetFiveMineStyle();
			SetSixMineStyle();
			SetSevenMineStyle();
		}

		public static Style UntouchedStyle { get; } = new Style();
		public static Style OpenStyle { get; } = new Style();
		public static Style FlaggedStyle { get; } = new Style();
		public static Style MineStyle { get; } = new Style();
		public static Style OneMineStyle { get; } = new Style();
		public static Style TwoMineStyle { get; } = new Style();
		public static Style ThreeMineStyle { get; } = new Style();
		public static Style FourMineStyle { get; } = new Style();
		public static Style FiveMineStyle { get; } = new Style();
		public static Style SixMineStyle { get; } = new Style();
		public static Style SevenMineStyle { get; } = new Style();

		private void SetUntouchedStyle()
		{
			UntouchedStyle.TargetType = typeof(Button);
			UntouchedStyle.Setters.Add(new Setter(Control.ForegroundProperty, BrushesStyles.Gray));
			UntouchedStyle.Setters.Add(new Setter(Control.BackgroundProperty, BrushesStyles.Gray));
		}

		private void SetOpenStyle()
		{
			OpenStyle.BasedOn = UntouchedStyle;
			OpenStyle.TargetType = typeof(Button);
			OpenStyle.Setters.Add(new Setter(Control.ForegroundProperty, BrushesStyles.LightGray));
		}

		private void SetFlaggedStyle()
		{
			FlaggedStyle.TargetType = typeof(Button);
			FlaggedStyle.Setters.Add(new Setter(Control.BackgroundProperty, BrushesStyles.Purple));
		}

		private void SetMineStyle()
		{
			MineStyle.TargetType = typeof(Button);
			MineStyle.Setters.Add(new Setter(Control.BackgroundProperty, BrushesStyles.Red));
		}

		private void SetOneMineStyle()
		{
			OneMineStyle.BasedOn = UntouchedStyle;
			OneMineStyle.TargetType = typeof(Button);
			OneMineStyle.Setters.Add(new Setter(Control.ForegroundProperty, BrushesStyles.Blue));
		}

		private void SetTwoMineStyle()
		{
			TwoMineStyle.BasedOn = UntouchedStyle;
			TwoMineStyle.TargetType = typeof(Button);
			TwoMineStyle.Setters.Add(new Setter(Control.ForegroundProperty, BrushesStyles.Green));
		}

		private void SetThreeMineStyle()
		{
			ThreeMineStyle.BasedOn = UntouchedStyle;
			ThreeMineStyle.TargetType = typeof(Button);
			ThreeMineStyle.Setters.Add(new Setter(Control.ForegroundProperty, BrushesStyles.Red));
		}

		private void SetFourMineStyle()
		{
			FourMineStyle.BasedOn = UntouchedStyle;
			FourMineStyle.TargetType = typeof(Button);
			FourMineStyle.Setters.Add(new Setter(Control.ForegroundProperty, BrushesStyles.Purple));
		}

		private void SetFiveMineStyle()
		{
			FiveMineStyle.BasedOn = UntouchedStyle;
			FiveMineStyle.TargetType = typeof(Button);
			FiveMineStyle.Setters.Add(new Setter(Control.ForegroundProperty, BrushesStyles.Maroon));
		}

		private void SetSixMineStyle()
		{
			SixMineStyle.BasedOn = UntouchedStyle;
			SixMineStyle.TargetType = typeof(Button);
			SixMineStyle.Setters.Add(new Setter(Control.ForegroundProperty, BrushesStyles.Turquoise));
		}

		private void SetSevenMineStyle()
		{
			SevenMineStyle.BasedOn = UntouchedStyle;
			SevenMineStyle.TargetType = typeof(Button);
			SevenMineStyle.Setters.Add(new Setter(Control.ForegroundProperty, BrushesStyles.Black));
		}
	}
}
