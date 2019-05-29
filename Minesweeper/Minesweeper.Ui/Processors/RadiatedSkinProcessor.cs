using System.Drawing;
using Minesweeper.Ui.Models;

namespace Minesweeper.Ui.Processors
{
    public class RadiatedSkinProcessor : BaseSkinProcessor
    {
        private readonly ISkinProcessor innerProcessor;

        protected override Bitmap SkinImage => Properties.Resources.radiated;

        public RadiatedSkinProcessor(ISkinProcessor innerProcessor)
        {
            this.innerProcessor = innerProcessor;
        }

        public override void Process(SkinType skinType)
        {
            if (skinType == SkinType.Radiated)
            {
                Process();
                return;
            }

            innerProcessor.Process(skinType);
        }
    }
}
