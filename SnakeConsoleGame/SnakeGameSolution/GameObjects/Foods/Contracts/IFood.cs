namespace SnakeGame.GameObjects.Foods.Contracts
{
    using SnakeGame.GameObjects.Contracts;

    public interface IFood : IPoint
    {
        int Score { get; }
    }
}
