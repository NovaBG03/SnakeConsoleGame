namespace SnakeGame.GameObjects
{
    using SnakeGame.GameObjects.Contracts;

    public class Border : IBorder
    {
        private const char DefaultVerticalSymbol = '█';
        private const char DefaultHorizontalSymbol = '\u25A0';

        public Border(Point topLeftCorner, Point downRightCorner)
        {
            this.VerticalSymbol = DefaultVerticalSymbol;
            this.HorizontalSymbol = DefaultHorizontalSymbol;
            this.TopLeftCorner = topLeftCorner;
            this.DownRightCorner = downRightCorner;
        }

        public Point TopLeftCorner { get; }

        public Point DownRightCorner { get; }

        public char VerticalSymbol { get; }

        public char HorizontalSymbol { get; }
    }
}
