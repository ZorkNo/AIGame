using System;
using System.Collections.Generic;
using AIGame.Interfaces;

namespace AIGame.CoreGame.Orders
{
    public class SonorScan : IOrder
    {
        public void Execute(IUnit unit, IMap map)
        {
            //Måske er der mere elegant måde at gøre det her 
            unit.Sensor.HasScanned=true;
        }
        public bool IsValid(IUnit unit, IMap map)
        {
            return true;
        }

        public string Render()
        {
            return "";
        }

        public bool FreeOrder()
        {
            return false;
        }

        //public void Initilize(IUnit unit, IMap map)
        //{
        //    int xSize = 5;
        //    int ySize = 3;
        //    unit.Sensor.ScannedArea = new ScannedArea();
        //    unit.Sensor.ScannedArea.SelfCoordinates = new Tuple<int, int>(2, 0);
        //    unit.Sensor.ScannedArea.InitilizeArea(xSize, ySize);

        //    for (int x = 0; x < xSize; x++)
        //    {
        //        for (int y = 0; y < xSize; y++)
        //        {
        //            var coor = ConvertMapCoordinates(unit.Facing, unit.Coordinates, new Tuple<int, int>(x - unit.Sensor.ScannedArea.SelfCoordinates.Item1, y - unit.Sensor.ScannedArea.SelfCoordinates.Item2));

        //            //only include view cone
        //            //if ((y == 1 && (x == 0 || x == 4)) || (y == 0 && x != 2))
        //            //{
        //            //    Terrain[x, y] = map.GetTerrain(coor);

        //            //}
        //            //else
        //            //{
        //            unit.Sensor.ScannedArea.Terrain[x, y] = map.GetTerrain(coor);
        //            List<IUnit> units = map.Units.FindAll(u => u.Coordinates.Equals(coor));
        //            foreach (IUnit unitOnMap in units)
        //            {
        //                unit.Sensor.ScannedArea.Targets.Add(new Target { Coordinates = new Tuple<int, int>(x, y), SelfCoordinates = unit.Sensor.ScannedArea.SelfCoordinates });
        //            }
        //            //}

        //        }
        //    }
        //}

        //public Tuple<int, int> ConvertMapCoordinates(Direction facing, Tuple<int, int> unitCoordinates, Tuple<int, int> scanCoordinates)
        //{
        //    Tuple<int, int> scan = RotateCoordinates(facing, scanCoordinates);

        //    int x = unitCoordinates.Item1 + scan.Item1;
        //    int y = unitCoordinates.Item2 + scan.Item2;

        //    return new Tuple<int, int>(x, y);
        //}
        //public static Tuple<int, int> RotateCoordinates(Direction facing, Tuple<int, int> coordinates)
        //{
        //    return RotateCoordinates(facing, coordinates.Item1, coordinates.Item2);
        //}
        //private static Tuple<int, int> RotateCoordinates(Direction facing, int x, int y)
        //{
        //    int newX = 0;
        //    int newY = 0;
        //    switch (facing)
        //    {
        //        case Direction.West:
        //            newX = x;
        //            newY = y;
        //            break;
        //        case Direction.North:
        //            newX = y;
        //            newY = -x;
        //            break;
        //        case Direction.East:
        //            newX = -x;
        //            newY = -y;
        //            break;
        //        case Direction.South:
        //            newX = -y;
        //            newY = x;
        //            break;
        //    }
        //    return new Tuple<int, int>(newX, newY);
        //}
    }
}

