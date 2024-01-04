using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAutoShotgun : MonoBehaviour, IWeaponEvent
{
    [SerializeField] float betweenDelay = 0.5f;
    float fireTime = 0;
    WeaponBullet _bulletBullet;
    Transform firePos;

    public void Init(WeaponBullet _weaponBullet)
    {
        _bulletBullet = _weaponBullet;
        firePos = transform.Find("");
    }

    bool isMouseDown = false;
    public void MouseDown()
    {
        isMouseDown = true;
    }

    public void MouseUp()
    {
        isMouseDown = false;
    }

    private void Update() {
        if (!isMouseDown || (Time.time - fireTime) < betweenDelay) return; // 준비 안됨
        
        fireTime = Time.time;
        CheckCard.instance.DrawCard();
        print("reload crad");

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 diff = mousePos - firePos.position;
        var angle = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;

        _bulletBullet.ShotFire(firePos.position, angle);
    }
}
