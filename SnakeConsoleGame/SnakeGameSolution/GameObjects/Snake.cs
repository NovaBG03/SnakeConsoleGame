namespace SnakeGame.GameObjects
{
    using System.Collections.Generic;
    using System.Linq;

    using SnakeGame.Enums;
    using SnakeGame.GameObjects.Contracts;

    public class Snake : ISnake, IDrawable
    {
        private const char DefaultSymbol = 'O';
        public const int DefaulLength = 6;

        private List<IPoint> body;

        public Snake(IPoint initializePoint)
        {
            this.Symbol = DefaultSymbol;
            this.body = new List<IPoint>();

            int currentX = initializePoint.CoordinateX;
            int currentY = initializePoint.CoordinateY;

            for (int i = 0; i < Snake.DefaulLength; i++)
            {
                this.body.Add(new Point(currentX++, currentY));
            }
        }

        public Direction CurrentDirection { get; set; }

        public IReadOnlyCollection<IPoint> Body
            => this.body.AsReadOnly();

        public IPoint CurrentHead
            => this.body.Last();

        public IPoint Tale
            => this.body.First();

        public IPoint NextHead
            => this.GetNextHead();

        public char Symbol { get; }

        private IPoint GetNextHead()
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
