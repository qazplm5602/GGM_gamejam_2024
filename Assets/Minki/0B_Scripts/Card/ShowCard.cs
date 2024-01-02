using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CardSprite {
    public Sprite[] sprites;
}

public class ShowCard : MonoBehaviour
{
    [SerializeField] private CardSprite[] _cardSprites;
    [SerializeField] private GameObject _cardPrefab;

    private void Update() {
        if(Input.GetKeyDown(KeyCode.P)) {
            CheckCard.instance.DrawCard();
            DisappearCard();
            StartCoroutine(ShowingCard());
        }
    }

    private IEnumerator ShowingCard() {
        yield return new WaitForSeconds(0.33f);

        Card[] cards = CheckCard.instance.playerCards;
        RankingInfo rankingInfo = CheckCard.instance.GetInfo();
        Dictionary<GameObject, int> cardObj = new Dictionary<GameObject, int>();

        for(int i = 0; i < 5; ++i) {
            GameObject obj = Instantiate(_cardPrefab, transform.position, Quaternion.Euler(0, 0, 75 - 37.5f * i), transform);
            obj.transform.position += obj.transform.up * 0.8f;
            obj.transform.position -= Vector3.up * 0.2f;
            obj.GetComponent<SpriteRenderer>().sprite = _cardSprites[(int)cards[i].cardShape].sprites[cards[i].cardNumber - 1];

            cardObj[obj] = cards[i].cardNumber;

            yield return new WaitForSeconds(0.05f);
        }

        yield return new WaitForSeconds(0.08f);

        foreach(GameObject obj in cardObj.Keys) {
            if(rankingInfo.ranking == Ranking.HIGHCARD || rankingInfo.ranking == Ranking.ONEPAIR || rankingInfo.ranking == Ranking.TWOPAIR || rankingInfo.ranking == Ranking.TRIPLE || rankingInfo.ranking == Ranking.FOURCARD) {
                if(cardObj[obj] == rankingInfo.cardData1.cardNumber || (rankingInfo.cardData2 != null && cardObj[obj] == rankingInfo.cardData2.cardNumber)) 
                    SetColor(obj.transform.GetChild(0).gameObject, rankingInfo.ranking);
                else obj.GetComponent<SpriteRenderer>().color = new Color(0.5f, 0.5f, 0.5f);
            }
            else SetColor(obj.transform.GetChild(0).gameObject, rankingInfo.ranking);
        }
    }

    private void SetColor(GameObject obj, Ranking ranking) {
        Color color = Color.white;

        switch(ranking) {
            case Ranking.HIGHCARD: color = Color.red; break;
            case Ranking.ONEPAIR: color = Color.red; break;
            case Ranking.TWOPAIR: color = Color.red; break;
            case Ranking.TRIPLE: color = Color.red; break;
            case Ranking.STRAIGHT: color = Color.red; break;
            case Ranking.FLUSH: color = Color.red; break;
            case Ranking.FULLHOUSE: color = Color.red; break;
            case Ranking.FOURCARD: color = Color.red; break;
            case Ranking.STRAIGHTFLUSH: color = Color.red; break;
            case Ranking.ROYALSTRAIGHTFLUSH: color = new Color(0.12f, 0, 1); break;
        }

        obj.SetActive(true);
        obj.GetComponent<SpriteRenderer>().material.SetColor("_Color", color * 7f);
    }

    public void DisappearCard() {
        for(int i = 0; i < transform.childCount; ++i) {
            StartCoroutine(MoveDownCard(gameObject.transform.GetChild(i).gameObject, 0.05f * i));
        }
    }

    private IEnumerator MoveDownCard(GameObject obj, float time) {
        yield return new WaitForSeconds(time);

        while(obj.transform.localPosition.y > 0f) {
            obj.transform.position -= obj.transform.up * Time.deltaTime * 8f;

            yield return null;
        }
        Destroy(obj);
    }
}
