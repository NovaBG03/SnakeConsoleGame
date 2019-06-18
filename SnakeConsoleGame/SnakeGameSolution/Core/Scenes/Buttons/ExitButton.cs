namespace SnakeGame.Core.Scenes.Buttons
{
    using System;

    using SnakeGame.GameObjects;

    public class ExitButton : Button
    {
        private const string ButtonText = "*   Exit    ";

        public ExitButton(Point topLeftPoint)
            : base(topLeftPoint, ButtonText)
        {
            
        }

        public override void Push()
        {
            Environment.Exit(0);
        }
    }
}
