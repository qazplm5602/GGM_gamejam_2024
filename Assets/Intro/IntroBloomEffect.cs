using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class IntroBloomEffect : MonoBehaviour
{
   [SerializeField] VolumeProfile _volume;
   Bloom _bloom;

   private void Awake() {
    _volume.TryGet<Bloom>(out Bloom _bloom);
   }

   private void Start() {
      print("Hello world!");
      StartCoroutine(StartHandler());
   }

   IEnumerator StartHandler() {
      float time = 0;

      while (true) {
         yield return null;
         
         time  += Time.deltaTime;
         // _bloom.intensity.SetValue(Mathf.Lerp(_bloom.intensity.GetValue<float>(), 500, time));
      }
   }
}
