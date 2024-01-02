using UnityEngine;

public class GunController : MonoBehaviour
{
    [SerializeField] WeaponBullet _bullet;
    IWeaponEvent eventHandler;

    #if UNITY_EDITOR
    [SerializeField] GameObject default_weapon;
    
    private void Awake() {
        SetWeapon(default_weapon);
    }
    #endif

    public void SetWeapon(GameObject weaponEntity) {
        var weapon = Instantiate(weaponEntity, transform);
        eventHandler = weapon.GetComponent<IWeaponEvent>();
        
        eventHandler.Init(_bullet);
    }

    private void Update() {
        if (Input.GetMouseButtonDown(0)) {
            eventHandler?.MouseDown();
        } else if (Input.GetMouseButtonUp(0)) {
            eventHandler?.MouseUp();
        }
    }
}

interface IWeaponEvent {
    void Init(WeaponBullet _weaponBullet);
    void MouseDown();
    void MouseUp();
}