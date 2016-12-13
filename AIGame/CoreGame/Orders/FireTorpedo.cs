using System;
using AIGame.Interfaces;

namespace AIGame.CoreGame.Orders
{
    public class FireTorpedo : IOrder
    {

        public Tuple<int, int> Coordinates;
        public CoordinateType CoordinateType;
        private string message;
        public FireTorpedo(Tuple<int, int> coordinates, CoordinateType coordinateType)
        {
            Coordinates = coordinates;
            CoordinateType = coordinateType;

        }
        public void Execute(IUnit unit, IMap map)
        {
            Tuple<int, int> coordinates = new Tuple<int, int>(0,0);
            if (CoordinateType == CoordinateType.Relative)
            {
                Tuple<int, int> rotatedCoordinates = Helper.RotateCoordinates(unit.Facing, Coordinates.Item1,
                    Coordinates.Item2);

                coordinates = new Tuple<int, int>(unit.Coordinates.Item1 + rotatedCoordinates.Item1,
                    unit.Coordinates.Item2 + rotatedCoordinates.Item2);
            }
            else
            {
                coordinates = Coordinates;
            }
            message = string.Format("{0}{1}{2}{3}", message, unit.Name, ": Firing", System.Environment.NewLine);
            foreach (IUnit unitOnMap in map.Units.FindAll(u => u.Coordinates.Equals(coordinates)))
            {
                unitOnMap.Health -= 34;
                if (unitOnMap.Health < 0)
                    unitOnMap.Health = 0;

                message = string.Format("{0}{1}{2}{3}", message, unitOnMap.Name, ": Hit!", System.Environment.NewLine);
            }

        }

        public bool IsValid(IUnit unit, IMap map)
        {
            if (Coordinates == null)
                return false;
            Tuple<int,int> RelativeCoordinates = Coordinates;
            if (CoordinateType == CoordinateType.Absolute)
            {
                RelativeCoordinates = new Tuple<int, int>(unit.Coordinates.Item1 - Coordinates.Item1, unit.Coordinates.Item2 - Coordinates.Item2);
                RelativeCoordinates = Helper.RotateCoordinates(unit.Facing,RelativeCoordinates.Item1,RelativeCoordinates.Item2);
            }

            
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

    public enum CoordinateType
    {
        Relative,
        Absolute,
    }
}

