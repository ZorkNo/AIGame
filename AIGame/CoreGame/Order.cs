using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIGame.CoreGame
{
    public interface iOrder
    {
        OrderType type();
    }
    public enum OrderType
    {
        Move,
        Turn,
        DoNothing
    }
    public class Move:iOrder
    {
        public OrderType type()
        {
            return OrderType.Move;
        }
    }
    public class Turn : iOrder
    {
        public Direction direction;

        public Turn (Direction _direction)
        {
            direction = _direction;
        }
        public OrderType type()
        {
            return OrderType.Turn;
        }
    }
    public class DoNothing : iOrder
    {
        public OrderType type()
        {
            return OrderType.DoNothing;
        }
    }


}
