using System;
using System.Collections.Generic;

namespace pb006 {
    public enum Outcome {
        VICTORY,
        DEFEAT,
        DRAW,
    }

    public class Game {
        // Outcome[x,y] == Outcome.VICTORY if strategy x beats strategy y 
        private Outcome[,] Matrix; 
        private string[] MoveNames;

        public Game(string[] moveNames, Outcome[,] matrix) {
            this.MoveNames = moveNames;
            this.Matrix = matrix;
        }

        // the result is from the point of view of Player 1
        public Outcome Play(int rounds, Strategy s1, Strategy s2, bool printRounds=false, bool printStats=false) {
            int? prevS1Move = null;
            int? prevS2Move = null;
            Outcome? prevOutcome = null;  // outcome is from the point of view of Player 1

            int[] stats = new int[Enum.GetNames(typeof(Outcome)).Length];

            if (printRounds) {
                Console.WriteLine("Player 1: {0}", s1.GetName(this.MoveNames));
                Console.WriteLine("Player 2: {0}", s2.GetName(this.MoveNames));
                Console.WriteLine("\nLET'S PLAY\n----------");
            }

            for (int i = 0; i < rounds; i++) {
                int newS1Move = s1.GetMove(prevS2Move, prevOutcome);
                int newS2Move = s2.GetMove(prevS1Move, prevOutcome == Outcome.DRAW ? Outcome.DRAW : (prevOutcome == Outcome.VICTORY ? Outcome.DEFEAT : Outcome.VICTORY));
                Outcome outcome = this.Matrix[newS1Move, newS2Move];

                prevS1Move = newS1Move;
                prevS2Move = newS2Move;
                prevOutcome = outcome;

                if (printRounds) {
                    Console.WriteLine("{0} x {1}: {2}", this.MoveNames[newS1Move],
                     this.MoveNames[newS2Move], outcome);
                }

                stats[(int) outcome] += 1;
            }

            if (printStats) {
                Console.WriteLine("");
                Console.WriteLine("Player 1: {0}  won {1} times", s1.GetName(this.MoveNames), stats[(int) Outcome.VICTORY]);
                Console.WriteLine("Player 2: {0}  won {1} times", s2.GetName(this.MoveNames), stats[(int) Outcome.DEFEAT]);
                Console.WriteLine("Match was a draw: {0} times", stats[(int) Outcome.DRAW]);
            }

            if (stats[(int) Outcome.VICTORY] > stats[(int) Outcome.DEFEAT])
                return Outcome.VICTORY;
            if (stats[(int) Outcome.VICTORY] < stats[(int) Outcome.DEFEAT])
                return Outcome.DEFEAT;
            return Outcome.DRAW;
        }
    }
}
