using System;
using System.Collections.Generic;
using System.Linq;

namespace AIGame.CoreGame
{
    public class Map
    {
        public Terrain[,] Terrain;
        public int XSize;
        public int YSize;
        private Random Rnd;


        public Map(int xSize, int ySize,Random rnd)
        {
            Rnd = rnd;
            XSize = xSize;
            YSize = ySize;
            GenerateMap(XSize,YSize);
        }

        public Terrain GetTerrain(Tuple<int,int> coordinates)
        {
            if (Helper.IsOutOfbounce(XSize, YSize, coordinates))
                return new Terrain(TerrainType.Edge);

            return Terrain[coordinates.Item1, coordinates.Item2];
        }
        private void GenerateMap(int xSize,int ySize)
        {
            Terrain = new Terrain[xSize+1, ySize+1];

            for (int x=0;x<= xSize;x++)
            {
                for (int y = 0; y <= ySize; y++)
                {
                    Terrain[x, y] = GenerateTerrain();
                }
            }
        }
        private Terrain GenerateTerrain()
        {
            int rndNumber = Rnd.Next(1, 100);

            if (rndNumber > 95)
                return new Terrain(TerrainType.Land);

            return new Terrain(TerrainType.Sea);
        }
        public Tuple<int, int> GetValidStartPosition(List<Unit> units)
        {
            Tuple<int, int> rndCoordinates;
            while (true)
            {
                int rndX = Rnd.Next(0, XSize);
                int rndY = Rnd.Next(0, YSize);
                rndCoordinates = new Tuple<int, int>(rndX, rndY);
                if(Terrain[rndX,rndY].Type != TerrainType.Land && !units.Any(u => u.Coordinates.Equals(rndCoordinates)))
                    break;
            }
            return rndCoordinates;
        }
    }
}
