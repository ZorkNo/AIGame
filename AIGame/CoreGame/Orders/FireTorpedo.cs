using System;
using AIGame.Interfaces;

namespace AIGame.CoreGame.Orders
{
    public class FireTorpedo : IOrder
    {

        public Tuple<int, int> Coordinates;
        public FireTorpedo(Tuple<int, int> coordinates)
        {
            Coordinates = coordinates;
        }
        public void Execute(IUnit unit, IMap map)
        {
            throw new NotImplementedException();
        }

        public bool IsValid(IUnit unit, IMap map)
        {
            throw new NotImplementedException();
        }
    }


}

