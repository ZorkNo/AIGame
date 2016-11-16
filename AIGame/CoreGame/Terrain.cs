
namespace AIGame.CoreGame
{
    public class Terrain
    {
        public TerrainType Type;

        public Terrain (TerrainType type)
        {
            Type = type;
        }
        public string Render()
        {
            switch (Type)
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
        Unknown,
        Sea,
        Land,
        Edge
    }
}
