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
        _bulletMain.eventListener[Ranking.TRIPLE] = Triple;
        _bulletMain.eventListener[Ranking.BACKSTRAIGHT] = Backstraight;
        _bulletMain.eventListener[Ranking.MOUNTAIN] = Mountain;
        _bulletMain.eventListener[Ranking.FOURCARD] = Fourcard;
    }

    private void OnDestroy() {
        _bulletMain.eventListener.Remove(Ranking.HIGHCARD);
        _bulletMain.eventListener.Remove(Ranking.STRAIGHT);
        _bulletMain.eventListener.Remove(Ranking.TRIPLE);
        _bulletMain.eventListener.Remove(Ranking.BACKSTRAIGHT);
        _bulletMain.eventListener.Remove(Ranking.MOUNTAIN);
        _bulletMain.eventListener.Remove(Ranking.FOURCARD);
    }


    void DefaultFire(Vector2 start, float angle) {
        var bullets = _bulletMain.CreateBullets();

        // TEST 총알
        for (int i = -2, k = 0; i < 3; i++, k ++)
        {
            bullets[k].transform.position = start;
            bullets[k].transform.rotation = Quaternion.AngleAxis(angle + (5 * i), Vector3.forward);
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

    void Triple(Vector2 start, float angle) {
        var bullets = _bulletMain.CreateBullets();

        for (int i = -2, k = 0; i < 3; i++, k ++)
        {
            bullets[k].transform.position = start;
            bullets[k].transform.rotation = Quaternion.AngleAxis(angle + (5 * i), Vector3.forward);

            // 화염디버프 적용 ㄱㄱ
        }
    }
    void Backstraight(Vector2 start, float angle) {
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
                bullet.transform.rotation = Quaternion.AngleAxis(angle + 180, Vector3.forward);
                bullet.transform.position = start;
                // bullet.transform.position += bullet.transform.right * i;
            }, i * .05f);
            i++;
        }
    }
    void Mountain(Vector2 start, float angle) {
        float?[,] spawnCoords = new float?[5, 5] {
         {null, null, 0,null,null},
            {-1, null,0,null,1},
            {-1, null,0,null,1},
            {-1, null,0,null,1},
            {-1,  -0.5f, 0,  0.5f,  1},
        };

        Queue<GameObject> cards = new();
        for (int i = 0; i < 3; i++)
        {
            foreach (var card in _bulletMain.CreateBullets())
            {
                card.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
                card.SetActive(false);

                cards.Enqueue(card);
            }
        }

        print("------ cards");
        print(cards.Count);
        
        for (int i = 0; i < spawnCoords.GetLength(0); i++)
        {
            for (int k = 0; k < spawnCoords.GetLength(1); k++) {
                var x = spawnCoords[i, k];
                if (x == null) continue;

                var entity = cards.Dequeue();
                Wait(() => {
                    entity.SetActive(true);
                    entity.transform.position = start;
                    entity.transform.position += entity.transform.up * x.Value;
                }, i * .01f);
            }
        }
    }

    void Fourcard(Vector2 start, float angle) {
        for (int domi = 0; domi < 4; domi++)
        {
            var bullets = _bulletMain.CreateBullets();

            for (int i = -2, k = 0; i < 2; i++, k ++)
            {
                bullets[k].SetActive(false);
                var idx = i;
                var _k = k;
                Wait(() => {
                    bullets[_k].SetActive(true);
                    bullets[_k].transform.position = start;
                    bullets[_k].transform.rotation = Quaternion.AngleAxis(angle + (5 * idx), Vector3.forward);
                }, domi * .05f);
            }
            Destroy(bullets[4]);
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
