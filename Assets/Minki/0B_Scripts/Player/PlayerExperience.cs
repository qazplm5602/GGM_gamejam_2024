using UnityEngine;

public class PlayerExperience : MonoBehaviour
{
    public int level = 1;

    private int _curNeedExp = 100;
    private int _curExp = 0;

    public void ExpUP(int value)
    {
        _curExp += value;
        GameManager.Instance.SetEXP(_curExp, _curNeedExp);
        print("경헟미오름");
        if (_curExp >= _curNeedExp)
        {
            print("레벨오름");
            GameManager.Instance.OnStatCanvas();
        }
    }

    public void CheckRemainLevelUP()
    {
        print("레벨업가능한지검사");
        if (_curExp >= _curNeedExp)
        {
            print("레벨업가능");
            ChangeLevel();
            GameManager.Instance.OnStatCanvas();
        }
    }

    public void ChangeLevel()
    {
        print("레벨올라서경험치줄이고최대치올림");
        _curExp -= _curNeedExp;
        _curNeedExp = (int)(_curNeedExp * 1.5f);
        ++level;
    }
    
    public void SetExp()
    {
        GameManager.Instance.SetEXP(_curExp, _curNeedExp);
    }
}
