using System.Collections.Generic;
using System.Linq;
using WebApi.Helpers;
using WebApi.Models;

namespace WebApi.Services
{
    public class GameService : ServicesBase, IGameService
    {
        private List<Card> _dealCards;
        private List<Card> _drawCards;
        private UserAccount _userAccountInfo;
        private readonly string _username = "dcassin5938";

        public GameService()
        {
            _dealCards = new List<Card>();
            _drawCards = new List<Card>();
            _userAccountInfo = dbContext.UserAccounts.Where(u => u.Username == _username).FirstOrDefault();
        }

        public GameModel Deal(int bet)
        {
            List<int> shuffle = GameAction.ShuffleDeck();
            _dealCards = new List<Card>();
            foreach (var item in shuffle)
            {
                var card = dbContext.Cards.Where(c => c.CardId == item).FirstOrDefault();
                _dealCards.Add(card);
            }
            GameModel model = new GameModel
            {
                Message = ApplicationConstants.HoldDrawMessage,
                Hand = _dealCards.Take(5).ToList(),
                GameOver = false,
                BetAmount = bet,
                DoubleBonusPaytable = dbContext.DoubleBonusPayouts.ToList(),
                Credits = (int)_userAccountInfo.Credits - bet
            };
            HandInfo.EvaluateHand(model, _dealCards, _userAccountInfo);
            if (model.Credits <= 0)
            {
                model.Credits = 100;
                _userAccountInfo.Credits = 100;
                dbContext.SaveChanges();
            }
            model.DoubleBonusPaytable = model.DoubleBonusPaytable.OrderByDescending(e => e.Id).ToList();
            return model;
        }

        public GameModel Draw(int[] arr, int bet)
        {
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
                Hand = _drawCards,
                GameOver = true,
                Credits = (int)_userAccountInfo.Credits,
                BetAmount = bet,
                DoubleBonusPaytable = dbContext.DoubleBonusPayouts.ToList()
            };
            HandInfo.EvaluateHand(model, _drawCards, _userAccountInfo);
            if (model.Credits <= 0)
            {
                model.Credits = 100;
                _userAccountInfo.Credits = 100;
                dbContext.SaveChanges();
            }
            return model;
        }

        public GameModel GetPaytable()
        {
            _userAccountInfo = dbContext.UserAccounts.Where(u => u.Username == _username).FirstOrDefault();
            GameModel model = new GameModel
            {
                Message = ApplicationConstants.GoodLuck,
                GameOver = false,
                DoubleBonusPaytable = dbContext.DoubleBonusPayouts.OrderByDescending(e => e.Id).ToList(),
                Credits = (int)_userAccountInfo.Credits
            };
            if (model.Credits <= 0)
            {
                model.Credits = 100;
                _userAccountInfo.Credits = 100;
                dbContext.SaveChanges();
            }
            return model;
        }
    }
}