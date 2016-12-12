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
        public Target(Tuple<int, int> coordinates, Tuple<int, int> selfCoordinates, Tuple<int, int> absoluteCoordinates)
        {
            this.coordinates = coordinates;
            this.selfCoordinates = selfCoordinates;
            AbsoluteCoordinates = absoluteCoordinates;
        }
        public Tuple<int, int> RelativeCoordinates
        {
            get
            {
                return new Tuple<int, int>(coordinates.Item1 - selfCoordinates.Item1,coordinates.Item2 - selfCoordinates.Item2 );    
            }
        }

        private Tuple<int, int> coordinates;

        private Tuple<int, int> selfCoordinates;
        public Tuple<int, int> AbsoluteCoordinates { get; private set; }
    }
}
