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
            XSize = 1;
            YSize = 1;
            InitilizeArea();
        }

        

        public void Initilize(IUnit unit, IMap map)
        {
            XSize = 5;
            YSize = 3;
            InitilizeArea();

            for (int x = 0; x < XSize ; x++)
            {
                for (int y = 0; y < YSize ; y++)
                {
                    //TODO Find units 

                    //only include view cone
                    //if ((y == 1 && (x == 0 || x == 4)) || (y == 0 && x != 2))
                    if(y == 0 && x == 2)
                    {
                        var coor = ConvertMapCoordinates(unit.Facing, unit.Coordinates, new Tuple<int, int>(x - 2, y));
                        Terrain[x, y] = map.GetTerrain(coor);

                    }
                    else
                    {
                        var coor = ConvertMapCoordinates(unit.Facing, unit.Coordinates, new Tuple<int, int>(x - 2, y));
                        Terrain[x, y] = map.GetTerrain(coor);
                    }


                }
            }
        }

        private Tuple<int, int> ConvertMapCoordinates(Direction facing, Tuple<int, int> unitCoordinates, Tuple<int,int> scanCoordinates)
        {

            //TODO Somethings wrong with this scanning is fucked up =) 
            Tuple<int, int> scan = Helper.RotateCoordinates(facing, scanCoordinates);

            int x = unitCoordinates.Item1 + scan.Item1;
            int y = unitCoordinates.Item2 + scan.Item2;
            
            return new Tuple<int, int>(x,y);
        }

        
    }
}
