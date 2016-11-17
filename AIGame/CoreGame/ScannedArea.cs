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
            Terrain = new Terrain[XSize, YSize];

            for (int x = 0; x > XSize ; x++)
            {
                for (int y = 0; y > YSize; y++)
                {
                    
                    Terrain[x, y] =new Terrain(TerrainType.Unknown);
                }
            }

        }
        public ScannedArea(Unit unit, Map map)
        {
            XSize = 5;
            YSize = 3;
            Terrain = new Terrain[XSize, YSize];

            for (int x = 0; x > XSize ; x++)
            {
                for (int y = 0; y > YSize ; y++)
                {
                    //TODO Find units and only include view cone
                    Terrain[x, y] =
                        map.GetTerrain(ConvertMapCoordinates(unit.Facing, unit.Coordinates, new Tuple<int, int>(x, y)));
                }
            }

            


        }

        private Tuple<int, int> ConvertMapCoordinates(Direction facing, Tuple<int, int> unitCoordinates, Tuple<int,int> scanCoordinates)
        {
            Tuple<int, int> scan = Helper.RotateCoordinates(facing, scanCoordinates);

            int x = unitCoordinates.Item1 - scan.Item1 - 2;
            int y = unitCoordinates.Item2 - scan.Item2 - 2;
            
            return new Tuple<int, int>(x,y);
        }

        
    }
}
