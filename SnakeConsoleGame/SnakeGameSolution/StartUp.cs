namespace SnakeGame
{
    using System;

    using SnakeGame.Core;
    using SnakeGame.Core.Contracts;
    using SnakeGame.Core.Scenes;
    using SnakeGame.Core.Scenes.Buttons;
    using SnakeGame.Core.Scenes.Buttons.Contracts;
    using SnakeGame.Core.Scenes.Contracts;
    using SnakeGame.Factories;
    using SnakeGame.Factories.Contracts;
    using SnakeGame.GameObjects;
    using SnakeGame.GameObjects.Contracts;
    using Utilities;

    public class StartUp
    {
        private static int middleConsoleWidth = Console.WindowWidth / 2;
        private static int middleConsoleHeight = Console.WindowHeight / 2;

        public static void Main()
        {
            ConsoleWindow.CustomizeConsole();

            IDrawManager drawManager = new ConsoleDrawManager();
            
            IBorder border = new Border(
                new Point(middleConsoleWidth - 25, middleConsoleHeight - 15),
                new Point(middleConsoleWidth + 25, middleConsoleHeight + 15));
            IFoodFactory foodFactory = new FoodFactory(new Random(), border);
            IScene pauseScene = new PauseScene(drawManager);
            IScene infoScene = new InfoScene(drawManager);
            IScene gameOverScene = new GameOverScene
                (
                drawManager,
                new PlayAgainButton(new Point(middleConsoleWidth, 30)), 
                new MenuButton(new Point(middleConsoleWidth, 35))
                );
            IButton playButton = new PlayButton
                (
                new Point(middleConsoleWidth, 30), 
                new Point(border.TopLeftCorner.CoordinateX + 2, border.TopLeftCorner.CoordinateY + 2), 
                drawManager, foodFactory, border, pauseScene, gameOverScene);
            IScene startMenu = new StartMenuScene(drawManager, playButton, new InfoButton(new Point(middleConsoleWidth, 35), infoScene), new ExitButton(new Point(middleConsoleWidth, 40)));

            Engine engine = new Engine(startMenu);
            engine.Run();
        }
    }
}
