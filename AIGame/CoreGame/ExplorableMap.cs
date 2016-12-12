using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AIGame.Interfaces;

namespace AIGame.CoreGame
{


    public class ExplorableMap :Area
    {
        public Tuple<int, int> SelfCoordinates => Self.Coordinates;

        public new Terrain[,] Terrain { get; private set; }
        private Terrain[,] RealTerrain { get; set; }
        public bool[,] Explored { get; set; }
        private IUnit Self { get; set; }
        private int XSize;
        private int YSize;

        public void Initilize(IUnit unit, IMap map)
        {
            RealTerrain = map.Terrain;
            Self = unit;
            Explored = new bool[map.XSize, map.YSize];
            XSize = map.XSize;
            YSize = map.YSize;
            InitilizeArea(XSize, YSize);
        }
        internal void InitilizeArea( int xSize, int ySize)
        {
            YSize = ySize;
            XSize = xSize;
            Terrain = new Terrain[YSize, XSize];

            for (int x = 0; x < XSize; x++)
            {
                for (int y = 0; y < YSize; y++)
                {
                    Terrain[x, y] = new Terrain(TerrainType.Unknown);
                }
            }
        }
        public void Explorer(int x,int y)
        {
            if (Helper.IsOutOfbounce(XSize ,YSize,new Tuple<int, int>(x,y)))
                return;

            Explored[x,y] = true;
            Terrain[x,y] = RealTerrain[x,y];
        }
    }
}
