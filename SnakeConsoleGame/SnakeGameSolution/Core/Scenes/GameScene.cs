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

    public class GameScene : IScene
    {
        private readonly ISnake snake;
        private readonly IDrawManager drawManager;
        private readonly IFoodFactory foodFactory;
        private readonly IBorder border;
        private readonly IScoreBoard scoreBoard;
        private readonly IScene pauseScene;
        private readonly IScene gameOverScene;
        private IFood spawnedFood;

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

                Thread.Sleep(100);
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
            this.drawManager.DrawPoint(snake.Body, (snake as IDrawable).Symbol);
        }

        private void DisplayScoreBoard()
        {
            this.drawManager.DrawText(this.scoreBoard.StartingPoint, this.scoreBoard.InfoMessage, 20, Coordinate.X);
        }

        private void DisplayBorder()
        {
            int height = border.DownRightCorner.CoordinateY - border.TopLeftCorner.CoordinateY + 1;
            int width = border.DownRightCorner.CoordinateX - border.TopLeftCorner.CoordinateX + 1;

            this.drawManager.DrawText(this.border.TopLeftCorner.CoordinateX, this.border.TopLeftCorner.CoordinateY, new string((this.border as IDrawable).Symbol, height), height, Coordinate.Y);
            this.drawManager.DrawText(this.border.DownRightCorner.CoordinateX, this.border.TopLeftCorner.CoordinateY, new string((this.border as IDrawable).Symbol, height), height, Coordinate.Y);

            this.drawManager.DrawText(this.border.TopLeftCorner.CoordinateX, this.border.TopLeftCorner.CoordinateY, new string((this.border as IDrawable).Symbol, width), width, Coordinate.X);
            this.drawManager.DrawText(this.border.TopLeftCorner.CoordinateX, this.border.DownRightCorner.CoordinateY, new string((this.border as IDrawable).Symbol, width), width, Coordinate.X);
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
                gameOverScene.Display();
            }
            else if (IsOnePoint(nextHead, this.spawnedFood))
            {
                this.scoreBoard.AddEatenFood(this.spawnedFood);
                this.DisplayScoreBoard();
                this.spawnedFood = null;
            }
            else
            {
                var oldTale = snake.Tale;
                this.snake.RemoveOldTale();
                this.drawManager.ClearPoint(oldTale);
            }

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
