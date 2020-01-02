using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementWithRootAnimation : MonoBehaviour
{
    public Animator animator;
    public float rotSpeed, jumpMagnitude;
    public Raycaster jumpRaycaster;
    public GameObject weapon;
    private float ver, hor, mouseY, mouseX, jump, fire1, oldJumpDistance;
    private Transform mainCam;
    private bool isJumping;
    public bool isCarryingItem;
    private bool swordAttack = false;
    private bool hasAlreadyDied;
    public float Distance;
    public bool hit;
    public bool isHit;
    public float maxDistance = 1.5f;
    private RaycastHit raycasthit;
    private GameObject targetEnemy;
    private SwordAttackManager saw;


    private void Start()
    {
        mainCam = Camera.main.transform;
    }

    private void Update()
    {

        if (!GetComponent<HealthManager>().isDead)
        {

            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                swordAttack = true;
                if (swordAttack)
                {

                    int deger = Random.Range(3, 5);
                    animator.SetTrigger("attack");
                    animator.SetInteger("attackSubState", deger);
                    swordAttack = false;
                }
                
            }

            //    //animasyonu dinlicez yuzde 95i gecince false yapicaz
            //    //get current animation state.
            //}
            if (!animator.GetAnimatorTransitionInfo(0).anyState && !animator.GetCurrentAnimatorStateInfo(0).IsName("Great_Sword_Impact"))
            {
                isHit = false;
                animator.GetComponent<HealthManager>().getHit = false;
            }

            if (!isHit && animator.GetComponent<HealthManager>().getHit)
            {

                isHit = true;
                animator.SetTrigger("isHit");
               

            }

          



            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                animator.SetBool("isUsingRifle", !animator.GetBool("isUsingRifle"));
                weapon.SetActive(animator.GetBool("isUsingRifle"));
            }

            ver = Input.GetAxis("Vertical");
            hor = Input.GetAxis("Horizontal");
            mouseY = Input.GetAxis("Mouse Y");
            mouseX = Input.GetAxis("Mouse X");
            jump = Input.GetAxis("Jump");
            fire1 = Input.GetAxis("Fire1");

            if (ver != 0)
            {
                MoveForwardBack();
            }
            if (hor != 0)
            {
                MoveLeftRight();
            }
            TurnLeftRight();
            LookUpDown();


            if (jump > 0)
            {
                Jump();
            }
            if (isJumping)
            {
                CheckJump();
            }

            if (fire1 > 0)
            {
                //weapon.GetComponent<WeaponController>().Fire();
            }

            //Debug.Log("jump="+jump+" isJumping="+isJumping);
        }
        else
        {
            if (!hasAlreadyDied)
            {
                hasAlreadyDied = true;
                animator.SetTrigger("isDead");
            }

        }

    }


    private void MoveForwardBack()
    {
        float multiplier = 1;
        if (Input.GetKey(KeyCode.LeftShift))
        {
            multiplier = 2;
        }
        animator.SetFloat("ver", ver * multiplier);
    }
    private void MoveLeftRight()
    {
        float multiplier = 1;
        if (Input.GetKey(KeyCode.LeftShift))
        {
            multiplier = 2;
        }
        animator.SetFloat("hor", hor * multiplier);
    }

    private void TurnLeftRight()
    {
        transform.rotation *= Quaternion.Euler(0, mouseX * rotSpeed, 0);
    }

    private void LookUpDown()
    {
        mainCam.GetComponent<CameraFollow>().SetLookUpDown(-mouseY);
    }

    private void Jump()
    {
        if (!isJumping)
        {
            isJumping = true;
            Vector3 jumpVector = transform.up * jumpMagnitude;
            GetComponent<Rigidbody>().AddForce(jumpVector, ForceMode.Impulse);
        }
    }
    private void CheckJump()
    {
        float dist = jumpRaycaster.GetDistanceFromRaycastForward(1);
        //Debug.Log(dist);
        if (dist > -1)
        {
            if (oldJumpDistance > dist && dist < 0.3f)
            {
                oldJumpDistance = 0;
                isJumping = false;
            }
            //Debug.Log(oldJumpDistance+" "+dist+" "+isJumping);
            oldJumpDistance = dist;
        }

    }

    //private void GetTarget(GameObject target)
    //{
    //    SwordAttackManager.targetEnemy

    //}

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            targetEnemy = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Enemy")
        {
            targetEnemy = null;
        }
    }

    private void ReduceEnemyHealth(float aReduce)
    {

       //saw.GetComponent<HealthManager>().ReduceHealth(aReduce);

    }

}
