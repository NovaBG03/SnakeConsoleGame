﻿namespace SnakeGame.GameObjects.Foods
{
    public class SuperFood : Food
    {
        private const char FoodSymbol = 'Ѫ';
        private const int FoodScore = 3;

        public SuperFood(int coordinateX, int coordinateY) 
            : base(coordinateX, coordinateY, FoodSymbol, FoodScore)
        {
        }
    }
}
