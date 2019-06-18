namespace SnakeGame.Core.Scenes
{
    using System;
    using SnakeGame.Core.Contracts;
    using SnakeGame.Core.Scenes.Buttons.Contracts;
    using SnakeGame.Core.Scenes.Contracts;
    using SnakeGame.CustomExceptions;
    using SnakeGame.Enums;

    public class StartMenuScene : IScene
    {
        private const string StartMenuMessage =
            "╭━━━━┳╮    ╭━━━╮           ╭╮   ╭━━━╮     ╭╮" + "\n" +
            "┃╭╮╭╮┃┃    ┃╭━╮┃           ┃┃   ┃╭━╮┃     ┃┃" + "\n" +
            "╰╯┃┃╰┫╰━┳━━┫┃ ╰╋━━┳━╮╭━━┳━━┫┃╭━━┫╰━━┳━╮╭━━┫┃╭┳━━╮" + "\n" +
            "  ┃┃ ┃╭╮┃┃━┫┃ ╭┫╭╮┃╭╮┫━━┫╭╮┃┃┃┃━╋━━╮┃╭╮┫╭╮┃╰╯┫┃━┫" + "\n" +
            "  ┃┃ ┃┃┃┃┃━┫╰━╯┃╰╯┃┃┃┣━━┃╰╯┃╰┫┃━┫╰━╯┃┃┃┃╭╮┃╭╮┫┃━┫" + "\n" +
            "  ╰╯ ╰╯╰┻━━┻━━━┻━━┻╯╰┻━━┻━━┻━┻━━┻━━━┻╯╰┻╯╰┻╯╰┻━━╯";

        private readonly IButton[] buttons;
        private readonly IDrawManager drawManager;

        private int index;

        public StartMenuScene(IDrawManager drawManager, params IButton[] buttons)
        {
            this.index = 0;
            this.buttons = buttons;
            this.drawManager = drawManager;
        }

        private IButton CurrentButton => this.buttons[index];

        public void Display()
        {
            Console.Clear();

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
                        Console.Clear();
                        this.DisplayButtons();
                        break;
                    case ConsoleKey.DownArrow:
                        this.MoveToDownButton();
                        break;
                    case ConsoleKey.UpArrow:
                        this.MoveToUpButton();
                        break;
                    case ConsoleKey.Escape:
                        Environment.Exit(0);
                        break;
                    default:
                        break;
                }
            }
        }

        private void DisplayCurrentButton()
        {
            this.drawManager.DrawButton(this.CurrentButton, ConsoleColor.Cyan);
        }

        private void PushButton()
        {
            string message = string.Empty;

            try
            {
                this.CurrentButton.Push();
            }
            catch (MenuSceneException me)
            {
                message = me.Message;
            }

            if (message == "newgame")
            {
                this.PushButton();
            }
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

        private void DisplayText()
        {
            drawManager.DrawText(Console.WindowWidth / 2 - 23, Console.WindowHeight / 2 - 10, StartMenuMessage, StartMenuMessage.Length, Coordinate.X);
        }
    }
}
