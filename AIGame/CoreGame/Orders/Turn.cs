using System;

namespace AIGame.CoreGame.Orders
{
    public class Turn : iOrder
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
            throw new NotImplementedException();
        }

        public bool IsValid(Unit unit, Map map)
        {
            throw new NotImplementedException();
        }
    }
}