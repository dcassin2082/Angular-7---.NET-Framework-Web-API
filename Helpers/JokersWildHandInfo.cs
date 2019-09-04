using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WebApi.Enums;
using WebApi.Models;

namespace WebApi.Helpers
{
    public class JokersWildHandInfo
    {
        public static void EvaluateHand(GameModel gameStatus, List<Card> cards, UserAccount userAccountInfo)
        {
            int[] faces = new int[14];
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
                case (int)Face.Joker:
                    faces[13]++;
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
            if (JokersWildHandValue.KingsOrBetter(faces))
            {
                if (gameStatus.GameOver)
                {
                    gameStatus.Message = "WINNER - PAIR OF KINGS OR BETTER";
                    gameStatus.WinAmount = gameStatus.BetAmount;
                    gameStatus.Credits += gameStatus.WinAmount;
                    gameStatus.HandName = "Kings or Better";
                    userAccountInfo.Credits = gameStatus.Credits;
                    context.Entry(userAccountInfo).State = EntityState.Modified;
                    context.SaveChanges();
                }
                else
                {
                    gameStatus.Message = "PAIR OF KINGS OR BETTER";
                }

            }
            if (JokersWildHandValue.RankHand(faces, suits) == HandRanking.TwoPair)
            {
                if (gameStatus.GameOver)
                {
                    gameStatus.Message = "WINNER - TWO PAIR";
                    gameStatus.WinAmount = gameStatus.BetAmount;
                    gameStatus.Credits += gameStatus.WinAmount;
                    gameStatus.HandName = "Two Pair";
                    userAccountInfo.Credits = gameStatus.Credits;
                    context.Entry(userAccountInfo).State = EntityState.Modified;
                    context.SaveChanges();
                }
                else
                {
                    gameStatus.Message = "TWO PAIR";
                }
            }
            if (JokersWildHandValue.Trips(faces))
            {
                if (gameStatus.GameOver)
                {
                    gameStatus.Message = "WINNER - 3 OF A KIND";
                    gameStatus.WinAmount = gameStatus.BetAmount * 2;
                    gameStatus.Credits += gameStatus.WinAmount;
                    gameStatus.HandName = "Three of a Kind";
                    userAccountInfo.Credits = gameStatus.Credits;
                    context.Entry(userAccountInfo).State = EntityState.Modified;
                    context.SaveChanges();
                }
                else
                {
                    gameStatus.Message = "3 OF A KIND";
                }
            }
            if (JokersWildHandValue.RankHand(faces, suits) == HandRanking.Straight || JokersWildHandValue.RankHand(faces, suits) == HandRanking.BroadwayStraight)
            {
                if (gameStatus.GameOver)
                {
                    gameStatus.Message = "WINNER - STRAIGHT";
                    gameStatus.WinAmount = gameStatus.BetAmount * 3;
                    gameStatus.Credits += gameStatus.WinAmount;
                    gameStatus.HandName = "Straight";
                    userAccountInfo.Credits = gameStatus.Credits;
                    context.Entry(userAccountInfo).State = EntityState.Modified;
                    context.SaveChanges();
                }
                else
                {
                    gameStatus.Message = "STRAIGHT";
                }
            }

            if (JokersWildHandValue.RankHand(faces, suits) == HandRanking.Flush)
            {
                if (gameStatus.GameOver)
                {
                    gameStatus.Message = "WINNER - FLUSH";
                    gameStatus.WinAmount = gameStatus.BetAmount * 5;
                    gameStatus.Credits += gameStatus.WinAmount;
                    gameStatus.HandName = "Flush";
                    userAccountInfo.Credits = gameStatus.Credits;
                    context.Entry(userAccountInfo).State = EntityState.Modified;
                    context.SaveChanges();
                }
                else
                {
                    gameStatus.Message = "FLUSH";
                }
            }
            if (JokersWildHandValue.RankHand(faces, suits) == HandRanking.FullHouse)
            {
                if (gameStatus.GameOver)
                {
                    gameStatus.Message = "WINNER - FULL HOUSE";
                    gameStatus.WinAmount = gameStatus.BetAmount * 7;
                    gameStatus.Credits += gameStatus.WinAmount;
                    gameStatus.HandName = "Full House";
                    userAccountInfo.Credits = gameStatus.Credits;
                    context.Entry(userAccountInfo).State = EntityState.Modified;
                    context.SaveChanges();
                }
                else
                {
                    gameStatus.Message = "FULL HOUSE";
                }
            }
            if (JokersWildHandValue.RankHand(faces, suits) == HandRanking.Quads)
            {
                if (gameStatus.GameOver)
                {
                    gameStatus.Message = "WINNER - 4 OF A KIND";
                    gameStatus.WinAmount = gameStatus.BetAmount * 17;
                    gameStatus.Credits += gameStatus.WinAmount;
                    gameStatus.HandName = "Four of a Kind";
                    userAccountInfo.Credits = gameStatus.Credits;
                    context.Entry(userAccountInfo).State = EntityState.Modified;
                    context.SaveChanges();
                }
                else
                {
                    gameStatus.Message = "4 OF A KIND";
                }
            }
            if (JokersWildHandValue.RankHand(faces, suits) == HandRanking.StraightFlush)
            {
                if (gameStatus.GameOver)
                {
                    gameStatus.Message = "WINNER - STRAIGHT FLUSH";
                    gameStatus.WinAmount = gameStatus.BetAmount * 50;
                    gameStatus.Credits += gameStatus.WinAmount;
                    gameStatus.HandName = "Straight Flush";
                    userAccountInfo.Credits = gameStatus.Credits;
                    context.Entry(userAccountInfo).State = EntityState.Modified;
                    context.SaveChanges();
                }
                else
                {
                    gameStatus.Message = "STRAIGHT FLUSH";
                }
            }

            if (JokersWildHandValue.RankHand(faces, suits) == HandRanking.WildRoyalFlush)
            {
                if (gameStatus.GameOver)
                {
                    gameStatus.WinAmount = gameStatus.BetAmount * 100;
                    gameStatus.Credits += gameStatus.WinAmount;
                    gameStatus.Message = "WINNER - ROYAL FLUSH WITH JOKER";
                    gameStatus.HandName = "Wild Royal Flush";
                    userAccountInfo.Credits = gameStatus.Credits;
                    context.Entry(userAccountInfo).State = EntityState.Modified;
                    context.SaveChanges();
                }
                else
                {
                    gameStatus.Message = "ROYAL FLUSH WITH JOKER";
                }
            }
            if (JokersWildHandValue.RankHand(faces, suits) == HandRanking.FiveOfAKind)
            {
                if (gameStatus.GameOver)
                {
                    gameStatus.Message = "WINNER - 5 OF A KIND";
                    gameStatus.WinAmount = gameStatus.BetAmount * 200;
                    gameStatus.Credits += gameStatus.WinAmount;
                    gameStatus.HandName = "Five of a Kind";
                    userAccountInfo.Credits = gameStatus.Credits;
                    context.Entry(userAccountInfo).State = EntityState.Modified;
                    context.SaveChanges();
                }
                else
                {
                    gameStatus.Message = "5 OF A KIND";
                }
            }

            if (JokersWildHandValue.RankHand(faces, suits) == HandRanking.RoyalFlush)
            {
                if (gameStatus.GameOver)
                {
                    if (gameStatus.BetAmount == 5)
                    {
                        gameStatus.WinAmount = gameStatus.BetAmount * 800;
                        gameStatus.Credits += gameStatus.WinAmount;
                        gameStatus.Message = "WINNER - ROYAL FLUSH NO JOKERS";
                        gameStatus.HandName = "Royal Flush";
                        userAccountInfo.Credits = gameStatus.Credits;
                        context.Entry(userAccountInfo).State = EntityState.Modified;
                        context.SaveChanges();
                    }
                    else
                    {
                        gameStatus.WinAmount = gameStatus.BetAmount * 250;
                        gameStatus.Credits += gameStatus.WinAmount;
                        gameStatus.Message = "WINNER - ROYAL FLUSH NO JOKERS";
                        gameStatus.HandName = "Royal Flush";
                        userAccountInfo.Credits = gameStatus.Credits;
                        context.Entry(userAccountInfo).State = EntityState.Modified;
                        context.SaveChanges();
                    }
                }
                else
                {
                    gameStatus.Message = "ROYAL FLUSH";
                }
            }
            context.Entry(userAccountInfo).State = EntityState.Modified;
            gameStatus.Credits = (int)userAccountInfo.Credits;
            context.SaveChanges();
        }
    }
}