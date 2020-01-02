using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarryMe : MonoBehaviour
{
    private bool isBeingCarried;
    private bool canCarryMe;
    private Transform player;
    private bool keyBlocker;


    private void OnTriggerEnter(Collider other) {
        if (other.tag=="Player") {
            UIManager uiManager = GameObject.FindGameObjectWithTag("UserInterface").GetComponent<UIManager>();
            uiManager.SetMessageText("Beni taşımak için F tuşuna bas.", 4.5f);
            canCarryMe=true;
            player = other.transform;
        }
    }
    private void OnTriggerExit(Collider other) {
        if (other.tag=="Player") {
            canCarryMe=false;
        }
    }

    private void Update() {
        if (canCarryMe) {
            if (Input.GetKeyDown(KeyCode.F)) {
                isBeingCarried = true;
                canCarryMe = false;
                keyBlocker = true;
                //player.GetComponent<PlayerMovement>().isCarryingItem = true;
                player.GetComponent<PlayerMovementWithRootAnimation>().isCarryingItem = true;
                UIManager uiManager = GameObject.FindGameObjectWithTag("UserInterface").GetComponent<UIManager>();
                uiManager.SetMessageText("Beni bırakmak için tekrar F tuşuna bas.", 7f);
            }
        }
        if (isBeingCarried) {
            transform.position = player.position + (player.forward * 1.2f);
            //transform.rotation = player.rotation;
            if (!keyBlocker) {
                if (Input.GetKeyDown(KeyCode.F)) {
                    isBeingCarried = false;
                    canCarryMe = true;
                    // player.GetComponent<PlayerMovement>().isCarryingItem = false;
                    player.GetComponent<PlayerMovementWithRootAnimation>().isCarryingItem = false;
                }
            }  
        }
        keyBlocker=false;
    }

    
}
