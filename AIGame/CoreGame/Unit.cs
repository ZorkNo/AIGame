using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AIGame.AI;
using AIGame.CoreGame.Orders;

namespace AIGame.CoreGame
{
    public class Unit
    {
        public string name;
        public Tuple<int, int> coordinates;
        public Direction facing;

        public int health = 100;
        public Side owner;
        public iAI ai;
        public Sensor sensor;


        public Unit(string _name, Side _owner, iAI _ai, Tuple<int, int> _coordinates)
        {
            name = _name;
            owner = _owner;
            ai = _ai;
            coordinates = _coordinates;
            facing = Direction.north;
            sensor = new Sensor();
            sensor.Health = health;
            sensor.Facing = facing;
        }
        public IOrder GetOrder()
        {
            return ai.GetOrder(sensor);
        }
   
        public void UpdateSensor(Map map)
        {
            sensor.Health = health;
            sensor.Facing = facing;
            
            

        }
    }
}
