using System;
using System.Linq;
using AIGame.CoreGame;
using AIGame.CoreGame.Orders;
using Rotate = AIGame.CoreGame.Orders.Rotate;

namespace AIGame.AI
{
    public class SimpleAiType : IAiType
    {
        public IAi GetAi(Random rnd)
        {
            if (rnd == null)
                throw new NullReferenceException("rnd is null: No random generator");
            return new SimpleAi(rnd);
        }
        public string Name
        {
            get { return "SimpleAi"; }
        }
    }
    public class SimpleAi:IAi
    {
        private int _turn=0;
        public Sensor Sensor;
        public Random Rnd;

        public SimpleAi(Random rnd)
        {
            Rnd = rnd;
        }
        public IOrder GetOrder(Sensor sensor)
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

            if (Rnd.Next(1, 100) > 65)
                return Rotate();


            return new Move();
  
        }

        private IOrder Rotate()
        {
            IOrder order;
            RotateDirection rotate = RotateDirection.Left;

            if (Rnd.Next(1, 100) > 85)
                rotate = RotateDirection.Right;

            order = new Rotate(rotate);
            return order;
        }
    }

}
