using System;
using System.Linq;
using AIGame.AI;
using AIGame.CoreGame;
using AIGame.Interfaces;

namespace AIGame.League
{
    public class Player
    {
        public Player(AiType aiType)
        {
            AiType = aiType;
            Id = Guid.NewGuid();
        }

        public void Reset()
        {
            Wins = 0;
            Loses = 0;
            Ties = 0;
            GamesPlayed = 0;
            EloRating = 2000;
        }
        public string GetArgs()
        {
            string args = "";
            if (AiType.Args != null)
                args = AiType.Args.Aggregate((i, j) => i + ":" + j);
            return args;
        }
        public AiType AiType { get; }

        public Guid Id { get; }
        public string AiName => AiType.Type.Name;
        public int Wins { get;  set; }
        public int Loses { get;  set; }
        public int Ties { get;  set; }
        public int GamesPlayed { get;  set; }
        public double EloRating = 2000;

        
        
    }
}
