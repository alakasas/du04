using System;
using System.Globalization;
using System.Collections.Generic;

namespace pb006
{
    public abstract class Strategy
    {
        protected int[] moves;

        public Strategy(int[] moves)
        {
            this.moves = moves;
        }

        public abstract int GetMove(int? move, Outcome? result);

        public bool? IsHistoryBased()
        {
            return null;
        }

        public virtual string GetName(string[] moveNames){
            string sentence = "[";
            foreach (int moveIdx in moves){
                sentence += moveNames[moveIdx] + ", ";
            }
            return this.GetType().Name + " " + sentence[..^2] + "]";
        }
    }

    class LinearStrategy : Strategy
    {
        private int lastMoveIdx = -1;

        public LinearStrategy(int[] moves) : base(moves)
        {

        }

        public override int GetMove(int? move, Outcome? result)
        {
            lastMoveIdx = (lastMoveIdx + 1) % moves.Length;
            return moves[lastMoveIdx];
        }

        public new bool? IsHistoryBased()
        {
            return false;
        }
    }

    class TrivialStrategy : LinearStrategy
    {
        public TrivialStrategy(int move) : base((new int[] {move}))
        {
        }

    }

    public abstract class HistoryBasedStrategy : Strategy
    {
        protected List<int> myMoves = new List<int> {};
        protected List<int> theirMoves = new List<int> {};
        protected List<Outcome> conclusions = new List<Outcome> {};

        public HistoryBasedStrategy(int[] moves) : base(moves)
        {
        }

        public new bool? IsHistoryBased()
        {
            return true;
        }

        public string GetHistory(){
            string sentence = "";
            for (int i = 0; i < conclusions.Count; i++){
                sentence += "(" + (myMoves[i]).ToString() + "x" + (theirMoves[i]).ToString()
                            + ": " + conclusions[i].ToString() + ")";
            }
            return sentence;
        }
    }

    class DelayedStrategy : HistoryBasedStrategy
    {
        private int delay;

        public DelayedStrategy(int[] moves, int delay) : base(moves)
        {
            this.delay = delay;
        }

        public override int GetMove(int? move, Outcome? result){
            if (move != null && result != null){
                theirMoves.Add(move.Value);
                conclusions.Add(result.Value);
            }

            int curentMove = theirMoves.Count < delay ? moves[myMoves.Count] : theirMoves[theirMoves.Count - delay];

            myMoves.Add(curentMove);
            return curentMove;
        }

        public override string GetName(string[] moveNames){
            return base.GetName(moveNames) + " (delay: " + delay.ToString() + ")";
        }

    }

    public class LastWinningForOpponent : HistoryBasedStrategy
    {
        public LastWinningForOpponent(int[] moves) : base(moves)
        {
        }

        public override int GetMove(int? move, Outcome? result){
            if (move != null && result != null){
                theirMoves.Add(move.Value);
                conclusions.Add(result.Value);
            }

            int? curentMove = null;

            for (int i = conclusions.Count - 1; i>=0; i--){
                if (conclusions[i] == Outcome.DEFEAT){
                    curentMove = theirMoves[i];
                    break;
                }
            }
            if (curentMove == null){
                curentMove = moves[myMoves.Count];
            }

            myMoves.Add(curentMove.Value);
            return curentMove.Value;
        }

    }

    public class RockPaperScissors : Game
    {
        public RockPaperScissors() :
                base(new string[] {"rock", "paper", "scissors"},
                     new Outcome[,] {{Outcome.DRAW, Outcome.DEFEAT, Outcome.VICTORY},
                                     {Outcome.VICTORY, Outcome.DRAW, Outcome.DEFEAT},
                                     {Outcome.DEFEAT, Outcome.VICTORY, Outcome.DRAW}})
        {
        }

    }
    public class SpockLizard : Game
    {
        public SpockLizard() :
                base(new string[] {"rock", "paper", "scissors", "spock", "lizard"},
                     new Outcome[,] {{Outcome.DRAW, Outcome.DEFEAT, Outcome.VICTORY, Outcome.DEFEAT, Outcome.VICTORY},
                                     {Outcome.VICTORY, Outcome.DRAW, Outcome.DEFEAT, Outcome.VICTORY, Outcome.DEFEAT},
                                     {Outcome.DEFEAT, Outcome.VICTORY, Outcome.DRAW, Outcome.DEFEAT, Outcome.VICTORY},
                                     {Outcome.VICTORY, Outcome.DEFEAT, Outcome.VICTORY, Outcome.DRAW, Outcome.DEFEAT},
                                     {Outcome.DEFEAT, Outcome.VICTORY, Outcome.DEFEAT, Outcome.VICTORY, Outcome.DRAW}})
        {
        }

    }

}
