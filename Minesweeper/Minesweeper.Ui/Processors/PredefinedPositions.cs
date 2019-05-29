using System.Drawing;

namespace Minesweeper.Ui.Processors
{
    public class PredefinedPositions
    {
        private const int DigitWidth = 13;
        private const int DigitHeight = 23;
        private const int CellWidth = 16;
        private const int CellHeight = 16;
        private const int FaceWidth = 26;
        private const int FaceHeight = 26;

        public class DigitPositions
        {
            public static Rectangle Tile0 { get; } = new Rectangle(0, 0, DigitWidth, DigitHeight);
            public static Rectangle Tile1 { get; } = new Rectangle(DigitWidth, 0, DigitWidth, DigitHeight);
            public static Rectangle Tile2 { get; } = new Rectangle(DigitWidth * 2, 0, DigitWidth, DigitHeight);
            public static Rectangle Tile3 { get; } = new Rectangle(DigitWidth * 3, 0, DigitWidth, DigitHeight);
            public static Rectangle Tile4 { get; } = new Rectangle(DigitWidth * 4, 0, DigitWidth, DigitHeight);
            public static Rectangle Tile5 { get; } = new Rectangle(DigitWidth * 5, 0, DigitWidth, DigitHeight);
            public static Rectangle Tile6 { get; } = new Rectangle(DigitWidth * 6, 0, DigitWidth, DigitHeight);
            public static Rectangle Tile7 { get; } = new Rectangle(DigitWidth * 7, 0, DigitWidth, DigitHeight);
            public static Rectangle Tile8 { get; } = new Rectangle(DigitWidth * 8, 0, DigitWidth, DigitHeight);
            public static Rectangle Tile9 { get; } = new Rectangle(DigitWidth * 9, 0, DigitWidth, DigitHeight);
        }

        public class CellPositions
        {
            public static Rectangle Cell0 { get; } = new Rectangle(0, DigitHeight, CellWidth, CellHeight);
            public static Rectangle Cell1 { get; } = new Rectangle(CellWidth, DigitHeight, CellWidth, CellHeight);
            public static Rectangle Cell2 { get; } = new Rectangle(CellWidth * 2, DigitHeight, CellWidth, CellHeight);
            public static Rectangle Cell3 { get; } = new Rectangle(CellWidth * 3, DigitHeight, CellWidth, CellHeight);
            public static Rectangle Cell4 { get; } = new Rectangle(CellWidth * 4, DigitHeight, CellWidth, CellHeight);
            public static Rectangle Cell5 { get; } = new Rectangle(CellWidth * 5, DigitHeight, CellWidth, CellHeight);
            public static Rectangle Cell6 { get; } = new Rectangle(CellWidth * 6, DigitHeight, CellWidth, CellHeight);
            public static Rectangle Cell7 { get; } = new Rectangle(CellWidth * 7, DigitHeight, CellWidth, CellHeight);
            public static Rectangle Cell8 { get; } = new Rectangle(0, DigitHeight + CellHeight, CellWidth, CellHeight);
            public static Rectangle MineUntouched { get; } = new Rectangle(CellWidth, DigitHeight + CellHeight, CellWidth, CellHeight);
            public static Rectangle MineExploded { get; } = new Rectangle(CellWidth * 2, DigitHeight + CellHeight, CellWidth, CellHeight);
            public static Rectangle MineMissFlag { get; } = new Rectangle(CellWidth * 3, DigitHeight + CellHeight, CellWidth, CellHeight);
            public static Rectangle Flag { get; } = new Rectangle(CellWidth * 4, DigitHeight + CellHeight, CellWidth, CellHeight);
            public static Rectangle Untouched { get; } = new Rectangle(CellWidth * 5, DigitHeight + CellHeight, CellWidth, CellHeight);
        }

        public class FacePositions
        {
            public static Rectangle FaceDead { get; } = new Rectangle(0, DigitHeight + (CellHeight * 2), FaceWidth, FaceHeight);
            public static Rectangle FaceSurprised { get; } = new Rectangle(FaceWidth, DigitHeight + (CellHeight * 2), FaceWidth, FaceHeight);
            public static Rectangle FaceSmile { get; } = new Rectangle(FaceWidth * 2, DigitHeight + (CellHeight * 2), FaceWidth, FaceHeight);
            public static Rectangle FaceWin { get; } = new Rectangle(FaceWidth * 3, DigitHeight + (CellHeight * 2), FaceWidth, FaceHeight);
        }
    }
}
