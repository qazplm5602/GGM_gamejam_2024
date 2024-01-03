using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class WeaponSkillHandlers : MonoBehaviour
{
    WeaponBullet _bulletMain;
    private void Awake() {
        _bulletMain = GetComponent<WeaponBullet>();

        _bulletMain.eventListener[Ranking.HIGHCARD] = DefaultFire;
        _bulletMain.eventListener[Ranking.ONEPAIR] = DefaultFire;
        _bulletMain.eventListener[Ranking.TWOPAIR] = DefaultFire;
        _bulletMain.eventListener[Ranking.STRAIGHT] = Straight;
        _bulletMain.eventListener[Ranking.TRIPLE] = Triple;
        _bulletMain.eventListener[Ranking.BACKSTRAIGHT] = Backstraight;
        _bulletMain.eventListener[Ranking.MOUNTAIN] = Mountain;
        _bulletMain.eventListener[Ranking.FOURCARD] = Fourcard;
        _bulletMain.eventListener[Ranking.FULLHOUSE] = Fullhouse;
        _bulletMain.eventListener[Ranking.FLUSH] = Flush;
    }

    private void OnDestroy() {
        _bulletMain.eventListener.Remove(Ranking.HIGHCARD);
        _bulletMain.eventListener.Remove(Ranking.ONEPAIR);
        _bulletMain.eventListener.Remove(Ranking.TWOPAIR);
        _bulletMain.eventListener.Remove(Ranking.STRAIGHT);
        _bulletMain.eventListener.Remove(Ranking.TRIPLE);
        _bulletMain.eventListener.Remove(Ranking.BACKSTRAIGHT);
        _bulletMain.eventListener.Remove(Ranking.MOUNTAIN);
        _bulletMain.eventListener.Remove(Ranking.FOURCARD);
        _bulletMain.eventListener.Remove(Ranking.FULLHOUSE);
        _bulletMain.eventListener.Remove(Ranking.FLUSH);
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
    
    void Fullhouse(Vector2 start, float angle) {
        List<GameObject> bullets = new();

        for (int i = 0; i < 2; i++)
            foreach (var item in _bulletMain.CreateBullets())
                bullets.Add(item);

        for (float i = -5, k = 0; i <= 5; i+=1.1f, k ++)
        {
            bullets[(int)k].transform.position = start;
            bullets[(int)k].transform.rotation = Quaternion.AngleAxis(angle + (2.5f * i), Vector3.forward);
        }
    }
    
    [SerializeField] GameObject[] shapeGroups;
    void Flush(Vector2 start, float angle) {
        CardShape shape = CheckCard.instance.playerCards[0].cardShape;
        int saveDamage = _bulletMain.GetDamange();        
        var bullets = _bulletMain.CreateBullets();
        for (int i = 1; i < 5; i++)
            Destroy(bullets[i]);

        bullets[0].transform.position = start;
        bullets[0].transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        CheckCard.instance.rankingInfo.ranking = Ranking.NONE;
        bullets[0].GetComponent<CardWeaponBullet>().damage = _bulletMain.GetDamange();
        CheckCard.instance.rankingInfo.ranking = Ranking.FLUSH;


        bullets[0].GetComponent<CardWeaponBullet>().OnCallback += (Collider2D other) => {
            var parent = Instantiate(shapeGroups[(int)shape]);
            parent.transform.position = other.ClosestPoint(bullets[0].transform.position);
            
            int i;
            for (i = 0; i < parent.transform.childCount; i++)
            {
                var card = parent.transform.GetChild(i);
                var bulletSys = card.AddComponent<CardWeaponBullet>();
                
                // bulletSys.speed = 24;
                bulletSys.damage = saveDamage;
                bulletSys.customDir = (card.position - parent.transform.position).normalized;
                bulletSys.OnCallback += (Collider2D other) => {
                    return false; // 삭제 방지
                };
            }

            for (i = 0; i < parent.transform.childCount; i++) {
                parent.transform.GetChild(i).transform.SetParent(null, true);
            }
            // parent.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            return true;
        };
    }

    void Wait(UnityAction callback, float time) {
        StartCoroutine(_Wait(callback, time));
    }
    IEnumerator _Wait(UnityAction cb, float time) {
        yield return new WaitForSeconds(time);
        cb.Invoke();
    }
}
