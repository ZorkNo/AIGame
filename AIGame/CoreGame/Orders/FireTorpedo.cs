using System;

namespace AIGame.CoreGame.Orders
{
    public class FireTorpedo : IOrder
    {

        public Tuple<int, int> Coordinates;
        public FireTorpedo(Tuple<int, int> coordinates)
        {
            Coordinates = coordinates;
        }
        public void Execute(Unit unit, Map map)
        {
            throw new NotImplementedException();
        }

        public bool IsValid(Unit unit, Map map)
        {
            throw new NotImplementedException();
        }
    }


}

