using System;
using AIGame.CoreGame;
using AIGame.CoreGame.Orders;
using Rotate = AIGame.CoreGame.Orders.Rotate;

namespace AIGame.AI
{
    public class SimpleAiType : IAiType
    {
        public Random Rnd;
        public IAi GetAi()
        {
            return new SimpleAi(Rnd);
        }

        public void SetRandomGenerator(Random rnd)
        {
            if(rnd==null)
                throw new NullReferenceException("rnd is null: No random generator");
            Rnd =rnd;
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
