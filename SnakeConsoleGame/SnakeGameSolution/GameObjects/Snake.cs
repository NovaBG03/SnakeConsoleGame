namespace SnakeGame.GameObjects
{
    using System.Collections.Generic;
    using System.Linq;

    using SnakeGame.Enums;
    using SnakeGame.GameObjects.Contracts;

    public class Snake : ISnake, IDrawable
    {
        public const char DefaultBodySymbol = 'O';
        private const char DefaultHeadUpSymbol = 'ʌ';
        private const char DefaultHeadDownSymbol = 'v';
        private const char DefaultHeadRightSymbol = '>';
        private const char DefaultHeadLeftSymbol = '<';
        public const int DefaulLength = 6;

        private List<IPoint> body;
        private Direction currentDirection;

        public Snake(IPoint initializePoint)
        {
            this.Symbol = DefaultHeadRightSymbol;
            this.body = new List<IPoint>();

            int currentX = initializePoint.CoordinateX;
            int currentY = initializePoint.CoordinateY;

            for (int i = 0; i < Snake.DefaulLength; i++)
            {
                this.body.Add(new Point(currentX++, currentY));
            }
        }

        public Direction CurrentDirection
        {
            get => currentDirection;
            set
            {
                switch (value)
                {
                    case Direction.Right:
                        this.Symbol = DefaultHeadRightSymbol;
                        break;
                    case Direction.Left:
                        this.Symbol = DefaultHeadLeftSymbol;
                        break;
                    case Direction.Down:
                        this.Symbol = DefaultHeadDownSymbol;
                        break;
                    case Direction.Up:
                        this.Symbol = DefaultHeadUpSymbol;
                        break;
                }

                currentDirection = value;
            }
        }

        public IReadOnlyCollection<IPoint> Body
            => this.body.AsReadOnly();

        public IPoint CurrentHead
            => this.body.Last();

        public IPoint Tale
            => this.body.First();

        public IPoint NextHead
            => this.GetNextHead();

        public char Symbol { get; private set; }

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
