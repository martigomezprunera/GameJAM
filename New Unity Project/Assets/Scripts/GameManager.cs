using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    #endregion

    #region METHODS
    void Start()
    {
        //Initialize
        roundDuration = 3f;
        countDownRound = roundDuration;
        numRound = 1;
        selectingActions = true;
    }

    void resetCountDownRound()
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
                CompareActions();
        }
    }

    void CompareActions()
    {
        Debug.Log("Se comparan las acciones");
        resetCountDownRound();
        selectingActions = false;
        myPlayer.ClearActions();
        numRound++;
        // do animations and when finish activate selectingActions
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
