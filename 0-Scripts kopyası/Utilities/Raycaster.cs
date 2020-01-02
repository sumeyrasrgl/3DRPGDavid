using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Raycaster : MonoBehaviour
{
    public RaycastHit RaycastForward(float aMaxDistance) {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, 
            aMaxDistance, 1<<0, QueryTriggerInteraction.Ignore)) {
            return hit;
        }
        return hit;
    }
    public float GetDistanceFromRaycastForward(float aMaxDistance) {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, aMaxDistance, 1<<0, QueryTriggerInteraction.Ignore)) {
            return hit.distance;
        }
        return -1f;
    }
}
