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
            unit.coordinates  = Helper.NewCoordinates(unit.coordinates, unit.facing);
        }

        public bool IsValid(Unit unit, Map map)
        {
            Tuple<int, int> newCoordinates= Helper.NewCoordinates(unit.coordinates,unit.facing );
            //Is out of bounce
            if (newCoordinates.Item1 < 0 || newCoordinates.Item2 < 0 || newCoordinates.Item1 > map.xSize || newCoordinates.Item2 > map.ySize)
            {
                Console.WriteLine(string.Format("{0}: is trying to move out of bounce", unit.name)); 
                return false;
            }
            //on land
            if (map.terrain[newCoordinates.Item1, newCoordinates.Item2].Type == TerrainType.Land ||
                map.terrain[newCoordinates.Item1, newCoordinates.Item2].Type == TerrainType.Edge)
            {
                Console.WriteLine(string.Format("{0}: is trying to move on land", unit.name));
                return false;
            }
            return true;
        }
    }
}