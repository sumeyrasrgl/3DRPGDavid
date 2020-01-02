using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class SwordAttackManager : MonoBehaviour
{
    

    public float damage = 30f;
    public Animator animator;
    public static GameObject targetEnemy;

    //RaycastHit hit;
    private void OnCollisionEnter(Collision other)
    {

        if (other.gameObject.GetComponent<HealthManager>())
        {

            if (true)
            {
                //other.gameObject.GetComponent<HealthManager>().ReduceHealth(damage);
            }
            targetEnemy = other.gameObject;

        }
        
    }
   
}
