using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public Transform bulletGenerationPointDummy;
    public float weaponSqrRange = 200f;
    public float bulletSpeed = 3f;
    
    private List<GameObject> bulletPool = new List<GameObject>();

    public void Fire() {
        GameObject myBullet = null;
        if (CountBulletPool()==0) {
            myBullet = Resources.Load("Bullet") as GameObject;
            Instantiate(myBullet);
        } else {
            myBullet = GetBulletFromPool();
            myBullet.transform.SetParent(bulletGenerationPointDummy);
        }
        myBullet.transform.position = bulletGenerationPointDummy.transform.position;
        myBullet.transform.rotation = bulletGenerationPointDummy.transform.rotation;

        myBullet.GetComponent<BulletManager>().weaponController = GetComponent<WeaponController>();

        myBullet.transform.SetParent(null);
        if (!myBullet.activeSelf) {
            myBullet.SetActive(true);
        }
        myBullet.GetComponent<Rigidbody>().velocity = bulletGenerationPointDummy.transform.forward * bulletSpeed;

    }

    public GameObject GetBulletFromPool() {
        GameObject mybullet = bulletPool[0];
        bulletPool.RemoveAt(0);
        return mybullet;
    }
    public void ReturnBulletToPool(GameObject aBullet) {
        aBullet.GetComponent<Rigidbody>().velocity=Vector3.zero;
        aBullet.SetActive(false);
        bulletPool.Add(aBullet);
    }
    public int CountBulletPool() {
        return bulletPool.Count;
    }
}
