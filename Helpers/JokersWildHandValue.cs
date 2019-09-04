using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApi.Enums;

namespace WebApi.Helpers
{
    public class JokersWildHandValue
    {
        public static HandRanking RankHand(int[] faces, int[] suits)
        {
            if (AnyPair(faces))
            {
                if (FiveOfAKind(faces))
                    return HandRanking.FiveOfAKind;
                else if (Quads(faces))
                    return HandRanking.Quads;
                else if (FullHouse(faces))
                    return HandRanking.FullHouse;
                else if (Trips(faces))
                    return HandRanking.Trips;
                else if (TwoPair(faces))
                    return HandRanking.TwoPair;
            }
            else
            {
                if (RoyalFlush(suits, faces))
                    return HandRanking.RoyalFlush;
                if (WildRoyalFlush(suits, faces))
                    return HandRanking.WildRoyalFlush;
                if (StraightFlush(suits, faces))
                    return HandRanking.StraightFlush;
                if (Flush(suits))
                    return HandRanking.Flush;
                if (BroadwayStraight(faces))
                    return HandRanking.BroadwayStraight;
                if (Straight(faces))
                    return HandRanking.Straight;
            }
            return HandRanking.Nothing;
        }

        public static bool AnyPair(int[] faces)
        {
            for (int i = 0; i < faces.Length - 1; i++)
            {
                if (faces[i] > 1)
                    return true;
            }
            return false;
        }

        public static bool KingsOrBetter(int[] faces)
        {
            if (faces[(int)Face.Joker] == 1)
            {
                if (!AnyPair(faces))
                {
                    if (faces[(int)Face.King] == 1 || faces[(int)Face.Ace] == 1)
                        return true;
                }
            }
            else
            {
                if (faces[(int)Face.Ace] == 2 || faces[(int)Face.King] == 2)
                    return true;
            }
            return false;
        }

        public static bool TwoPair(int[] faces)
        {
            if (faces[(int)Face.Joker] == 0)
            {
                for (int i = 0; i < faces.Length - 1; i++)
                {
                    for (int j = 0; j < faces.Length - 1; j++)
                    {
                        if (faces[i] == 2)
                        {
                            if (!(j == i))
                            {
                                if (faces[j] == 2)
                                    return true;
                            }
                        }
                    }
                }
            }
            return false;
        }

        public static bool Trips(int[] faces)
        {
            if (faces[(int)Face.Joker] == 1)
            {
                for (int i = 0; i < faces.Length - 1; i++)
                {
                    if (faces[i] == 2)
                        return true;
                }
            }
            else if (faces[(int)Face.Joker] == 2)
            {
                for (int i = 0; i < faces.Length - 1; i++)
                {
                    if (faces[i] == 1)
                        return true;
                }
            }
            else
            {
                for (int i = 0; i < faces.Length - 1; i++)
                {
                    if (faces[i] == 3)
                        return true;
                }
            }
            return false;
        }

        public static bool Straight(int[] faces)
        {
            if (faces[(int)Face.Joker] > 0)
            {
                for (int i = 0; i < faces.Length - 1; i++)
                {
                    if (faces[i] > 1)
                        return false;
                }
                int[] arrayBounds = new int[13];
                for (int i = 0; i < faces.Length - 1; i++)
                    arrayBounds[i] = faces[i];
                int upperBound = Array.LastIndexOf(arrayBounds, 1);
                int lowerBound = Array.IndexOf(arrayBounds, 1);
                if (upperBound - lowerBound <= 4)
                    return true;
            }
            else
            {
                for (int i = 0; i < faces.Length - 1; i++)
                {
                    if (faces[i] > 1)
                        return false;
                }
                for (int i = 0; i <= faces.Length - 6; i++)
                {
                    if (faces[i] == 1)
                        if (faces[i + 1] == 1)
                            if (faces[i + 2] == 1)
                                if (faces[i + 3] == 1)
                                    if (faces[i + 4] == 1)
                                        return true;

                }
            }
            return false;
        }

