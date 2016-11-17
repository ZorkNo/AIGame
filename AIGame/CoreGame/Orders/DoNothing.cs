using System;

namespace AIGame.CoreGame.Orders
{
    public class DoNothing : IOrder
    {
        public void Execute(Unit unit, Map map)
        {
        }
        public bool IsValid(Unit unit, Map map)
        {
            return true;
        }
    }


}
