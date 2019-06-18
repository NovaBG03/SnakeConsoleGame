namespace SnakeGame.Core
{
    using System;
    using System.Collections.Generic;

    using SnakeGame.Core.Contracts;
    using SnakeGame.Core.Scenes.Buttons.Contracts;
    using SnakeGame.Enums;
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

        public void DrawButton(IButton button, ConsoleColor backgroundColor = ConsoleColor.Black)
        {
            int coordinateX = button.TopLeftPoint.CoordinateX - button.Text.Length / 2;
            int coordinateY = button.TopLeftPoint.CoordinateY;
            int textLength = button.Text.Length;

            Console.ForegroundColor = backgroundColor;
            this.DrawText(coordinateX, coordinateY, button.Text, textLength, Coordinate.X);

            SetDefaultCursorPosition();

            this.DrawText(coordinateX - 1, coordinateY - 1, new string('|', 3), 3, Coordinate.Y);
            this.DrawText(coordinateX + textLength, coordinateY - 1, new string('|', 3), 3, Coordinate.Y);
            this.DrawText(coordinateX, coordinateY - 1, new string('-', textLength), textLength, Coordinate.X);
            this.DrawText(coordinateX, coordinateY + 1, new string('-', textLength), textLength, Coordinate.X);

            SetDefaultCursorPosition();
        }

        private static void SetDefaultCursorPosition()
        {
            Console.ForegroundColor = ConsoleColor.Black;
            Console.BackgroundColor = ConsoleColor.White;
            Console.SetCursorPosition(ConsoleDrawManager.DefaultCursorPositionX, ConsoleDrawManager.DefaultCursorPositionY);
        }

        public void DrawText(IPoint startingPoint, string text, int lineLength, Coordinate coordinate)
        {
            int coordinateX = startingPoint.CoordinateX;
            int coordinateY = startingPoint.CoordinateY;

            this.DrawText(coordinateX, coordinateY, text, lineLength, coordinate);
        }

        public void DrawText(int coordinateX, int coordinateY, string text, int lineLength, Coordinate coordinate)
        {
            string[] lines = text.Split(new[] { Environment.NewLine, "\n" }, StringSplitOptions.None);

            switch (coordinate)
            {
                case Coordinate.X:
                    int startingCoordinateX = coordinateX;

                    foreach (var line in lines)
                    {
                        foreach (var ch in line)
                        {
                            this.DrawPoint(coordinateX, coordinateY, ch);
                            coordinateX++;

                            if (coordinateX == startingCoordinateX + lineLength)
                            {
                                coordinateX = startingCoordinateX;
                                coordinateY++;
                            }
                        }

                        coordinateX = startingCoordinateX;
                        coordinateY++;
                    }
                    break;

                case Coordinate.Y:
                    int startingCoordinateY = coordinateY;

                    foreach (var line in lines)
                    {
                        foreach (var ch in line)
                        {
                            this.DrawPoint(coordinateX, coordinateY, ch);
                            coordinateY++;

                            if (coordinateY == startingCoordinateY + lineLength)
                            {
                                coordinateY = startingCoordinateY;
                                coordinateX++;
                            }
                        }

                        coordinateY = startingCoordinateY;
                        coordinateX++;
                    }
                    break;

            }

            SetDefaultCursorPosition();
        }
    }
}
