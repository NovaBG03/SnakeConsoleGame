namespace SnakeGame.Factories.Contracts
{
    using SnakeGame.GameObjects.Foods.Contracts;

    public interface IFoodFactory
    {
        IFood CreateFood();
    }
}
