using System;
using System.Collections.Generic;
using System.Linq;
using AIGame.Interfaces;

namespace AIGame.CoreGame
{
    public class Sensor
    {
        public int Health;
        public Terrain Infront;
        public bool IsUnitInfront;
        public bool HasScanned;
        public Direction SelfFacing;
        public Tuple<int,int> SelfCoordinates = new Tuple<int, int>(0,0);
        public List<Signal> Signals = new List<Signal>();
        public Terrain[,] Terrain { get; set; }
        public List<ITarget> Targets { get; set; }

    }
}