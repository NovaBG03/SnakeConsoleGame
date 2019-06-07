namespace SnakeGame.Factories
{
    using System;
    using System.Linq;
    using System.Reflection;

    using SnakeGame.Factories.Contracts;
    using SnakeGame.GameObjects.Contracts;
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

            var foodTypes = Assembly
                .GetExecutingAssembly()
                .GetTypes()
                .Where(t => typeof(IFood).IsAssignableFrom(t) 
                    && t.IsClass 
                    && !t.IsAbstract)
                .ToArray();

            int index = this.random.Next(0, foodTypes.Count());
            var foodType = foodTypes[index];

            var food = (IFood)Activator.CreateInstance(foodType, new object[] { foodX, foodY });

            return food;
        }
    }
}
