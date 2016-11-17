using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIGame.CoreGame
{
    public class Area
    {
        public Terrain[,] Terrain;
        public List<Unit> Units=new List<Unit>();
        public int XSize;
        public int YSize;
        public Terrain GetTerrain(Tuple<int, int> coordinates)
        {
            if (Helper.IsOutOfbounce(XSize, YSize, coordinates))
                return new Terrain(TerrainType.Edge);

            return Terrain[coordinates.Item1, coordinates.Item2];
        }
        public void RenderArea()
        {

            for (int x = 0; x <= XSize; x++)
            {
                string line = "";
                for (int y = 0; y <= YSize; y++)
                {
                    line += RenderCoordinate(x, y);
                }
                Console.WriteLine(line);
            }
        }
        private string RenderCoordinate(int x, int y)
        {
            foreach (Unit unit in Units)
            {
                if (unit.Coordinates.Equals(new Tuple<int, int>(x, y)))
                    return unit.Name;
            }
            return Terrain[x, y].Render();
        }
    }
}
