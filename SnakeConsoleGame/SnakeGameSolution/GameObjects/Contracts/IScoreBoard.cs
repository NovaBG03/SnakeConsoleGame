namespace SnakeGame.GameObjects.Contracts
{
    using SnakeGame.GameObjects.Foods.Contracts;

    public interface IScoreBoard
    {
        IPoint StartingPoint { get; }

        string InfoMessage { get; }

        void AddEatenFood(IFood food);
    }
}
