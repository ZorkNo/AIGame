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
        public void Initilize()
        {
            InitilizeArea(1,1);
        }

        

        public void Initilize(IUnit unit, IMap map)
        {
            int xSize = 5;
            int ySize = 3;
            InitilizeArea(xSize, ySize);

            for (int x = 0; x < XSize ; x++)
            {
                for (int y = 0; y < YSize ; y++)
                {
                    //TODO Find units 

                    //only include view cone
                    //if ((y == 1 && (x == 0 || x == 4)) || (y == 0 && x != 2))
                    //{
                    //    var coor = ConvertMapCoordinates(unit.Facing, unit.Coordinates, new Tuple<int, int>(x - 2, y));
                    //    Terrain[x, y] = map.GetTerrain(coor);

                    //}
                    //else
                    //{
                        var coor = ConvertMapCoordinates(unit.Facing, unit.Coordinates, new Tuple<int, int>(x - 2, y));
                        Terrain[x, y] = map.GetTerrain(coor);
                    //}


                }
            }
        }

        public Tuple<int, int> ConvertMapCoordinates(Direction facing, Tuple<int, int> unitCoordinates, Tuple<int,int> scanCoordinates)
        {

            //TODO Somethings wrong with it this scanning is fucked up =) 
            Tuple<int, int> scan = RotateCoordinates(facing, scanCoordinates);

            int x = unitCoordinates.Item1 + scan.Item1;
            int y = unitCoordinates.Item2 + scan.Item2;
            
            return new Tuple<int, int>(x,y);
        }
        public static Tuple<int, int> RotateCoordinates(Direction facing, Tuple<int, int> coordinates)
        {
            return RotateCoordinates(facing, coordinates.Item1, coordinates.Item2);
        }
        private static Tuple<int, int> RotateCoordinates(Direction facing, int x, int y)
        {
            switch (facing)
            {
                case Direction.North:
                    break;
                case Direction.West:
                    x = -y;
                    y = x;
                    break;
                case Direction.South:
                    x = -x;
                    y = -y;
                    break;
                case Direction.East:
                    x = y;
                    y = -x;
                    break;
            }
            return new Tuple<int, int>(x, y);
        }

    }
}
