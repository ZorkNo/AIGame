using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AIGame.CoreGame;
using AIGame.CoreGame.Orders;

namespace AIGame.AI
{
    public class ScanNFireAiType : IAiType
    {
        public IAi GetAi(Random rnd)
        {
            return new ScanNFireAi(rnd);
        }
        public string Name
        {
            get { return "ScanNFireAi"; }
        }
    }
    public class ScanNFireAi : IAi
    {
        public Random Rnd;
        private int fireCounter = 0;
        private Tuple<int, int> target;
        private int _turn = 0;

        public ScanNFireAi(Random rnd)
        {
            Rnd = rnd;
        }
        public IOrder GetOrder(Sensor sensor)
        {
            if (sensor.ScannedArea.Targets.Any())
            {
                fireCounter = 3;
                target = sensor.ScannedArea.Targets.First().RelativeCoordinates;
            }

            if (fireCounter > 0)
            {
                fireCounter--;
                return new FireTorpedo(target);
            }

            if (_turn%2 == 0)
            {
                return new SonorScan();
            }
            else
            {
                return new Rotate(RotateDirection.Right);
            }
        }
        private Tuple<int, int> getCoordinates()
        {
            int rndX = Rnd.Next(1, 6);
            int rndY = Rnd.Next(1, 4);
            return new Tuple<int, int>(rndX - 2, rndY);
        }
    }
}
