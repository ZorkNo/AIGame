using System;
using AIGame.Interfaces;

namespace AIGame.CoreGame.Orders
{
    public class Move:IOrder
    {
        public void Execute(IUnit unit, IMap map)
        {
            unit.Coordinates  = Helper.NewCoordinates(unit.Coordinates, unit.Facing);
        }

        public bool IsValid(IUnit unit, IMap map)
        {
            Tuple<int, int> newCoordinates= Helper.NewCoordinates(unit.Coordinates,unit.Facing );

            bool isOutOfbounce = Helper.IsOutOfbounce(map.XSize, map.YSize, newCoordinates);
            //Is out of bounce
            if (isOutOfbounce)
            {
                Console.WriteLine(string.Format("{0}: is trying to move out of bounce", unit.Name)); 
                return false;
            }
            //on land
             if (map.Terrain[newCoordinates.Item1, newCoordinates.Item2].Type == TerrainType.Land ||
                map.Terrain[newCoordinates.Item1, newCoordinates.Item2].Type == TerrainType.Edge)
            {
                Console.WriteLine(string.Format("{0}: is trying to move on land", unit.Name));
                return false;
            }
            return true;
        }

        
    }
}