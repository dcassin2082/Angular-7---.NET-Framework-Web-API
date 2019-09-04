using WebApi.Enums;

namespace WebApi.Helpers
{
    public class DoubleBonusHandValue
    {
        public static HandRanking RankHand(int[] faces, int[] suits)
        {
            if (AnyPair(faces))
            {
                if (QuadsFiveThruKing(faces))
                    return HandRanking.Quads;
                else if (QuadAces(faces))
                    return HandRanking.QuadAces;
                else if (QuadAcesLowKicker(faces))
                    return HandRanking.QuadAcesLowKicker;
                else if (LowQuads(faces))
                    return HandRanking.LowQuads;
                else if (LowQuadsAceKicker(faces))
                    return HandRanking.LowQuadsWithAce;
                else if (FullHouse(faces))
                    return HandRanking.FullHouse;
                else if (Trips(faces))
                    return HandRanking.Trips;
                else if (TwoPair(faces))
                    return HandRanking.TwoPair;
                else if (JacksOrBetter(faces))
                    return HandRanking.JacksOrBetter;
            }
            else
            {
                if (RoyalFlush(suits, faces))
                    return HandRanking.RoyalFlush;
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
            for (int i = 0; i < faces.Length; i++)
            {
                if (faces[i] > 1)
                    return true;
            }
            return false;
        }

        public static bool JacksOrBetter(int[] faces)
        {
            if (faces[(int)Face.Ace] == 2 || faces[(int)Face.Jack] == 2 || faces[(int)Face.Queen] == 2 || faces[(int)Face.King] == 2)
                return true;
            return false;
        }

        public static bool TwoPair(int[] faces)
        {
            for (int i = 0; i < faces.Length; i++)
            {
                for (int j = 0; j < faces.Length; j++)
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

        public static bool Trips(int[] faces)
        {
            for (int i = 0; i < faces.Length; i++)
            {
                if (faces[i] == 3)
                    return true;
            }
            return false;
        }

        public static bool Straight(int[] faces)
        {
            for (int i = 0; i < faces.Length; i++)
            {
                if (faces[i] > 1)
                    return false;
            }
            for (int i = 0; i <= faces.Length - 5; i++)
            {
                if (faces[i] == 1)
                    if (faces[i + 1] == 1)
                        if (faces[i + 2] == 1)
                            if (faces[i + 3] == 1)
                                if (faces[i + 4] == 1)
                                    return true;

            }
            return false;
        }

        public static bool BroadwayStraight(int[] faces)
        {
            if (faces[(int)Face.Ace] == 1)
                if (faces[(int)Face.Ten] == 1)
                    if (faces[(int)Face.Jack] == 1)
                        if (faces[(int)Face.Queen] == 1)
                            if (faces[(int)Face.King] == 1)
                                return true;
            return false;
        }

        public static bool Flush(int[] suits)
        {
            for (int i = 0; i < suits.Length; i++)
            {
                if (suits[i] == 5)
                    return true;
            }
            return false;
        }

        public static bool FullHouse(int[] faces)
        {
            for (int i = 0; i < faces.Length; i++)
            {
                for (int j = 0; j < faces.Length; j++)
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
            return false;
        }

        public static bool QuadsFiveThruKing(int[] faces)
        {
            for (int i = 0; i < faces.Length; i++)
            {
                if (faces[0] > 1 || faces[1] > 1 || faces[2] > 1 || faces[3] > 1)
                    return false;
                if (faces[i] == 4)
                    return true;
            }
            return false;
        }
        public static bool QuadAces(int[] faces)
        {
            // 4 aces with 5-king kicker pays 160/1
            if (faces[0] == 4)
            {
                for (int i = 1; i <= 3; i++)
                {
                    if (faces[i] == 1)
                        return false;
                }
                return true;
            }
            return false;
        }
        public static bool QuadAcesLowKicker(int[] faces)
        {
            // 4 aces with 2,3,4 kicker pays 400/1
            if (faces[0] == 4)
            {
                for (int i = 1; i <= 3; i++)
                {
                    if (faces[i] == 1)
                        return true;
                }
            }
            return false;
        }
        public static bool LowQuads(int[] faces)
        {
            // quad 2, 3 or 4 with no ace pays 80/1
            if (faces[1] == 4 || faces[2] == 4 || faces[3] == 4)
            {
                if (faces[0] == 0)
                    return true;
            }
            return false;
        }
        public static bool LowQuadsAceKicker(int[] faces)
        {
            // quad 2, 3 or 4 with ace pays 160/1
            if (faces[1] == 4 || faces[2] == 4 || faces[3] == 4)
            {
                if (faces[0] == 1)
                    return true;
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

        public static bool RoyalFlush(int[] suits, int[] faces)
        {
            if (BroadwayStraight(faces))
                if (Flush(suits))
                    return true;
            return false;
        }
    }
}