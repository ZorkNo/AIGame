using System;
using AIGame.CoreGame;
using AIGame.CoreGame.Orders;
using Rotate = AIGame.CoreGame.Orders.Rotate;

namespace AIGame.AI
{
    public class RunAwayAI : BaseAi
    {
        private int _health = 0;

        public RunAwayAI(Random random, params string[] args) : base(random, args) { }

        public override IOrder GetOrder(Sensor sensor)
        {
            //Move if hit
            bool hit = false;
            if (sensor.Health != _health)
            {
                _health = sensor.Health;
                hit = true;
            }
            
            if(hit && sensor.Infront.Type != TerrainType.Land && sensor.Infront.Type == TerrainType.Edge)
                return new Move();

            if (_random.Next(1, 100) > 50 && hit == false)
                return new FireTorpedo(getCoordinates());

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
        private Tuple<int, int> getCoordinates()
        {
            int rndX = _random.Next(1, 6);
            int rndY = _random.Next(1, 4);
            return new Tuple<int, int>(rndX - 2, rndY);
        }
    }

}
