namespace SnakeGame.GameObjects
{
    using SnakeGame.GameObjects.Contracts;

    public class Point : IPoint
    {
        public Point(int coordinateX, int coordinateY)
        {
            CoordinateX = coordinateX;
            CoordinateY = coordinateY;
        }

        public int CoordinateX { get; set; }

        public int CoordinateY { get; set; }
    }
}
