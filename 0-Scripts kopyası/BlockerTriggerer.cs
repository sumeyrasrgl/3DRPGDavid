using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockerTriggerer : MonoBehaviour
{
    public GameObject blocker;

    private void OnTriggerEnter(Collider other) {
        Debug.Log(other.tag);
        //if (other.tag=="Player") {
            blocker.GetComponent<Animator>().SetTrigger("TurnOn");
        //}
    }
}
