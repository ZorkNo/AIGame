using System;
using AIGame.Interfaces;

namespace AIGame.CoreGame.Orders
{
    public class Move:IOrder
    {
        private string message="";
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
                message = string.Format("{0}{1}{2}{3}", message, unit.Name, ": is trying to move out of bounce", System.Environment.NewLine);
                return false;
            }
            //on land
             if (map.Terrain[newCoordinates.Item1, newCoordinates.Item2].Type == TerrainType.Land ||
                map.Terrain[newCoordinates.Item1, newCoordinates.Item2].Type == TerrainType.Edge)
            {
                message = string.Format("{0}{1}{2}{3}", message, unit.Name, ": is trying to move on land", System.Environment.NewLine);
                return false;
            }
            return true;
        }

        public string Render()
        {
            return message;
        }

        public bool FreeOrder()
        {
            return false;
        }
    }
}