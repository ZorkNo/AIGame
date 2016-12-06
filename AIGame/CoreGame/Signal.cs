using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AIGame.Interfaces;

namespace AIGame.CoreGame
{
    public class Signal : ISignal
    {
        public DirectionPrecise Direction { get; }
        public Broadcast Broadcast { get; }
        public SignalType Type { get; }

        public Signal(DirectionPrecise direction, Broadcast broadcast, SignalType type)
        {
            Direction = direction;
            Broadcast = broadcast;
            Type = type;
        }
    }
}
