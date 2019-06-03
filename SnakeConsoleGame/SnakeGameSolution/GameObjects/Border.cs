﻿namespace SnakeGame.GameObjects
{
    using SnakeGame.GameObjects.Contracts;

    public class Border : IBorder, IDrawable
    {
        public Border(Point topLeftCorner, Point downRightCorner, char symbol)
        {
            this.Symbol = symbol;
            this.TopLeftCorner = topLeftCorner;
            this.DownRightCorner = downRightCorner;
        }

        public Point TopLeftCorner { get; }

        public Point DownRightCorner { get; }

        public char Symbol { get; }
    }
}