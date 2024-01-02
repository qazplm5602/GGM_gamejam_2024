using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class WeaponSkillHandlers : MonoBehaviour
{
    WeaponBullet _bulletMain;
    private void Awake() {
        _bulletMain = GetComponent<WeaponBullet>();

        _bulletMain.eventListener[Ranking.HIGHCARD] = DefaultFire;
        _bulletMain.eventListener[Ranking.STRAIGHT] = Straight;
    }

    private void OnDestroy() {
        _bulletMain.eventListener.Remove(Ranking.HIGHCARD);
        _bulletMain.eventListener.Remove(Ranking.STRAIGHT);
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

    void Straight(Vector2 start, float angle) {
        var bullets = _bulletMain.CreateBullets();

        // 순서대로 정렬
        bullets = bullets.OrderBy(n => {
            string name = n.GetComponentInChildren<SpriteRenderer>().sprite.name;
            string number = name.Substring(name.IndexOf("_") + 1);
            if (number == "A") number = "1";
            return int.Parse(number);
        }).ToArray();

        int i = 0;
        foreach (var bullet in bullets)
        {
            bullet.SetActive(false);
            bullet.GetComponentInChildren<SpriteRenderer>().sortingOrder = i;
            Wait(() => {
                bullet.SetActive(true);
                bullet.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
                bullet.transform.position = start;
                // bullet.transform.position += bullet.transform.right * i;
            }, i * .05f);
            i++;
        }
    }

    void Wait(UnityAction callback, float time) {
        StartCoroutine(_Wait(callback, time));
    }
    IEnumerator _Wait(UnityAction cb, float time) {
        yield return new WaitForSeconds(time);
        cb.Invoke();
    }
}
