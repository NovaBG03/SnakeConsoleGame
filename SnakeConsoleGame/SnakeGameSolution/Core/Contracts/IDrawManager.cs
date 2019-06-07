namespace SnakeGame.Core.Contracts
{
    using System.Collections.Generic;

    using SnakeGame.GameObjects.Contracts;

    public interface IDrawManager
    {
        void DrawPoint(IEnumerable<IPoint> points, char symbol);

        void DrawPoint(IPoint point, char symbol);

        void DrawPoint(int coordinateX, int coordinateY, char symbol);

        void ClearPoint(IPoint point);

        void DrawText(IPoint startingPoint, string text, int lineLength);
    }
}
