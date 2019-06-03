namespace SnakeGame.GameObjects.Contracts
{
    using System.Collections.Generic;

    using SnakeGame.Enums;

    public interface ISnake
    {
        Direction CurrentDirection { get; set; }

        IReadOnlyCollection<Point> Body { get; }

        Point CurrentHead { get; }

        Point NextHead { get; }

        Point Tale { get; }

        void AddNextHead();

        void RemoveOldTale();
    }
}
