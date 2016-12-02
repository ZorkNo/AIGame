
namespace AIGame.CoreGame
{

    public enum Side
    {
        Red,
        Blue
    }
    public enum RotateDirection
    {
        Left,
        Right
    }
    public enum GameResult
    {
        RedWin,
        BlueWin,
        Tie,
        GameNotEnded
    }
    public enum Result
    {
        Win=2,
        Tie=1,
        Lost=0
    }
    public enum GameMode
    {
        //FullInfomation
        HiddenInfo1ShipSmall,
        HiddenInfo1ShipLarge,
        HiddenInfo2ShipLarge,
        //HiddenInfoBroadcast

    }
    public enum Direction
    {
        North = 0,
        West = 1,
        South = 2,
        East = 3
    }

}
