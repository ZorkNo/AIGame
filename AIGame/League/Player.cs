using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AIGame.AI;
using AIGame.CoreGame;

namespace AIGame.League
{
    public class Player
    {
        public IAiType AiType;
        public int Wins;
        public int Loses;
        public int Ties;
        public int GamesPlayed;

        public void AddGame(Game game)
        {
            GamesPlayed++;

            if (game.BlueAi.Name ==AiType.Name && game.GameResult == GameResult.BlueWin)
            { 
                Wins++;
                return;
            }

            if (game.RedAi.Name == AiType.Name && game.GameResult == GameResult.RedWin)
            { 
                Wins++;
                return;
            }

            if (game.GameResult == GameResult.Tie)
            {
                Ties++;
                return;
            }
            Loses++;
            //throw new Exception("no result");
        }
    }
}
