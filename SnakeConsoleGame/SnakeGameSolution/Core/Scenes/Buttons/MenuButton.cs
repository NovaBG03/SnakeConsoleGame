using SnakeGame.CustomExceptions;
using SnakeGame.GameObjects.Contracts;

namespace SnakeGame.Core.Scenes.Buttons
{
    public class MenuButton : Button
    {
        private const string ButtonText = "*   Menu    ";


        public MenuButton(IPoint topLeftPoint)
            : base(topLeftPoint, ButtonText)
        {

        }

        public override void Push()
        {
            throw new MenuSceneException();
        }
    }
}
