namespace SnakeGame.GameObjects.Foods
{
    public class SpecialFood : Food
    {
        private const char FoodSymbol = '+';
        private const int FoodScore = 2;

        public SpecialFood(int coordinateX, int coordinateY) 
            : base(coordinateX, coordinateY, FoodSymbol, FoodScore)
        {
        }
    }
}
