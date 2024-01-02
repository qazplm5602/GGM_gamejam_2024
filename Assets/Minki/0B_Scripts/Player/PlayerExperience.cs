using UnityEngine;

public class PlayerExperience : MonoBehaviour
{
    public int level = 1;

    private int _curNeedExp = 100;
    private int _curExp = 0;

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.layer == 6) {
            _curExp += 30;
            PoolManager.Instance.Push("Exp", other.gameObject);
            if(_curExp >= _curNeedExp) {
                _curExp -= _curNeedExp;
                _curNeedExp = (int)(_curNeedExp * 1.5f);
                ++level;
            }
        }
    }
}
