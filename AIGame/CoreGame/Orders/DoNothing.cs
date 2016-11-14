using System;

namespace AIGame.CoreGame.Orders
{
    public class DoNothing : iOrder
    {
        public OrderType Type()
        {
            return OrderType.DoNothing;
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
