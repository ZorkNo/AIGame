using System;
using AIGame.Interfaces;

namespace AIGame.CoreGame.Orders
{
    public class DoNothing : IOrder
    {
        public void Execute(IUnit unit, IMap map)
        {
        }
        public bool IsValid(IUnit unit, IMap map)
        {
            return true;
        }

        public string Render()
        {
            return "";
        }
    }


}
