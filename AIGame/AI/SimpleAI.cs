using System;
using System.Linq;
using AIGame.CoreGame;
using AIGame.CoreGame.Orders;
using Rotate = AIGame.CoreGame.Orders.Rotate;

namespace AIGame.AI
{
    public class SimpleAi : BaseAi
    {
        private int _turn=0;

        public SimpleAi(Random random) : base(random) { }

        public override IOrder GetOrder(Sensor sensor)
        {
            _turn++;
            //Hvis det en lige tur scan
            if (_turn % 2 == 0)
                return new SonorScan();

            if (sensor.ScannedArea.Targets.Any())
            {
                IOrder fireOrder = new FireTorpedo(sensor.ScannedArea.Targets.First().RelativeCoordinates);
                return fireOrder;
            }

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
