using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponDoubleShoutgun : MonoBehaviour, IWeaponEvent
{
    [SerializeField] int default_damage;
    [SerializeField] float betweenTime = .3f;
    [SerializeField] float reloadTime = 3;
    [SerializeField] float deadDistance = 2;
    [SerializeField] Transform firePos;
    bool isReload = false;
    float fireTime = 0;
    WeaponBullet _weaponBullet;

    public void Init(WeaponBullet weaponBullet)
    {
        // 초기 설정
        _weaponBullet = weaponBullet;

        isReload = true;
        StartCoroutine(WeaponReload());

        _weaponBullet.SetReloadTime(reloadTime);
        _weaponBullet.SetDefaultDamage(default_damage);
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
        if (isReload || _weaponBullet.fireDisable) return;
        if ((Time.time - fireTime) <= betweenTime) return;
        
        if (isMouseDown) {
            if (_weaponBullet.isEmpty) { // 장전 ㄱㄱ
                print("weapon reloading...");
                isReload = true;
                StartCoroutine(WeaponReload());
                return;
            }
             
            // 마우스 좌표 ㄱㄱ
            {
                Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector3 diff = mousePos - firePos.position;
                var angle = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
                // 데드존
                if (Vector2.Distance((Vector2)mousePos, (Vector2)transform.root.position) < deadDistance) return;

                _weaponBullet.ShotFire(firePos.position, angle);
                _weaponBullet.Bridge_Showcard(false, true);
            }

            fireTime = Time.time;
            _weaponBullet.SetAmmo(_weaponBullet.ammo - 5);
            print("fire!!");
        } else {

        }
    }

    IEnumerator WeaponReload() {
        CheckCard.instance.DrawCard();
        _weaponBullet.Bridge_Showcard(true, true);
        yield return new WaitForSeconds(reloadTime);
        print("weapon reloaded!");
        _weaponBullet.SetAmmo(10);
        isReload = false;
    }
}
