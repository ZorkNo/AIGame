using System;

namespace AIGame.CoreGame.Orders
{
    public class Move:IOrder
    {

        public OrderType Type()
        {
            return OrderType.Move;
        }
        public void Execute(Unit unit, Map map)
        {
            unit.Coordinates  = Helper.NewCoordinates(unit.Coordinates, unit.Facing);
        }

        public bool IsValid(Unit unit, Map map)
        {
            Tuple<int, int> newCoordinates= Helper.NewCoordinates(unit.Coordinates,unit.Facing );
            //Is out of bounce
            if (newCoordinates.Item1 < 0 || newCoordinates.Item2 < 0 || newCoordinates.Item1 > map.XSize || newCoordinates.Item2 > map.YSize)
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