using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBullet : MonoBehaviour
{
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
}
