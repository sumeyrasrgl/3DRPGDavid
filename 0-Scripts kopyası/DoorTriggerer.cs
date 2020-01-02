using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTriggerer : MonoBehaviour
{
    public DoorOpen door;
    public float doorOpenThreshold = 350;
    public float currentWeightOnPlatform;

    private void OnTriggerEnter(Collider other) {
        if (other.tag=="Ball") {
            currentWeightOnPlatform += other.GetComponent<Rigidbody>().mass;
        }
        if (other.tag == "Chair") {
            currentWeightOnPlatform += other.GetComponent<Rigidbody>().mass;
        }
        if (other.tag == "Player") {
            currentWeightOnPlatform += other.GetComponent<Rigidbody>().mass;
        }
        if (currentWeightOnPlatform>=doorOpenThreshold) 
            door.OpenDoor();
    }

    private void OnTriggerExit(Collider other) {
        if (other.tag=="Ball") {
            currentWeightOnPlatform -= other.GetComponent<Rigidbody>().mass;
        }
        if (other.tag == "Chair") {
            currentWeightOnPlatform -= other.GetComponent<Rigidbody>().mass;
        }
        if (other.tag == "Player") {
            currentWeightOnPlatform -= other.GetComponent<Rigidbody>().mass;
        }
        door.CloseDoor();
    }
}
