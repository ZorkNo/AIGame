using System;

namespace AIGame.CoreGame.Orders
{
    public class Fire : IOrder
    {
        public OrderType Type()
        {
            return OrderType.Scan;
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

