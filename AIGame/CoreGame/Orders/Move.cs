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
            unit.coordinates  = NewCoordinates(unit);
        }

        public bool IsValid(Unit unit, Map map)
        {
            Tuple<int, int> newCoordinates= NewCoordinates(unit);
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

        private Tuple<int, int> NewCoordinates(Unit unit)
        {
            Tuple<int, int> newCoordinates;
            switch (unit.facing)
            {
                case Direction.north:
                    newCoordinates = new Tuple<int, int>(unit.coordinates.Item1 - 1, unit.coordinates.Item2);
                    
                    break;
                case Direction.south:
                    newCoordinates = new Tuple<int, int>(unit.coordinates.Item1 + 1, unit.coordinates.Item2);
                    break;
                case Direction.east:
                    newCoordinates = new Tuple<int, int>(unit.coordinates.Item1, unit.coordinates.Item2 - 1);
                    break;
                case Direction.west:
                    newCoordinates = new Tuple<int, int>(unit.coordinates.Item1, unit.coordinates.Item2 + 1);
                    break;
                default:
                    throw new Exception("no direction");
            }
            return newCoordinates;
        }
    }
}