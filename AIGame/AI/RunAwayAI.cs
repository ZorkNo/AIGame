﻿using System;
using AIGame.CoreGame;
using AIGame.CoreGame.Orders;
using Rotate = AIGame.CoreGame.Orders.Rotate;

namespace AIGame.AI
{
    public class RunAwayAi : BaseAi
    {
        //private int _turn=0;
        private int health = 0;

        public RunAwayAi(Random random) : base(random) { }

        public override IOrder GetOrder(Sensor sensor)
        {
            //_turn++;

            bool hit = false;
            if (sensor.Health != health)
            {
                health = sensor.Health;
                hit = true;
            }

            if (_random.Next(1, 100) > 75 && hit == false)
                return new FireTorpedo(getCoordinates());

            if (sensor.Infront.Type == TerrainType.Land || sensor.Infront.Type == TerrainType.Edge)
                return Rotate();

            if (_random.Next(1, 100) > 65 && hit==false)
                return Rotate();

            return new Move();
  
        }

        private IOrder Rotate()
        {
            IOrder order;
            RotateDirection rotate = RotateDirection.Left;

            if (_random.Next(1, 100) > 85)
                rotate = RotateDirection.Right;

            order = new Rotate(rotate);
            return order;
        }
        private Tuple<int, int> getCoordinates()
        {
            int rndX = _random.Next(1, 6);
            int rndY = _random.Next(1, 4);
            return new Tuple<int, int>(rndX - 2, rndY);
        }
    }

}