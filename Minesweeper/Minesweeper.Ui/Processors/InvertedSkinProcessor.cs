using System;
using Minesweeper.Ui.Models;

namespace Minesweeper.Ui.Processors
{
    public class InvertedSkinProcessor : BaseSkinProcessor
    {
        private readonly ISkinProcessor innerProcessor;

        protected override string Path { get; set; } = $@"{AppDomain.CurrentDomain.BaseDirectory}\Images\inverted.png";

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
