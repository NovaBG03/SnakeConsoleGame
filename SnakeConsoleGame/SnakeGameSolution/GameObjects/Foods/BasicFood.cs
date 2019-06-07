namespace SnakeGame.GameObjects.Foods
{
    public class BasicFood : Food
    {
        private const char FoodSymbol = '~';
        private const int FoodScore = 1;

        public BasicFood(int coordinateX, int coordinateY) 
            : base(coordinateX, coordinateY, FoodSymbol, FoodScore)
        {
        }
    }
}
