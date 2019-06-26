namespace SnakeGame.Core.Scenes
{
    using System;

    using SnakeGame.Core.Contracts;
    using SnakeGame.Core.Scenes.Buttons;
    using SnakeGame.Core.Scenes.Buttons.Contracts;
    using SnakeGame.Core.Scenes.Contracts;
    using SnakeGame.CustomExceptions;
    using SnakeGame.Enums;

    public class GameOverScene : IScene
    {
        private const string GameOverMessage = 
            "╭━━━╮        ╭━━━╮" + "\n" +
            "┃╭━╮┃        ┃╭━╮┃" + "\n" +
            "┃┃ ╰╋━━┳╮╭┳━━┫┃╱┃┣╮╭┳━━┳━╮" + "\n" +
            "┃┃╭━┫╭╮┃╰╯┃┃━┫┃╱┃┃╰╯┃┃━┫╭╯" + "\n" +
            "┃╰┻━┃╭╮┃┃┃┃┃━┫╰━╯┣╮╭┫┃━┫┃" + "\n" +
            "╰━━━┻╯╰┻┻┻┻━━┻━━━╯╰╯╰━━┻╯";
        private const string PointsMessageFormat = "Your score was: {0}";

        private readonly IButton[] buttons;
        private readonly IDrawManager drawManager;
        private int index;

        public GameOverScene(IDrawManager drawManager, params Button[] buttons)
        {
            this.index = 0;
            this.buttons = buttons;
            this.drawManager = drawManager;
        }

        public int PlayerScore { get; set; }

        private string PointsMessage => string.Format(PointsMessageFormat, this.PlayerScore);

        private IButton CurrentButton => this.buttons[index];

        public void Display()
        {
            Console.Clear();
            this.index = 0;

            while (true)
            {
                this.DisplayText();
                this.DisplayButtons();
                this.DisplayCurrentButton();

                var keyAsChar = Console.ReadKey().Key;
                switch (keyAsChar)
                {
                    case ConsoleKey.Enter:
                        this.PushButton();
                        break;
                    case ConsoleKey.DownArrow:
                        this.MoveToDownButton();
                        break;
                    case ConsoleKey.UpArrow:
                        this.MoveToUpButton();
                        break;
                    case ConsoleKey.Escape:
                        throw new MenuSceneException();
                    default:
                        break;
                }
            }
        }

        private void DisplayText()
        {
            drawManager.DrawText(Console.WindowWidth / 2 - 8, Console.WindowHeight / 2 - 4, PointsMessage, PointsMessage.Length, Coordinate.X);
            drawManager.DrawText(Console.WindowWidth / 2 - 12, Console.WindowHeight / 2 - 10, GameOverMessage, GameOverMessage.Length, Coordinate.X);
        }

        private void DisplayCurrentButton()
        {
            this.drawManager.DrawButton(this.CurrentButton, ConsoleColor.Cyan);
        }

        private void PushButton()
        {
            this.CurrentButton.Push();
        }

        private void MoveToUpButton()
        {
            if (index == 0)
            {
                this.index = this.buttons.Length - 1;
                return;
            }

            this.index--;
        }

        private void MoveToDownButton()
        {
            if (index == this.buttons.Length - 1)
            {
                this.index = 0;
                return;
            }

            this.index++;
        }

        private void DisplayButtons()
        {
            foreach (var button in this.buttons)
            {
                drawManager.DrawButton(button);
            }
        }
    }
}
