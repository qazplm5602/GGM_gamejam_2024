using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WeaponBullet : MonoBehaviour
{
    [SerializeField] int default_damage;
    [SerializeField] GameObject cardBulletTemplate;
    [SerializeField] Sprite[] cards;
    [SerializeField] ShowCard _showCard;
    Dictionary<string, Sprite> cardSpriteIndex = new();
    public Dictionary<Ranking, UnityAction<Vector2 /* start */, float /* dir(angle) */>> eventListener = new();
    public bool fireDisable = false;

    private void Awake() {
        // 카드 sprite 인덱싱
        foreach (var item in cards)
        {
            cardSpriteIndex[item.name] = item;
        }
    }

    int _ammo;
    public int ammo {
        get => _ammo;
    }
    public bool isEmpty {
        get => _ammo <= 0;
    }

    public void SetAmmo(int amount) {
        _ammo = amount;

        // 여기에서 UI 연동
    }

    public void Bridge_Showcard(bool active, bool arg1 = false) {
        if (active)
            StartCoroutine(_showCard.ShowingCard(arg1));
        else
            _showCard.DisappearCard(arg1);
    }

    public void ShotFire(Vector2 start, float angle) {
        var ranking = CheckCard.instance.rankingInfo.ranking;
        // Debug.LogWarning("ShotFire Debug Code!! L39");
        // ranking = Ranking.ROYALSTRAIGHTFLUSH;
        if (eventListener.TryGetValue(ranking, out var cb)) {
            cb(start, angle);
        } else {
            throw new System.Exception(ranking.ToString()+"에 대응하는 이벤트가 없습니다.");
        }
    }

    public GameObject[] CreateBullets() {
        var bullets = new GameObject[CheckCard.instance.playerCards.Length];
        
        // 데미지 계산
        int damage = Mathf.RoundToInt(default_damage * (GetRankingValue(CheckCard.instance.rankingInfo.ranking) / 100) * ((GetMaxRanking() / 100f) + 1));

        int i = 0;
        foreach (var card in CheckCard.instance.playerCards)
        {
            var shapeName = card.cardShape.ToString().ToLower();
            if (card.cardShape == CardShape.CLUB) shapeName = "clover";

            var spriteName = shapeName+"_"+(card.cardNumber == 1 || card.cardNumber == 14 ? "A" : card.cardNumber);
            var bullet = Instantiate(cardBulletTemplate);
            bullet.GetComponent<CardWeaponBullet>().damage = damage;
            bullets[i] = bullet;

            bullet.GetComponentInChildren<SpriteRenderer>().sprite = cardSpriteIndex[spriteName];
            i++;
        }

        return bullets;
    }

    float GetRankingValue(Ranking rank) {
        switch (rank)
        {
            case Ranking.ONEPAIR:
                return 120;
            case Ranking.TWOPAIR:
                return 140;
            case Ranking.STRAIGHT:
                return 120;
            case Ranking.BACKSTRAIGHT:
                return 145;
            case Ranking.MOUNTAIN:
                return 65;
            case Ranking.FOURCARD:
                return 144;
            case Ranking.FLUSH:
                return 80;
            case Ranking.STRAIGHTFLUSH:
                return 80;
            case Ranking.BACKSTRAIGHTFLUSH:
                return 105;
            case Ranking.ROYALSTRAIGHTFLUSH:
                return 2000;
            default:
                return 100;
        }
    }
    int GetMaxRanking() => CheckCard.instance.rankingInfo.cardData2?.cardNumber ?? (CheckCard.instance.rankingInfo.ranking == Ranking.MOUNTAIN || CheckCard.instance.rankingInfo.ranking == Ranking.FULLHOUSE || CheckCard.instance.rankingInfo.ranking == Ranking.BACKSTRAIGHT ? 14 : CheckCard.instance.rankingInfo.cardData1.cardNumber);
    public int GetDamange() => Mathf.RoundToInt(default_damage * (GetRankingValue(CheckCard.instance.rankingInfo.ranking) / 100) * ((GetMaxRanking() / 100f) + 1));
    public Sprite GetCardSprite(string name) => cardSpriteIndex[name];
}