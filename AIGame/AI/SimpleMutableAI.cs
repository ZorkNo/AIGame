using System;
using System.Linq;
using AIGame.CoreGame;
using AIGame.CoreGame.Orders;
using AIGame.Interfaces;
using Rotate = AIGame.CoreGame.Orders.Rotate;

namespace AIGame.AI
{

    public class SimpleMutableAI : BaseAi
    {
        private Tuple<int, int> _target;
        private readonly MutableParameters _mutableParameters;
        private int _fireCounter = 0;

        public SimpleMutableAI(Random random, params string[] args) : base(random, args)
        {
            _mutableParameters = new MutableParameters(args);
        }

        public override IOrder GetOrder(Sensor sensor)
        {

            if (sensor.Targets.Any())
            {
                _fireCounter = _mutableParameters.FireCounter;
                _target = sensor.Targets.First().RelativeCoordinates;
            }

            if (_random.Next(1, 100) < _mutableParameters.FireChance && _fireCounter > 0)
            {
                _fireCounter--;
                return new FireTorpedo(_target, CoordinateType.Relative);
            }

            //Hvis det en lige tur scan
            if (_random.Next(1, 100) < _mutableParameters.ScanChance)
                return new SonorScan();

            if (sensor.Infront.Type == TerrainType.Land || sensor.Infront.Type == TerrainType.Edge)
                return Rotate();

            if (_random.Next(1, 100) < _mutableParameters.RotateChance)
                return Rotate();

            if (_random.Next(1, 100) < _mutableParameters.MoveChange)
                return new Move();

            return new FireTorpedo(getCoordinates(), CoordinateType.Relative);
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

    public class MutableParameters
    {
        public MutableParameters(params string[] args)
        {
            
            int outVal = 0;
            FireChance = 100;
            ScanChance = 86;
            RotateChance = 17;
            MoveChange = 2;
            FireCounter = 4;

            if (args == null || args.Length!=5)
                return;
            if (int.TryParse(args[0],out outVal))
                FireChance = int.Parse(args[0]);
            if (int.TryParse(args[1], out outVal))
                ScanChance = int.Parse(args[1]);
            if (int.TryParse(args[2], out outVal))
                RotateChance = int.Parse(args[2]);
            if (int.TryParse(args[3], out outVal))
                MoveChange = int.Parse(args[3]);
            if (int.TryParse(args[4], out outVal))
                FireCounter = int.Parse(args[4]);
        }
        public int FireChance { get; set; }
        public int ScanChance { get; set; }
        public int RotateChance { get; set; }
        public int MoveChange { get; set; }
        public int FireCounter { get; set; }
        private static String[] Mutate(String[] args, Random rnd)
        {
            string[] mutations = RandomGens(rnd);
            string[] mutant = new string[5];
            args.CopyTo(mutant, 0);
            int mutationNumber = rnd.Next(0, 5);
            mutant[mutationNumber] = mutations[mutationNumber];

            return mutant;
        }
        private static String[] RandomGens(Random rnd)
        {

            return new string[]
            {
                rnd.Next(0, 100).ToString(), rnd.Next(0, 100).ToString(), rnd.Next(0, 100).ToString(),
                rnd.Next(0, 100).ToString(), rnd.Next(0, 5).ToString()
            };
        }
    }
}
