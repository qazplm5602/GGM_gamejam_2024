using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponShotgun : MonoBehaviour, IWeaponEvent
{
    [SerializeField] float betweenTime = .3f;
    [SerializeField] float reloadTime = 3;
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
        _weaponBullet.SetAmmo(5);
        isReload = false;
    }
}
