using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WebApi.Enums;
using WebApi.Models;

namespace WebApi.Helpers
{
    public class DeucesWildHandInfo
    {
        public static void EvaluateHand(GameModel gameStatus, List<CardDeucesWild> cards, UserAccount userAccountInfo)
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

        private static void GetCardFaceAndSuit(int[] faces, int[] suits, CardDeucesWild card)
        {
            switch (card.RankId)
            {
                case (int)DeucesWildFaces.Deuce:
                    faces[0]++;
                    break;
                case (int)DeucesWildFaces.Ace:
                    faces[1]++;
                    break;
                case (int)Face.Three:
                    faces[2]++;
                    break;
                case (int)DeucesWildFaces.Four:
                    faces[3]++;
                    break;
                case (int)DeucesWildFaces.Five:
                    faces[4]++;
                    break;
                case (int)DeucesWildFaces.Six:
                    faces[5]++;
                    break;
                case (int)DeucesWildFaces.Seven:
                    faces[6]++;
                    break;
                case (int)DeucesWildFaces.Eight:
                    faces[7]++;
                    break;
                case (int)DeucesWildFaces.Nine:
                    faces[8]++;
                    break;
                case (int)DeucesWildFaces.Ten:
                    faces[9]++;
                    break;
                case (int)DeucesWildFaces.Jack:
                    faces[10]++;
                    break;
                case (int)DeucesWildFaces.Queen:
                    faces[11]++;
                    break;
                case (int)DeucesWildFaces.King:
                    faces[12]++;
                    break;
            }
            switch (card.SuitId)
            {
                case (int)DeucesWildSuits.Clubs:
                    suits[0]++;
                    break;
                case (int)DeucesWildSuits.Diamonds:
                    suits[1]++;
                    break;
                case (int)DeucesWildSuits.Hearts:
                    suits[2]++;
                    break;
                case (int)DeucesWildSuits.Spades:
                    suits[3]++;
                    break;
                case (int)DeucesWildSuits.Deuces:
                    suits[4]++;
                    break;
            }
        }

        private static void RankHand(int[] faces, int[] suits, GameModel gameStatus, UserAccount userAccountInfo)
        {
            SmartEntities context = new SmartEntities();
            if (DeucesWildHandValue.Trips(faces, suits))
            {
                if (gameStatus.GameOver)
                {
                    gameStatus.Message = "WINNER - 3 OF A KIND";
                    gameStatus.WinAmount = gameStatus.BetAmount;
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
            if (DeucesWildHandValue.RankHand(faces, suits) == HandRanking.Straight || DeucesWildHandValue.RankHand(faces, suits) == HandRanking.BroadwayStraight)
            {
                if (gameStatus.GameOver)
                {
                    gameStatus.Message = "WINNER - STRAIGHT";
                    gameStatus.WinAmount = gameStatus.BetAmount * 2;
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

            if (DeucesWildHandValue.RankHand(faces, suits) == HandRanking.Flush)
            {
                if (gameStatus.GameOver)
                {
                    gameStatus.Message = "WINNER - FLUSH";
                    gameStatus.WinAmount = gameStatus.BetAmount * 2;
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
            if (DeucesWildHandValue.RankHand(faces, suits) == HandRanking.FullHouse)
            {
                if (gameStatus.GameOver)
                {
                    gameStatus.Message = "WINNER - FULL HOUSE";
                    gameStatus.WinAmount = gameStatus.BetAmount * 3;
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
            if (DeucesWildHandValue.RankHand(faces, suits) == HandRanking.Quads)
            {
                if (gameStatus.GameOver)
                {
                    gameStatus.Message = "WINNER - 4 OF A KIND";
                    gameStatus.WinAmount = gameStatus.BetAmount * 4;
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
            if (DeucesWildHandValue.RankHand(faces, suits) == HandRanking.StraightFlush)
            {
                if (gameStatus.GameOver)
                {
                    gameStatus.Message = "WINNER - STRAIGHT FLUSH";
                    gameStatus.WinAmount = gameStatus.BetAmount * 9;
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

            if (DeucesWildHandValue.RankHand(faces, suits) == HandRanking.WildRoyalFlush)
            {
                if (gameStatus.GameOver)
                {
                    gameStatus.WinAmount = gameStatus.BetAmount * 25;
                    gameStatus.Credits += gameStatus.WinAmount;
                    gameStatus.Message = "WINNER - WILD ROYAL FLUSH";
                    gameStatus.HandName = "Wild Royal Flush";
                    userAccountInfo.Credits = gameStatus.Credits;
                    context.Entry(userAccountInfo).State = EntityState.Modified;
                    context.SaveChanges();
                }
                else
                {
                    gameStatus.Message = "WILD ROYAL FLUSH";
                }
            }
            if (DeucesWildHandValue.RankHand(faces, suits) == HandRanking.FiveOfAKind)
            {
                if (gameStatus.GameOver)
                {
                    gameStatus.Message = "WINNER - 5 OF A KIND";
                    gameStatus.WinAmount = gameStatus.BetAmount * 15;
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
            if (DeucesWildHandValue.RankHand(faces, suits) == HandRanking.FourDeuces)
            {
                if (gameStatus.GameOver)
                {
                    gameStatus.Message = "WINNER - 4 DEUCES";
                    gameStatus.WinAmount = gameStatus.BetAmount * 200;
                    gameStatus.Credits += gameStatus.WinAmount;
                    gameStatus.HandName = "Five of a Kind";
                    userAccountInfo.Credits = gameStatus.Credits;
                    context.Entry(userAccountInfo).State = EntityState.Modified;
                    context.SaveChanges();
                }
                else
                {
                    gameStatus.Message = "4 DEUCES";
                }
            }
            if (DeucesWildHandValue.RankHand(faces, suits) == HandRanking.RoyalFlush)
            {
                if (gameStatus.GameOver)
                {
                    if (gameStatus.BetAmount == 5)
                    {
                        gameStatus.WinAmount = gameStatus.BetAmount * 800;
                        gameStatus.Credits += gameStatus.WinAmount;
                        gameStatus.Message = "WINNER - ROYAL FLUSH";
                        gameStatus.HandName = "Royal Flush";
                        userAccountInfo.Credits = gameStatus.Credits;
                        context.Entry(userAccountInfo).State = EntityState.Modified;
                        context.SaveChanges();
                    }
                    else
                    {
                        gameStatus.WinAmount = gameStatus.BetAmount * 250;
                        gameStatus.Credits += gameStatus.WinAmount;
                        gameStatus.Message = "WINNER - ROYAL FLUSH";
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