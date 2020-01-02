using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReleaseBall : MonoBehaviour
{
    public GameObject ball;


    private void OnTriggerEnter(Collider other) {
        if (other.tag=="Player") {
            ball.GetComponent<Rigidbody>().isKinematic=false;
            
        }
    }
}
