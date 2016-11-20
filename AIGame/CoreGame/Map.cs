﻿using System;
using System.Collections.Generic;
using System.Linq;
using AIGame.Interfaces;

namespace AIGame.CoreGame
{
    public class Map: Area, IMap
    {
        private Random Rnd;

        public Map(int xSize, int ySize,Random rnd, List<IUnit> units)
        {
            Rnd = rnd;
            GenerateMap(xSize, ySize);
            Units = units;

            foreach (IUnit unit in Units)
            {
                unit.Coordinates = GetValidStartPosition(Units);
            }

        }
        private void GenerateMap(int xSize,int ySize)
        {
            //Der måske en mere interassant kort generering med Perlin noise

            Terrain = new Terrain[xSize, ySize];

            for (int x=0;x< xSize;x++)
            {
                for (int y = 0; y < ySize; y++)
                {
                    Terrain[x, y] = GenerateTerrain();
                }
            }
        }
        private Terrain GenerateTerrain()
        {
            int rndNumber = Rnd.Next(1, 100);

            //TODO change back to 95
            if (rndNumber > 50)
                return new Terrain(TerrainType.Land);

            return new Terrain(TerrainType.Sea);
        }
        public Tuple<int, int> GetValidStartPosition(List<IUnit> units)
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
