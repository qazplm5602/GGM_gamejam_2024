using UnityEngine;

public class CkeckCard : MonoBehaviour
{
    public static CkeckCard instance;
    private void Awake() {
        instance = this;
    }
    public Card[] playerCards;
    public RankingInfo rankingInfo;

    private void Start() {
        playerCards = new Card[5];
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.K)) {
            GetCard();
            rankingInfo = CheckedCard();
        }
    }

    public RankingInfo GetInfo() {
        return rankingInfo;
    }

    private void GetCard() {
        for (int i = 0; i < 5; ++i) {
            Card newCard = GetRandomCard.instance.GetRandom();
            foreach (Card iter in playerCards) {
                if (newCard.cardNumber == iter.cardNumber && newCard.cardShape == iter.cardShape) {
                    GetCard();
                    return;
                }
            }
            playerCards[i] = newCard;
        }
    }

    private RankingInfo CheckedCard() {
        if (HandRankings.instance.RoyalStraightFlushCheck(playerCards).Item1) {
            print(HandRankings.instance.RoyalStraightFlushCheck(playerCards).Item2.cardShape + " Royal Straight Flush");        
            return new RankingInfo(Ranking.ROYALSTRAIGHTFLUSH, HandRankings.instance.RoyalStraightFlushCheck(playerCards).Item2);
        }
        if (HandRankings.instance.BackStraightFlushCheck(playerCards).Item1) {
            print(HandRankings.instance.BackStraightFlushCheck(playerCards).Item2.cardShape + " Back Straight Flush");
            return new RankingInfo(Ranking.BACKSTRAIGHTFLUSH, HandRankings.instance.BackStraightFlushCheck(playerCards).Item2);
        }
        if (HandRankings.instance.StraightFlushCheck(playerCards).Item1) {
            print(HandRankings.instance.StraightFlushCheck(playerCards).Item2.cardShape + " Straight Flush");
            return new RankingInfo(Ranking.STRAIGHTFLUSH, HandRankings.instance.StraightFlushCheck(playerCards).Item2);
        }
        if (HandRankings.instance.FourCardCheck(playerCards).Item1) {
            print(HandRankings.instance.FourCardCheck(playerCards).Item2.cardNumber + " Four card");
            return new RankingInfo(Ranking.FOURCARD, HandRankings.instance.FourCardCheck(playerCards).Item2);
        }
        if (HandRankings.instance.FullHouseCheck(playerCards)) {
            print(" FullHouse");
            return new RankingInfo(Ranking.FULLHOUSE);
        }
        if (HandRankings.instance.FlushCheck(playerCards).Item1) {
            print(HandRankings.instance.FlushCheck(playerCards).Item2.cardShape + " Flush");
            return new RankingInfo(Ranking.FLUSH, HandRankings.instance.FlushCheck(playerCards).Item2);
        }
        if (HandRankings.instance.MountainCheck(playerCards)) {
            print(" Mountine");
            return new RankingInfo(Ranking.MOUNTAIN);
        }
        if (HandRankings.instance.BackStraightCheck(playerCards)) {
            print(" Back straight");
            return new RankingInfo(Ranking.BACKSTRAIGHT);
        }
        if (HandRankings.instance.StraightCheck(playerCards)) {
            print(" Straight");
            return new RankingInfo(Ranking.STRAIGHT);
        }
        if (HandRankings.instance.TripleCheck(playerCards).Item1) {
            print(HandRankings.instance.TripleCheck(playerCards).Item2.cardNumber + " Triple");
            return new RankingInfo(Ranking.TRIPLE, HandRankings.instance.TripleCheck(playerCards).Item2);
        }
        if (HandRankings.instance.TwoPairCheck(playerCards).Item1) {
            print(HandRankings.instance.TwoPairCheck(playerCards).Item2.cardNumber + " Two pair");
            return new RankingInfo(Ranking.TWOPAIR, HandRankings.instance.TwoPairCheck(playerCards).Item2);
        }
        if (HandRankings.instance.OnePairCheck(playerCards).Item1) {
            print(HandRankings.instance.OnePairCheck(playerCards).Item2.cardNumber + " One pair");
            return new RankingInfo(Ranking.ONEPAIR, HandRankings.instance.OnePairCheck(playerCards).Item2);
        }
        print(" High Card");
        return new RankingInfo(Ranking.HIGHCARD, HandRankings.instance.BackStraightFlushCheck(playerCards).Item2);
    }
}
