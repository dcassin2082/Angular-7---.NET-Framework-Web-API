using System.Collections.Generic;

namespace WebApi.Models
{
    public class GameModel
    {
        public List<Card> Hand { get; set; }

        public List<DoubleBonusPayout> DoubleBonusPaytable { get; set; }

        public List<DeucesWildPayout> DeucesWildPaytable { get; set; }

        public List<JokersWildPayout> JokersWildPaytable { get; set; }

        public string Message { get; set; }

        public bool GameOver { get; set; }

        public int Credits { get; set; }

        public int BetAmount { get; set; }

        public int WinAmount { get; set; }

        public string HandName { get; set; }

        public int? Bet1Pays { get; set; }

        public int? Bet2Pays { get; set; }

        public int? Bet3Pays { get; set; }

        public int? Bet4Pays { get; set; }

        public int? Bet5Pays { get; set; }
    }
}