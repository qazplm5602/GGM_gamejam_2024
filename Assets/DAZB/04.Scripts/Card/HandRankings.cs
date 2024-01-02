using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandRankings : MonoBehaviour
{
    public static HandRankings instance;

    private void Awake() {
        instance = this;
    }

    // One Pair
    public bool OnePairCheck(Card[] cards) {
        for (int i = 0; i < cards.Length; ++i) {
            int same = 1;
            for (int j = 0; j < cards.Length; ++j) {
                if (i != j && cards[i].cardNumber == cards[j].cardNumber) {
                    ++same;
                    if (same == 2) {
                        return true;
                    }
                }
            }
        }
        return false;
    }

    // Two Pair
    public bool TwoPairCheck(Card[] cards) {
        int pairCount = 0;
        for (int i = 0; i < cards.Length; ++i) {
            for (int j = i + 1; j < cards.Length; ++j) {
                if (cards[i].cardNumber == cards[j].cardNumber) {
                    pairCount++;
                    if (pairCount == 2) {
                        return true;
                    }
                }
            }
        }

        return false;
    }

    // Three of a kind
    public bool TripleCheck(Card[] cards) {
        for (int i = 0; i < cards.Length; ++i) {
            int same = 1;
            for (int j = 0; j < cards.Length; ++j) {
                if (i != j && cards[i].cardNumber == cards[j].cardNumber) {
                    ++same;
                    if (same == 3) {
                        return true;
                    }
                }
            }
        }
        return false;
    }

    // Straight
    public bool StraightCheck(Card[] cards) {
        Card[] sortedCard = CardSort(cards);
        for (int i = 0; i < cards.Length; ++i) {
            if (i == 0) {
                continue;
            }
            else if (sortedCard[i].cardNumber - sortedCard[i - 1].cardNumber != 1) {
                return false;
            }
        }
        return true;
    }

    // BackStraight
    public bool BackStraightCheck(Card[] cards) {
        Card[] sortedCard = CardSort(cards);
        if (!StraightCheck(sortedCard)) return  false;
        if (sortedCard[0].cardNumber != 1) return false;
        return true;
    }

    // Mountain
    public bool MountainCheck(Card[] cards) {
        Card[] sortedCard = CardSort(cards);
        if (sortedCard[0].cardNumber != 10) {
            return false;
        }
        for (int i = 0; i < sortedCard.Length; ++i) {
            if (i == 0) {
                continue;
            }
            else if (sortedCard[i].cardNumber - sortedCard[i - 1].cardNumber != 1) {
                return false;
            }
        }
        return true;
    }

    // Flush
    public bool FlushCheck(Card[] cards) {
        for (int i = 0; i < cards.Length; ++i) {
            if (i == 0) continue;
            else if (cards[i].cardShape != cards[i - 1].cardShape) {
                return false;
            }
        }
        return true;
    }

    // Full house
    public bool FullHouseCheck(Card[] cards) {
        Dictionary<int, int> cardCount = new();
        foreach(Card iter in cards) {
            if (cardCount.ContainsKey(iter.cardNumber)) {
                cardCount[iter.cardNumber]++;
            }
            else {
                cardCount.Add(iter.cardNumber, 1);
            }
        }

        bool hasTreple = false;
        bool hasPair = false;

        foreach (int count in cardCount.Values) {
            if (count == 3) {
                hasTreple = true;
            }
            else {
                hasPair = true;
            }
        }
        return hasPair && hasTreple;
    }

    // four of a kind
    public bool FourCardCheck(Card[] cards) {
        for (int i = 0; i < cards.Length; ++i) {
            int same = 1;
            for (int j = 0; j < cards.Length; ++j) {
                if (i != j && cards[i].cardNumber == cards[j].cardNumber) {
                    ++same;
                    if (same == 4) {
                        return true;
                    }
                }
            }
        }
        return false;
    }

    // Straight Flush
    public bool StraightFlushCheck(Card[] cards) {
        if (!StraightCheck(cards)) return false;
        for (int i = 0; i < cards.Length; ++i) {
            if (i == 0) {
                continue;
            }
            else if (cards[i].cardShape != cards[i - 1].cardShape) {
                return false;   
            }
        }
        return true;
    }

    // back straight flush
    public bool BackStraightFlushCheck(Card[] cards) {
        if (!BackStraightCheck(cards)) return false;
        for (int i = 0; i < cards.Length; ++i) {
            if (i == 0) {
                continue;
            }
            else if (cards[i].cardShape != cards[i - 1].cardShape) {
                return false;   
            }
        }
        return true;
    }

    // Royal Straight Flush
    public bool RoyalStraightFlushCheck(Card[] cards) {
        if (!MountainCheck(cards)) return false;
        for (int i = 0; i < cards.Length; ++i) {
            if (i == 0) {
                continue;
            }
            else if (cards[i].cardShape != cards[i - 1].cardShape) {
                return false;   
            }
        }
        return true;
    }

/// <summary>
/// 카드 배열을 정렬
/// </summary>
/// <param name="cards">카드의 배열</param>
    public Card[] CardSort(Card[] cards) {
        for (int i = 0; i < cards.Length; ++i) {
            int min = i;
            for (int j = i + 1; j < cards.Length; ++j) {
                if (cards[min].cardNumber > cards[j].cardNumber) {
                    min = j;
                }
            }
            (cards[i], cards[min]) = (cards[min], cards[i]);
        }
        return cards;
    }
}
