using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProgressbarManager : MonoBehaviour
{
    public Transform dummyObjectForUI;
    public HealthProgressbarManager enemyProgressBar;
    private bool isActive;
    public void SetEnemyHealth(float aRate)
    {
        if (enemyProgressBar == null)
        {
            enemyProgressBar = GameObject.FindGameObjectWithTag("EnemyProgressbar").GetComponent<HealthProgressbarManager>();
        }
        if (enemyProgressBar == null)
        {
            GameObject epbCanvas = Instantiate (Resources.Load("EnemyProgressbar") as GameObject);
            epbCanvas.GetComponent<Canvas>().worldCamera = Camera.main;//camerayi icine set ediyoruz
            enemyProgressBar = epbCanvas.GetComponent<HealthProgressbarManager>();
        }
        enemyProgressBar.transform.SetParent(dummyObjectForUI.transform);//childladik
        enemyProgressBar.transform.localPosition = Vector3.zero; //sifirladik degerleri ayni yere gelsin diye
        enemyProgressBar.transform.rotation = Camera.main.transform.rotation;
        enemyProgressBar.SetProgressBar(aRate);
        isActive = true;
    }

    private void Update()
    {
        if (isActive)//surekli canavar uzeridndeki progresbari gormemizi saglar
        {
            enemyProgressBar.transform.rotation = Camera.main.transform.rotation;
            if (dummyObjectForUI.childCount == 0)
            {
                isActive = false;
            }
        }
    }
}
