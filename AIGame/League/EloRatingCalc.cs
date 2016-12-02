using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AIGame.CoreGame;

namespace AIGame.League
{
    //Elo rating from 
    //https://metinmediamath.wordpress.com/2013/11/27/how-to-calculate-the-elo-rating-including-example/
    public class EloRatingCalc
    {
        public static double GetRating(Result result, double oldRating, double opponentRating)
        {
            double kFactor = 4;
            double newRating =oldRating + kFactor *((result.GetHashCode()/2.0) - CalculateChanceOfWinning(oldRating, opponentRating));
            return newRating;
            
        }
        
        private static double CalculateChanceOfWinning(double oldRating, double opponentRating)
        {
            double oldTransformedRating = GetTransformedRating(oldRating);
            double opponentTransformedRating = GetTransformedRating(opponentRating);

            double chance = oldTransformedRating /(oldTransformedRating + opponentTransformedRating);

            return chance;
        }
        private static double GetTransformedRating(double currentRating)
        {
            double startRating = 400;
            return Math.Pow(10, (currentRating / startRating));
        }
    }
}
