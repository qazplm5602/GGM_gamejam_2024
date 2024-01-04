using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAutoShotgun : MonoBehaviour, IWeaponEvent
{
    [SerializeField] float betweenDelay = 0.5f;
    [SerializeField] float deadDistance = 2;
    float fireTime = 0;
    WeaponBullet _bulletBullet;
    Transform firePos;

    public void Init(WeaponBullet _weaponBullet)
    {
        _bulletBullet = _weaponBullet;
        firePos = transform.Find("FirePos");

        CheckCard.instance.DrawCard();
        _bulletBullet.Bridge_Showcard(true);
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
        if (!isMouseDown || _bulletBullet.fireDisable || (Time.time - fireTime) < betweenDelay) return; // 준비 안됨
        
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (Vector2.Distance((Vector2)mousePos, (Vector2)transform.root.position) < deadDistance) return;
        
        fireTime = Time.time;
        _bulletBullet.Bridge_Showcard(false);
        CheckCard.instance.DrawCard();
        _bulletBullet.Bridge_Showcard(true);
        print("reload crad");

        Vector3 diff = mousePos - firePos.position;
        var angle = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;

        _bulletBullet.ShotFire(firePos.position, angle);
    }
}
