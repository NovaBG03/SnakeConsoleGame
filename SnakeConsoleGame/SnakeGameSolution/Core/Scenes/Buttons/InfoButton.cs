namespace SnakeGame.Core.Scenes.Buttons
{
    using SnakeGame.Core.Scenes.Contracts;
    using SnakeGame.GameObjects.Contracts;

    public class InfoButton : Button
    {
        private const string ButtonText = "*   Info    ";

        private IScene infoScene;

        public InfoButton(IPoint topLeftPoint, IScene infoScene)
            : base(topLeftPoint, ButtonText)
        {
            this.infoScene = infoScene;
        }

        public override void Push()
        {
            this.infoScene.Display();
        }
    }
}
