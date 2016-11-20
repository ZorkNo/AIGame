using System;
using AIGame.Interfaces;

namespace AIGame.CoreGame.Orders
{
    public class FireTorpedo : IOrder
    {

        public Tuple<int, int> RelativeCoordinates;
        public FireTorpedo(Tuple<int, int> relativeCoordinates)
        {
            RelativeCoordinates = relativeCoordinates;
        }
        public void Execute(IUnit unit, IMap map)
        {
            Tuple<int, int> Coordinates= new Tuple<int, int>(unit.Coordinates.Item1 + RelativeCoordinates.Item1,unit.Coordinates.Item2 + RelativeCoordinates.Item2  );

            //TODO remove console 
            Console.WriteLine("Clack......");
            foreach (IUnit unitOnMap in map.Units.FindAll(u => u.Coordinates.Equals(Coordinates)))
            {
                unit.Health -= 34;
                Console.WriteLine("Boooom!");
            }

        }

        public bool IsValid(IUnit unit, IMap map)
        {
            //TODO Max range
            return true;
        }
    }


}

