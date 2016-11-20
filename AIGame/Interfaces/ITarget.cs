using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AIGame.CoreGame;

namespace AIGame.Interfaces
{
    public interface ITarget
    {
        Tuple<int, int> Coordinates { get; set; }
        Tuple<int, int> RelativeCoordinates { get; }
        Tuple<int, int> SelfCoordinates { get; set; }
    }
}
