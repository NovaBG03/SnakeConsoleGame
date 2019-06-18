namespace SnakeGame.Core.Scenes.Buttons
{
    using SnakeGame.Core.Scenes.Buttons.Contracts;
    using SnakeGame.GameObjects.Contracts;

    public abstract class Button : IButton
    {
        protected Button(IPoint topLeftPoint, string text)
        {
            this.TopLeftPoint = topLeftPoint;
            this.Text = text;
        }

        public IPoint TopLeftPoint { get; private set; }

        public string Text { get; private set; }

        public abstract void Push();
    }
}
