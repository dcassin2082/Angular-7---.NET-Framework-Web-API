using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using WebApi.Enums;
using WebApi.Models;

namespace WebApi.Helpers
{
    public class DoubleBonusHandInfo
    {
        public static void EvaluateHand(GameModel gameStatus, List<Card> cards, UserAccount userAccountInfo)
        {
            int[] faces = new int[13];
            int[] suits = new int[5];

            if (!gameStatus.GameOver)
                userAccountInfo.Credits -= gameStatus.BetAmount;

            foreach (var card in cards.Take(5).ToList())
            {
                GetCardFaceAndSuit(faces, suits, card);
            }
            RankHand(faces, suits, gameStatus, userAccountInfo);
        }

        private static void GetCardFaceAndSuit(int[] faces, int[] suits, Card card)
        {
            switch (card.RankId)
            {
                case (int)Face.Ace:
                    faces[0]++;
                    break;
                case (int)Face.Deuce:
                    faces[1]++;
                    break;
                case (int)Face.Three:
                    faces[2]++;
                    break;
                case (int)Face.Four:
                    faces[3]++;
                    break;
                case (int)Face.Five:
                    faces[4]++;
                    break;
                case (int)Face.Six:
                    faces[5]++;
                    break;
                case (int)Face.Seven:
                    faces[6]++;
                    break;
                case (int)Face.Eight:
                    faces[7]++;
                    break;
                case (int)Face.Nine:
                    faces[8]++;
                    break;
                case (int)Face.Ten:
                    faces[9]++;
                    break;
                case (int)Face.Jack:
                    faces[10]++;
                    break;
                case (int)Face.Queen:
                    faces[11]++;
                    break;
                case (int)Face.King:
                    faces[12]++;
                    break;
            }
            switch (card.SuitId)
            {
                case (int)Suits.Clubs:
                    suits[0]++;
                    break;
                case (int)Suits.Spades:
                    suits[1]++;
                    break;
                case (int)Suits.Hearts:
                    suits[2]++;
                    break;
                case (int)Suits.Diamonds:
                    suits[3]++;
                    break;
                case (int)Suits.Jokers:
                    suits[4]++;
                    break;
            }
        }

