using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApi.Helpers;
using WebApi.Models;

namespace WebApi.Services
{
    public class DeucesWildService : ServicesBase, IDeucesWildService
    {
        private static List<CardDeucesWild> _dealCards;
        private static List<CardDeucesWild> _drawCards;

        public GameModel Deal(int bet, string username)
        {
            // 48 51 35 27 50 
            _dealCards = new List<CardDeucesWild>();
            UserAccount userAccount = dbContext.UserAccounts.Where(u => u.Username == username).FirstOrDefault();
            List<int> shuffle = GameAction.ShuffleDeck();
            foreach (var item in shuffle)
            {
                var card = dbContext.CardDeucesWilds.Where(c => c.CardId == item).FirstOrDefault();
                _dealCards.Add(card);
            }
            GameModel model = new GameModel
            {
                Message = ApplicationConstants.HoldDrawMessage,
                HandDeucesWild = _dealCards.Take(5).ToList(),
                GameOver = false,
                BetAmount = bet,
                DeucesWildPaytable = dbContext.DeucesWildPayouts.ToList(),
                Credits = (int)userAccount.Credits - bet
            };
            DeucesWildHandInfo.EvaluateHand(model, _dealCards, userAccount);
            if (model.Credits <= 0)
            {
                model.Credits = 100;
                userAccount.Credits = 100;
                dbContext.SaveChanges();
            }
            model.DeucesWildPaytable = model.DeucesWildPaytable.OrderByDescending(e => e.Id).ToList();
            return model;
        }

        public GameModel Draw(int[] arr, int bet, string username)
        {
            _drawCards = new List<CardDeucesWild>();
            UserAccount userAccount = dbContext.UserAccounts.Where(u => u.Username == username).FirstOrDefault();
            for (int i = 0; i < arr.Length; i++)
            {
                if (arr[i] == 0)
                {
                    _drawCards.Add(_dealCards[i + 5]);
                }
                else
                {
                    _drawCards.Add(_dealCards[i]);
                }
            }
            GameModel model = new GameModel
            {
                Message = ApplicationConstants.GameOver,
                HandDeucesWild = _drawCards,
                GameOver = true,
                Credits = (int)userAccount.Credits,
                BetAmount = bet,
                DeucesWildPaytable = dbContext.DeucesWildPayouts.ToList()
            };
            DeucesWildHandInfo.EvaluateHand(model, _drawCards, userAccount);
            if (model.Credits <= 0)
            {
                model.Credits = 100;
                userAccount.Credits = 100;
                dbContext.SaveChanges();
            }
            return model;
        }

        public GameModel GetPaytable(string username)
        {
            UserAccount userAccount = dbContext.UserAccounts.Where(u => u.Username == username).FirstOrDefault();
            GameModel model = new GameModel
            {
                Message = ApplicationConstants.GoodLuck,
                GameOver = false,
                DeucesWildPaytable = dbContext.DeucesWildPayouts.OrderByDescending(e => e.Id).ToList(),
                Credits = (int)userAccount.Credits
            };
            if (model.Credits <= 0)
            {
                model.Credits = 100;
                userAccount.Credits = 100;
                dbContext.SaveChanges();
            }
            return model;
        }
    }
}