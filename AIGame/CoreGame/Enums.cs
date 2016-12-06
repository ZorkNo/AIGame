
namespace AIGame.CoreGame
{

    public enum Side
    {
        Red,
        Blue,
        NoSide,
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
    public enum DirectionPrecise
    {
        North = 0,
        NorthNorthEast=1,
        NorthEast = 2,
        EastNorthEast = 3,
        East = 4,
        EastSouthEast = 5,
        SouthEast=6,
        SouthSouthEast=7,
        South = 8,
       
        SouthSouthWest=9,
        SouthWest=10,
        WestSouthWest=11,
        West = 12,
        WestNorthWest=13,
        
        NorthWest=14,
        NorthNorthWest=15,
        OnTop=-2,
        Unknown=-1,
    }
    public enum SignalType
    {
        Broadcast,
        //Noise
        //Flash
    }

}
