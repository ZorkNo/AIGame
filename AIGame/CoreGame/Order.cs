using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIGame.CoreGame
{
    public interface iOrder
    {
        OrderType Type();
        void SetUnit(Unit unit);
        void Execute();
    }
    public enum OrderType
    {
        Move,
        Turn,
        DoNothing
    }
    public class Move:iOrder
    {
        public Unit Unit;
        public OrderType Type()
        {
            return OrderType.Move;
        }

        public void SetUnit(Unit unit)
        {
            Unit=unit;
        }

        public void Execute()
        {
            switch (Unit.facing)
            {
                case Direction.north:
                    Unit.coordinates = new Tuple<int, int>(Unit.coordinates.Item1 + 1, Unit.coordinates.Item2);
                    break;
                case Direction.south:
                    Unit.coordinates = new Tuple<int, int>(Unit.coordinates.Item1 - 1, Unit.coordinates.Item2);
                    break;
                case Direction.east:
                    Unit.coordinates = new Tuple<int, int>(Unit.coordinates.Item1 , Unit.coordinates.Item2-1);
                    break;
                case Direction.west:
                    Unit.coordinates = new Tuple<int, int>(Unit.coordinates.Item1, Unit.coordinates.Item2+1);
                    break;
                default:
                    throw new Exception("no direction");
            }

        }
    }
    public class Turn : iOrder
    {
        public Direction Direction;
        public Unit Unit;

        public Turn (Direction _direction)
        {
            Direction = _direction;
        }
        public OrderType Type()
        {
            return OrderType.Turn;
        }

        public void SetUnit(Unit unit)
        {
            Unit=unit;
        }

        public void Execute()
        {
            throw new NotImplementedException();
        }
    }
    public class DoNothing : iOrder
    {
        public OrderType Type()
        {
            return OrderType.DoNothing;
        }

        public void SetUnit(Unit unit)
        {
            throw new NotImplementedException();
        }

        public void Execute()
        {
            throw new NotImplementedException();
        }
    }


}
