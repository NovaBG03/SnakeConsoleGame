namespace SnakeGame.GameObjects.Foods
{
    using SnakeGame.GameObjects.Contracts;
    using SnakeGame.GameObjects.Foods.Contracts;

    public abstract class Food : Point, IFood, IDrawable
    {
        protected Food(int coordinateX, int coordinateY, char symbol, int score)
            : base(coordinateX, coordinateY)
        {
            this.Symbol = symbol;
            this.Score = score;
        }

        public char Symbol { get; }

        public int Score { get; }

        public override string ToString()
        {
            return this.Symbol.ToString();
        }
    }
}
