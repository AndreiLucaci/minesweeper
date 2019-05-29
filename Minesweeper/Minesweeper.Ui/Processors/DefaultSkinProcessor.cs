using System.Drawing;
using Minesweeper.Ui.Models;

namespace Minesweeper.Ui.Processors
{
    public class DefaultSkinProcessor : BaseSkinProcessor
    {
        protected override Bitmap SkinImage => Properties.Resources.skin;

        public override void Process(SkinType skinType)
        {
            Process();
        }
    }
}
