using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public enum Ranking
{
    HIGHCARD,
    ONEPAIR,
    TWOPAIR,
    TRIPLE,
    STRAIGHT,
    BACKSTRAIGHT,
    MOUNTAIN,
    FLUSH,
    FULLHOUSE,
    FOURCARD,
    STRAIGHTFLUSH,
    BACKSTRAIGHTFLUSH,
    ROYALSTRAIGHTFLUSH,
    NONE
}

[System.Serializable]
public class RankingInfo {
    public RankingInfo(Ranking ranking, Card cardData) {
        this.ranking = ranking;
        this.cardData = cardData;
    }

    public RankingInfo(Ranking ranking) {
        this.ranking = ranking;
    }
    public Ranking ranking;
    public Card cardData;
}
