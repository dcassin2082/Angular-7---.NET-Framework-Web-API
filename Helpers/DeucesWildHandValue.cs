using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApi.Enums;

namespace WebApi.Helpers
{
    public class DeucesWildHandValue
    {
        public static HandRanking RankHand(int[] faces, int[] suits)
        {
            if (RoyalFlush(suits, faces))
                return HandRanking.RoyalFlush;
            else if (FourDeuces(faces))
                return HandRanking.FourDeuces;
            else if (WildRoyalFlush(suits, faces))
                return HandRanking.WildRoyalFlush;
            else if (FiveOfAKind(faces))
                return HandRanking.FiveOfAKind;
            else if (StraightFlush(suits, faces))
                return HandRanking.StraightFlush;
            else if (Quads(faces, suits))
                return HandRanking.Quads;
            else if (FullHouse(faces))
                return HandRanking.FullHouse;
            else if (Flush(suits, faces))
                return HandRanking.Flush;
            else if (BroadwayStraight(faces))
                return HandRanking.BroadwayStraight;
            else if (Straight(faces, suits))
                return HandRanking.Straight;
            else if (Trips(faces, suits))
                return HandRanking.Trips;
            else
                return HandRanking.Nothing;
        }

        public static bool AnyPair(int[] faces)
        {
            for (int i = 1; i < faces.Length; i++)
            {
                if (faces[i] > 1)
                    return true;
            }
            return false;
        }

        public static bool FourDeuces(int[] faces)
        {
            if (faces[(int)DeucesWildFaces.Deuce] == 4)
                return true;
            return false;
        }

