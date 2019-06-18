namespace SnakeGame.Core.Scenes.Buttons.Contracts
{
    using SnakeGame.GameObjects.Contracts;

    public interface IButton
    {
        IPoint TopLeftPoint { get; }

        string Text { get; }

        void Push();
    }
}
