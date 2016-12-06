using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using AIGame.AI;

namespace AIGame.League
{
    public class Ecosystem
    {
        public void RunEco()
        {

            Random rnd = new Random((int)DateTime.Now.Ticks);
            List<Player> EcoPlayers= GetNewPlayers(20, rnd);
            for (int i = 0; i < 1000; i++)
            {
                AIGame.League.League league = new AIGame.League.League();

                EcoPlayers.AddRange( new List<Player> {
                    new Player(AiType.Create<ScanNFireAi>()),
                    new Player(AiType.Create<ScanNFireAi>()),
                    new Player(AiType.Create<ScanNFireAi>()),
                    new Player(AiType.Create<ScanNFireAi>()),
                    new Player(AiType.Create<ScanNFireAi>()),
                    new Player(AiType.Create<ScanNFireAi>()),
                    new Player(AiType.Create<ScanNFireAi>()),
                    });

                EcoPlayers =league.Tournament(EcoPlayers,TournamentType.Dropout,500);

                EcoPlayers.RemoveAll(p => p.AiType.Type == typeof(ScanNFireAi));

                //Clean out bad players
                int removeCount = EcoPlayers.Count-2;
                for (int j = 0; j < removeCount; j++)
                {

                    var lastPlayer = EcoPlayers.OrderBy(p => p.Wins).ThenBy(l => l.Ties).First();

                    //Console.WriteLine("{0}: Score results Games played:{1} Wins:{2} Ties:{3} Loses:{4} Elo:{5} Args:{6}",
                    //lastPlayer.AiName, lastPlayer.GamesPlayed, lastPlayer.Wins, lastPlayer.Ties, lastPlayer.Loses, Math.Round(lastPlayer.EloRating, 0), lastPlayer.GetArgs());
                    //
                    EcoPlayers.Remove(lastPlayer);
                }

                
                List<Player> newPlayers = new List<Player>();

                //Make chilren
                bool bestPlayer = true;
                foreach (Player player in EcoPlayers.OrderByDescending(p => p.Wins).ThenByDescending(l => l.Ties))
                {
                    //Console.WriteLine("{0}: Score results Games played:{1} Wins:{2} Ties:{3} Loses:{4} Elo:{5} Args:{6}",
                    //player.AiName, player.GamesPlayed, player.Wins, player.Ties, player.Loses, Math.Round(player.EloRating, 0), player.GetArgs());

                    player.Reset();
                    if(bestPlayer)
                    { 
                        for (int m = 0; m < 4; m++)
                        {
                            newPlayers.Add(GetMutantet(player.AiType.Args, rnd));
                        }
                        bestPlayer = false;
                    }
                }
                EcoPlayers.AddRange(newPlayers);

                //Console.WriteLine(EcoPlayers.Count);
                //Console.ReadKey();
            }
        }

        private static Player GetMutantet(String[] args, Random rnd)
        {
            return new Player(
                AiType.Create<SimpleMutableAi>(Mutate(args,rnd)));
        }

        private static String[] Mutate(String[] args,Random rnd)
        {
            string[] mutations = RandomGens(rnd);
            string[] mutant=new string[5];
            args.CopyTo(mutant,0);
            int mutationNumber = rnd.Next(0,5);
            mutant[mutationNumber] = mutations[mutationNumber];

            return mutant;
        }
        private static String[] RandomGens(Random rnd)
        {
            
            return new string[]
            {
                rnd.Next(0, 100).ToString(), rnd.Next(0, 100).ToString(), rnd.Next(0, 100).ToString(),
                rnd.Next(0, 100).ToString(), rnd.Next(0, 5).ToString()
            };
        }
        private static List<Player> GetNewPlayers(int numberOfPlayers,Random rnd)
        {
            List<Player> leaguePlayers=new List<Player>();
            for (int i = 0; i < numberOfPlayers; i++)
            {
                leaguePlayers.Add(
                    new Player(
                        AiType.Create<SimpleMutableAi>(RandomGens(rnd))));
            }
            return leaguePlayers;
        }
    }
}