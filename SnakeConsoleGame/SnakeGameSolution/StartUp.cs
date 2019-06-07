namespace SnakeGame
{
    using System;

    using SnakeGame.Core;
    using SnakeGame.Core.Contracts;
    using SnakeGame.Factories;
    using SnakeGame.Factories.Contracts;
    using SnakeGame.GameObjects;
    using SnakeGame.GameObjects.Contracts;
    using Utilities;

    public class StartUp
    {
        public static void Main()
        {
            ConsoleWindow.CustomizeConsole();

            ISnake snake = new Snake('O');
            IDrawManager drawManager = new ConsoleDrawManager();
            //IBorder border = new Border(
            //    new Point(Console.WindowTop, Console.WindowLeft), 
            //    new Point(Console.WindowWidth - 1, Console.WindowHeight - 1),
            //    '=');
            IBorder border = new Border(
                new Point(Console.WindowTop, Console.WindowLeft),
                new Point(50, 30),
                '=');
            IScoreBoard scoreBoard = new ScoreBoard(new Point(border.DownRightCorner.CoordinateX + 3, border.TopLeftCorner.CoordinateY + 3), Snake.DefaulLength);
            IFoodFactory foodFactory = new FoodFactory(new Random(), border);
            Engine engine = new Engine(snake, drawManager, foodFactory, border, scoreBoard);
            engine.Run();
        }
    }
}
