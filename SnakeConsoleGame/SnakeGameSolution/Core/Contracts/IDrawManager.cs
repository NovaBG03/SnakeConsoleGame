namespace SnakeGame.Core.Contracts
{
    using System;
    using System.Collections.Generic;

    using SnakeGame.Core.Scenes.Buttons.Contracts;
    using SnakeGame.Enums;
    using SnakeGame.GameObjects.Contracts;

    //TODO Segregate interface
    public interface IDrawManager
    {
        void DrawPoint(IEnumerable<IPoint> points, char symbol);

        void DrawPoint(IPoint point, char symbol);

        void DrawPoint(int coordinateX, int coordinateY, char symbol);

        void ClearPoint(IPoint point);

        void DrawText(IPoint startingPoint, string text, int lineLength, Coordinate coordinate);

        void DrawText(int coordinateX, int coordinateY, string text, int lineLength, Coordinate coordinate);

        void DrawButton(IButton button, ConsoleColor color = ConsoleColor.Black);
    }
}
