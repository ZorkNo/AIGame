using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AIGame.CoreGame;
using AIGame.Interfaces;

namespace TestAIGame
{
    public class TestHelper
    {
        public static IMap GetMap()
        {
            int xSize = 5;
            int ySize = 3;
            Terrain[,] Terrain = new Terrain[xSize, ySize];

            for (int x = 0; x < xSize; x++)
            {
                for (int y = 0; y < ySize; y++)
                {
                    Terrain[x, y] = new Terrain(TerrainType.Sea);
                }
            }
            IMap map = new Map(xSize, ySize, new Random(), new List<IUnit>());
            map.Terrain = Terrain;
            return map;
        }
    }
}
