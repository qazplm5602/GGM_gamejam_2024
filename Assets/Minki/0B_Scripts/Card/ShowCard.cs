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
            StartCoroutine(ShowingCard());
        }
    }

    private IEnumerator ShowingCard() {
        Card[] cards = CkeckCard.instance.playerCards;
        RankingInfo rankingInfo = CkeckCard.instance.GetInfo();
        Dictionary<GameObject, int> cardObj = new Dictionary<GameObject, int>();

        for(int i = 0; i < 5; ++i) {
            GameObject obj = Instantiate(_cardPrefab, transform.position, Quaternion.Euler(0, 0, 75 - 37.5f * i), transform);
            obj.transform.position += obj.transform.up * 0.8f;
            obj.transform.position -= Vector3.up * 0.2f;
            obj.GetComponent<SpriteRenderer>().sprite = _cardSprites[(int)cards[i].cardShape].sprites[cards[i].cardNumber - 1];

            cardObj[obj] = cards[i].cardNumber;

            yield return new WaitForSeconds(0.05f);
        }

        yield return new WaitForSeconds(0.2f);

        foreach(GameObject obj in cardObj.Keys) {
            if(rankingInfo.ranking == Ranking.HIGHCARD || rankingInfo.ranking == Ranking.ONEPAIR || rankingInfo.ranking == Ranking.TWOPAIR || rankingInfo.ranking == Ranking.TRIPLE || rankingInfo.ranking == Ranking.FOURCARD) {
                if(cardObj[obj] == rankingInfo.cardData.cardNumber) obj.transform.GetChild(0).gameObject.SetActive(true);
                else obj.GetComponent<SpriteRenderer>().color = new Color(0.5f, 0.5f, 0.5f);
            }
            else obj.transform.GetChild(0).gameObject.SetActive(true);
        }
    }
}
