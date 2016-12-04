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
                Console.WriteLine($"{player.AiName}: score results Games played:{player.GamesPlayed} Wins:{player.Wins} Ties:{player.Ties} Loses:{player.Loses} ");
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
                Player.Create<ScanNFireAi>()
            };
        }

        private void RunAllGames()
        {

            foreach (Player blue in Players)
            {
                foreach (Player red in Players)
                {
                    if (blue.AiType == red.AiType)
                         continue;

                    Console.WriteLine();
                    Console.WriteLine($"{blue.AiName} vs {red.AiName}");
                    Parallel.For(0, gamesPerMatchUp, i =>
                        {
                            PlayGame(blue, red, true, i);
                            if (gamesPlayed% 250 == 0)
                                Console.Write(".");
                        });
                }
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
                    blue.AddGame(game);
                    red.AddGame(game);
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
                    gamesPlayed++;

                }
            }
            else
            {
                blue.AddGame(game);
                red.AddGame(game);
                gamesPlayed++;
            }
            
        }
    }
}
