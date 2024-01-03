using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance = null;

    [SerializeField] private SerializableDict<AudioClip> _audioSerializeDictionary;

    private Dictionary<string, AudioClip> _audioDictionary = new Dictionary<string, AudioClip>();
    private AudioSource _audioSource;

    private void Awake() {
        if(Instance == null) Instance = this;
        else Destroy(gameObject);

        _audioSource = GetComponent<AudioSource>();

        _audioDictionary = _audioSerializeDictionary.GetDict();
    }

    public void PlaySound(string name) {
        if(_audioDictionary.ContainsKey(name)) {
            _audioSource.PlayOneShot(_audioDictionary[name]);
        }
        else Debug.LogError("[AudioMAnager] Not found clip name!");
    }
}

[System.Serializable]
public class SerializableDict<T>
{
    public List<SerializeData<T>> data;
    private Dictionary<string, T> dict = new Dictionary<string, T>();

    public Dictionary<string, T> GetDict() {
        for(int i = 0; i < data.Count; ++i) {
            dict.Add(data[i].key, data[i].value);
        }
        return dict;
    }
}

[System.Serializable]
public class SerializeData<T>
{
    public string key;
    public T value;
}