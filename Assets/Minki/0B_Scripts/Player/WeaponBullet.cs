using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBullet : MonoBehaviour
{
    [SerializeField] GameObject cardBulletTemplate;
    [SerializeField] Sprite[] cards;
    Dictionary<string, Sprite> cardSpriteIndex = new();

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
