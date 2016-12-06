using System;

namespace AIGame.CoreGame
{
    public static class Helper
    {
        public static Tuple<int, int> NewCoordinates(Tuple<int, int> oldCoordinates, Direction direction)
        {
            Tuple<int, int> newCoordinates;
            switch (direction)
            {
                case Direction.North:
                    newCoordinates = new Tuple<int, int>(oldCoordinates.Item1, oldCoordinates.Item2 + 1);
                    break;
                case Direction.South:
                    newCoordinates = new Tuple<int, int>(oldCoordinates.Item1 , oldCoordinates.Item2 - 1);
                    break;
                case Direction.East:
                    newCoordinates = new Tuple<int, int>(oldCoordinates.Item1 - 1, oldCoordinates.Item2 );
                    break;
                case Direction.West:
                    newCoordinates = new Tuple<int, int>(oldCoordinates.Item1 + 1, oldCoordinates.Item2 );
                    break;
                default:
                    throw new Exception("no direction");
            }
            return newCoordinates;
        }
        public static bool IsOutOfbounce(int xSize, int ySize, Tuple<int, int> newCoordinates)
        {
            return newCoordinates.Item1 < 0 || newCoordinates.Item2 < 0 ||
                   newCoordinates.Item1 >= xSize || newCoordinates.Item2 >= ySize;
        }

        public static double GetAngle(Tuple<int, int> seenFromCoordinates, Tuple<int, int> toCoordinates)
        {
            int y = toCoordinates.Item1- seenFromCoordinates.Item1 ;
            int x =  toCoordinates.Item2- seenFromCoordinates.Item2 ;

            if (x == 0 && y == 0)
                return -1;

            double radians = Math.Atan2(y, x);
            double angle = radians * (180 / Math.PI);

            angle = angle < 0 ? 360 + angle : angle;

            return angle;
        }
        public static DirectionPrecise GetDirection(Tuple<int, int> seenFromCoordinates, Tuple<int, int> toCoordinates)
        {
            double angle = GetAngle(seenFromCoordinates, toCoordinates);

            if (angle == -1)
                return DirectionPrecise.OnTop;

            if (angle > (360 - 11.25d) || angle < 11.25d)
                return DirectionPrecise.North;

            return (DirectionPrecise) (int) Math.Ceiling((angle - 11.25d) / 22.5d);
        }
        public static Tuple<int, int> RotateCoordinates(Direction facing, int x, int y)
        {
            int newX = 0;
            int newY = 0;
            switch (facing)
            {
                case Direction.North:
                    newX = x;
                    newY = y;
                    break;
                case Direction.West:
                    newX = y;
                    newY = -x;
                    break;
                case Direction.South:
                    newX = -x;
                    newY = -y;
                    break;
                case Direction.East:
                    newX = -y;
                    newY = x;
                    break;
            }
            return new Tuple<int, int>(newX, newY);
        }
    }
}