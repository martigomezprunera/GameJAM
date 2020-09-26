using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimations : MonoBehaviour
{
    #region VARIABLE
    public Animator animator;
    int hit = 1;
    #endregion

    #region UPDATE
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            LighAttack();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            ChargingHeavy();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            HeavyAttack();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            Parry();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            ParryFail();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            Dodge();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            Hit();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            Exhaust();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            CancellingAttack();
        }
    }
    #endregion

    #region LIGHT ATTACK
    public void LighAttack()
    {
        //animator.SetBool("LightAttack", true);
        animator.SetTrigger("Attack");
    }
    #endregion

    #region CHARGING HEAVY
    public void ChargingHeavy()
    {
        animator.SetBool("ChargingHeavy", true);
    }
    #endregion

    #region HEAVY ATTACK
    public void HeavyAttack()
    {
        animator.SetTrigger("HeavyAttack");
        animator.SetBool("ChargingHeavy", false);
    }
    #endregion

    #region CANCELLING HEAVY
    public void CancellingAttack()
    {
        Hit();
    }
    #endregion

    #region PARRY
    public void Parry()
    {
        animator.SetTrigger("Parry");
    }
    #endregion

    #region PARRY FAIL 
    public void ParryFail()
    {
        animator.SetTrigger("ParryFail");
    }
    #endregion

    #region DODGE
    public void Dodge()
    {
        animator.SetTrigger("Dodge");
    }
    #endregion

    #region HIT
    public void Hit()
    {

        if (hit == 1)
        {
            hit = 2;
            animator.SetBool("Hit1", true);
            if (animator.GetBool("ChargingHeavy"))
            {
                animator.SetBool("ChargingHeavy", false);
                Exhaust();
            }
            return;
        }
        else if (hit == 2)
        {
            hit = 1;
            animator.SetBool("Hit2", true);

            if (animator.GetBool("ChargingHeavy"))
            {
                animator.SetBool("ChargingHeavy", false);
                Exhaust();
            }
            return;
        }



    }
    #endregion

    #region EXHAUST
    public void Exhaust()
    {
        animator.SetBool("Exhausted", true);
        Invoke("GoToIdle", 1.5f);
    }
    #endregion

    #region GO TO IDLE
    public void GoToIdle()
    {
        animator.SetBool("HeavyAttack", false);
        animator.SetBool("ChargingHeavy", false);
        animator.SetBool("LightAttack", false);
        animator.SetBool("Dodge", false);
        animator.SetBool("Parry", false);
        animator.SetBool("ParryFail", false);
        animator.SetBool("Hit1", false);
        animator.SetBool("Hit2", false);
        animator.SetBool("Exhausted", false);
    }
    #endregion

}
