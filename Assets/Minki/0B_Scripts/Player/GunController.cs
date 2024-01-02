using UnityEngine;

public class GunController : MonoBehaviour
{
    private Animator _animator;

    private void Awake() {
        _animator = (Animator)GetComponent("Animator");
    }

    private void Update() {
        if(Input.GetMouseButtonDown(0)) {
            _animator.SetTrigger("Shot");
        }
        else if(Input.GetMouseButtonDown(1)) {
            _animator.SetTrigger("Reload");
        }
    }
}
