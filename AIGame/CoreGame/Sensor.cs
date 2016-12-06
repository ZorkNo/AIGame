using System.Collections.Generic;

namespace AIGame.CoreGame
{
    public class Sensor
    {
        public int Health;
        public Terrain Infront;
        public bool IsUnitInfront;
        public bool HasScanned;
        public ScannedArea ScannedArea;
        public Direction SelfFacing;
        public List<Signal> Signals = new List<Signal>();
    }
}