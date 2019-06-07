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

        public void DrawPoint(IEnumerable<IPoint> points, char symbol)
        {
            foreach (var point in points)
            {
                this.DrawPoint(point, symbol);
            }
        }

        public void DrawPoint(IPoint point, char symbol)
        {
            int coordinateX = point.CoordinateX;
            int coordinateY = point.CoordinateY;

            Console.SetCursorPosition(coordinateX, coordinateY);
            Console.Write(symbol);

            SetDefaultCursorPosition();
        }

        public void DrawPoint(int coordinateX, int coordinateY, char symbol)
        {
            Console.SetCursorPosition(coordinateX, coordinateY);
            Console.Write(symbol);

            SetDefaultCursorPosition();
        }

        public void ClearPoint(IPoint point)
        {
            this.DrawPoint(point , ConsoleDrawManager.RemovedPointSymbol);
        }

        private static void SetDefaultCursorPosition()
        {
            Console.SetCursorPosition(ConsoleDrawManager.DefaultCursorPositionX, ConsoleDrawManager.DefaultCursorPositionY);
        }

        public void DrawText(IPoint startingPoint, string text, int lineLength)
        {
            int coordinateX = startingPoint.CoordinateX;
            int coordinateY = startingPoint.CoordinateY;

            string[] lines = text.Split(new[] { Environment.NewLine }, StringSplitOptions.None);

            foreach (var line in lines)
            {
                foreach (var ch in line)
                {
                    this.DrawPoint(coordinateX, coordinateY, ch);
                    coordinateX++;

                    if (coordinateX == startingPoint.CoordinateX + lineLength)
                    {
                        coordinateX = startingPoint.CoordinateX;
                        coordinateY++;
                    }
                }

                coordinateX = startingPoint.CoordinateX;
                coordinateY++;
            }

            SetDefaultCursorPosition();
        }
    }
}
