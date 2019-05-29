using System.Drawing;
using Minesweeper.Ui.Models;

namespace Minesweeper.Ui.Processors
{
    public class InvertedSkinProcessor : BaseSkinProcessor
    {
        private readonly ISkinProcessor innerProcessor;

        protected override Bitmap SkinImage => Properties.Resources.inverted;

        public InvertedSkinProcessor(ISkinProcessor innerProcessor)
        {
            this.innerProcessor = innerProcessor;
        }

        public override void Process(SkinType skinType)
        {
            if (skinType == SkinType.Inverted)
            {
                Process();
                return;
            }

            innerProcessor.Process(skinType);
        }
    }
}
