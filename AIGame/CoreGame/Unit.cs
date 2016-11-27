using System;
using System.Linq;
using AIGame.AI;
using AIGame.CoreGame.Orders;
using AIGame.Interfaces;

namespace AIGame.CoreGame
{


    public class Unit : IUnit
    {
        public string Name { get; set; }
        public Tuple<int, int> Coordinates { get; set; }
        public Direction Facing { get; set; }

        public int Health { get; set; }

        public bool IsDead => Health <= 0;

        public Side Owner { get; set; }
        public IAi Ai { get; set; }
        public Sensor Sensor { get; set; }
        public IOrder LastOrder { get; set; }

        public Unit(string name, Side owner, IAi ai)
        {
            Name = name;
            Owner = owner;
            Ai = ai;
            Facing = Direction.North;
            Health = 100;

            Sensor = new Sensor();
            Sensor.Health = Health;
            Coordinates = new Tuple<int, int>(-1,-1);
        }
        public IOrder GetOrder()
        {
            return Ai.GetOrder(Sensor);
        }

        public string Render()
        {
            string message = "";

            message = string.Format("{0}{1}{2}{3}", message, "Unit:", Name, System.Environment.NewLine);
            message = string.Format("{0}{1}{2}{3}", message, "Facing:", Facing, System.Environment.NewLine);
            message = string.Format("{0}{1}{2},{3}{4}", message, "Coordinates:", Coordinates.Item1 ,Coordinates.Item2, System.Environment.NewLine);
            return message;
        }
        public void UpdateSensor(IMap map)
        {
            Sensor.Health = Health;

            Tuple<int, int> infrontXy= Helper.NewCoordinates(Coordinates, Facing);
            Sensor.Infront = map.GetTerrain(infrontXy);
            Sensor.IsUnitInfront = map.Units.Any(u => u.Coordinates.Equals(infrontXy));

            if (Sensor.HasScanned)
            {
                Sensor.HasScanned = false;
                Sensor.ScannedArea = new ScannedArea();
                Sensor.ScannedArea.Initilize(this,map);

                //TODO remove rendering
                //Console.WriteLine(Facing);
                //Sensor.ScannedArea.ConsoleRenderArea();
                
            }
            else
            {
                Sensor.ScannedArea = new ScannedArea();
                Sensor.ScannedArea.Initilize();
            }
        }
         
    }
}
