using System;
using AIGame.Interfaces;

namespace AIGame.CoreGame.Orders
{
    public class FireTorpedo : IOrder
    {

        public Tuple<int, int> RelativeCoordinates;
        private string message;
        public FireTorpedo(Tuple<int, int> relativeCoordinates)
        {
            RelativeCoordinates = relativeCoordinates;
        }
        public void Execute(IUnit unit, IMap map)
        {
            Tuple<int, int> rotatedCoordinates = Helper.RotateCoordinates(unit.Facing, RelativeCoordinates.Item1,RelativeCoordinates.Item2);

            Tuple <int, int> coordinates= new Tuple<int, int>(unit.Coordinates.Item1 + rotatedCoordinates.Item1,unit.Coordinates.Item2 + rotatedCoordinates.Item2  );

            message = string.Format("{0}{1}{2}{3}", message, unit.Name, ": Firing", System.Environment.NewLine);
            foreach (IUnit unitOnMap in map.Units.FindAll(u => u.Coordinates.Equals(coordinates)))
            {
                unitOnMap.Health -= 34;
                message = string.Format("{0}{1}{2}{3}", message, unitOnMap.Name, ": Hit!", System.Environment.NewLine);
            }

        }

        public bool IsValid(IUnit unit, IMap map)
        {
            if (RelativeCoordinates == null)
                return false;

            var isValid = !(RelativeCoordinates.Item1 < -2 || RelativeCoordinates.Item2 < 0 ||
                          RelativeCoordinates.Item1 > 2 || RelativeCoordinates.Item2 > 3);
            return isValid;
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