        public static bool TwoPair(int[] faces)
        {
            for (int i = 1; i < faces.Length; i++)
            {
                for (int j = 1; j < faces.Length; j++)
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
            return false;
        }

        public static bool Trips(int[] faces, int[] suits)
        {
            if (faces[(int)DeucesWildFaces.Deuce] > 2)
            {
                return false;
            }
            else if (faces[(int)DeucesWildFaces.Deuce] == 2)
            {
                for (int i = 1; i < faces.Length; i++)
                {
                    if (faces[i] > 1)
                        return false;
                }
                for (int i = 0; i < suits.Length - 1; i++)
                {
                    if (suits[i] > 2)
                        return false;
                }
                return true;
            }
            else if (faces[(int)DeucesWildFaces.Deuce] == 1)
            {
                for (int i = 0; i < faces.Length; i++)
                {
                    if (faces[i] == 2)
                    {
                        return true;
                    }
                }
                return false;
            }
            else
            {
                for (int i = 1; i < faces.Length; i++)
                {
                    if (faces[i] == 3)
                        return true;
                }
            }
            return false;
        }

        public static bool Straight(int[] faces, int[] suits)
        {
            for (int i = 1; i < faces.Length; i++)
            {
                if (faces[i] > 1)
                    return false;
            }
            int[] arrayBounds = new int[12];
            for (int i = 1; i < faces.Length; i++)
            {
                arrayBounds[i - 1] = faces[i];
            }
            int upperBound = Array.LastIndexOf(arrayBounds, 1);
            int lowerBound = Array.IndexOf(arrayBounds, 1);
            if (upperBound - lowerBound <= 4)
            {
                if (faces[(int)DeucesWildFaces.Deuce] > 3)
                {
                    for (int i = 1; i < faces.Length; i++)
                    {
                        if (faces[i] > 1)
                            return false;
                    }
                    for (int i = 0; i < suits.Length - 1; i++)
                    {
                        if (suits[i] > 1)
                            return false;
                    }
                    return true;
                }
                else if (faces[(int)DeucesWildFaces.Deuce] == 2)
                {
                    for (int i = 1; i < faces.Length; i++)
                    {
                        if (faces[i] > 1)
                            return false;
                    }
                    for (int i = 0; i < suits.Length - 1; i++)
                    {
                        if (suits[i] > 2)
                            return false;
                    }
                    return true;
                }
                else if (faces[(int)DeucesWildFaces.Deuce] == 1)
                {
                    for (int i = 0; i < suits.Length - 1; i++)
                    {
                        if (suits[i] > 3)
                            return false;
                    }
                    return true;
                }
                else
                {
                    for (int i = 1; i < faces.Length; i++)
                    {
                        if (faces[i] > 1)
                            return false;
                    }
                    return true;
                }
            }
            return false;
        }

        public static bool BroadwayStraight(int[] faces)
        {
            if (faces[(int)DeucesWildFaces.Deuce] == 3)
            {
                if (faces[(int)DeucesWildFaces.Ten] == 1 && faces[(int)DeucesWildFaces.Jack] == 1)
                    return true;
                else if (faces[(int)DeucesWildFaces.Ten] == 1 && faces[(int)DeucesWildFaces.King] == 1)
                    return true;
                else if (faces[(int)DeucesWildFaces.Ten] == 1 && faces[(int)DeucesWildFaces.Ace] == 1)
                    return true;
                else if (faces[(int)DeucesWildFaces.Ten] == 1 && faces[(int)DeucesWildFaces.Queen] == 1)
                    return true;
                else if (faces[(int)DeucesWildFaces.Jack] == 1 && faces[(int)DeucesWildFaces.Queen] == 1)
                    return true;
                else if (faces[(int)DeucesWildFaces.Jack] == 1 && faces[(int)DeucesWildFaces.King] == 1)
                    return true;
                else if (faces[(int)DeucesWildFaces.Jack] == 1 && faces[(int)DeucesWildFaces.Ace] == 1)
                    return true;
                else if (faces[(int)DeucesWildFaces.Queen] == 1 && faces[(int)DeucesWildFaces.King] == 1)
                    return true;
                else if (faces[(int)DeucesWildFaces.Queen] == 1 && faces[(int)DeucesWildFaces.Ace] == 1)
                    return true;
                else if (faces[(int)DeucesWildFaces.King] == 1 && faces[(int)DeucesWildFaces.Ace] == 1)
                    return true;
            }
            else if (faces[(int)DeucesWildFaces.Deuce] == 2)
            {
                if (faces[(int)DeucesWildFaces.Ten] == 1 && faces[(int)DeucesWildFaces.Jack] == 1 && faces[(int)DeucesWildFaces.Queen] == 1)
                    return true;
                else if (faces[(int)DeucesWildFaces.Ten] == 1 && faces[(int)DeucesWildFaces.Jack] == 1 && faces[(int)DeucesWildFaces.King] == 1)
                    return true;
                else if (faces[(int)DeucesWildFaces.Ten] == 1 && faces[(int)DeucesWildFaces.Jack] == 1 && faces[(int)DeucesWildFaces.Ace] == 1)
                    return true;
                else if (faces[(int)DeucesWildFaces.Ten] == 1 && faces[(int)DeucesWildFaces.Queen] == 1 && faces[(int)DeucesWildFaces.King] == 1)
                    return true;
                else if (faces[(int)DeucesWildFaces.Ten] == 1 && faces[(int)DeucesWildFaces.Queen] == 1 && faces[(int)DeucesWildFaces.Ace] == 1)
                    return true;
                else if (faces[(int)DeucesWildFaces.Ten] == 1 && faces[(int)DeucesWildFaces.King] == 1 && faces[(int)DeucesWildFaces.Ace] == 1)
                    return true;
                else if (faces[(int)DeucesWildFaces.Jack] == 1 && faces[(int)DeucesWildFaces.Queen] == 1 && faces[(int)DeucesWildFaces.King] == 1)
                    return true;
                else if (faces[(int)DeucesWildFaces.Jack] == 1 && faces[(int)DeucesWildFaces.Queen] == 1 && faces[(int)DeucesWildFaces.Ace] == 1)
                    return true;
                else if (faces[(int)DeucesWildFaces.Jack] == 1 && faces[(int)DeucesWildFaces.King] == 1 && faces[(int)DeucesWildFaces.Ace] == 1)
                    return true;
                else if (faces[(int)DeucesWildFaces.Queen] == 1 && faces[(int)DeucesWildFaces.King] == 1 && faces[(int)DeucesWildFaces.Ace] == 1)
                    return true;
            }
            else if (faces[(int)DeucesWildFaces.Deuce] == 1)
            {
                if (faces[(int)DeucesWildFaces.Ten] == 0)
                {
                    if (faces[(int)DeucesWildFaces.Ace] == 1)
                        if (faces[(int)DeucesWildFaces.Jack] == 1)
                            if (faces[(int)DeucesWildFaces.Queen] == 1)
                                if (faces[(int)DeucesWildFaces.King] == 1)
                                    return true;
                }
                else if (faces[(int)Face.Jack] == 0)
                {
                    if (faces[(int)DeucesWildFaces.Ace] == 1)
                        if (faces[(int)DeucesWildFaces.Ten] == 1)
                            if (faces[(int)DeucesWildFaces.Queen] == 1)
                                if (faces[(int)DeucesWildFaces.King] == 1)
                                    return true;
                }
                else if (faces[(int)DeucesWildFaces.Queen] == 0)
                {
                    if (faces[(int)DeucesWildFaces.Ace] == 1)
                        if (faces[(int)DeucesWildFaces.Ten] == 1)
                            if (faces[(int)DeucesWildFaces.Jack] == 1)
                                if (faces[(int)DeucesWildFaces.King] == 1)
                                    return true;
                }
                else if (faces[(int)DeucesWildFaces.King] == 0)
                {
                    if (faces[(int)DeucesWildFaces.Ace] == 1)
                        if (faces[(int)DeucesWildFaces.Ten] == 1)
                            if (faces[(int)DeucesWildFaces.Jack] == 1)
                                if (faces[(int)DeucesWildFaces.Queen] == 1)
                                    return true;
                }
                else if (faces[(int)DeucesWildFaces.Ace] == 0)
                {
                    if (faces[(int)DeucesWildFaces.Ten] == 1)
                        if (faces[(int)DeucesWildFaces.Jack] == 1)
                            if (faces[(int)DeucesWildFaces.Queen] == 1)
                                if (faces[(int)DeucesWildFaces.King] == 1)
                                    return true;
                }
            }
            else
            {
                if (faces[(int)DeucesWildFaces.Ace] == 1)
                    if (faces[(int)DeucesWildFaces.Ten] == 1)
                        if (faces[(int)DeucesWildFaces.Jack] == 1)
                            if (faces[(int)DeucesWildFaces.Queen] == 1)
                                if (faces[(int)DeucesWildFaces.King] == 1)
                                    return true;
            }
            return false;
        }

        public static bool Flush(int[] suits, int[] faces)
        {
            int[] arrayBounds = new int[12];
            for (int i = 1; i < faces.Length; i++)
                arrayBounds[i - 1] = (faces[i]);
            int upperBound = Array.LastIndexOf(arrayBounds, 1);
            int lowerBound = Array.IndexOf(arrayBounds, 1);
            if (suits[(int)DeucesWildSuits.Deuces] == 4)
            {
                return false;
            }
            else if (suits[(int)DeucesWildSuits.Deuces] == 3)
            {
                for (int i = 0; i < suits.Length - 1; i++)
                {
                    if (suits[i] == 2)
                    {
                        return true;
                    }
                }
            }
            else if (suits[(int)DeucesWildSuits.Deuces] == 2)
            {
                for (int i = 0; i < suits.Length - 1; i++)
                {
                    if (suits[i] == 3)
                    {
                        return true;
                    }
                }
            }
            else if (suits[(int)DeucesWildSuits.Deuces] == 1)
            {
                for (int i = 0; i < suits.Length - 1; i++)
                {
                    if (suits[i] == 4)
                    {
                        return true;
                    }
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
            if (faces[(int)DeucesWildFaces.Deuce] > 2)
            {
                return false;
            }
            else if (faces[(int)DeucesWildFaces.Deuce] == 2)
            {
                for (int i = 1; i < faces.Length; i++)
                {
                    if (faces[i] == 2)
                        return true;
                }
            }
            else if (faces[(int)DeucesWildFaces.Deuce] == 1)
            {
                if (TwoPair(faces))
                    return true;
                return false;
            }
            else
            {
                for (int i = 1; i < faces.Length; i++)
                {
                    for (int j = 1; j < faces.Length; j++)
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

        public static bool Quads(int[] faces, int[] suits)
        {
            if (faces[(int)DeucesWildFaces.Deuce] > 3)
                return false;
            if (faces[(int)DeucesWildFaces.Deuce] == 3)
            {
                for (int i = 1; i < faces.Length; i++)
                {
                    if (faces[i] > 1)
                        return false;
                }
                return true;
            }
            else if (faces[(int)DeucesWildFaces.Deuce] == 2)
            {
                for (int i = 1; i < faces.Length; i++)
                {
                    if (faces[i] == 2)
                    {
                        return true;
                    }
                }
                return false;
            }
            else if (faces[(int)DeucesWildFaces.Deuce] == 1)
            {
                for (int i = 1; i < faces.Length; i++)
                {
                    if (faces[i] == 3)
                        return true;
                }
            }
            else
            {
                for (int i = 1; i < faces.Length; i++)
                {
                    if (faces[i] == 4)
                        return true;
                }
            }
            return false;
        }

        public static bool FiveOfAKind(int[] faces)
        {
            if (faces[(int)DeucesWildFaces.Deuce] == 4)
            {
                return false;
            }
            else if (faces[(int)DeucesWildFaces.Deuce] == 3)
            {
                for (int i = 1; i < faces.Length; i++)
                {
                    if (faces[i] == 2)
                        return true;
                }
            }
            else if (faces[(int)DeucesWildFaces.Deuce] == 2)
            {
                for (int i = 1; i < faces.Length; i++)
                {
                    if (faces[i] == 3)
                        return true;
                }
            }
            else if (faces[(int)DeucesWildFaces.Deuce] == 1)
            {
                for (int i = 1; i < faces.Length; i++)
                {
                    if (faces[i] == 4)
                        return true;
                }
            }
            return false;
        }

        public static bool StraightFlush(int[] suits, int[] faces)
        {

            if (Straight(faces, suits))
                if (Flush(suits, faces))
                    return true;
            return false;
        }

        public static bool WildRoyalFlush(int[] suits, int[] faces)
        {
            if (faces[(int)DeucesWildFaces.Deuce] > 0)
            {
                if (BroadwayStraight(faces))
                    if (Flush(suits, faces))
                        return true;
            }
            return false;
        }
        public static bool RoyalFlush(int[] suits, int[] faces)
        {
            if (faces[(int)DeucesWildFaces.Deuce] == 0)
                if (BroadwayStraight(faces))
                    if (Flush(suits, faces))
                        return true;
            return false;
        }
    }
}