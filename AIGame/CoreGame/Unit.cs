using System;
using System.Collections.Generic;
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
        public List<IOrder> LastOrders { get; set; }

        public ExplorableMap Map;

        public Unit(string name, Side owner, IAi ai)
        {
            Name = name;
            Owner = owner;
            Ai = ai;
            Facing = Direction.North;
            Health = 100;

            Sensor = new Sensor();
            Sensor.Health = Health;
            Sensor.Targets = new List<ITarget>();
            Coordinates = new Tuple<int, int>(-1,-1);
            LastOrders = new List<IOrder>();
        }
        public IOrder GetOrder()
        {
            return Ai.GetOrder(Sensor);
        }
        public string Render()
        {
            string message = "";

            message = string.Format("{0}{1}{2}{3}", message, "Unit:", Name, System.Environment.NewLine);
            message = string.Format("{0}{1}{2}{3}", message, "Side:", Owner, System.Environment.NewLine);
            message = string.Format("{0}{1}{2}{3}", message, "AI:", Ai.ToString(), System.Environment.NewLine);
            message = string.Format("{0}{1}{2}{3}", message, "Facing:", Facing, System.Environment.NewLine);
            message = string.Format("{0}{1}{2}{3}", message, "Health:",Health, System.Environment.NewLine);
            return message;
        }
        public void UpdateSensor(IMap map)
        {
            Update(this, map);

            if(Map==null)
            {
                Map = new ExplorableMap();
                Map.Initilize(this, map);
            }
            Map.Explorer(this.Coordinates.Item1, this.Coordinates.Item2);
            if (Sensor.HasScanned)
            {
                Sensor.HasScanned = false;        
                Scan(this,map);
            }
            else
            {
                NoScan(this, map);
            }
            Sensor.Terrain = Map.Terrain;
            Sensor.Targets = Map.Targets;
        }
        public void Update(IUnit unit, IMap map)
        {
            Health = unit.Health;
            Sensor.SelfFacing = unit.Facing;
            Sensor.SelfCoordinates = Coordinates; 

            Tuple<int, int> infrontXy = Helper.NewCoordinates(unit.Coordinates, unit.Facing);
            Sensor.Infront = map.GetTerrain(infrontXy);
            Sensor.IsUnitInfront = map.Units.Any(u => u.Coordinates.Equals(infrontXy));

            Sensor.Signals = new List<Signal>();
            foreach (ISignalOrigin signalOrigin in map.SignalOrigins)
            {
                Sensor.Signals.Add(signalOrigin.GetSignal(unit));
            }
        }

        public void Scan(IUnit unit, IMap map)
        {
            int xSize = 5;
            int ySize = 3;
            for (int x = 0; x < xSize; x++)
            {
                for (int y = 0; y < ySize; y++)
                {
                    ScanField(unit, map, x, y);
                }
            }

        }
        public void NoScan(IUnit unit, IMap map)
        {
            ScanField(unit, map, 2, 0);
            ScanField(unit, map, 2, 1);
        }

        private void ScanField(IUnit unit, IMap map, int x, int y)
        {
            Tuple<int, int> selfCoordinates = new Tuple<int, int>(2, 0);
            var coor = ConvertMapCoordinates(unit.Facing, unit.Coordinates,
                new Tuple<int, int>(x - selfCoordinates.Item1, y - selfCoordinates.Item2));

            //only include view cone
            if ((y == 1 && (x == 0 || x == 4)) || (y == 0 && x != 2))
            {
            }
            else
            {
                Map.Explorer(coor.Item1, coor.Item2);

                List<IUnit> units =
                    map.Units.FindAll(u => u.Coordinates.Item1 == coor.Item1 && u.Coordinates.Item2 == coor.Item2);
                Map.Targets = new List<ITarget>();
                foreach (IUnit unitOnMap in units)
                {
                    if (!unitOnMap.IsDead && unit.Name != unitOnMap.Name)
                        Map.Targets.Add(new Target(new Tuple<int, int>(x, y), unit.Coordinates, coor));
                }
            }
        }

        public Tuple<int, int> ConvertMapCoordinates(Direction facing, Tuple<int, int> unitCoordinates, Tuple<int, int> scanCoordinates)
        {
            Tuple<int, int> scan = Helper.RotateCoordinates(facing, scanCoordinates.Item1, scanCoordinates.Item2);

            int x = unitCoordinates.Item1 + scan.Item1;
            int y = unitCoordinates.Item2 + scan.Item2;

            return new Tuple<int, int>(x, y);
        }
    }
}
