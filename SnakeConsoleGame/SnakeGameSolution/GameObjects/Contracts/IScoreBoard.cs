namespace SnakeGame.GameObjects.Contracts
{
    using SnakeGame.GameObjects.Foods.Contracts;

    public interface IScoreBoard
    {
        IPoint StartingPoint { get; }

        string InfoMessage { get; }

        string FoodMessage { get; }

        int PlayerScore { get; }

        void AddEatenFood(IFood food);
    }
}
