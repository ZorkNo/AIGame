using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AIGame.CoreGame;

namespace AIGame.Interfaces
{
    public interface ISignalOrigin
    {
        Tuple<int, int> OriginCoordinates { get; }
        Side Broadcaster { get; }
        Broadcast Broadcast { get; }
        SignalType Type { get; }
        int Age { get; set; }
        Signal GetSignal(IUnit unit);
    }


}
