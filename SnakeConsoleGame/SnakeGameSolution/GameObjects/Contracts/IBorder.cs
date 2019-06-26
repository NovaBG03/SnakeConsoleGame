namespace SnakeGame.GameObjects.Contracts
{
    public interface IBorder
    {
        Point TopLeftCorner { get; }

        Point DownRightCorner { get; }

        char VerticalSymbol { get; }

        char HorizontalSymbol { get; }
    }
}
