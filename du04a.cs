using System;
using System.Globalization;
namespace pb006
{
    abstract class Strategy
    {
        private int[] moves = new int[] {};

        public Strategy(int[] moves)
        {
            this.moves = moves;
        }
        public int GetMove(int? move, Outcome? result)
        {
            int change = 0;
            if (result == Outcome.VICTORY)
                change--;
            if (result == Outcome.DEFEAT)
                change++;

            if (move == null && result == null)
                return moves[0];

            foreach (int myMove in moves){
                if ((myMove + change) % moves.Length == move)
                    return moves[(myMove + 1) % moves.Length];
            }
            return -1;
            
        }

        public bool? IsHistoryBased()
        {
            return null;
        }

        public string GetName(string[] moveNames){
            return this.GetType().Name;
        }
    }
}
