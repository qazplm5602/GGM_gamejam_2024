using UnityEngine;

public class PlayerExperience : MonoBehaviour
{
    public int level = 1;

    public int _curNeedExp = 100;
    public int _curExp = 0;
    public void ExpUP()
    {
        _curExp += 30;
        print(_curExp);
        print(_curNeedExp);
        GameManager.Instance.SetEXP(_curExp, _curNeedExp);
        if (_curExp >= _curNeedExp)
        {
            GameManager.Instance.OnStatCanvas();
        }
    }

    public void CheckRemainLevelUP()
    {
        if (_curExp >= _curNeedExp)
        {
            ChangeLevel();
            GameManager.Instance.OnStatCanvas();
        }
    }

    public void ChangeLevel()
    {
        _curExp -= _curNeedExp;
        _curNeedExp = (int)(_curNeedExp * 1.5f);
        ++level;
        print("현재 레벨 : " + level);
        print("현재 경험치 :" + _curExp);
        print("현재 필요 경험치 : " + _curNeedExp);
    }
    
    public void SetExp()
    {
        GameManager.Instance.SetEXP(_curExp, _curNeedExp);
    }
}
