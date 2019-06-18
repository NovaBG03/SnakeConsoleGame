namespace SnakeGame.Core
{
    using SnakeGame.Core.Scenes.Contracts;

    public class Engine
    {
        private IScene startMenu;

        public Engine(IScene startMenu)
        {
            this.startMenu = startMenu;
        }

        public void Run()
        {
            this.startMenu.Display();
        }
    }
}
