using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIGame.CoreGame
{
    public class Terrain
    {
        public TerrainType type;

        public Terrain (TerrainType type1)
        {
            type = type1;
        }
        public string Render()
        {
            switch (type)
            {
                case TerrainType.Land:
                    return ".";
                case TerrainType.Sea:
                    return "~";
                case TerrainType.Edge:
                    return "#";
                default:
                    return "~";

            }
               
        }
        
    }
    public enum TerrainType
    {
        Sea,
        Land,
        Edge

    }
}
