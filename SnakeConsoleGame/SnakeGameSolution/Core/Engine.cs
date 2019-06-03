namespace SnakeGame.Core
{
    using System;
    using System.Linq;
    using System.Threading;

    using SnakeGame.Core.Contracts;
    using SnakeGame.Enums;
    using SnakeGame.Factories.Contracts;
    using SnakeGame.GameObjects;
    using SnakeGame.GameObjects.Contracts;
    using SnakeGame.GameObjects.Foods.Contracts;

    public class Engine
    {
        private ISnake snake;
        private IDrawManager drawManager;
        private IFoodFactory foodFactory;
        private IFood spawnedFood;
        private IBorder border;

        public Engine(ISnake snake, IDrawManager drawManager, IFoodFactory foodFactory, IBorder border)
        {
            this.snake = snake;
            this.drawManager = drawManager;
            this.foodFactory = foodFactory;
            this.border = border;
        }

        public void Run()
        {
            SpawnBorder();
            this.drawManager.Draw(snake.Body, (snake as IDrawable).Symbol);

            while (true)
            {
                if (Console.KeyAvailable)
                {
                    var key = Console.ReadKey();
                    SetNewDirection(key);
                }

                if (this.spawnedFood == null)
                {
                    SpawnFood();
                }

                MoveSnake();

                Thread.Sleep(100);
            }
        }

        private void SpawnBorder()
        {
            for (int i = 0; i < this.border.DownRightCorner.CoordinateX; i++)
            {
                this.drawManager.Draw(new Point(i, this.border.TopLeftCorner.CoordinateY), (this.border as IDrawable).Symbol);
                this.drawManager.Draw(new Point(i, this.border.DownRightCorner.CoordinateY), (this.border as IDrawable).Symbol);
            }

            for (int i = 0; i < this.border.DownRightCorner.CoordinateY; i++)
            {
                this.drawManager.Draw(new Point(this.border.TopLeftCorner.CoordinateX, i), (this.border as IDrawable).Symbol);
                this.drawManager.Draw(new Point(this.border.DownRightCorner.CoordinateX, i), (this.border as IDrawable).Symbol);
            }
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
            
            this.drawManager.Draw(this.spawnedFood, (this.spawnedFood as IDrawable).Symbol);
        }

        private void MoveSnake()
        {
            var nextHead = this.snake.NextHead;

            if (this.IsFromSnakeBody(nextHead) || IsFromBorder(nextHead))
            {
                Environment.Exit(0);
            }
            else if (IsOnePoint(nextHead, this.spawnedFood))
            {
                this.spawnedFood = null;
            }
            else
            {
                var oldTale = snake.Tale;
                this.snake.RemoveOldTale();
                this.drawManager.ClearPoint(oldTale);
            }

            this.snake.AddNextHead();
            this.drawManager.Draw(snake.CurrentHead, (snake as IDrawable).Symbol);
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

        private void SetNewDirection(ConsoleKeyInfo key)
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
                    Environment.Exit(0);
                    break;
                default:
                    break;
            }
        }
    }
}
