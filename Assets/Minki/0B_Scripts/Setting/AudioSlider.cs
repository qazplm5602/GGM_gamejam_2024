using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class AudioSlider : MonoBehaviour
{
    [SerializeField] private AudioMixer _mixer;
    [SerializeField] private Sprite[] _cards;

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

        float soundValue = (intValue - 1) / 12f;
        if(intValue == 0) _mixer.SetFloat("Master", -80f);
        else _mixer.SetFloat("Master", Mathf.Lerp(-40f, 0f, soundValue));
    }
}
