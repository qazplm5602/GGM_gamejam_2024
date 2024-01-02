using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UIElements;
using TMPro;

public class StatUpCard : MonoBehaviour
{
    StatUpCanvas statUpParent;
    [SerializeField] private GameObject[] Friends;
    [SerializeField] private TextMeshProUGUI description;
    [SerializeField] private char[] shapes = new char[] { '♠', '♥', '♦', '♣' };
    Vector2 originPos;
    Vector2 originScale;
    int randShape;
    float randPercent;
    private void Awake()
    {
        description = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        originPos = transform.position;
        originScale = transform.localScale;
        statUpParent = transform.parent.GetComponent<StatUpCanvas>();
    }
    private void OnEnable()
    {
        randShape = Random.Range(0, shapes.Length);
        randPercent = Random.Range(1, 4) + Random.Range(1, 10) * 0.1f;
        description.text = $"{shapes[randShape]}카드가 나올 확률이\n" +
                           $"{randPercent}% 증가합니다.";
        transform.position = originPos;
        transform.localScale = originScale;
        transform.rotation = Quaternion.identity;
        transform.DORotate(new Vector2(0, 360), 0.6f, RotateMode.WorldAxisAdd).SetEase(Ease.OutQuad)
            .OnComplete(() => EnableText());
    }
    void EnableText()
    {
        if (transform.name == "Option3")
        {
            transform.parent.GetComponent<StatUpCanvas>().text.SetActive(true);
        }
    }

    public void SelectCard()
    {
        DisableFriends();
        statUpParent.DisableText();
        Sequence seq = DOTween.Sequence();
        seq.Append(transform.DOMove(Vector2.zero, 0.4f).SetEase(Ease.InQuad));
        seq.Join(transform.DOScale(new Vector2(7, 7), 0.5f).OnComplete(() => ShinyEffect()));
        seq.AppendInterval(0.4f);

        seq.Append(transform.DOScale(new Vector2(0, 0), 0.4f).SetEase(Ease.OutQuad).OnComplete(() => statUpParent.DisableAll()));
    }

    void ShinyEffect()
    {
        print("와 밝아짐");
    }

    void DisableFriends()
    {
        foreach (GameObject item in Friends)
        {
            item.SetActive(false);
        }
    }
}
