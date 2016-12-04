using System;
using AIGame.AI;
using AIGame.Interfaces;

namespace AIGame.CoreGame
{
    public static class AIFactory 
    {
        public static IAi CreateAi(Type aiType, Random random, string[] args) 
        {
            if (!aiType.IsSubclassOf(typeof(BaseAi)))
                throw new ArgumentException("Not BaseAi class", nameof(aiType));
            return (IAi) Activator.CreateInstance(aiType, random, args);
        }
    }
}
