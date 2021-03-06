﻿using System;
using System.Linq;
using AIGame.CoreGame;
using AIGame.CoreGame.Orders;
using AIGame.Interfaces;
using Rotate = AIGame.CoreGame.Orders.Rotate;

namespace AIGame.AI
{

    public class SimplePlusAI : BaseAi
    {
        private int _turn=0;
        private int _fireCounter = 0;
        private Tuple<int, int> _target;

        public SimplePlusAI(Random random, params string[] args) : base(random, args) { }

        public override IOrder GetOrder(Sensor sensor)
        {
            _turn++;

            if (sensor.Targets.Any())
            {
                _fireCounter = 3;
                _target = sensor.Targets.First().RelativeCoordinates;
            }

            if (_fireCounter > 0)
            {
                _fireCounter--;
                return new FireTorpedo(_target, CoordinateType.Relative);
            }

            //Hvis det en lige tur scan
            if (_turn % 2 == 0)
                return new SonorScan();

            if (sensor.Infront.Type == TerrainType.Land || sensor.Infront.Type == TerrainType.Edge)
                return Rotate();

            if (_random.Next(1, 100) > 65)
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
    }

}
