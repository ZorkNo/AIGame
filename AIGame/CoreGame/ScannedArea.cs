﻿//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using AIGame.Interfaces;

//namespace AIGame.CoreGame
//{


//    public class ScannedArea: Area, IScannedArea
//    {
//        public Tuple<int, int> SelfCoordinates { get; set; }

//        public void Initilize()
//        {
//            InitilizeArea(1,1);
//        }
//        public void Initilize(IUnit unit, IMap map)
//        {
//            int xSize = 5;
//            int ySize = 3;
//            SelfCoordinates = new Tuple<int, int>(2,0);
//            InitilizeArea(xSize, ySize);

//            for (int x = 0; x < XSize ; x++)
//            {
//                for (int y = 0; y < YSize ; y++)
//                {
//                    var coor = ConvertMapCoordinates(unit.Facing, unit.Coordinates, new Tuple<int, int>(x - SelfCoordinates.Item1, y - SelfCoordinates.Item2));

//                    //only include view cone
//                    //if ((y == 1 && (x == 0 || x == 4)) || (y == 0 && x != 2))
//                    //{
//                    //    Terrain[x, y] = map.GetTerrain(coor);

//                    //}
//                    //else
//                    //{
//                    Terrain[x, y] = map.GetTerrain(coor);
//                    List<IUnit> units = map.Units.FindAll(u => u.Coordinates.Item1 == coor.Item1 && u.Coordinates.Item2 == coor.Item2);
//                    //foreach (IUnit unitOnMap in units)
//                    //{
//                    //    if(!unitOnMap.IsDead &&  unit.Name != unitOnMap.Name)
//                    //        Targets.Add(new Target { Coordinates = new Tuple<int, int>(x, y), SelfCoordinates = SelfCoordinates });
//                    //}
//                    //}

//                }
//            }
//        }

        
//    }
//}
