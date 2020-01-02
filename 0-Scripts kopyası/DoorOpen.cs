using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpen : MonoBehaviour
{
    public GameObject hinge;
    public GameObject openDoorRotationDummy;
    public GameObject closeDoorRotationDummy;

    private bool isAnimating;
    private bool isOpening;
    private bool isClosing;
    private Quaternion targetRot;
    private Quaternion initialRot;
    string task="";


    public void OpenDoor() {
        if (isAnimating) {
            StopCoroutine(AnimateDoor());
        }
        if (!isOpening) {
            isOpening =true;
            isAnimating=true;
            initialRot = hinge.transform.rotation;
            targetRot = openDoorRotationDummy.transform.rotation;
            StartCoroutine(AnimateDoor());
            isClosing=false;
            task="open";

        }
        
        
    }
    public void CloseDoor() {
        if (isAnimating) {
            StopCoroutine(AnimateDoor());
        }
        if (!isClosing) {
            isClosing = true;
            isAnimating = true;
            initialRot = hinge.transform.rotation;
            targetRot = closeDoorRotationDummy.transform.rotation;
            StartCoroutine(AnimateDoor());
            isOpening = false;
            task="close";
        }
    }

    private IEnumerator AnimateDoor() {
        float currentTime=0;
        Debug.Log("currentTime="+currentTime);
        float animationDuration = 1;
        while(currentTime<animationDuration) {
            Debug.Log(task + " "+ (currentTime/animationDuration));
            hinge.transform.rotation = Quaternion.Slerp(
                initialRot,
                targetRot,
                currentTime/animationDuration
            );
            currentTime+=Time.deltaTime;
            yield return null;
        }
        hinge.transform.rotation = targetRot;
        isAnimating = false;
        Debug.Log("End of animation");
        StopCoroutine(AnimateDoor());

    }
}
