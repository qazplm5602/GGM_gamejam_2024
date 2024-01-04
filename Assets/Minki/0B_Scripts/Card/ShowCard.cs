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

    private IEnumerator ShowingCard(bool doubleBarrel = false) {
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
        if(doubleBarrel) {
            for(int i = 0; i < 5; ++i) {
                GameObject obj = Instantiate(_cardPrefab, transform.position, Quaternion.Euler(0, 0, 75 - 37.5f * i), transform);
                obj.transform.position += obj.transform.up * 0.85f;
                obj.transform.position -= Vector3.up * 0.2f;
                obj.transform.position += obj.transform.right * 0.05f;
                SpriteRenderer sr = obj.GetComponent<SpriteRenderer>();
                sr.sprite = _cardSprites[(int)cards[i].cardShape].sprites[cards[i].cardNumber - 1];
                sr.sortingOrder--;
                obj.transform.GetChild(0).GetComponent<SpriteRenderer>().sortingOrder--;

                cardObj[obj] = cards[i].cardNumber;

                yield return new WaitForSeconds(0.05f);
            }
        }

        yield return new WaitForSeconds(0.08f);

        foreach(GameObject obj in cardObj.Keys) {
            if(rankingInfo.ranking == Ranking.HIGHCARD || rankingInfo.ranking == Ranking.ONEPAIR || rankingInfo.ranking == Ranking.TWOPAIR || rankingInfo.ranking == Ranking.TRIPLE || rankingInfo.ranking == Ranking.FOURCARD) {
                if(cardObj[obj] == rankingInfo.cardData1.cardNumber || (rankingInfo.cardData1.cardNumber == 1 && cardObj[obj] == 14) || (rankingInfo.cardData1.cardNumber == 14 && cardObj[obj] == 1) || (rankingInfo.cardData2 != null && cardObj[obj] == rankingInfo.cardData2.cardNumber)) 
                    SetColor(obj.transform.GetChild(0).gameObject, rankingInfo.ranking);
                else obj.GetComponent<SpriteRenderer>().color = new Color(0.5f, 0.5f, 0.5f);
            }
            else SetColor(obj.transform.GetChild(0).gameObject, rankingInfo.ranking);
        }
    }

    private void SetColor(GameObject obj, Ranking ranking) {
        if(obj == null) return;

        Color color = Color.white;

        switch(ranking) {
            case Ranking.HIGHCARD: color = new Color(1, 0.82f, 0); break;
            case Ranking.ONEPAIR: color = new Color(0.4f, 1, 0); break;
            case Ranking.TWOPAIR: color = new Color(0, 1, 0.128f); break;
            case Ranking.TRIPLE: color = new Color(0, 0.749f, 0.59f); break;
            case Ranking.STRAIGHT: color = new Color(0, 0.39f, 1); break;
            case Ranking.BACKSTRAIGHT: color = new Color(1, 0.373f, 0); break;
            case Ranking.MOUNTAIN: color = new Color(1, 0.179f, 0); break;
            case Ranking.FLUSH: color = new Color(1, 0.088f, 0); break;
            case Ranking.FULLHOUSE: color = new Color(1, 0, 0.1f); break;
            case Ranking.FOURCARD: color = new Color(1, 0, 0); break;
            case Ranking.STRAIGHTFLUSH: color = new Color(1, 0, 0.883f); break;
            case Ranking.BACKSTRAIGHTFLUSH: color = new Color(0.37f, 0, 1); break;
            case Ranking.ROYALSTRAIGHTFLUSH: color = new Color(0.12f, 0, 1); break;
        }

        obj.SetActive(true);
        obj.GetComponent<SpriteRenderer>().material.SetColor("_Color", color * 5f);
    }

    public void DisappearCard(bool doubleBarrel = false) {
        if(doubleBarrel) {
            if(transform.childCount < 5) return;
            for(int i = 0; i < 5; ++i) {
                StartCoroutine(MoveDownCard(transform.GetChild(i).gameObject, 0.05f * i));
            }
        }
        else {
            for(int i = 0; i < transform.childCount - 5; ++i) Destroy(transform.GetChild(i).gameObject);

            for(int i = transform.childCount - 5; i < transform.childCount; ++i) {
                if(i >= 0)
                    StartCoroutine(MoveDownCard(transform.GetChild(i).gameObject, 0.05f * (i - transform.childCount - 5)));
                else break;
            }
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
