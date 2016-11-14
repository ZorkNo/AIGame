using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIGame.CoreGame
{
    public class Map
    {
        public Terrain[,] terrain;
        public int xSize;
        public int ySize;


        public Map(int _xSize, int _ySize)
        {
            Random rnd = new Random();
            xSize = _xSize;
            ySize = _ySize;
            generateMap(xSize,ySize, rnd);
        }
  
        private void generateMap(int xSize,int ySize, Random rnd)
        {
            terrain = new Terrain[xSize+1, ySize+1];

            for (int x=0;x<= xSize;x++)
            {
                for (int y = 0; y <= ySize; y++)
                {
                    terrain[x, y] = generateTerrain(rnd);
                }
            }
        }
        private Terrain generateTerrain(Random rnd)
        {
            int rndNumber = rnd.Next(1, 100);

            if (rndNumber > 95)
                return new Terrain(TerrainType.Land);

            return new Terrain(TerrainType.Sea);
        }
        public Tuple<int, int> GetValidStartPosition(List<Unit> units)
        {
            Random rnd = new Random();
            Tuple<int, int> rndCoordinates;
            while (1==1)
            {
                int rndX = rnd.Next(0, xSize);
                int rndY = rnd.Next(0, ySize);
                rndCoordinates = new Tuple<int, int>(rndX, rndY);
                if(terrain[rndX,rndY].Type != TerrainType.Land && !units.Any(u => u.coordinates.Equals(rndCoordinates)))
                    break;
            }
            return rndCoordinates;
        }
    }
}
