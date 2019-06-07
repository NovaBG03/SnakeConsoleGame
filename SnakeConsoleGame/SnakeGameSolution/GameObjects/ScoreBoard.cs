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
        private const string EatenFoodMessageFormat = "Snake have eaten: {0}";

        private readonly ICollection<IFood> eatenFoods;
        private int snakeLength;
        private int playerScore;

        public ScoreBoard(IPoint startingPoint, int defaulSnakeLength)
        {
            this.StartingPoint = startingPoint;
            this.snakeLength = defaulSnakeLength;
            this.playerScore = default(int);
            this.eatenFoods = new List<IFood>();
        }

        public string InfoMessage => this.GenerateMessage();

        public IPoint StartingPoint { get; }

        public void AddEatenFood(IFood food)
        {
            this.eatenFoods.Add(food);
            this.snakeLength++;
            this.playerScore += food.Score;
        }

        private string GenerateMessage()
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendLine(string.Format(SnakeLenthMessageFormat, this.snakeLength))
                .AppendLine(string.Format(ScoreMessageFormat, this.playerScore))
                .AppendLine(string.Format(EatenFoodMessageFormat, string.Join(" ", this.eatenFoods)));

            return builder.ToString().TrimEnd();
        }
    }
}
