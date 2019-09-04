using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApi.Helpers
{
    public class ApplicationConstants
    {
        public const string HoldDrawMessage = "HOLD / DRAW 1 TO 5 CARDS";
        public const string GameOver = "GAME OVER";
        public const string GoodLuck = "GOOD LUCK";
        public const string JacksOrBetter = "JACKS OR BETTER";
        public const string TwoPair = "TWO PAIR";
        public const string ThreeOfAKind = "3 OF A KIND";
        public const string Straight = "STRAIGHT";
        public const string Flush = "FLUSH";
        public const string FullHouse = "FULL HOUSE";
        public const string FourFivesThruKings = "4 FIVES THRU KINGS";
        public const string StraightFlush = "STRAIGHT FLUSH";
        public const string RoyalFlush = "ROYAL FLUSH";
        public const string FourTwosThreesFours = "4 2s, 3s, 4s";
        public const string FourTwosThreesFoursWithAce = "4 2s, 3s, 4s w/ACE";
        public const string FourAces = "4 ACES";
        public const string FourAcesWithAnyTwoThreeFour = "4 ACES w/ANY 2, 3, 4";
        public const string Winner = "WINNER - ";
        public const string SqlExec = "exec addUserAccount";
    }
}