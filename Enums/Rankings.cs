namespace WebApi.Enums
{
    public enum Face
    {
        Ace = 0, Deuce, Three, Four, Five, Six, Seven,
        Eight, Nine, Ten, Jack, Queen, King, Joker
    };

    public enum HandRanking
    {
        Nothing = 0, JacksOrBetter, TwoPair, Trips, Straight, BroadwayStraight, Flush, FullHouse,
        Quads, StraightFlush, RoyalFlush, WildRoyalFlush, FiveOfAKind, KingsOrBetter, FourDeuces, QuadAces, QuadAcesLowKicker, LowQuads, LowQuadsWithAce
    }

    public enum Suits
    {
        Clubs = 0, Spades, Hearts, Diamonds, Jokers
    };

    public enum DeucesWildSuits
    {
        Clubs = 0, Diamonds, Hearts, Spades, Deuces
    };

    public enum DeucesWildFaces
    {
        Deuce = 0, Ace, Three, Four, Five, Six, Seven,
        Eight, Nine, Ten, Jack, Queen, King
    };

}