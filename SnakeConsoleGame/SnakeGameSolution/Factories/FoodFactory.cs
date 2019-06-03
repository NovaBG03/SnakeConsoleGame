namespace SnakeGame.Factories
{
    using System;

    using SnakeGame.Factories.Contracts;
    using SnakeGame.GameObjects.Contracts;
    using SnakeGame.GameObjects.Foods;
    using SnakeGame.GameObjects.Foods.Contracts;

    public class FoodFactory : IFoodFactory
    {
        private Random random;
        private IBorder border;

        public FoodFactory(Random random, IBorder border)
        {
            this.random = random;
            this.border = border;
        }

        public IFood CreateFood()
        {
            int foodX = random.Next(border.TopLeftCorner.CoordinateX, border.DownRightCorner.CoordinateX);
            int foodY = random.Next(border.TopLeftCorner.CoordinateY, border.DownRightCorner.CoordinateY);

            //TODO generate random food
            IFood food = new BasicFood(foodX, foodY);

            return food;
        }
    }
}