        private static void RankHand(int[] faces, int[] suits, GameModel gameStatus, UserAccount userAccountInfo)
        {
            SmartEntities context = new SmartEntities();
            List<DoubleBonusPayout> paytable = new List<DoubleBonusPayout>();
            DoubleBonusPayout payout = new DoubleBonusPayout();

            if (DoubleBonusHandValue.RankHand(faces, suits) == HandRanking.JacksOrBetter)
            {
                if (gameStatus.GameOver)
                {
                    gameStatus.Message = $"{ApplicationConstants.Winner}{ApplicationConstants.JacksOrBetter}";
                    gameStatus.WinAmount = gameStatus.BetAmount;
                    gameStatus.Credits += gameStatus.WinAmount;
                    userAccountInfo.Credits = gameStatus.Credits;
                    context.Entry(userAccountInfo).State = EntityState.Modified;
                    context.SaveChanges();
                }
                else
                {
                    gameStatus.Message = ApplicationConstants.JacksOrBetter;
                }

            }
            if (DoubleBonusHandValue.RankHand(faces, suits) == HandRanking.TwoPair)
            {
                if (gameStatus.GameOver)
                {
                    gameStatus.Message = $"{ApplicationConstants.Winner}{ApplicationConstants.TwoPair}";
                    gameStatus.WinAmount = gameStatus.BetAmount * 1;
                    gameStatus.Credits += gameStatus.WinAmount;
                    userAccountInfo.Credits = gameStatus.Credits;
                    context.Entry(userAccountInfo).State = EntityState.Modified;
                    context.SaveChanges();
                }
                else
                {
                    gameStatus.Message = ApplicationConstants.TwoPair;
                }
            }
            if (DoubleBonusHandValue.RankHand(faces, suits) == HandRanking.Trips)
            {
                if (gameStatus.GameOver)
                {
                    gameStatus.Message = $"{ApplicationConstants.Winner}{ApplicationConstants.ThreeOfAKind}";
                    gameStatus.WinAmount = gameStatus.BetAmount * 3;
                    gameStatus.Credits += gameStatus.WinAmount;
                    userAccountInfo.Credits = gameStatus.Credits;
                    context.Entry(userAccountInfo).State = EntityState.Modified;
                    context.SaveChanges();
                }
                else
                {
                    gameStatus.Message = ApplicationConstants.ThreeOfAKind;
                }
            }
            if (DoubleBonusHandValue.RankHand(faces, suits) == HandRanking.Straight || DoubleBonusHandValue.RankHand(faces, suits) == HandRanking.BroadwayStraight)
            {
                if (gameStatus.GameOver)
                {
                    gameStatus.Message = $"{ApplicationConstants.Winner}{ApplicationConstants.Straight}";
                    gameStatus.WinAmount = gameStatus.BetAmount * 4;
                    gameStatus.Credits += gameStatus.WinAmount;
                    userAccountInfo.Credits = gameStatus.Credits;
                    context.Entry(userAccountInfo).State = EntityState.Modified;
                    context.SaveChanges();
                }
                else
                {
                    gameStatus.Message = ApplicationConstants.Straight;
                }
            }

            if (DoubleBonusHandValue.RankHand(faces, suits) == HandRanking.Flush)
            {
                if (gameStatus.GameOver)
                {
                    gameStatus.Message = $"{ApplicationConstants.Winner}{ApplicationConstants.Flush}";
                    gameStatus.WinAmount = gameStatus.BetAmount * 5;
                    gameStatus.Credits += gameStatus.WinAmount;
                    userAccountInfo.Credits = gameStatus.Credits;
                    context.Entry(userAccountInfo).State = EntityState.Modified;
                    context.SaveChanges();
                }
                else
                {
                    gameStatus.Message = ApplicationConstants.Flush;
                }
            }
            if (DoubleBonusHandValue.RankHand(faces, suits) == HandRanking.FullHouse)
            {
                if (gameStatus.GameOver)
                {
                    gameStatus.Message = $"{ApplicationConstants.Winner}{ApplicationConstants.FullHouse}";
                    gameStatus.WinAmount = gameStatus.BetAmount * 9;
                    gameStatus.Credits += gameStatus.WinAmount;
                    userAccountInfo.Credits = gameStatus.Credits;
                    context.Entry(userAccountInfo).State = EntityState.Modified;
                    context.SaveChanges();
                }
                else
                {
                    gameStatus.Message = ApplicationConstants.FullHouse;
                }
            }
            if (DoubleBonusHandValue.RankHand(faces, suits) == HandRanking.Quads)
            {
                if (gameStatus.GameOver)
                {
                    gameStatus.Message = $"{ApplicationConstants.Winner}{ApplicationConstants.FourFivesThruKings}";
                    gameStatus.WinAmount = gameStatus.BetAmount * 50;
                    gameStatus.Credits += gameStatus.WinAmount;
                    userAccountInfo.Credits = gameStatus.Credits;
                    context.Entry(userAccountInfo).State = EntityState.Modified;
                    context.SaveChanges();
                }
                else
                {
                    gameStatus.Message = ApplicationConstants.FourFivesThruKings;
                }
            }
            if (DoubleBonusHandValue.RankHand(faces, suits) == HandRanking.StraightFlush)
            {
                if (gameStatus.GameOver)
                {
                    gameStatus.Message = $"{ApplicationConstants.Winner}{ApplicationConstants.StraightFlush}";
                    gameStatus.WinAmount = gameStatus.BetAmount * 50;
                    gameStatus.Credits += gameStatus.WinAmount;
                    userAccountInfo.Credits = gameStatus.Credits;
                    context.Entry(userAccountInfo).State = EntityState.Modified;
                    context.SaveChanges();
                }
                else
                {
                    gameStatus.Message = ApplicationConstants.StraightFlush;
                }
            }
            if (DoubleBonusHandValue.RankHand(faces, suits) == HandRanking.RoyalFlush)
            {
                if (gameStatus.BetAmount == 5)
                {
                    gameStatus.WinAmount = gameStatus.BetAmount * 800;
                    gameStatus.Credits += gameStatus.WinAmount;
                    gameStatus.Message = $"{ApplicationConstants.Winner}{ApplicationConstants.RoyalFlush}";
                    userAccountInfo.Credits = gameStatus.Credits;
                    context.Entry(userAccountInfo).State = EntityState.Modified;
                    context.SaveChanges();
                }
                else if(gameStatus.BetAmount < 5)
                {
                    gameStatus.WinAmount = gameStatus.BetAmount * 250;
                    gameStatus.Credits += gameStatus.WinAmount;
                    gameStatus.Message = $"{ApplicationConstants.Winner}{ApplicationConstants.RoyalFlush}";
                    userAccountInfo.Credits = gameStatus.Credits;
                    context.Entry(userAccountInfo).State = EntityState.Modified;
                    context.SaveChanges();
                }
                else
                {
                    gameStatus.Message = ApplicationConstants.RoyalFlush;
                }
            }
            if (DoubleBonusHandValue.RankHand(faces, suits) == HandRanking.LowQuads)
            {
                if (gameStatus.GameOver)
                {
                    gameStatus.Message = $"{ApplicationConstants.Winner}{ApplicationConstants.FourTwosThreesFours}";
                    gameStatus.WinAmount = gameStatus.BetAmount * 80;
                    gameStatus.Credits += gameStatus.WinAmount;
                    userAccountInfo.Credits = gameStatus.Credits;
                    context.Entry(userAccountInfo).State = EntityState.Modified;
                    context.SaveChanges();
                }
                else
                {
                    gameStatus.Message = ApplicationConstants.FourTwosThreesFours;
                }
            }
            if (DoubleBonusHandValue.RankHand(faces, suits) == HandRanking.LowQuadsWithAce)
            {
                if (gameStatus.GameOver)
                {
                    gameStatus.Message = $"{ApplicationConstants.Winner}{ApplicationConstants.FourTwosThreesFoursWithAce}";
                    gameStatus.WinAmount = gameStatus.BetAmount * 160;
                    gameStatus.Credits += gameStatus.WinAmount;
                    userAccountInfo.Credits = gameStatus.Credits;
                    context.Entry(userAccountInfo).State = EntityState.Modified;
                    context.SaveChanges();
                }
                else
                {
                    gameStatus.Message = ApplicationConstants.FourTwosThreesFoursWithAce;
                }
            }
            if (DoubleBonusHandValue.RankHand(faces, suits) == HandRanking.QuadAces)
            {
                if (gameStatus.GameOver)
                {
                    gameStatus.Message = $"{ApplicationConstants.Winner}{ApplicationConstants.FourAces}";
                    gameStatus.WinAmount = gameStatus.BetAmount * 160;
                    gameStatus.Credits += gameStatus.WinAmount;
                    userAccountInfo.Credits = gameStatus.Credits;
                    context.Entry(userAccountInfo).State = EntityState.Modified;
                    context.SaveChanges();
                }
                else
                {
                    gameStatus.Message = ApplicationConstants.FourAces;
                }
            }
            if (DoubleBonusHandValue.RankHand(faces, suits) == HandRanking.QuadAcesLowKicker)
            {
                if (gameStatus.GameOver)
                {
                    gameStatus.Message = $"{ApplicationConstants.Winner}{ApplicationConstants.FourAcesWithAnyTwoThreeFour}";
                    gameStatus.WinAmount = gameStatus.BetAmount * 400;
                    gameStatus.Credits += gameStatus.WinAmount;
                    userAccountInfo.Credits = gameStatus.Credits;
                    context.Entry(userAccountInfo).State = EntityState.Modified;
                    context.SaveChanges();
                }
                else
                {
                    gameStatus.Message = ApplicationConstants.FourAcesWithAnyTwoThreeFour;
                }
            }
            context.Entry(userAccountInfo).State = EntityState.Modified;
            gameStatus.Credits = (int)userAccountInfo.Credits;
            context.SaveChanges();
        }
    }
}