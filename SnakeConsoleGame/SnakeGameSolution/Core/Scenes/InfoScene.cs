namespace SnakeGame.Core.Scenes
{
    using System;

    using SnakeGame.Core.Contracts;
    using SnakeGame.Core.Scenes.Contracts;
    using SnakeGame.CustomExceptions;
    using SnakeGame.Enums;

    public class InfoScene : IScene
    {
        private const string Message = "Info - Comming soon...";
        private const string ArrowsText = "      ┌─────┐" + "\n" +
                                          "      │  ʌ  │" + "\n" +
                                          "      │  │  │" + "\n" +
                                          "┌─────┼─────┼─────┐" + "\n" +
                                          "│ <══ │  ║  │ ══> │" + "\n" +
                                          "│     │  v  │     │" + "\n" +
                                          "└─────┴─────┴─────┘";
        private const string ControlsHeading = "╭━╮    ╭╮      ╭━╮" + "\n" +
                                               "┃╭╋━┳━┳┫╰┳┳┳━┳╮┃━┫" + "\n" +
                                               "┃╰┫╋┃┃┃┃╭┫╭┫╋┃╰╋━┃" + "\n" +
                                               "╰━┻━┻┻━┻━┻╯╰━┻━┻━╯" + "\n";
        private const string ControlsText = "Use arrows on your keyboard" + "\n" +
                                            "to navigate  throw the menu " + "\n" +
                                            "and  to  control  the snake" + "\n";
        private const string HowToPlayHeading = "╭╮╭╮    ╭━━╮ ╭━╮       " + "\n" +
                                                "┃╰╯┣━┳┳┳╋╮╭╋━┫╋┣╮╭━╮╭┳╮" + "\n" +
                                                "┃╭╮┃╋┃┃┃┃┃┃┃╋┃╭┫╰┫╋╰┫┃┃" + "\n" +
                                                "╰╯╰┻━┻━━╯╰╯╰━┻╯╰━┻━━╋╮┃" + "\n" +
                                                "                    ╰━╯" + "\n";


        private IDrawManager drawManager;

        public InfoScene(IDrawManager drawManager)
        {
            this.drawManager = drawManager;
        }

        public void Display()
        {
            Console.Clear();

            while (true)
            {
                drawManager.DrawText((Console.WindowWidth - Message.Length) / 2, Console.WindowHeight / 2, Message, Message.Length, Coordinate.X);
                this.DisplayControlsInfo(Console.WindowWidth / 3 - 11);
                this.DisplayHowToPlayInfo(Console.WindowWidth / 3 * 2 - 11);

                if (Console.KeyAvailable)
                {
                    var keyAsChar = Console.ReadKey().Key;
                    switch (keyAsChar)
                    {
                        case ConsoleKey.Escape:
                            throw new MenuSceneException();
                        default:
                            break;
                    }
                }
            }
        }

        private void DisplayHowToPlayInfo(int coordinateX)
        {
            drawManager.DrawText(coordinateX, Console.WindowHeight / 2 - 15, HowToPlayHeading, HowToPlayHeading.Length, Coordinate.X);
        }

        private void DisplayControlsInfo(int coordinateX)
        {
            drawManager.DrawText(coordinateX, Console.WindowHeight / 2 - 15, ControlsHeading, ControlsHeading.Length, Coordinate.X);
            drawManager.DrawText(coordinateX, Console.WindowHeight / 2 - 10, ArrowsText, ArrowsText.Length, Coordinate.X);
            drawManager.DrawText(coordinateX - 4, Console.WindowHeight / 2 - 2, ControlsText, ControlsText.Length, Coordinate.X);
        }
    }
}
