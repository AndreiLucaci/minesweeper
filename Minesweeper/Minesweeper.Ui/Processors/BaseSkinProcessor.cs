using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Media.Imaging;
using Minesweeper.Ui.Constants;
using Minesweeper.Ui.Models;

namespace Minesweeper.Ui.Processors
{
    public abstract class BaseSkinProcessor : ISkinProcessor
    {
        protected abstract string Path { get; set; }

        public abstract void Process(SkinType skinType);

        protected void Process()
        {
            var image = LoadImage(Path);

            ProcessDigits(image);
            ProcessCells(image);
            ProcessFaces(image);
        }

        private void ProcessDigits(Image image)
        {
            DigitStyles.Tile0 = GetPartOfImage(image, PredefinedPositions.DigitPositions.Tile0);
            DigitStyles.Tile1 = GetPartOfImage(image, PredefinedPositions.DigitPositions.Tile1);
            DigitStyles.Tile2 = GetPartOfImage(image, PredefinedPositions.DigitPositions.Tile2);
            DigitStyles.Tile3 = GetPartOfImage(image, PredefinedPositions.DigitPositions.Tile3);
            DigitStyles.Tile4 = GetPartOfImage(image, PredefinedPositions.DigitPositions.Tile4);
            DigitStyles.Tile5 = GetPartOfImage(image, PredefinedPositions.DigitPositions.Tile5);
            DigitStyles.Tile6 = GetPartOfImage(image, PredefinedPositions.DigitPositions.Tile6);
            DigitStyles.Tile7 = GetPartOfImage(image, PredefinedPositions.DigitPositions.Tile7);
            DigitStyles.Tile8 = GetPartOfImage(image, PredefinedPositions.DigitPositions.Tile8);
            DigitStyles.Tile9 = GetPartOfImage(image, PredefinedPositions.DigitPositions.Tile9);
        }

        private void ProcessCells(Image image)
        {
            CellStyles.Cell0 = GetPartOfImage(image, PredefinedPositions.CellPositions.Cell0);
            CellStyles.Cell1 = GetPartOfImage(image, PredefinedPositions.CellPositions.Cell1);
            CellStyles.Cell2 = GetPartOfImage(image, PredefinedPositions.CellPositions.Cell2);
            CellStyles.Cell3 = GetPartOfImage(image, PredefinedPositions.CellPositions.Cell3);
            CellStyles.Cell4 = GetPartOfImage(image, PredefinedPositions.CellPositions.Cell4);
            CellStyles.Cell5 = GetPartOfImage(image, PredefinedPositions.CellPositions.Cell5);
            CellStyles.Cell6 = GetPartOfImage(image, PredefinedPositions.CellPositions.Cell6);
            CellStyles.Cell7 = GetPartOfImage(image, PredefinedPositions.CellPositions.Cell7);
            CellStyles.Cell8 = GetPartOfImage(image, PredefinedPositions.CellPositions.Cell8);
            CellStyles.MineImagePath = GetPartOfImage(image, PredefinedPositions.CellPositions.MineUntouched);
            CellStyles.FlagImagePath = GetPartOfImage(image, PredefinedPositions.CellPositions.Flag);
            CellStyles.MineExplodedImagePath = GetPartOfImage(image, PredefinedPositions.CellPositions.MineExploded);
            CellStyles.MineWrongFlagPath = GetPartOfImage(image, PredefinedPositions.CellPositions.MineMissFlag);
            CellStyles.UntouchedImagePath = GetPartOfImage(image, PredefinedPositions.CellPositions.Untouched);
        }

        private void ProcessFaces(Image image)
        {
            FaceStyles.FaceDead = GetPartOfImage(image, PredefinedPositions.FacePositions.FaceDead);
            FaceStyles.FaceSmile = GetPartOfImage(image, PredefinedPositions.FacePositions.FaceSmile);
            FaceStyles.FaceWin = GetPartOfImage(image, PredefinedPositions.FacePositions.FaceWin);
        }

        private BitmapImage GetPartOfImage(Image image, Rectangle rect)
        {
            var destImg = new Bitmap(rect.Width, rect.Height);

            var destRect = new Rectangle(0, 0, rect.Width, rect.Height);

            CopyRegionIntoImage(image, rect, ref destImg, destRect);

            return Bitmap2BitmapImage(destImg);
        }

        private static void CopyRegionIntoImage(Image srcBitmap, Rectangle srcRegion, ref Bitmap destBitmap, Rectangle destRegion)
        {
            using (var grD = Graphics.FromImage(destBitmap))
            {
                grD.DrawImage(srcBitmap, destRegion, srcRegion, GraphicsUnit.Pixel);
            }
        }

        [System.Runtime.InteropServices.DllImport("gdi32.dll")]
        private static extern bool DeleteObject(IntPtr hObject);

        private BitmapImage Bitmap2BitmapImage(Bitmap bitmap)
        {
            using (var memory = new MemoryStream())
            {
                bitmap.Save(memory, ImageFormat.Png);
                memory.Position = 0;

                var bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.StreamSource = memory;
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.EndInit();
                bitmapImage.Freeze();

                return bitmapImage;
            }
        }

        protected static Image LoadImage(string path)
        {
            return Image.FromFile(path);
        }
    }
}
