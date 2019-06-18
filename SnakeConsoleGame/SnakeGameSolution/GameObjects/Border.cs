namespace SnakeGame.GameObjects
{
    using SnakeGame.GameObjects.Contracts;

    public class Border : IBorder, IDrawable
    {
        private const char DefaultSymbol = '=';

        public Border(Point topLeftCorner, Point downRightCorner)
        {
            this.Symbol = DefaultSymbol;
            this.TopLeftCorner = topLeftCorner;
            this.DownRightCorner = downRightCorner;
        }

        public Point TopLeftCorner { get; }

        public Point DownRightCorner { get; }

        public char Symbol { get; }
    }
}
