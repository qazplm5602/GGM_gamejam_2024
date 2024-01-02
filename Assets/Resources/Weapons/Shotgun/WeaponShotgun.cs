using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponShotgun : MonoBehaviour, IWeaponEvent
{
    [SerializeField] float betweenTime = .3f;
    [SerializeField] float reloadTime = 3;
    [SerializeField] float deadDistance = 2;
    [SerializeField] Transform firePos;
    [SerializeField] GameObject bullet;
    Animator _animator;
    bool isReload = false;
    float fireTime = 0;
    WeaponBullet _weaponBullet;

    public void Init(WeaponBullet weaponBullet)
    {
        // 초기 설정
        _animator = GetComponent<Animator>();
        _weaponBullet = weaponBullet;
        _weaponBullet.SetAmmo(5);

        CheckCard.instance.DrawCard();
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
        if ((Time.time - fireTime) <= betweenTime) return;
        
        if (isMouseDown) {
            if (_weaponBullet.isEmpty) { // 장전 ㄱㄱ
                print("weapon reloading...");
                _animator.SetTrigger("Reload");
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
                // var bullets = _weaponBullet.CreateBullets();

                // // TEST 총알
                // for (int i = -2, k = 0; i < 3; i++, k ++)
                // {
                //     bullets[k].transform.position = firePos.position;
                //     bullets[k].transform.rotation = Quaternion.AngleAxis(angle + (15 * i), Vector3.forward);
                //     // entity.transform.right = firePos.right;
                //     // print(entity.transform.right);
                //     // print(firePos.right);
                // }
            }

            fireTime = Time.time;
            _weaponBullet.SetAmmo(0);
            _animator.SetTrigger("Shot");
            print("fire!!");
        } else {

        }
    }

    IEnumerator WeaponReload() {
        yield return new WaitForSeconds(reloadTime);
        print("weapon reloaded!");
        CheckCard.instance.DrawCard();
        _weaponBullet.SetAmmo(5);
        isReload = false;
    }
}
