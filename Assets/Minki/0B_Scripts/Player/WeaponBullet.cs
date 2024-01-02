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

    public GameObject CreateBullet(Card card) {
        var spriteName = card.cardShape.ToString().ToLower()+"_"+(card.cardNumber == 1 || card.cardNumber == 14 ? "A" : card.cardNumber);
        var bullet = Instantiate(cardBulletTemplate);

        bullet.GetComponent<SpriteRenderer>().sprite = cardSpriteIndex[spriteName];
        return null;
    }
}
