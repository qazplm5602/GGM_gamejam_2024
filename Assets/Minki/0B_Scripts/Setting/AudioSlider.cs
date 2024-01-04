using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using TMPro;
using UnityEngine.Rendering;

public class AudioSlider : MonoBehaviour
{
    [SerializeField] private AudioMixer _mixer;
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private Sprite[] _cards;
    [SerializeField] private string _mixerName;

    private Slider _slider;
    private Image _image;

    private void Awake() {
        _slider = GetComponent<Slider>();
        _image = transform.GetChild(2).GetChild(0).GetComponent<Image>();

        _slider.onValueChanged.AddListener(SliderChange);
    }

    private void SliderChange(float value) {
        int intValue = (int)value;

        _image.sprite = _cards[intValue];
        text.text = $"Volume : {intValue}";
        float soundValue = (intValue - 1) / 12f;
        if(intValue == 0) _mixer.SetFloat(_mixerName, -80f);
        else _mixer.SetFloat(_mixerName, Mathf.Lerp(-40f, 0f, soundValue));
    }
}
