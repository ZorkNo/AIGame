using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIGame.CoreGame
{
    public class ScannedArea: Area
    {
        public ScannedArea()
        {
            XSize = 1;
            YSize = 1;
            InitilizeArea();
        }

        

        public ScannedArea(Unit unit, Map map)
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
                    if ((y == 1 && (x == 0 || x == 4)) || (y == 0 && x != 2))
                    { 
                        Terrain[x, y] = new Terrain(TerrainType.Unknown);

                    }
                    else
                    {
                        Terrain[x, y] =
                        map.GetTerrain(ConvertMapCoordinates(unit.Facing, unit.Coordinates, new Tuple<int, int>(x - 3, y)));
                    }
                    
                    
                }
            }
        }

        private Tuple<int, int> ConvertMapCoordinates(Direction facing, Tuple<int, int> unitCoordinates, Tuple<int,int> scanCoordinates)
        {

            //TODO Somethings wrong with this scanning i fuckup =) 
            Tuple<int, int> scan = Helper.RotateCoordinates(facing, scanCoordinates);

            int x = unitCoordinates.Item1 - scan.Item1;
            int y = unitCoordinates.Item2 - scan.Item2;
            
            return new Tuple<int, int>(x,y);
        }

        
    }
}
