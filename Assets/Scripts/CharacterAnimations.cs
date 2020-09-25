using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimations : MonoBehaviour
{
    #region VARIABLE
    public Animator animator;
    #endregion

    #region UPDATE
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            LighAttack();
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            HeavyAttack();
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            Parry();
        }
        else if (Input.GetKeyDown(KeyCode.F))
        {
            Dodge();
        }
    }
    #endregion

    #region LIGHT ATTACK
    public void LighAttack()
    {
        animator.SetBool("LightAttack", true);
        Invoke("GoToIdle", 1f);
    }
    #endregion

    #region HEAVY ATTACK
    public void HeavyAttack()
    {
        animator.SetBool("HeavyAttack", true);
        Invoke("GoToIdle", 2f);
    }
    #endregion

    #region PARRY
    public void Parry()
    {
        animator.SetBool("Parry", true);
        Invoke("GoToIdle", 0.5f);
    }
    #endregion

    #region DODGE
    public void Dodge()
    {
        animator.SetBool("Dodge", true);
        Invoke("GoToIdle", 0.5f);
    }
    #endregion

    #region GO TO IDLE
    public void GoToIdle()
    {
        animator.SetBool("HeavyAttack", false);
        animator.SetBool("LightAttack", false);
        animator.SetBool("Dodge", false);
        animator.SetBool("Parry", false);
    }
    #endregion

}
