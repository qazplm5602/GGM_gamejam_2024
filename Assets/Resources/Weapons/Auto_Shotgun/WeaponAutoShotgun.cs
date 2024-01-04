using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAutoShotgun : MonoBehaviour, IWeaponEvent
{
    [SerializeField] float betweenDelay = 0.5f;
    [SerializeField] float deadDistance = 2;
    float fireTime = 0;
    bool isReload = false;
    WeaponBullet _bulletBullet;
    Transform firePos;

    public void Init(WeaponBullet _weaponBullet)
    {
        _bulletBullet = _weaponBullet;
        firePos = transform.Find("FirePos");

        StartCoroutine(Reload());
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
        if (isReload) return;

        if (!isMouseDown || _bulletBullet.fireDisable || (Time.time - fireTime) < betweenDelay) return; // 준비 안됨
        
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (Vector2.Distance((Vector2)mousePos, (Vector2)transform.root.position) < deadDistance) return;
        

        Vector3 diff = mousePos - firePos.position;
        var angle = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;

        // fireTime = Time.time;
        _bulletBullet.ShotFire(firePos.position, angle);
        _bulletBullet.Bridge_Showcard(false);
        StartCoroutine(Reload());
    }

    IEnumerator Reload() {
        isReload = true;
        print("reloading...");
        yield return new WaitForSeconds(betweenDelay / 2f);
        print("realoding try...");
        CheckCard.instance.DrawCard();
        _bulletBullet.Bridge_Showcard(true);
        yield return new WaitForSeconds(betweenDelay / 2f);
        print("reloaded!");
        isReload = false;
    }
}
