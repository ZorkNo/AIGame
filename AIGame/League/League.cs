using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AIGame.AI;
using AIGame.CoreGame;
using AIGame.Interfaces;

namespace AIGame.League
{
    public class League
    {
        private int gamesPerMatchUp=1000;
        private int gamesPlayed = 0, blueWins = 0, redWins = 0, ties = 0, didNotFinish = 0;
            
        public List<Player> Players;
        private MatchUp _matchUp = new MatchUp();

        public void RunSingleMatchUp()
        {
            AddPlayers();
            Stopwatch sw = new Stopwatch();
            sw.Start();
            Console.Clear();
            Console.WriteLine("Running");

            RunAllGames();

            Console.WriteLine("");
            sw.Stop();
            Console.WriteLine($"Games: {gamesPlayed} Calculation time seconds:{sw.Elapsed.TotalSeconds}");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write($"Blue wins : {blueWins} ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write($"Red wins : {redWins} ");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine($"Ties : {ties}, did not finish : {didNotFinish}");
            Console.WriteLine();
            foreach (Player player in Players)
            { 
                Console.WriteLine("{0}: score results Games played:{1} Wins:{2} Ties:{3} Loses:{4} Elo:{5}",
                    player.AiType.Name, player.GamesPlayed, player.Wins, player.Ties,player.Loses,player.EloRating);
            }
            Console.ReadKey();
        }

        private void AddPlayers()
        {
            Players = new List<Player>
            {
                Player.Create<DoNothingAI>(),
                Player.Create<RandomAI>(),
                Player.Create<SimpleAi>(),
                Player.Create<RunAwayAi>(),
                Player.Create<FireAllTheTimeAi>(),
                Player.Create<ScanNFireAi>(),
                Player.Create<SimplePlusAi>(),
            };
        }

        private void RunAllGames()
        {
            for(int i =0; i <100;i++)
            { 
                foreach (Player blue in Players)
                {
                    foreach (Player red in Players)
                    {
                        if(blue.AiType.Name !=red.AiType.Name)
                        {
                            //Console.WriteLine();
                            //Console.WriteLine($"{blue.AiType.Name} vs {red.AiType.Name}");
                            Parallel.For(0, 10, j =>
                            {
                                PlayGame(blue, red, true, j);
                            
                            });
                        }
                    }
                }
                if (i % 1 == 0)
                    Console.Write(".");
            }
        }
        public void AddGame(Game game, Player bluePlayer, Player redPlayer)
        {
            bluePlayer.GamesPlayed++;
            redPlayer.GamesPlayed++;
            double blueElo = bluePlayer.EloRating;
            double redElo = redPlayer.EloRating;

            if (game.GameResult == GameResult.BlueWin)
            {
                bluePlayer.EloRating = EloRatingCalc.GetRating(Result.Win, blueElo, redElo);
                redPlayer.EloRating = EloRatingCalc.GetRating(Result.Lost, blueElo, redElo);

                bluePlayer.Wins++;
                redPlayer.Loses++;
                return;
            }

            if (game.GameResult == GameResult.RedWin)
            {
                bluePlayer.EloRating = EloRatingCalc.GetRating(Result.Lost , blueElo, redElo);
                redPlayer.EloRating = EloRatingCalc.GetRating(Result.Win, blueElo, redElo);
                
                bluePlayer.Loses++;
                redPlayer.Wins++;
                return;
            }

            if (game.GameResult == GameResult.Tie)
            {
                bluePlayer.EloRating = EloRatingCalc.GetRating(Result.Tie, blueElo, redElo);
                redPlayer.EloRating = EloRatingCalc.GetRating(Result.Tie, blueElo, redElo);

                bluePlayer.Ties++;
                redPlayer.Ties++;
                return;
            }
        }
        private void PlayGame(Player blue, Player red,bool withLock, int gameInt)
        {
            long longRnd = Environment.TickCount + gameInt;
            Random rnd = new Random((int)longRnd);

            Game game = new Game(blue.AiType, red.AiType, GameMode.HiddenInfo2ShipLarge, rnd);
            game.PlayUntilEnd();

            if(withLock)
            { 
                lock (Players)
                {
            switch (game.GameResult)
            {
                case GameResult.RedWin:
                    redWins++;
                    break;
                case GameResult.BlueWin:
                    blueWins++;
                    break;
                case GameResult.Tie:
                    ties++;
                    break;
                case GameResult.GameNotEnded:
                    didNotFinish++;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            AddGame(game,blue,red);
                    gamesPlayed++;

                }
            }
            else
            {

                AddGame(game, blue, red);
                gamesPlayed++;
            }
            
        }
    }
}
