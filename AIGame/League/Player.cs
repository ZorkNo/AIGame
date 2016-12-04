using System;
using AIGame.AI;
using AIGame.CoreGame;
using AIGame.Interfaces;

namespace AIGame.League
{
    public class Player
    {
        public static Player Create<T>() where T : BaseAi
        {
            return new Player(typeof(T));
        }
        public Type AiType { get; }

        public string AiName => AiType.Name;
        public int Wins { get; private set; }
        public int Loses { get; private set; }
        public int Ties { get; private set; }
        public int GamesPlayed { get; private set; }
        public double EloRating = 2000;

        private Player(Type aiType)
        {
            AiType = aiType;
        }
        
    }
}
