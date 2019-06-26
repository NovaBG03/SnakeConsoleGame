namespace SnakeGame.Core.Scenes
{
    using System;
    using System.Linq;
    using System.Threading;

    using SnakeGame.Core.Contracts;
    using SnakeGame.Enums;
    using SnakeGame.Factories.Contracts;
    using SnakeGame.GameObjects.Contracts;
    using SnakeGame.GameObjects.Foods.Contracts;
    using SnakeGame.Core.Scenes.Contracts;
    using SnakeGame.GameObjects;

    public class GameScene : IScene
    {
        private const int GameMaxSpeed = 70;
        private const int GameMinSpeed = 150;

        private readonly ISnake snake;
        private readonly IDrawManager drawManager;
        private readonly IFoodFactory foodFactory;
        private readonly IBorder border;
        private readonly IScoreBoard scoreBoard;
        private readonly IScene pauseScene;
        private readonly IScene gameOverScene;
        private IFood spawnedFood;
        private int gameSpeed;

        public GameScene(ISnake snake, IDrawManager drawManager, IFoodFactory foodFactory, IBorder border, IScoreBoard scoreBoard, IScene pauseScene, IScene gameOverScene)
        {
            this.snake = snake;
            this.drawManager = drawManager;
            this.foodFactory = foodFactory;
            this.border = border;
            this.scoreBoard = scoreBoard;
            this.pauseScene = pauseScene;
            this.gameOverScene = gameOverScene;
            this.spawnedFood = null;
            this.GameSpeed = GameMinSpeed;
        }

        private int GameSpeed
        {
            get => this.gameSpeed;
            set
            {
                if (value < GameMaxSpeed)
                {
                    this.gameSpeed = GameMaxSpeed;
                    return;
                }

                this.gameSpeed = value;
            }
        }

        public void Display()
        {
            DisplayAll();

            while (true)
            {
                if (Console.KeyAvailable)
                {
                    var key = Console.ReadKey();
                    GetUserInput(key);
                }

                if (this.spawnedFood == null)
                {
                    SpawnFood();
                }

                MoveSnake();

                Thread.Sleep(this.GameSpeed);
            }
        }

        private void DisplayAll()
        {
            Console.Clear();
            DisplayBorder();
            DisplayScoreBoard();
            DisplaySnake();
            DisplayFood();
        }

        private void DisplayFood()
        {
            if (this.spawnedFood != null)
            {
                this.drawManager.DrawPoint(this.spawnedFood, (this.spawnedFood as IDrawable).Symbol);
            }
        }

        private void DisplaySnake()
        {
            this.drawManager.DrawPoint(snake.Body, Snake.DefaultBodySymbol);
        }

        private void DisplayScoreBoard()
        {
            this.drawManager.DrawText(this.scoreBoard.StartingPoint, this.scoreBoard.InfoMessage, 20, Coordinate.X);
            this.drawManager.DrawText(this.scoreBoard.StartingPoint.CoordinateX, this.scoreBoard.StartingPoint.CoordinateY + 2, this.scoreBoard.FoodMessage, 30, Coordinate.X);
        }

        private void DisplayBorder()
        {
            int height = border.DownRightCorner.CoordinateY - border.TopLeftCorner.CoordinateY + 1;
            int width = border.DownRightCorner.CoordinateX - border.TopLeftCorner.CoordinateX + 1;

            this.drawManager.DrawText(this.border.TopLeftCorner.CoordinateX, this.border.TopLeftCorner.CoordinateY, new string(this.border.HorizontalSymbol, width), width, Coordinate.X);
            this.drawManager.DrawText(this.border.TopLeftCorner.CoordinateX, this.border.DownRightCorner.CoordinateY, new string(this.border.HorizontalSymbol, width), width, Coordinate.X);

            this.drawManager.DrawText(this.border.TopLeftCorner.CoordinateX, this.border.TopLeftCorner.CoordinateY, new string(this.border.VerticalSymbol, height), height, Coordinate.Y);
            this.drawManager.DrawText(this.border.DownRightCorner.CoordinateX, this.border.TopLeftCorner.CoordinateY, new string(this.border.VerticalSymbol, height), height, Coordinate.Y);

        }

        private void SpawnFood()
        {
            while (true)
            {
                this.spawnedFood = foodFactory.CreateFood();
                if (!IsFromBorder(spawnedFood) && !IsFromSnakeBody(spawnedFood))
                {
                    break;
                }
            }

            DisplayFood();
        }

        private void MoveSnake()
        {
            var nextHead = this.snake.NextHead;

            if (this.IsFromSnakeBody(nextHead) || IsFromBorder(nextHead))
            {
                (gameOverScene as GameOverScene).PlayerScore = this.scoreBoard.PlayerScore;
                gameOverScene.Display();
            }
            else if (IsOnePoint(nextHead, this.spawnedFood))
            {
                this.scoreBoard.AddEatenFood(this.spawnedFood);
                this.GameSpeed -= this.spawnedFood.Score;
                this.DisplayScoreBoard();
                this.spawnedFood = null;
            }
            else
            {
                var oldTale = snake.Tale;
                this.snake.RemoveOldTale();
                this.drawManager.ClearPoint(oldTale);
            }

            this.drawManager.DrawPoint(this.snake.CurrentHead, Snake.DefaultBodySymbol);

            this.snake.AddNextHead();
            this.drawManager.DrawPoint(snake.CurrentHead, (snake as IDrawable).Symbol);
        }

        private bool IsFromBorder(IPoint point)
        {
            return point.CoordinateX == border.TopLeftCorner.CoordinateX
                || point.CoordinateX == border.DownRightCorner.CoordinateX
                || point.CoordinateY == border.TopLeftCorner.CoordinateY
                || point.CoordinateY == border.DownRightCorner.CoordinateY;
        }

        private bool IsOnePoint(IPoint firstPoint, IPoint secondPoint)
        {
            return firstPoint.CoordinateX == secondPoint.CoordinateX
                            && firstPoint.CoordinateY == secondPoint.CoordinateY;
        }

        private bool IsFromSnakeBody(IPoint nextHead)
        {
            return this.snake.Body.Any(p => IsOnePoint(nextHead, p));
        }

        private void GetUserInput(ConsoleKeyInfo key)
        {
            var keyAsChar = key.Key;
            switch (keyAsChar)
            {
                case ConsoleKey.RightArrow:
                    if (snake.CurrentDirection == Direction.Left)
                    {
                        return;
                    }
                    snake.CurrentDirection = Direction.Right;
                    break;
                case ConsoleKey.LeftArrow:
                    if (snake.CurrentDirection == Direction.Right)
                    {
                        return;
                    }
                    snake.CurrentDirection = Direction.Left;
                    break;
                case ConsoleKey.DownArrow:
                    if (snake.CurrentDirection == Direction.Up)
                    {
                        return;
                    }
                    snake.CurrentDirection = Direction.Down;
                    break;
                case ConsoleKey.UpArrow:
                    if (snake.CurrentDirection == Direction.Down)
                    {
                        return;
                    }
                    snake.CurrentDirection = Direction.Up;
                    break;
                case ConsoleKey.Escape:
                    this.pauseScene.Display();
                    this.DisplayAll();
                    break;
                default:
                    break;
            }
        }
    }
}
