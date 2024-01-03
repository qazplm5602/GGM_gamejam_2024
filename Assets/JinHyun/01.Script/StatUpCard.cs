using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.UI;

public class StatUpCard : MonoBehaviour
{
    StatUpCanvas statUpParent;
    [SerializeField] private GameObject[] Friends;
    [SerializeField] private TextMeshProUGUI description;
    [SerializeField] private char[] shapes = new char[] { '♠', '♦', '♥', '♣' };

    Image jokerImage;
    Vector2 originPos;
    Vector2 originScale;
    int randShape;
    float randPercent;
    bool selected = false;
    private void Awake()
    {
        jokerImage = GetComponent<Image>();
        description = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        originPos = transform.position;
        originScale = transform.localScale;
        statUpParent = transform.parent.GetComponent<StatUpCanvas>();
    }
    private void OnEnable()
    {
        if (jokerImage.material == statUpParent.cardShinyMat)
        {
            print("원상태");
            jokerImage.material = null;
        }
        selected = false;
        print(selected);
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
            Time.timeScale = 0;
        }
    }

    void DisableFriends()
    {
        foreach (GameObject item in Friends)
        {
            item.SetActive(false);
        }
    }
    public void SelectCard()
    {
        //Disable GameObject Except Selected Card
        Time.timeScale = 1;
        DisableFriends();
        statUpParent.DisableText();
        selected = true;
        Exit();

        //Add Weight to Selected Card's Increase Stat Info
        GetRandomCard.instance.SetWeight(randShape, randPercent);

        //Selected Card's Position to Move Zero, Add Shiny Effect, Decrease Size to 0
        Sequence seq = DOTween.Sequence();
        seq.Join(transform.DOScale(new Vector2(8, 8), 0.4f));
        seq.Append(transform.DOMove(Camera.main.ViewportToScreenPoint(new Vector2(0.5f, 0.5f)), 0.4f).SetEase(Ease.OutQuad));
        seq.AppendInterval(0.4f);

        seq.Append(transform.DOScale(new Vector2(0, 0), 0.4f).SetEase(Ease.OutQuad).OnComplete(() => statUpParent.DisableAll()));
    }

    public void Hover()
    {
        if(!selected) transform.localScale = transform.localScale * 1.2f;
        print("들어옴");
    }

    public void Exit()
    {
        if(!selected) transform.localScale = originScale;
        print("나감");
    }

}
