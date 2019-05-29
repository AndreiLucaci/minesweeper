using System;
using Minesweeper.Ui.Models;

namespace Minesweeper.Ui.Processors
{
    public class DefaultSkinProcessor : BaseSkinProcessor
    {
        protected override string Path { get; set; } = $@"{AppDomain.CurrentDomain.BaseDirectory}\Images\skin.png";

        public override void Process(SkinType skinType)
        {
            Process();
        }
    }
}
