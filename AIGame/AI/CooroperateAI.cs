using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AIGame.CoreGame;
using AIGame.CoreGame.Orders;

namespace AIGame.AI
{
    public class CooroperateAI : BaseAi
    {
        private int _turn = 0;
        private int _fireCounter = 0;
        private Tuple<int, int> _target;
        private Tuple<int, int> _friendly;
        private bool _justBroadcasted = false;

        public CooroperateAI(Random random, params string[] args) : base(random, args) { }
        
        public override IOrder GetOrder(Sensor sensor)
        {
            

            if (sensor.Signals.Any(s => s.Direction != DirectionPrecise.OnTop && s.Broadcast.Type == BroadcastType.Encrypted
             && !s.Broadcast.Message.StartsWith("**")))
            {
                Signal signal =
                    sensor.Signals.First(
                        s => s.Direction != DirectionPrecise.OnTop && s.Broadcast.Type == BroadcastType.Encrypted 
                        && !s.Broadcast.Message.StartsWith("**" ));
                
                int x = int.Parse(  signal.Broadcast.Message.Split(':')[0]);
                int y = int.Parse(signal.Broadcast.Message.Split(':')[1]);
                _friendly= new Tuple<int, int>(x,y);
            }
            else
            {
                _friendly = null;
            }
            if (_justBroadcasted == false)
            {
                _turn++;
                _justBroadcasted = true;
                string message = string.Format("{0}:{1}",sensor.SelfCoordinates.Item1 ,sensor.SelfCoordinates.Item2 );
                return new BroadcastOrder(new Broadcast {Message =message,Type = BroadcastType.Encrypted });
            }
            else
            {
                
                _justBroadcasted = false;
            }

            if (sensor.Targets.Any() &&
                (_friendly == null || sensor.Targets.Any(t => t.AbsoluteCoordinates.Item1 !=_friendly.Item1 && t.AbsoluteCoordinates.Item2!=_friendly.Item2)))
            {
                _fireCounter = 3;
                _target = sensor.Targets.First().RelativeCoordinates;
            }

            if (_fireCounter > 0)
            {
                _fireCounter--;
                return new FireTorpedo(_target,CoordinateType.Relative);
            }

            //Hvis det en lige tur scan
            if (_turn % 2 == 0)
                return new SonorScan();

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
