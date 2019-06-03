namespace SnakeGame.Core
{
    using System;
    using System.Collections.Generic;

    using SnakeGame.Core.Contracts;
    using SnakeGame.GameObjects.Contracts;

    public class ConsoleDrawManager : IDrawManager
    {
        private const char RemovedPointSymbol = ' ';
        private const int DefaultCursorPositionX = 0;
        private const int DefaultCursorPositionY = 0;

        public ConsoleDrawManager()
        {

        }

        public void Draw(IEnumerable<IPoint> points, char symbol)
        {
            foreach (var point in points)
            {
                this.Draw(point, symbol);
            }
        }

        public void Draw(IPoint point, char symbol)
        {
            int coordinateX = point.CoordinateX;
            int coordinateY = point.CoordinateY;

            Console.SetCursorPosition(coordinateX, coordinateY);
            Console.Write(symbol);

            SetDefaultCursorPosition();
        }

        public void ClearPoint(IPoint point)
        {
            this.Draw(point , ConsoleDrawManager.RemovedPointSymbol);
        }

        private static void SetDefaultCursorPosition()
        {
            Console.SetCursorPosition(ConsoleDrawManager.DefaultCursorPositionX, ConsoleDrawManager.DefaultCursorPositionY);
        }
    }
}
