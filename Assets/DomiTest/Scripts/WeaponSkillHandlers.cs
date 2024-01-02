using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSkillHandlers : MonoBehaviour
{
    WeaponBullet _bulletMain;
    private void Awake() {
        _bulletMain = GetComponent<WeaponBullet>();

        _bulletMain.eventListener[Ranking.HIGHCARD] = DefaultFire;
    }

    private void OnDestroy() {
        _bulletMain.eventListener.Remove(Ranking.HIGHCARD);
    }


    void DefaultFire(Vector2 start, float angle) {
        var bullets = _bulletMain.CreateBullets();

        // TEST 총알
        for (int i = -2, k = 0; i < 3; i++, k ++)
        {
            bullets[k].transform.position = start;
            bullets[k].transform.rotation = Quaternion.AngleAxis(angle + (15 * i), Vector3.forward);
        }
    }
}
