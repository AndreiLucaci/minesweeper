using Minesweeper.Ui.Models;

namespace Minesweeper.Ui.Processors
{
    public class CustomSkinProcessor : BaseSkinProcessor
    {
        private ISkinProcessor _innerProcessor { get; }

        protected override string Path { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

        public CustomSkinProcessor(ISkinProcessor innerProcessor)
        {
            this._innerProcessor = innerProcessor;
        }

        public override void Process(SkinType skinType)
        {
            if (skinType == SkinType.Custom)
            {
                Process();
                return;
            }

            _innerProcessor.Process(skinType);
        }
    }
}
