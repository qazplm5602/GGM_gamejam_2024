using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WeaponBullet : MonoBehaviour
{
    [SerializeField] int default_damage;
    [SerializeField] GameObject cardBulletTemplate;
    [SerializeField] Sprite[] cards;
    Dictionary<string, Sprite> cardSpriteIndex = new();
    public Dictionary<Ranking, UnityAction<Vector2 /* start */, float /* dir(angle) */>> eventListener = new();

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

    public void ShotFire(Vector2 start, float angle) {
        var ranking = CkeckCard.instance.rankingInfo.ranking;
        if (eventListener.TryGetValue(ranking, out var cb)) {
            cb(start, angle);
        } else {
            throw new System.Exception(ranking.ToString()+"에 대응하는 이벤트가 없습니다.");
        }
    }

    public GameObject[] CreateBullets() {
        var bullets = new GameObject[CkeckCard.instance.playerCards.Length];
        
        int i = 0;
        foreach (var card in CkeckCard.instance.playerCards)
        {
            var shapeName = card.cardShape.ToString().ToLower();
            if (card.cardShape == CardShape.CLUB) shapeName = "clover";

            var spriteName = shapeName+"_"+(card.cardNumber == 1 || card.cardNumber == 14 ? "A" : card.cardNumber);
            var bullet = Instantiate(cardBulletTemplate);
            bullets[i] = bullet;

            bullet.GetComponentInChildren<SpriteRenderer>().sprite = cardSpriteIndex[spriteName];
            i++;
        }

        return bullets;
    }
}
