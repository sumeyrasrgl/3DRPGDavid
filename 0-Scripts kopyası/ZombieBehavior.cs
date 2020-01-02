using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieBehavior : MonoBehaviour {
    public enum State { idle, attack, dead };
    public State zomState = State.idle;
    public Animator animator;
    public float minAttackSqrDistance = 3f;
    private bool isDecisionTimerActive;
    private GameObject target;
    private bool isAttackTypeDefined;

   
    

    private void Update() {
        if (zomState!=State.dead) {
            if (GetComponent<HealthManager>().isDead) {
                zomState = State.dead;
            }
            
            if (zomState==State.idle) {
                ExecuteIdleState();
            } else if (zomState==State.attack) {
                ExecuteAttackingState();
            } else if (zomState==State.dead) {
                ExecuteDeadState();
            }
        }  
    }

    private void ExecuteIdleState() {
        // state icinde gerekli degiskenleri belirle (karar mekanizmasi)
        if (animator.GetInteger("attackSubState")>0) {
            animator.SetInteger("attackSubState",0);
        }
        if (!isDecisionTimerActive) {
            isDecisionTimerActive=true;
            int idleSubState = Random.Range(0,2);
            float duration = Random.Range(2,11);
            StartCoroutine(SetDecisionForAllStates(duration, idleSubState));
        } 
    }
    private void ExecuteAttackingState() {
        // state icinde gerekli degiskenleri belirle (karar mekanizmasi)
        Vector3 direction = target.transform.position - transform.position;
        if (direction.sqrMagnitude > minAttackSqrDistance) {
            // kos ve hedefi yakala
            Quaternion targetRotation = Quaternion.LookRotation(direction, transform.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 0.3f);
            //animator.Set
            animator.SetInteger("attackSubState", 1);
            animator.SetTrigger("attack");
            

        } else {
            // saldir
            Quaternion targetRotation = Quaternion.LookRotation(direction, transform.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 0.3f);

            if (!isAttackTypeDefined) {
                isAttackTypeDefined = true;
                int attackType = Random.Range(2,4);

                animator.SetInteger("attackSubState", attackType);
                animator.SetTrigger("attack");
                

            }

            if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime>0.95f){
                isAttackTypeDefined=false;
                
            }

        }
        // animatore gerekli komutlari gonder (animator parametrelerini set et)

    }

    private void ExecuteDeadState() {
        int deadAnimationIndex = Random.Range(0,2);
        animator.SetInteger("deadAnimationIndex", deadAnimationIndex);
        animator.SetTrigger("isDead");
        //Destroy(GetComponent<ZombieBehavior>());
    }

    private IEnumerator SetDecisionForAllStates(float aDuration, int aSubstateNo) {
        yield return new WaitForSeconds(aDuration);
        MakeIdleDecision(aSubstateNo);
        StopCoroutine(SetDecisionForAllStates(aDuration, aSubstateNo));
        isDecisionTimerActive=false;
    }

    private void MakeIdleDecision(int anIdleSubState) {
        // animatore gerekli komutlari gonder (animator parametrelerini set et)
        animator.SetInteger("idleSubState", anIdleSubState);
        animator.SetTrigger("idleSubStateChangeTrigger"); 
    }


    private void OnTriggerEnter(Collider other) {
        if (other.tag=="Player") {
            target = other.gameObject;
            zomState = State.attack;
        }
    }
    private void OnTriggerExit(Collider other) {
        if (other.tag=="Player") {
            zomState = State.idle;
            target = null;
        }
    }

    private void ReducePlayerHealth(float aReduceAmount) {
        //bunu editorde event olarak cagirdigimiz icin burada bir deger girisi olmadi
        target.GetComponent<HealthManager>().ReduceHealth(aReduceAmount);
    }

}
