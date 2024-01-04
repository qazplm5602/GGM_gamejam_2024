using UnityEngine;

public class PlayerExperience : MonoBehaviour
{
    public int level = 1;

    private int _curNeedExp = 100;
    private int _curExp = 0;

    public void ExpUP(int value)
    {
        _curExp += value;
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
        print("안녕하ㅛㅔㅇ료");
        _curExp -= _curNeedExp;
        _curNeedExp = (int)(_curNeedExp * 1.5f);
        ++level;
    }
    
    public void SetExp()
    {
        GameManager.Instance.SetEXP(_curExp, _curNeedExp);
    }
}
