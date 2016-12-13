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
        private int _gamePlayedGoal=1000;
        private int gamesPlayed = 0, blueWins = 0, redWins = 0, ties = 0, didNotFinish = 0;
        private GameMode _gameMode;
            
        public List<Player> Players;
        public List<Player> CurrentPlayers;

        public List<Player> Tournament()
        {
            return Tournament(GetLeaguePlayers(),TournamentType.Dropout,20000,GameMode.HiddenInfo2ShipLarge);
        }

        public List<Player> Tournament(List<Player> players,TournamentType tournamentType, int gamePlayedGoal,GameMode gameMode)
        {
            Players = players;
            _gamePlayedGoal = gamePlayedGoal;
            _gameMode = gameMode;
            Stopwatch sw = new Stopwatch();
            sw.Start();
            
            Console.WriteLine("Running Gamemode:{0} TournamentType:{1} GameplayedGoal:{2}", gameMode, tournamentType, gamePlayedGoal);

            RunAllGames(tournamentType);

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
            int rank = Players.Count;
            foreach (Player player in Players.OrderByDescending(p => p.Wins).ThenByDescending((l => l.Ties)))
            {

                Console.WriteLine("{0}: Score results Games played:{1} Wins:{2} Ties:{3} Loses:{4} Elo:{5} Args:{6}",
                    player.AiName, player.GamesPlayed, player.Wins, player.Ties,player.Loses, Math.Round(player.EloRating,0), player.GetArgs());
            }
            
            return Players;

        }

        private List<Player> GetLeaguePlayers()
        {
            Random rnd = new Random((int)DateTime.Now.Ticks);
            List<Player> leaguePlayers = new List<Player>
            {
                new Player(AiType.Create<SimpleMutableAI>()),
                new Player(AiType.Create<NewMutableAI>("94","99","4","0","99","1")),
                new Player(AiType.Create<NewMutableAI>()),
                new Player(AiType.Create<DoNothingAI>()),
                new Player(AiType.Create<RandomAI>()),
                new Player(AiType.Create<RunAwayAI>()),
                new Player(AiType.Create<CryBabyAI>()),
                new Player(AiType.Create<FireAllTheTimeAI>()),
                new Player(AiType.Create<ScanNFireAI>()),
                new Player(AiType.Create<SimplePlusAI>()),
                new Player(AiType.Create<CooroperateAI>()),
            };

            return leaguePlayers;
        }

        private void RunAllGames(TournamentType tournamentType)
        {
            if(tournamentType == TournamentType.AllvsAll)
                AllvsAllTournement();

            if(tournamentType == TournamentType.Dropout)
                DropoutTournement();

            if (tournamentType == TournamentType.VersusTop)
                VersusTopTournement();
        }

        private void AllvsAllTournement()
        {
            int parallelMatchUps = 10;
            int playerCombination = Players.Count*(Players.Count - 1);
            int gameInterations = (int) Math.Ceiling((_gamePlayedGoal/(double) (playerCombination*parallelMatchUps)));
            for (int i = 0; i < gameInterations; i++)
            {
                RunIteration(Players, Players);
                if (i%1 == 0)
                    Console.Write(".");
            }
        }
        private void VersusTopTournement()
        {
            int parallelMatchUps = 10;
            int playerCombination = Players.Count * (Players.Count - 1);
            int gameInterations = (int)Math.Ceiling((_gamePlayedGoal / (double)(playerCombination * parallelMatchUps)));

            List<Player> firstPlayers = new List<Player> {Players.First()};
            Players.RemoveAt(0);
            List<Player> otherPlayers = Players;

            for (int i = 0; i < gameInterations; i++)
            {
                RunIteration(firstPlayers ,otherPlayers);
                if (i % 1 == 0)
                    Console.Write(".");
            }
        }

        private void RunIteration(List<Player> bluePlayers, List<Player> redPlayers)
        {
            foreach (Player blue in bluePlayers)
            {
                foreach (Player red in redPlayers)
                {
                    if (blue.Id != red.Id)
                    {
                        Parallel.For(0, 10, j => { PlayGame(blue, red, true, j); });
                    }
                }
            }
        }

        private void DropoutTournement()
        {
            int parallelMatchUps = 10;
            int playerCombination = Players.Count * (Players.Count - 1);
            int gameInterations = (int)Math.Ceiling((_gamePlayedGoal / (double)(playerCombination * parallelMatchUps)));
            int gameInterarionsPerDropout = (int)Math.Ceiling((gameInterations / (double)(Players.Count)));
            int gameInterarionsSinceDropout = 0;

            CurrentPlayers = new List<Player>();
            CurrentPlayers.AddRange(Players);
            for (int i = 0; i < gameInterations; i++)
            {
                playerCombination = CurrentPlayers.Count * (CurrentPlayers.Count - 1);
                gameInterations = (int)Math.Ceiling((_gamePlayedGoal / (double)(playerCombination * parallelMatchUps)));
                gameInterarionsPerDropout = (int)Math.Ceiling((gameInterations / (double)(CurrentPlayers.Count)));

                RunIteration(CurrentPlayers, CurrentPlayers);

                if (i % 1 == 0)
                    Console.Write(".");
                gameInterarionsSinceDropout++;

                if (gameInterarionsSinceDropout > gameInterarionsPerDropout)
                {
                    gameInterarionsSinceDropout = 0;
                    Player removePlayer = CurrentPlayers.OrderBy(p => p.Wins).ThenBy(l => l.Ties).First();
                    CurrentPlayers.Remove(removePlayer);
                    Console.WriteLine();
                    Console.WriteLine($"Player:{removePlayer.AiName}{removePlayer.GetArgs()} Rank:{CurrentPlayers.Count}");
                }


            }
        }
        private void PlayGame(Player blue, Player red,bool withLock, int gameInt)
        {
            long longRnd = Environment.TickCount + gameInt;
            Random rnd = new Random((int)longRnd);

            Game game = new Game(blue.AiType, red.AiType, GameMode.HiddenInfo1ShipLarge, rnd);
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

    public  enum TournamentType
    {
        AllvsAll,
        Dropout,
        VersusTop
    }
}
