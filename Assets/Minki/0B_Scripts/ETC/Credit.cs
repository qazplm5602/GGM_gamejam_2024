using UnityEngine;

public class Credit : MonoBehaviour
{
    [SerializeField] private float _speed = 0.5f;

    private void Update() {
        if(transform.position.y >= 57f) return;

        transform.position += Vector3.up * Time.deltaTime * _speed;

        if(Input.GetMouseButton(0)) _speed = 2f;
        else if(Input.GetMouseButton(1)) _speed = 0f;
        else _speed = 1; 
    }
}
