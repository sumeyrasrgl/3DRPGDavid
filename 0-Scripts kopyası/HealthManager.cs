using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour {
    public float health = 100f;
    public float maxHealth = 100f;
    public bool isDead,getHit;

    public void ReduceHealth(float aReducemount) {
        health-=aReducemount;
        
        if (health<=0) {
            Die();
        }
        if(GetComponent<BridgeToUIManager>()) {
            GetComponent<BridgeToUIManager>().SetPlayerUIHealthProgressBar(health/maxHealth);
            getHit = true;
        }

        if (GetComponent<EnemyProgressbarManager>())
        {
            GetComponent<EnemyProgressbarManager>().SetEnemyHealth(health / maxHealth);
        }
    }
    

    public void IncreaseHealth(float anIncreaseRate) {
        health+=anIncreaseRate;
        if (health>maxHealth)
            health = maxHealth;
        if(GetComponent<BridgeToUIManager>()) {
            GetComponent<BridgeToUIManager>().SetPlayerUIHealthProgressBar(health/maxHealth);
        }
        if (GetComponent<EnemyProgressbarManager>())//zombinin caninin arttirdigimiz yer lazim olursa diye yazdik
        {
            GetComponent<EnemyProgressbarManager>().SetEnemyHealth(health / maxHealth);
        }
    }

    private void Die() {
        isDead = true;
    }

    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.GetComponent<HealthManager>())
        {
            if (collision.gameObject.tag == ("Player"))
            {
                ReduceHealth(20);
            }

        }
    }

}
