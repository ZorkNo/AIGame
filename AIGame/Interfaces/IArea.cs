using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AIGame.CoreGame;

namespace AIGame.Interfaces
{
    public interface IArea
    {
        Terrain[,] Terrain { get; set; }
        List<IUnit> Units { get; set; }
        int XSize { get; }
        int YSize { get; }
        Terrain GetTerrain(Tuple<int, int> coordinates);
        void ConsoleRenderArea();
        string RenderArea();
    }
}
