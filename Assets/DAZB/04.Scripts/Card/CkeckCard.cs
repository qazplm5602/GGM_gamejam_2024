using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CkeckCard : MonoBehaviour
{
    public Card[] playerCards;

    private void Update() {
        if (Input.GetKeyDown(KeyCode.K)) {
            print(CheckedCard());
        }
    }

    private string CheckedCard() {
        if (HandRankings.instance.RoyalStraightFlushCheck(playerCards)) return "RSF";
        if (HandRankings.instance.BackStraightFlushCheck(playerCards)) return "BSF";
        if (HandRankings.instance.StraightFlushCheck(playerCards)) return "SF";
        if (HandRankings.instance.FourCardCheck(playerCards)) return "FC";
        if (HandRankings.instance.FullHouseCheck(playerCards)) return "FH";
        if (HandRankings.instance.FlushCheck(playerCards)) return "F";
        if (HandRankings.instance.MountainCheck(playerCards)) return "M";
        if (HandRankings.instance.BackStraightCheck(playerCards)) return "BS";
        if (HandRankings.instance.StraightCheck(playerCards)) return "S";
        if (HandRankings.instance.TripleCheck(playerCards)) return "T";
        if (HandRankings.instance.TwoPairCheck(playerCards)) return "TP";
        if (HandRankings.instance.OnePairCheck(playerCards)) return "OP";
        return "HC";        
    }
}
