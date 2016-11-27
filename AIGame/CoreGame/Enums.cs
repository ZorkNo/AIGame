
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
    public enum GameMode
    {
        //FullInfomation
        HiddenInfo1ShipSmallNoBroadcast,
        HiddenInfo1ShipLargeNoBroadcast,
        HiddenInfo2ShipLargeNoBroadcast,
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
