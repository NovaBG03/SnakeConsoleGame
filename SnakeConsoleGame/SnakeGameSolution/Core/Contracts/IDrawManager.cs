namespace SnakeGame.Core.Contracts
{
    using System.Collections.Generic;

    using SnakeGame.GameObjects.Contracts;

    public interface IDrawManager
    {
        void Draw(IEnumerable<IPoint> points, char symbol);

        void Draw(IPoint point, char symbol);

        void ClearPoint(IPoint point);
    }
}
