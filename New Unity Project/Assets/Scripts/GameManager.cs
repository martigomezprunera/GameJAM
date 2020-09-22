using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum actions { ATACAR, ATACARFUERTE1, ATACARFUERTE2, PARRY1, PARRY2, ESQUIVAR, EXHAUST };

public class GameManager : MonoBehaviour
{
    #region VARIABLES
    //public//////////////////////
    public int numRound;
    public Player myPlayer;

    //Private//////////////////////
    private float countDownRound;
    private bool selectingActions;
    private float roundDuration;
    private List<actions> iaActions;
    private int aux;
    #endregion

    #region METHODS
    void Start()
    {
        //Initialize
        roundDuration = 3f;
        countDownRound = roundDuration;
        numRound = 1;
        aux = 0;
        selectingActions = true;
    }

    void ResetCountDownRound()
    {
        countDownRound = roundDuration;
    }

    void HandleRound(bool selecting)
    {
        if (selecting)
        {
            if (countDownRound > 0f)
            {
                countDownRound -= Time.deltaTime;
                Debug.Log(countDownRound);
            }
            else
            {
                if (myPlayer.myActions.Count == 0)
                {
                    for (int i = 0; i < numRound; i++)
                    {
                        myPlayer.myActions.Add(actions.EXHAUST);
                    }
                }
                //doAnimations();
                CompareActions();
            }                
        }
    }

    void doAnimations()
    {        
        myPlayer.DoAction(myPlayer.myActions[aux]);
        //activar animacion de ia
    }

    void CompareActions()
    {
        /*if (aux < numRound)
        {
            switch (myPlayer.myActions[aux])
            {
                case actions.ATACAR:
                    switch (iaActions[aux])
                    {
                        case actions.ATACAR:
                            myPlayer.getDamage(10);
                            break;
                        case actions.ATACARFUERTE1:
                            //agent get light damage
                            break;
                        case actions.ATACARFUERTE2:
                            myPlayer.getDamage(20);
                            //agent get light damage
                            break;
                        case actions.PARRY1:
                            myPlayer.getDamage(10);
                            break;
                        case actions.PARRY2:
                            //agent get light damage
                            break;
                        case actions.ESQUIVAR:
                            //nothing
                            break;
                        default:
                            break;
                    }
                    break;
                case actions.ATACARFUERTE1:
                    switch (iaActions[aux])
                    {
                        case actions.ATACAR:
                            myPlayer.getDamage(10);
                            break;
                        case actions.ATACARFUERTE1:
                            //nada
                            break;
                        case actions.ATACARFUERTE2:
                            myPlayer.getDamage(20);
                            //Cortar el siguiente movimiento de myplayer
                            break;
                        case actions.PARRY1:
                            //Nothing
                            break;
                        case actions.PARRY2:
                            //Nothing
                            break;
                        case actions.ESQUIVAR:
                            //Nothing
                            break;
                        default:
                            break;
                    }
                    break;
                case actions.ATACARFUERTE2:
                    switch (iaActions[aux])
                    {
                        case actions.ATACAR:
                            myPlayer.getDamage(10);
                            //agent get heavy damage
                            break;
                        case actions.ATACARFUERTE1:
                            //agent get heavy damage y cortar su siguiente movimiento
                            break;
                        case actions.ATACARFUERTE2:
                            myPlayer.getDamage(20);
                            //agent get heavy damage
                            break;
                        case actions.PARRY1:
                            myPlayer.getDamage(10);
                            break;
                        case actions.PARRY2:
                            //agent get heavy damage
                            break;
                        case actions.ESQUIVAR:
                            //nothing
                            break;
                        default:
                            break;
                    }
                    break;
                case actions.PARRY1:
                    switch (iaActions[aux])
                    {
                        case actions.ATACAR:
                            //agent get light damage
                            break;
                        case actions.ATACARFUERTE1:
                            //nada
                            break;
                        case actions.ATACARFUERTE2:
                            //agent get light damage
                            break;
                        case actions.PARRY1:
                            //nada
                            break;
                        case actions.PARRY2:
                            //nada
                            break;
                        case actions.ESQUIVAR:
                            //nada
                            break;
                        default:
                            break;
                    }
                    break;
                case actions.PARRY2:
                    switch (iaActions[aux])
                    {
                        case actions.ATACAR:
                            myPlayer.getDamage(10);
                            break;
                        case actions.ATACARFUERTE1:
                            //nothing
                            break;
                        case actions.ATACARFUERTE2:
                            myPlayer.getDamage(20);
                            break;
                        case actions.PARRY1:
                            //nothing
                            break;
                        case actions.PARRY2:
                            //nothing
                            break;
                        case actions.ESQUIVAR:
                            //nothing
                            break;
                        default:
                            break;
                    }
                    break;
                case actions.ESQUIVAR:
                    switch (iaActions[aux])
                    {
                        case actions.ATACAR:
                            break;
                        case actions.ATACARFUERTE1:
                            break;
                        case actions.ATACARFUERTE2:
                            break;
                        case actions.PARRY1:
                            break;
                        case actions.PARRY2:
                            break;
                        case actions.ESQUIVAR:
                            break;
                        default:
                            break;
                    }
                    break;
                default:
                    break;
            }
            aux++;
            doAnimations();
        }
        else*/
            FinishRound();

    }

    void FinishRound()
    {
        ResetCountDownRound();
        selectingActions = false;
        myPlayer.ClearActions();
        numRound++;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        HandleRound(selectingActions);

        if (Input.GetKeyDown("space"))
        {
            selectingActions = true; 
        }
    }
    #endregion
}
