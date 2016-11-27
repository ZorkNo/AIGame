using System;
using AIGame.CoreGame;

namespace AIGame.League
{
    public class MatchUp
    {
        public int gamesPlayed = 0;
        public int redWins = 0;
        public int redTies = 0;
        public int blueWins = 0;
        public int blueTies = 0;

        public void AddGame(Game game)
        {
            gamesPlayed++;
            switch (game.GameResult)
            {
                case GameResult.RedWin:
                    this.redWins++;
                    break;
                case GameResult.BlueWin:
                    this.blueWins++;
                    break;
                case GameResult.Tie:
                    this.redTies++;
                    this.blueTies++;
                    break;
                default:
                    throw new Exception("Unknown result");
            }
        }
    }
}