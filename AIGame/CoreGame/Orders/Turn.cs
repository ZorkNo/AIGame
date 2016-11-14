using System;

namespace AIGame.CoreGame.Orders
{
    public class Turn : IOrder
    {
        public Direction Direction;

        public Turn (Direction _direction)
        {
            Direction = _direction;
        }
        public OrderType Type()
        {
            return OrderType.Turn;
        }


        public void Execute(Unit unit, Map map)
        {
            unit.facing = Direction;
        }

        public bool IsValid(Unit unit, Map map)
        {
            return true;
        }
    }
}