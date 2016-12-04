using System;
using System.Linq;
using AIGame.CoreGame;
using AIGame.CoreGame.Orders;
using AIGame.Interfaces;
using Rotate = AIGame.CoreGame.Orders.Rotate;

namespace AIGame.AI
{

    public class MutableAi : BaseAi
    {
        private Tuple<int, int> _target;
        private MutableParameters mutableParameters;
        private int _fireCounter = 0;

        public MutableAi(Random random, string[] args) : base(random, args)
        {
            mutableParameters = new MutableParameters(args);
        }

        public override IOrder GetOrder(Sensor sensor)
        {

            if (sensor.ScannedArea.Targets.Any())
            {
                _fireCounter = mutableParameters.FireCounter;
                _target = sensor.ScannedArea.Targets.First().RelativeCoordinates;
            }

            if (_random.Next(1, 100) > mutableParameters.FireChance && _fireCounter > 0)
            {
                _fireCounter--;
                return new FireTorpedo(_target);
            }

            //Hvis det en lige tur scan
            if (_random.Next(1, 100) > mutableParameters.ScanChance)
                return new SonorScan();

            if (sensor.Infront.Type == TerrainType.Land || sensor.Infront.Type == TerrainType.Edge)
                return Rotate();

            if (_random.Next(1, 100) > mutableParameters.RotateChance)
                return Rotate();

            if (_random.Next(1, 100) > mutableParameters.MoveChange)
                return new Move();
  
            return new DoNothing();
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

    internal class MutableParameters
    {
        public MutableParameters(string[] args)
        {
            //100
            FireChance = int.Parse(args[0]);
            //50
            ScanChance = int.Parse(args[1]);
            //65
            RotateChance = int.Parse(args[2]);
            //100
            MoveChange = int.Parse(args[3]);
            //3
            FireCounter = int.Parse(args[4]);
        }
        public int FireChance { get; set; }
        public int ScanChance { get; set; }
        public int RotateChance { get; set; }
        public int MoveChange { get; set; }
        public int FireCounter { get; set; }
    }
}
