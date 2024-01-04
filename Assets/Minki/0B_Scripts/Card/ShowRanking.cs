using System.Collections;
using UnityEngine;
using TMPro;

public class ShowRanking : MonoBehaviour
{
    private TextMeshPro _text;

    private void Awake() {
        _text = GetComponent<TextMeshPro>();
    }

    public void Show(Color color) {
        _text.color = color;
        
        StartCoroutine(ShowRoutine(color));
    }

    private IEnumerator ShowRoutine(Color color) {
        float timer = 0f;

        while(timer < 0.1f) {
            timer += Time.deltaTime;
            _text.fontSize = Mathf.Lerp(5f, 8f, timer * 10);
            yield return null;
        }
        timer = 0f;
        while(timer < 0.3f) {
            timer += Time.deltaTime;
            _text.fontSize = Mathf.Lerp(8f, 5f, timer * 3.33f);
            _text.color = new Color(color.r, color.g, color.b, Mathf.Lerp(1f, 0f, timer * 3.33f));
            yield return null;
        }
        Destroy(gameObject);
    }
}
