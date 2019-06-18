namespace SnakeGame.Core.Scenes.Buttons
{
    using System;
    using SnakeGame.Core.Contracts;
    using SnakeGame.Core.Scenes.Contracts;
    using SnakeGame.Factories.Contracts;
    using SnakeGame.GameObjects;
    using SnakeGame.GameObjects.Contracts;

    public class PlayButton : Button
    {
        private const string ButtonText = "*   Play    ";

        private readonly IPoint defaultSnakePoint;
        private readonly IDrawManager drawManager;
        private readonly IFoodFactory foodFactory;
        private readonly IBorder border;
        private readonly IScene pauseScene;
        private readonly IScene gameOverScene;

        public PlayButton(
            IPoint topLeftPoint, 
            IPoint defaultSnakePoint,
            IDrawManager drawManager, 
            IFoodFactory foodFactory, 
            IBorder border, 
            IScene pauseScene,
            IScene gameOverScene) 
            : base(topLeftPoint, ButtonText)
        {
            this.defaultSnakePoint = defaultSnakePoint;
            this.drawManager = drawManager;
            this.foodFactory = foodFactory;
            this.border = border;
            this.pauseScene = pauseScene;
            this.gameOverScene = gameOverScene;
        }

        public override void Push()
        {
            ISnake snake = new Snake(this.defaultSnakePoint);
            IScoreBoard scoreBoard = new ScoreBoard(new Point(this.border.DownRightCorner.CoordinateX + 3, this.border.TopLeftCorner.CoordinateY + 3), snake.Body.Count);
            IScene gameScene = 
                new GameScene(snake, this.drawManager, this.foodFactory, this.border, scoreBoard, this.pauseScene, this.gameOverScene);
            gameScene.Display();
        }
    }
}
