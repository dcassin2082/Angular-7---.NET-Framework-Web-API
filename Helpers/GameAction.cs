using System;
using System.Collections.Generic;
using System.Linq;

namespace WebApi.Helpers
{
    public class GameAction
    {
        public static List<int> ShuffleDeck()
        {
            List<int> cards = new List<int>();
            int count = 0;
            Random r = new Random();
            do
            {
                int card = r.Next(1, 53);
                if (!cards.Contains(card))
                {
                    cards.Add(card);
                    count++;
                }
            } while (count < 52);
            return cards.Take(10).ToList();
        }

        public static List<int> ShuffleDeckWithJokers()
        {
            List<int> cards = new List<int>();
            int count = 0;
            Random r = new Random();
            do
            {
                int card = r.Next(1, 55);
                if (!cards.Contains(card))
                {
                    cards.Add(card);
                    count++;
                }
            } while (count < 54);
            return cards.Take(10).ToList();
        }

        public static List<int> ShuffleHoldem()
        {
            List<int> cards = new List<int>();
            int count = 0;
            Random r = new Random();
            do
            {
                int card = r.Next(1, 53);
                if (!cards.Contains(card))
                {
                    cards.Add(card);
                    count++;
                }
            } while (count < 52);
            return cards.Take(25).ToList();
        }
    }
}