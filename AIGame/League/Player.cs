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
        public double EloRating =2000;
        public int GamesPlayed;
    }
}
