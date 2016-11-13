using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AIGame.AI;

namespace AIGame.CoreGame
{
    public class Unit
    {
        private int health = 100;
        public string name;
        private side owner;
        public Tuple<int, int> coordinates;
        public Direction facing;
        private iAI ai;
        private UnitSensor unitSensor;


        public Unit(string _name, side _owner, iAI _ai, Tuple<int, int> _coordinates)
        {
            name = _name;
            owner = _owner;
            ai = _ai;
            coordinates = _coordinates;
        }
        public iOrder getOrder()
        {
            return ai.getOrder(unitSensor);
        }
        public void UpdateUnitSensor(Map map)
        {
            unitSensor.health = health;
            unitSensor.facing = facing;

        }
    }
    public class UnitSensor
    {
        public int health;
        public Direction facing;
        public Terrain infront;
    }
}
