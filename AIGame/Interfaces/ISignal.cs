using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AIGame.CoreGame;

namespace AIGame.Interfaces
{
    public interface ISignal
    {
        DirectionPrecise Direction { get; }
        Broadcast Broadcast { get; }
        SignalType Type { get; }
    }


}
