using System;
using System.Linq;
using AIGame.CoreGame;
using AIGame.CoreGame.Orders;

namespace AIGame.AI
{
    public class ScanNFirePludAI : BaseAi
    {
        private int _fireCounter = 0;
        private Tuple<int, int> _target;
        private Tuple<int, int> _friendlies;
        private int _turn = 0;

        public ScanNFirePludAI(Random random, params string[] args) : base(random, args) { }

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

            if (_turn%2 == 0)
            {
                return new SonorScan();
            }
            else
            {
                return new Rotate(RotateDirection.Right);
            }
            
        }
        /* unused code atm, todo: remove or use
        private Tuple<int, int> getCoordinates()
        {
            int rndX = _random.Next(1, 6);
            int rndY = _random.Next(1, 4);
            return new Tuple<int, int>(rndX - 2, rndY);
        }
        */
    }
}
