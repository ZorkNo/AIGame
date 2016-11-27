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