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
        public List<ITarget> Targets { get; set; }
        

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

        public Area()
        {
            Units = new List<IUnit>();
            Targets = new List<ITarget>();
        }
        public Terrain GetTerrain(Tuple<int, int> coordinates)
        {
            if (Helper.IsOutOfbounce(XSize, YSize, coordinates))
                return new Terrain(TerrainType.Edge);

             return Terrain[coordinates.Item1, coordinates.Item2];
        }

        internal void InitilizeArea(int XSize,int YSize)
        {
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
            Console.WriteLine(RenderArea());
        }
        public string RenderArea()
        {
            string line = "";
            for (int x = XSize-1; x >=0; x--)
            {
                for (int y = 0; y < YSize ; y++)
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
            foreach (ITarget target in Targets)
            {
                if (target.Coordinates.Equals(new Tuple<int, int>(x, y)))
                    return "X";
            }
            return Terrain[x, y].Render();
        }
    }
}
