using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Minesweeper.Ui.Constants;
using Minesweeper.Ui.Models;

namespace Minesweeper.Ui.Processors
{
    public class DefaultSkinProcessor : BaseSkinProcessor
    {
        private const int DigitWidth = 13;
        private const int DigitHeight = 23;
        private int cellWidth = 16;
        private int cellHeight = 16;
        private int faceWidth = 26;
        private int faceHeight = 26;

        public override void Process(SkinType skinType)
        {
            Process();
        }

        private void Process()
        {
            var image = LoadImage($@"{AppDomain.CurrentDomain.BaseDirectory}\Images\skin.png");

            //var image = BaseStyles.Skin;

            ProcessDigits(image);

            ProcessCells(image);

            ProcessFaces(image);
        }

        private void ProcessDigits(Image image)
        {
            var widthCounter = 0;
            DigitStyles.Tile0 = GetPartOfImage(image, new Rectangle(widthCounter, 0, DigitWidth, DigitHeight));
            DigitStyles.Tile1 = GetPartOfImage(image, new Rectangle(widthCounter += DigitWidth, 0, DigitWidth, DigitHeight));
            DigitStyles.Tile2 = GetPartOfImage(image, new Rectangle(widthCounter += DigitWidth, 0, DigitWidth, DigitHeight));
            DigitStyles.Tile3 = GetPartOfImage(image, new Rectangle(widthCounter += DigitWidth, 0, DigitWidth, DigitHeight));
            DigitStyles.Tile4 = GetPartOfImage(image, new Rectangle(widthCounter += DigitWidth, 0, DigitWidth, DigitHeight));
            DigitStyles.Tile5 = GetPartOfImage(image, new Rectangle(widthCounter += DigitWidth, 0, DigitWidth, DigitHeight));
            DigitStyles.Tile6 = GetPartOfImage(image, new Rectangle(widthCounter += DigitWidth, 0, DigitWidth, DigitHeight));
            DigitStyles.Tile7 = GetPartOfImage(image, new Rectangle(widthCounter += DigitWidth, 0, DigitWidth, DigitHeight));
            DigitStyles.Tile8 = GetPartOfImage(image, new Rectangle(widthCounter += DigitWidth, 0, DigitWidth, DigitHeight));
            DigitStyles.Tile9 = GetPartOfImage(image, new Rectangle(widthCounter + DigitWidth, 0, DigitWidth, DigitHeight));
        }

        private void ProcessCells(Image image)
        {
            
        }

        private void ProcessFaces(Image image)
        {
            
        }

        private BitmapImage GetPartOfImage(Image image, Rectangle rect)
        {
            var destImg = new Bitmap(rect.Width, rect.Height);

            var destRect = new Rectangle(0, 0, rect.Width, rect.Height);

            CopyRegionIntoImage(image, rect, ref destImg, destRect);

            return Bitmap2BitmapImage(destImg);
        }

        public static void CopyRegionIntoImage(Image srcBitmap, Rectangle srcRegion, ref Bitmap destBitmap, Rectangle destRegion)
        {
            using (var grD = Graphics.FromImage(destBitmap))
            {
                grD.DrawImage(srcBitmap, destRegion, srcRegion, GraphicsUnit.Pixel);
            }
        }

        [System.Runtime.InteropServices.DllImport("gdi32.dll")]
        public static extern bool DeleteObject(IntPtr hObject);

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
    }
}