        public static bool BroadwayStraight(int[] faces)
        {
            if (faces[(int)Face.Joker] == 1)
            {
                if (faces[(int)Face.Ten] == 0)
                {
                    if (faces[(int)Face.Ace] == 1)
                        if (faces[(int)Face.Jack] == 1)
                            if (faces[(int)Face.Queen] == 1)
                                if (faces[(int)Face.King] == 1)
                                    return true;
                }
                else if (faces[(int)Face.Jack] == 0)
                {
                    if (faces[(int)Face.Ace] == 1)
                        if (faces[(int)Face.Ten] == 1)
                            if (faces[(int)Face.Queen] == 1)
                                if (faces[(int)Face.King] == 1)
                                    return true;
                }
                else if (faces[(int)Face.Queen] == 0)
                {
                    if (faces[(int)Face.Ace] == 1)
                        if (faces[(int)Face.Ten] == 1)
                            if (faces[(int)Face.Jack] == 1)
                                if (faces[(int)Face.King] == 1)
                                    return true;
                }
                else if (faces[(int)Face.King] == 0)
                {
                    if (faces[(int)Face.Ace] == 1)
                        if (faces[(int)Face.Ten] == 1)
                            if (faces[(int)Face.Jack] == 1)
                                if (faces[(int)Face.Queen] == 1)
                                    return true;
                }
                else if (faces[(int)Face.Ace] == 0)
                {
                    if (faces[(int)Face.Ten] == 1)
                        if (faces[(int)Face.Jack] == 1)
                            if (faces[(int)Face.Queen] == 1)
                                if (faces[(int)Face.King] == 1)
                                    return true;
                }
            }
            else if (faces[(int)Face.Joker] == 2)
            {
                if (faces[(int)Face.Ten] == 1 && faces[(int)Face.Jack] == 1 && faces[(int)Face.Queen] == 1)
                    return true;
                else if (faces[(int)Face.Ten] == 1 && faces[(int)Face.Jack] == 1 && faces[(int)Face.King] == 1)
                    return true;
                else if (faces[(int)Face.Ten] == 1 && faces[(int)Face.Jack] == 1 && faces[(int)Face.Ace] == 1)
                    return true;
                else if (faces[(int)Face.Ten] == 1 && faces[(int)Face.Queen] == 1 && faces[(int)Face.King] == 1)
                    return true;
                else if (faces[(int)Face.Ten] == 1 && faces[(int)Face.Queen] == 1 && faces[(int)Face.Ace] == 1)
                    return true;
                else if (faces[(int)Face.Ten] == 1 && faces[(int)Face.King] == 1 && faces[(int)Face.Ace] == 1)
                    return true;
                else if (faces[(int)Face.Jack] == 1 && faces[(int)Face.Queen] == 1 && faces[(int)Face.King] == 1)
                    return true;
                else if (faces[(int)Face.Jack] == 1 && faces[(int)Face.Queen] == 1 && faces[(int)Face.Ace] == 1)
                    return true;
                else if (faces[(int)Face.Jack] == 1 && faces[(int)Face.King] == 1 && faces[(int)Face.Ace] == 1)
                    return true;
                else if (faces[(int)Face.Queen] == 1 && faces[(int)Face.King] == 1 && faces[(int)Face.Ace] == 1)
                    return true;
            }
            else
            {
                if (faces[(int)Face.Ace] == 1)
                    if (faces[(int)Face.Ten] == 1)
                        if (faces[(int)Face.Jack] == 1)
                            if (faces[(int)Face.Queen] == 1)
                                if (faces[(int)Face.King] == 1)
                                    return true;
            }
            return false;
        }

        public static bool Flush(int[] suits)
        {
            if (suits[(int)Suits.Jokers] == 1)
            {
                for (int i = 0; i < suits.Length - 1; i++)
                {
                    if (suits[i] == 4)
                        return true;
                }
            }
            else if (suits[(int)Suits.Jokers] == 2)
            {
                for (int i = 0; i < suits.Length - 2; i++)
                {
                    if (suits[i] == 3)
                        return true;
                }
            }
            else
            {
                for (int i = 0; i < suits.Length; i++)
                {
                    if (suits[i] == 5)
                        return true;
                }
            }
            return false;
        }

        public static bool FullHouse(int[] faces)
        {
            if (faces[(int)Face.Joker] == 2)
            {
                return false;
            }
            else if (faces[(int)Face.Joker] == 1)
            {
                for (int i = 0; i < faces.Length - 1; i++)
                {
                    for (int j = 0; j < faces.Length - 1; j++)
                    {
                        if (!(j == i))
                        {
                            if ((faces[i] == 2 && faces[j] == 2))
                            {
                                return true;
                            }
                        }
                    }
                }
            }
            else
            {
                for (int i = 0; i < faces.Length - 1; i++)
                {
                    for (int j = 0; j < faces.Length - 1; j++)
                    {
                        if (!(j == i))
                        {
                            if ((faces[i] == 2 && faces[j] == 3) || (faces[i] == 3 && faces[j] == 2))
                            {
                                return true;
                            }
                        }
                    }
                }
            }
            return false;
        }

        public static bool Quads(int[] faces)
        {
            if (faces[(int)Face.Joker] == 1)
            {
                for (int i = 0; i < faces.Length - 1; i++)
                {
                    if (faces[i] == 3)
                        return true;
                }
            }
            else if (faces[(int)Face.Joker] == 2)
            {
                for (int i = 0; i < faces.Length - 1; i++)
                {
                    if (faces[i] == 2)
                        return true;
                }
            }
            else
            {
                for (int i = 0; i < faces.Length - 1; i++)
                {
                    if (faces[i] == 4)
                        return true;
                }
            }
            return false;
        }

        public static bool FiveOfAKind(int[] faces)
        {
            if (faces[(int)Face.Joker] == 1)
                for (int i = 0; i < faces.Length - 1; i++)
                {
                    if (faces[i] == 4)
                        return true;
                }
            else if (faces[(int)Face.Joker] == 2)
            {
                for (int i = 0; i < faces.Length - 1; i++)
                {
                    if (faces[i] == 3)
                        return true;
                }
            }
            return false;
        }

        public static bool StraightFlush(int[] suits, int[] faces)
        {

            if (Straight(faces))
                if (Flush(suits))
                    return true;
            return false;
        }

        public static bool WildRoyalFlush(int[] suits, int[] faces)
        {
            if (faces[(int)Face.Joker] > 0)
                if (BroadwayStraight(faces))
                    if (Flush(suits))
                        return true;
            return false;
        }

        public static bool RoyalFlush(int[] suits, int[] faces)
        {
            if (faces[(int)Face.Joker] == 0)
                if (BroadwayStraight(faces))
                    if (Flush(suits))
                        return true;
            return false;
        }
    }
}