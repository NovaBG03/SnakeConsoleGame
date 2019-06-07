namespace SnakeGame.GameObjects
{
    using System.Collections.Generic;
    using System.Linq;

    using SnakeGame.Enums;
    using SnakeGame.GameObjects.Contracts;

    public class Snake : ISnake, IDrawable
    {
        private const int InitializeX = 2;
        private const int InitializeY = 2;
        public const int DefaulLength = 6;

        private List<Point> body;

        public Snake(char symbol)
        {
            this.Symbol = symbol;
            this.body = new List<Point>();

            for (int i = 0; i < Snake.DefaulLength; i++)
            {
                int currentX = Snake.InitializeX + i;
                int currentY = Snake.InitializeY;
                this.body.Add(new Point(currentX, currentY));
            }
        }

        public Direction CurrentDirection { get; set; }

        public IReadOnlyCollection<Point> Body
            => this.body.AsReadOnly();

        public Point CurrentHead
            => this.body.Last();

        public Point Tale
            => this.body.First();

        public Point NextHead
            => this.GetNextHead();

        public char Symbol { get; }

        private Point GetNextHead()
        {
            switch (this.CurrentDirection)
            {
                case Direction.Right:
                    return new Point(CurrentHead.CoordinateX + 1, CurrentHead.CoordinateY);
                case Direction.Left:
                    return new Point(CurrentHead.CoordinateX - 1, CurrentHead.CoordinateY);
                case Direction.Down:
                    return new Point(CurrentHead.CoordinateX, CurrentHead.CoordinateY + 1);
                case Direction.Up:
                    return new Point(CurrentHead.CoordinateX, CurrentHead.CoordinateY - 1);
                default:
                    return this.CurrentHead;
            }
        }

        public void RemoveOldTale()
        {
            this.body.RemoveAt(0);
        }

        public void AddNextHead()
        {
            this.body.Add(this.NextHead);
        }
    }
}
