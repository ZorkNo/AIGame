using System;
using System.Linq;
using AIGame.CoreGame;
using AIGame.CoreGame.Orders;
using AIGame.Interfaces;
using Rotate = AIGame.CoreGame.Orders.Rotate;

namespace AIGame.AI
{

    public class NewMutableAI : BaseAi
    {
        private Tuple<int, int> _target;
        private int _health = 0;
        public readonly NewMutableParameters _mutableParameters;
        private int _fireCounter = 0;

        public NewMutableAI(Random random, params string[] args) : base(random, args)
        {
            _mutableParameters = new NewMutableParameters(args);
        }

        public override IOrder GetOrder(Sensor sensor)
        {

            //Move if hit
            bool hit = false;
            if (sensor.Health != _health)
            {
                _health = sensor.Health;
                hit = true;
            }

            if (_random.Next(1, 100) < _mutableParameters.RunAwayChance && hit &&
                sensor.Infront.Type != TerrainType.Land && sensor.Infront.Type == TerrainType.Edge)
            {
                _fireCounter = 0;
                return new Move();
            }

            if (sensor.Targets.Any() || sensor.IsUnitInfront)
            {
                _fireCounter = _mutableParameters.FireCounter;
                _target = sensor.IsUnitInfront? new Tuple<int, int>(0,1) : sensor.Targets.First().RelativeCoordinates;
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
            {
                _fireCounter = 0;
                return Rotate();
            }

            if (_random.Next(1, 100) < _mutableParameters.RotateChance)
            {
                _fireCounter = 0;
                return Rotate();
            }

            if (_random.Next(1, 100) < _mutableParameters.MoveChange)
            {
                _fireCounter = 0;
                return new Move();
            }

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

    public class NewMutableParameters
    {
        public NewMutableParameters(params string[] args)
        {

            int outVal = 0;
            RunAwayChance = 53;
            FireChance = 94;
            ScanChance = 10;
            RotateChance = 0;
            MoveChange = 98;
            FireCounter = 4;

            if (args == null || args.Length != 6)
                return;
            if (int.TryParse(args[0], out outVal))
                RunAwayChance = outVal;
            if (int.TryParse(args[1], out outVal))
                FireChance = outVal;
            if (int.TryParse(args[2], out outVal))
                ScanChance = outVal;
            if (int.TryParse(args[3], out outVal))
                RotateChance = outVal;
            if (int.TryParse(args[4], out outVal))
                MoveChange = outVal;
            if (int.TryParse(args[5], out outVal))
                FireCounter = outVal;
        }
        public int RunAwayChance { get; set; }
        public int FireChance { get; set; }
        public int ScanChance { get; set; }
        public int RotateChance { get; set; }
        public int MoveChange { get; set; }
        public int FireCounter { get; set; }
        public  static String[] Mutate(String[] args, Random rnd)
        {
            string[] mutations = RandomGens(rnd);
            string[] mutant = new string[6];
            args.CopyTo(mutant, 0);
            int mutationNumber = rnd.Next(0, 6);
            mutant[mutationNumber] = mutations[mutationNumber];

            return mutant;
        }
        public static String[] SmallMutate(String[] args, Random rnd)
        {
            string[] mutations = RandomGens(rnd);
            string[] mutant = new string[6];
            args.CopyTo(mutant, 0);
            int mutationNumber = rnd.Next(0, 6);
            mutant[mutationNumber] = mutations[mutationNumber];

            return mutant;
        }
        public static String[] RandomGens(Random rnd)
        {

            return new string[]
            {
                rnd.Next(0, 100).ToString(), rnd.Next(0, 100).ToString(), rnd.Next(0, 100).ToString(),
                rnd.Next(0, 100).ToString(), rnd.Next(0, 100).ToString(), rnd.Next(1, 6).ToString()
            };
        }
    }
}
