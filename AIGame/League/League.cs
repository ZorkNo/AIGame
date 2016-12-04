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
        private int gamePlayedGoal=10000;
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
            Console.WriteLine($"Games: {gamesPlayed} Calculation time seconds:{sw.Elapsed.TotalSeconds} Seconds per 100 games {Math.Round(sw.Elapsed.TotalSeconds * 100 / gamesPlayed,3)  }");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write($"Blue wins : {blueWins} ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write($"Red wins : {redWins} ");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine($"Ties : {ties}, did not finish : {didNotFinish}");
            Console.WriteLine();
            foreach (Player player in Players)
            {
                string args = "";
                if (player.AiType.Args!=null)
                    args = player.AiType.Args.Aggregate((i, j) => i + ":" + j);

                Console.WriteLine("{0}: Score results Games played:{1} Wins:{2} Ties:{3} Loses:{4} Elo:{5} Args:{6}",
                    player.AiName, player.GamesPlayed, player.Wins, player.Ties,player.Loses, Math.Round(player.EloRating,0), args);
            }
            Console.ReadKey();
        }

        private void AddPlayers()
        {
            Players = new List<Player>
            {
                new Player(AiType.Create<MutableAi>(new string[]{ "0","50","65","0","3"})),
                //new Player(AiType.Create<DoNothingAI>()),
                //new Player(AiType.Create<RandomAI>()),
                //new Player(AiType.Create<SimpleAi>()),
                //new Player(AiType.Create<RunAwayAi>()),
                //new Player(AiType.Create<FireAllTheTimeAi>()),
                //new Player(AiType.Create<ScanNFireAi>()),
                new Player(AiType.Create<SimplePlusAi>()),
                
            };
        }

        private void RunAllGames()
        {
            int parallelMatchUps = 10;
            int playerCombination = Players.Count*(Players.Count - 1);
            int gameInterations = (int) Math.Ceiling((gamePlayedGoal /(double)(playerCombination*parallelMatchUps)));
            for (int i =0; i < gameInterations; i++)
            { 
                foreach (Player blue in Players)
                {
                    foreach (Player red in Players)
                    {
                        if(blue.AiName !=red.AiName)
                        {
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
        public void AddGame(Game game, Player bluePlayer, Player redPlayer)
        {
            AddGlobalCounter(game);
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
                bluePlayer.EloRating = EloRatingCalc.GetRating(Result.Lost, blueElo, redElo);
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
        private void AddGlobalCounter(Game game)
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
        }
    }
}
