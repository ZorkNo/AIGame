using System;
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
