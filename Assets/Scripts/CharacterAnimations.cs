using System;
using UnityEngine;

public class CharacterAnimations : MonoBehaviour
{
    #region VARIABLE
    public Animator animator;
    #endregion


    #region EVENTS

    public event Action OnSlash;
    public event Action OnHit;
    public event Action OnParrySlash;
    public event Action OnParry;
    public void TriggerSlash()
    {
        OnSlash?.Invoke();
    }
    public void TriggerHit()
    {
        OnHit?.Invoke();
    }
    public void TriggerParry()
    {
        OnParry?.Invoke();
    }
    public void TriggerParrySlash()
    {
        OnParrySlash?.Invoke();
    }

    #endregion

    #region LIGHT ATTACK
    public void LighAttack()
    {
        animator.SetTrigger("Attack");
    }
    #endregion

    #region CHARGING HEAVY
    public void ChargingHeavy()
    {
        animator.SetTrigger("HeavyAttack");
    }
    #endregion

    #region HEAVY ATTACK
    public void HeavyAttack()
    {
        animator.SetTrigger("HeavyAttack");
        animator.SetBool("ChargingHeavy", false);
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
        if (animator.GetBool("ChargingHeavy"))
        {
            animator.SetTrigger("HitCharged");
            animator.SetBool("ChargingHeavy", false);
        }
        else 
        {
            animator.SetTrigger("Hit");
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
        animator.SetBool("ChargingHeavy", false);
        animator.SetBool("Exhausted", false);
        animator.SetBool("HeavyCancelled", false);
    }
    #endregion

}
