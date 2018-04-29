using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gamblar
{
    class Program
    {
        private static float StartingCash = 1000;
        private static float OddsOfWinning = .45f;
        private static Random rand;
        private static float CurrentCash;

        static void Main(string[] args)
        {
            int totalRounds = 0;
            rand = new Random();
            Console.WriteLine("Let's start gambling.");
            Console.WriteLine("Odds of Winning: " + OddsOfWinning * 100 + "%");
            StringBuilder graphFile = new StringBuilder();
            for (int i = 0; i < 250; i++)
            {
                List<float> session = DoSession(10);
                totalRounds += session.Count;
                Console.WriteLine("You went broke after " + session.Count + " rounds");
                foreach (float value in session)
                {
                    graphFile.Append(value + ",");
                }
                graphFile.Append("0\n");
            }
            int averageSession = (int)((float)totalRounds / 250);
            Console.WriteLine("Using this strategy, you lost " + StartingCash * 250 + ", going broke after an average of " + averageSession + " rounds");
            //File.WriteAllText(@"C:\Session.csv", graphFile.ToString());
            Console.Write("The end.");
            Console.Read();
        }

        private static List<float> DoSession(float startingAmount)
        {
            List<float> ret = new List<float>();
            CurrentCash = StartingCash;
            Gamble(startingAmount, ret);
            return ret;
        }

        private static void Gamble(float betAmount, List<float> tally)
        {
            if(betAmount > CurrentCash)
            {
                betAmount = CurrentCash;
            }
            tally.Add(CurrentCash);
            bool win = rand.NextDouble() < OddsOfWinning;
            if(win)
            {
                CurrentCash += betAmount;
                Gamble(10, tally);
            }
            else
            {
                CurrentCash -= betAmount;
                if (CurrentCash <= 0)
                {
                    return;
                }
                Gamble(betAmount * 2, tally);
            }
        }
    }
}
