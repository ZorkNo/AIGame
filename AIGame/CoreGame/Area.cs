using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using AIGame.Interfaces;

namespace AIGame.CoreGame
{


    public class Area : IArea
    {
        public Terrain[,] Terrain { get; set; }
        public List<IUnit> Units { get; set; }

        public int XSize
        {
            get
            {
                if(Terrain == null )
                    return 0;
                
                return Terrain.GetUpperBound(0)+1;
            }
        }

        public int YSize
        {
            get
            {
                if (Terrain == null)
                    return 0;
                return Terrain.GetUpperBound(1)+1;
            }
        }

        public Terrain GetTerrain(Tuple<int, int> coordinates)
        {
            if (Helper.IsOutOfbounce(Terrain.GetUpperBound(0), Terrain.GetUpperBound(1), coordinates))
                return new Terrain(TerrainType.Edge);

             return Terrain[coordinates.Item1, coordinates.Item2];
        }

        internal void InitilizeArea(int XSize,int YSize)
        {
            Units = new List<IUnit>();
            Terrain = new Terrain[XSize, YSize];

            for (int x = 0; x < XSize; x++)
            {
                for (int y = 0; y < YSize; y++)
                {
                    Terrain[x, y] = new Terrain(TerrainType.Unknown);
                }
            }
        }
        public void ConsoleRenderArea()
        {

            for (int x = 0; x < XSize; x++)
            {
                string line = "";
                for (int y = 0; y < YSize; y++)
                {
                    line += RenderCoordinate(x, y);
                }
                Console.WriteLine(line);
            }
        }
        public string RenderArea()
        {
            string line = "";
            for (int x = 0; x < XSize; x++)
            {
                for (int y = 0; y < YSize; y++)
                {
                    line += RenderCoordinate(x, y);
                }
                line +=System.Environment.NewLine;
            }
            return line;
        }
        private string RenderCoordinate(int x, int y)
        {
            foreach (IUnit unit in Units)
            {
                if (unit.Coordinates.Equals(new Tuple<int, int>(x, y)))
                    return unit.Name;
            }
             return Terrain[x, y].Render();
        }
    }
}
