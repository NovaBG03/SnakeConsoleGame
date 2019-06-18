namespace SnakeGame.Core.Scenes
{
    using System;

    using SnakeGame.Core.Contracts;
    using SnakeGame.Core.Scenes.Contracts;
    using SnakeGame.CustomExceptions;
    using SnakeGame.Enums;

    public class PauseScene : IScene
    {
        private const string Message = "Pause... \n Press Enter To Continue";
        private readonly IDrawManager drawManager;

        public PauseScene(IDrawManager drawManager)
        {
            this.drawManager = drawManager;

        }

        public void Display()
        {
            //Console.Clear();

            while (true)
            {
                drawManager.DrawText((Console.WindowWidth - Message.Length) / 2, Console.WindowHeight / 2, Message, Message.Length, Coordinate.X);

                if (Console.KeyAvailable)
                {
                    var keyAsChar = Console.ReadKey().Key;
                    switch (keyAsChar)
                    {
                        case ConsoleKey.Enter:
                            return;
                        case ConsoleKey.Escape:
                            throw new MenuSceneException();
                        default:
                            break;
                    }
                }
            }
        }
    }
}
