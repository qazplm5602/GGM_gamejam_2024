using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class StatUpCard : MonoBehaviour
{
    private void OnEnable()
    {
        transform.DORotate(new Vector3(0, 360, 0), 0.6f, RotateMode.WorldAxisAdd).SetEase(Ease.OutQuad);
    }
    private void OnDisable()
    {
        transform.rotation = Quaternion.identity;
    }
}
