namespace SnakeGame.Core.Scenes.Buttons
{
    using SnakeGame.CustomExceptions;
    using SnakeGame.GameObjects.Contracts;

    public class PlayAgainButton : Button
    {
        private const string ButtonText = "* Play Again";


        public PlayAgainButton(IPoint topLeftPoint)
            : base(topLeftPoint, ButtonText)
        {

        }

        public override void Push()
        {
            throw new MenuSceneException("newgame");
        }
    }
}
