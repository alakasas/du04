using System;
using System.Collections.Generic;

namespace pb006
{
    static class Test
    {   
        static int Main(){
            LinearStrategy s1 = new LinearStrategy(new int[] {2, 1, 0});
            Strategy s2 = new TrivialStrategy(1);
            Console.WriteLine("1: {0}", s1.IsHistoryBased());  
            Console.WriteLine("2: {0}", s2.IsHistoryBased());   
            Game game = new RockPaperScissors();
            Console.WriteLine("end: {0}", game.Play(4, s1, s2, true, true)); 

            
            LinearStrategy s3 = new LinearStrategy(new int[] {2, 1, 0});
            HistoryBasedStrategy s4 = new DelayedStrategy(new int[] {0, 1, 1}, 2);
            Console.WriteLine("xd:{0}",s4.GetName(new string[] {"rock", "paper", "scissors"}) ) ;
            Console.WriteLine("3: {0}", s3.IsHistoryBased());
            Console.WriteLine("4: {0}", s4.IsHistoryBased());
            Game game2 = new RockPaperScissors();
            Console.WriteLine("end: {0}", game2.Play(6, s3, s4, true)); 
            Console.WriteLine("end2: {0}", s4.GetHistory());

            LastWinningForOpponent s5 = new LastWinningForOpponent(new int[] {1, 0, 2, 3});
            LinearStrategy s6 = new LinearStrategy(new int[] {3, 4, 0, 1, 4, 3, 0});
            Game game3 = new SpockLizard();
            game3.Play(7, s5, s6, true, false);
            Console.WriteLine("end3: {0}", s5.GetHistory());

            return 0;
        }
    }
}