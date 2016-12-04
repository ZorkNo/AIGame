using System;
using System.Linq;
using AIGame.CoreGame;
using AIGame.CoreGame.Orders;
using AIGame.Interfaces;
using Rotate = AIGame.CoreGame.Orders.Rotate;

namespace AIGame.AI
{

    public class SimplePlusAi : IAi
    {
        private int _turn=0;
        private Random _rnd;
        private int _fireCounter = 0;
        private Tuple<int, int> _target;

        public SimplePlusAi(Random rnd)
        {
            _rnd = rnd;
        }
        public IOrder GetOrder(Sensor sensor)
        {
            _turn++;

            if (sensor.ScannedArea.Targets.Any())
            {
                _fireCounter = 3;
                _target = sensor.ScannedArea.Targets.First().RelativeCoordinates;
            }

            if (_fireCounter > 0)
            {
                _fireCounter--;
                return new FireTorpedo(_target);
            }

            //Hvis det en lige tur scan
            if (_turn % 2 == 0)
                return new SonorScan();

            if (sensor.Infront.Type == TerrainType.Land || sensor.Infront.Type == TerrainType.Edge)
                return Rotate();

            if (_rnd.Next(1, 100) > 65)
                return Rotate();

            return new Move();
  
        }

        private IOrder Rotate()
        {
            IOrder order;
            RotateDirection rotate = RotateDirection.Left;

            if (_rnd.Next(1, 100) > 85)
                rotate = RotateDirection.Right;

            order = new Rotate(rotate);
            return order;
        }
    }

}
