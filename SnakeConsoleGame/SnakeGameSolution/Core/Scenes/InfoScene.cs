namespace SnakeGame.Core.Scenes
{
    using System;
    using System.Linq;
    using SnakeGame.Core.Contracts;
    using SnakeGame.Core.Scenes.Contracts;
    using SnakeGame.CustomExceptions;
    using SnakeGame.Enums;

    public class InfoScene : IScene
    {
        private const string ArrowsPictureText = "      ┌─────┐" + "\n" +
                                                 "      │  ʌ  │" + "\n" +
                                                 "      │  │  │" + "\n" +
                                                 "┌─────┼─────┼─────┐" + "\n" +
                                                 "│ <══ │  ║  │ ══> │" + "\n" +
                                                 "│     │  v  │     │" + "\n" +
                                                 "└─────┴─────┴─────┘";
        private const string ControlsHeading = "╭━╮    ╭╮      ╭━╮" + "\n" +
                                               "┃╭╋━┳━┳┫╰┳┳┳━┳╮┃━┫" + "\n" +
                                               "┃╰┫╋┃┃┃┃╭┫╭┫╋┃╰╋━┃" + "\n" +
                                               "╰━┻━┻┻━┻━┻╯╰━┻━┻━╯";
        private const string ControlsText = "Use arrows on your keyboard" + "\n" +
                                            "to navigate  throw the menu" + "\n" +
                                            "and  to  control  the snake." + "\n";
        private const string HowToPlayHeading = "╭╮╭╮    ╭━━╮ ╭━╮       " + "\n" +
                                                "┃╰╯┣━┳┳┳╋╮╭╋━┫╋┣╮╭━╮╭┳╮" + "\n" +
                                                "┃╭╮┃╋┃┃┃┃┃┃┃╋┃╭┫╰┫╋╰┫┃┃" + "\n" +
                                                "╰╯╰┻━┻━━╯╰╯╰━┻╯╰━┻━━╋╮┃" + "\n" +
                                                "                    ╰━╯";
        private const string HowToPlayText = "Your main goal is to grow your" + "\n" +
                                             "snake. When  you  eat food the" + "\n" +
                                             "snake  grows   and  speeds  up.";
        private const string SnakePictureText = "▄▄▀█▄   ▄       ▄     " + "\n" +
                                                "▀▀▀██  ███     ███    " + "\n" +
                                                " ▄██▀ █████   █████   " + "\n" +
                                                "███▀▄███ ███ ███ ███ ▄" + "\n" +
                                                "▀█████▀   ▀███▀   ▀██▀";
        private const string GameCreatorHeading = "╭━━━╮        ╭━━━╮       ╭╮      " + "\n" +
                                                  "┃╭━╮┃        ┃╭━╮┃      ╭╯╰╮     " + "\n" +
                                                  "┃┃ ╰╋━━┳╮╭┳━━┫┃ ╰╋━┳━━┳━┻╮╭╋━━┳━╮" + "\n" +
                                                  "┃┃╭━┫╭╮┃╰╯┃┃━┫┃ ╭┫╭┫┃━┫╭╮┃┃┃╭╮┃╭╯" + "\n" +
                                                  "┃╰┻━┃╭╮┃┃┃┃┃━┫╰━╯┃┃┃┃━┫╭╮┃╰┫╰╯┃┃ " + "\n" +
                                                  "╰━━━┻╯╰┻┻┻┻━━┻━━━┻╯╰━━┻╯╰┻━┻━━┻╯ ";
        private const string GameCreatorName = "╭━┳┳┳╮╭┳╮    ╭┳╮             " + "\n" +
                                               "┃┃┃┣┫┣╋┫╰┳━╮ ┃╭╋━┳┳┳━┳┳━┳━┳━╮" + "\n" +
                                               "┃┃┃┃┃━┫┃╭┫╋╰╮┃╰┫╋┃┃┃┃┃┃╋┣╮┃╭╯" + "\n" +
                                               "╰┻━┻┻┻┻┻━┻━━╯╰┻┻━╋╮┣┻━┻━╯╰━╯ " + "\n" +
                                               "                 ╰━╯";
        private const string GameCreatorPictureText =  " ▄▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▄  " + "\n" +
                                                       "█░░░█░░░░░░░░░░▄▄░██░█" + "\n" +
                                                       "█░▀▀█▀▀░▄▀░▄▀░░▀▀░▄▄░█" + "\n" +
                                                       "█░░░▀░░░▄▄▄▄▄░░██░▀▀░█" + "\n" +
                                                       " ▀▄▄▄▄▄▀     ▀▄▄▄▄▄▄▀ ";



        private readonly IDrawManager drawManager;

        public InfoScene(IDrawManager drawManager)
        {
            this.drawManager = drawManager;
        }

        public void Display()
        {
            Console.Clear();

            while (true)
            {
                this.DisplayControlsInfo(Console.WindowWidth / 3 - 11, Console.WindowHeight / 2 - 2);
                this.DisplayHowToPlayInfo(Console.WindowWidth / 3 * 2 - 11, Console.WindowHeight / 2 - 4);
                this.DisplayGameCreator(Console.WindowWidth / 2 - 11, Console.WindowHeight / 3 * 2);

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

        private void DisplayHowToPlayInfo(int coordinateX, int coordinateY)
        {
            drawManager.DrawText(coordinateX, coordinateY - 11, HowToPlayHeading, HowToPlayHeading.Length, Coordinate.X);
            drawManager.DrawText(coordinateX - 4, coordinateY - 5, HowToPlayText, HowToPlayText.Length, Coordinate.X);
            drawManager.DrawText(coordinateX, coordinateY, SnakePictureText, SnakePictureText.Length, Coordinate.X);
        }

        private void DisplayGameCreator(int coordinateX, int coordinateY)
        {
            drawManager.DrawText(coordinateX - 5, coordinateY - 11, GameCreatorHeading, GameCreatorHeading.Length, Coordinate.X);
            drawManager.DrawText(coordinateX, coordinateY - 5, GameCreatorName, GameCreatorName.Length, Coordinate.X);
            drawManager.DrawText(coordinateX, coordinateY + 1, GameCreatorPictureText, GameCreatorPictureText.Length, Coordinate.X);
        }

        private void DisplayControlsInfo(int coordinateX, int coordinateY)
        {
            drawManager.DrawText(coordinateX, coordinateY - 13, ControlsHeading, ControlsHeading.Length, Coordinate.X);
            drawManager.DrawText(coordinateX, coordinateY - 8, ArrowsPictureText, ArrowsPictureText.Length, Coordinate.X);
            drawManager.DrawText(coordinateX - 4, coordinateY, ControlsText, ControlsText.Length, Coordinate.X);
        }
    }
}
