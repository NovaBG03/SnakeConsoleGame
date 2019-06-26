namespace SnakeGame.Core.Scenes
{
    using System;
    using System.Linq;

    using SnakeGame.Core.Contracts;
    using SnakeGame.Core.Scenes.Contracts;
    using SnakeGame.CustomExceptions;
    using SnakeGame.Enums;

    public class PauseScene : IScene
    {
        private const string PauseMessage = "╭━━━╮            " + "\n" +
                                            "┃╭━╮┃            " + "\n" +
                                            "┃╰━╯┣━━┳╮╭┳━━┳━━╮" + "\n" +
                                            "┃╭━━┫╭╮┃┃┃┃━━┫┃━┫" + "\n" +
                                            "┃┃  ┃╭╮┃╰╯┣━━┃┃━┫" + "\n" +
                                            "╰╯  ╰╯╰┻━━┻━━┻━━╯";
        private const string TextMessage = "Press Enter To Continue";
        private readonly IDrawManager drawManager;

        public PauseScene(IDrawManager drawManager)
        {
            this.drawManager = drawManager;

        }

        public void Display()
        {
            //Console.Clear();
            this.DrawMessages(Console.WindowWidth / 2 + 1, Console.WindowHeight / 4);

            while (true)
            {
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

        private void DrawMessages(int coordinateX, int coordinateY)
        {
            string PauseMessageLine = PauseMessage.Split().First();

            this.drawManager.DrawText(coordinateX - 8, coordinateY - 6, PauseMessage, PauseMessage.Length, Coordinate.X);
            this.drawManager.DrawText(coordinateX - (TextMessage.Length / 2), coordinateY, TextMessage, TextMessage.Length, Coordinate.X);

        }
    }
}
