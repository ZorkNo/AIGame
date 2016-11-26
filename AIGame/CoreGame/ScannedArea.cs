using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AIGame.Interfaces;

namespace AIGame.CoreGame
{


    public class ScannedArea: Area, IScannedArea
    {
        public Tuple<int, int> SelfCoordinates { get; set; }

        public void Initilize()
        {
            InitilizeArea(1,1);
        }
        public void Initilize(IUnit unit, IMap map)
        {
            int xSize = 5;
            int ySize = 3;
            SelfCoordinates = new Tuple<int, int>(2,0);
            InitilizeArea(xSize, ySize);

            for (int x = 0; x < XSize ; x++)
            {
                for (int y = 0; y < YSize ; y++)
                {
                    var coor = ConvertMapCoordinates(unit.Facing, unit.Coordinates, new Tuple<int, int>(x - SelfCoordinates.Item1, y - SelfCoordinates.Item2));

                    //only include view cone
                    //if ((y == 1 && (x == 0 || x == 4)) || (y == 0 && x != 2))
                    //{
                    //    Terrain[x, y] = map.GetTerrain(coor);

                    //}
                    //else
                    //{
                    Terrain[x, y] = map.GetTerrain(coor);
                    List<IUnit> units = map.Units.FindAll(u => u.Coordinates.Equals(coor));
                    foreach (IUnit unitOnMap in units)
                    {
                        if(!unitOnMap.IsDead &&  unit.Name != unitOnMap.Name)
                            Targets.Add(new Target { Coordinates = new Tuple<int, int>(x, y), SelfCoordinates = SelfCoordinates });
                    }
                    //}

                }
            }
        }

        public Tuple<int, int> ConvertMapCoordinates(Direction facing, Tuple<int, int> unitCoordinates, Tuple<int,int> scanCoordinates)
        {
            Tuple<int, int> scan = RotateCoordinates(facing, scanCoordinates);

            int x = unitCoordinates.Item1 + scan.Item1;
            int y = unitCoordinates.Item2 + scan.Item2;
            
            return new Tuple<int, int>(x,y);
        }
        public static Tuple<int, int> RotateCoordinates(Direction facing, Tuple<int, int> coordinates)
        {
            return Helper.RotateCoordinates(facing, coordinates.Item1, coordinates.Item2);
        }
    }
}
