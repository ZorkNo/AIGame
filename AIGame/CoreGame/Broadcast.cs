using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIGame.CoreGame
{
    public class Broadcast
    {
        public BroadcastType Type;
        public string Message;
    }

    public enum BroadcastType
    {
        Encrypted,
        Open
    }
}
