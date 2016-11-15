using System;
using AIGame.AI;
using AIGame.CoreGame.Orders;

namespace AIGame.CoreGame
{
    public class Unit
    {
        public string Name;
        public Tuple<int, int> Coordinates;
        public Direction Facing;

        public int Health = 100;
        public Side Owner;
        public IAi Ai;
        public Sensor Sensor;


        public Unit(string name, Side owner, IAi ai)
        {
            Name = name;
            Owner = owner;
            Ai = ai;
            Facing = Direction.North;
            Sensor = new Sensor();
            Sensor.Health = Health;
            Coordinates = new Tuple<int, int>(-1,-1);
        }
        public IOrder GetOrder()
        {
            return Ai.GetOrder(Sensor);
        }
   
        public void UpdateSensor(Map map)
        {
            Sensor.Health = Health;

            Tuple<int, int> infrontXy= Helper.NewCoordinates(Coordinates, Facing);
            Sensor.Infront = map.GetTerrain(infrontXy);



        }
         
    }
}
