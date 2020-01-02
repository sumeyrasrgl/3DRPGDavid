using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour {
    public WeaponController weaponController;
    public float damage = 30f;
    public Vector3 initialPosition;

    private void OnEnable() {
        initialPosition = transform.position;
    }

    private void Update() {
        if ((transform.position-initialPosition).sqrMagnitude>weaponController.weaponSqrRange) {
            weaponController.ReturnBulletToPool(gameObject);
        }
    }

    private void OnCollisionEnter(Collision other) {
        if (other.gameObject.GetComponent<HealthManager>()) {
            other.gameObject.GetComponent<HealthManager>().ReduceHealth(damage);
        }
        weaponController.ReturnBulletToPool(gameObject);
    }
}
