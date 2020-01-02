using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed, rotSpeed, jumpMagnitude;
    public Raycaster jumpRaycaster;
    private float ver, hor, mouseY, mouseX, jump, fire1, oldJumpDistance;
    private Transform mainCam;
    private bool isJumping;
    public bool isCarryingItem;

    private void Start() {
        mainCam = Camera.main.transform;
    }
    
    private void Update() {
        ver = Input.GetAxis("Vertical");
        hor = Input.GetAxis("Horizontal");
        mouseY = Input.GetAxis("Mouse Y");
        mouseX = Input.GetAxis("Mouse X");
        jump = Input.GetAxis("Jump");
        fire1 = Input.GetAxis("Fire1");
        
        if (ver!=0) {
            MoveForwardBack();
        }
        if (hor!=0) {
            MoveLeftRight();
        }
        TurnLeftRight();
        LookUpDown();


        if (jump>0) {
            Jump();
        }
        if (isJumping) {
            CheckJump();
        }
        //Debug.Log("jump="+jump+" isJumping="+isJumping);
    }


    private void MoveForwardBack() {
        float ms = moveSpeed;
        if (isCarryingItem) {
            ms = moveSpeed / 2;
        }
        transform.position += (transform.forward * (ver * ms));
    }
    private void MoveLeftRight() {
        float ms = moveSpeed;
        if (isCarryingItem) {
            ms = moveSpeed / 2;
        }
        transform.position += (transform.right * (hor * ms));
    }

    private void TurnLeftRight() {
        transform.rotation *= Quaternion.Euler(0, mouseX * rotSpeed, 0);
    }

    private void LookUpDown() {
        mainCam.GetComponent<CameraFollow>().SetLookUpDown(-mouseY);
    }

    private void Jump() {
        if (!isJumping) {
            isJumping = true;
            Vector3 jumpVector = transform.up * jumpMagnitude;
            GetComponent<Rigidbody>().AddForce(jumpVector, ForceMode.Impulse);
        } 
    }
    private void CheckJump() {
        float dist = jumpRaycaster.GetDistanceFromRaycastForward(1);
        //Debug.Log(dist);
        if (dist>-1) {  
            if (oldJumpDistance>dist && dist<0.3f) {
                oldJumpDistance=0;
                isJumping=false;
            }
            //Debug.Log(oldJumpDistance+" "+dist+" "+isJumping);
            oldJumpDistance = dist;
        }
        
    }

    

    private void Fire() {

    }
}
