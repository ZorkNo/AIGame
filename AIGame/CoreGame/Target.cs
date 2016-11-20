using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AIGame.Interfaces;

namespace AIGame.CoreGame
{


    public class Target : ITarget
    {
        public Tuple<int, int> RelativeCoordinates
        {
            get
            {
                return new Tuple<int, int>(Coordinates.Item1 - SelfCoordinates.Item1,Coordinates.Item2 - SelfCoordinates.Item2 );    
            }
        }

        public Tuple<int, int> Coordinates { get; set; }

        public Tuple<int, int> SelfCoordinates { get; set; }
    }
}
