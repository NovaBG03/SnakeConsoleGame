namespace SnakeGame.GameObjects
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using SnakeGame.GameObjects.Contracts;
    using SnakeGame.GameObjects.Foods.Contracts;

    public class ScoreBoard : IScoreBoard
    {
        private const string SnakeLenthMessageFormat = "Snake length: {0}";
        private const string ScoreMessageFormat = "Score: {0}p.";
        private const string EatenFoodMessageFormat = "Snake has eaten: {0}";

        private readonly ICollection<IFood> eatenFoods;
        private int snakeLength;

        public ScoreBoard(IPoint startingPoint, int defaulSnakeLength)
        {
            this.StartingPoint = startingPoint;
            this.snakeLength = defaulSnakeLength;
            this.PlayerScore = default(int);
            this.eatenFoods = new List<IFood>();
        }

        public string InfoMessage => this.GenerateInfoMessage();

        public string FoodMessage => this.GenerateFoodMessage();

        private string GenerateFoodMessage()
        {
            return string.Format(EatenFoodMessageFormat, string.Join(" ", this.eatenFoods));
        }

        public IPoint StartingPoint { get; }

        public int PlayerScore { get; private set; }

        public void AddEatenFood(IFood food)
        {
            this.eatenFoods.Add(food);
            this.snakeLength++;
            this.PlayerScore += food.Score;
        }

        private string GenerateInfoMessage()
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendLine(string.Format(SnakeLenthMessageFormat, this.snakeLength))
                .AppendLine(string.Format(ScoreMessageFormat, this.PlayerScore));

            return builder.ToString().TrimEnd();
        }
    }
}
