namespace SnakeGame.GameObjects.Contracts
{
    using System.Collections.Generic;

    using SnakeGame.Enums;

    public interface ISnake
    {
        Direction CurrentDirection { get; set; }

        IReadOnlyCollection<IPoint> Body { get; }

        IPoint CurrentHead { get; }

        IPoint NextHead { get; }

        IPoint Tale { get; }

        void AddNextHead();

        void RemoveOldTale();
    }
}
