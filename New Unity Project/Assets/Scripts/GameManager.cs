using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum actions { ATACAR, ATACARFUERTE1, ATACARFUERTE2, PARRY1, PARRY2, ESQUIVAR, EXHAUST, NONE };

public enum RoundState
{
    NONE,
    SELECTING_ACTION,
    DOING_ACTIONS,
    GOING_NEXT_ROUND
};

public class GameManager : MonoBehaviour
{
    #region VARIABLES
    //public//////////////////////
    public int numRound = 0;
    [Header("AGENTS")]
    public Player myPlayer;
    public Enemy enemy;
    [Header("TEXTS")]
    public Text roundText;
    public Text mesageText;
    public Text timerText;
    public Text enemyActionsText;
    [Header("TIMERS")]
    public float timeStartingRound = 3f;
    [SerializeField] private float roundDuration = 5f;

    //Private//////////////////////
    private float countDownRound;
    private List<actions> iaActions;
    private int aux;

    private RoundState roundState  = RoundState.NONE;
    private float waitingRoundTimer = 0f;

    #endregion

    #region METHODS

    #region START
     void Start()
    {
        //Initialize
        countDownRound = roundDuration;
        aux = 0;     

        ChangeRoundSate(RoundState.GOING_NEXT_ROUND);
    }
    #endregion

    #region FIXED UPDATE
    void FixedUpdate()
    {
        HandleRound();
        
    }
    #endregion

    #region HANDLE ROUND
    void HandleRound()
    {  

        switch (roundState)
        {
            case RoundState.NONE:
                {
                    break;
                }
            case RoundState.SELECTING_ACTION:
                {
                    waitingRoundTimer -= Time.deltaTime;
                    timerText.text = "Time for select: " + waitingRoundTimer;

                    if (waitingRoundTimer <= 0)
                    {
                        ChangeRoundSate(RoundState.DOING_ACTIONS);
                    }
                    break;
                }
            case RoundState.DOING_ACTIONS:
                {
                    waitingRoundTimer -= Time.deltaTime;
                    timerText.text = "Time foing actions: " + waitingRoundTimer;
                    if (waitingRoundTimer <= 0)
                    {
                        ChangeRoundSate(RoundState.GOING_NEXT_ROUND);
                    }
                    break;
                }
            case RoundState.GOING_NEXT_ROUND:
                {
                    waitingRoundTimer -= Time.deltaTime;
                    timerText.text = "Time to next Round: " + waitingRoundTimer;
                    if (waitingRoundTimer <= 0)
                    {
                        ChangeRoundSate(RoundState.SELECTING_ACTION);
                    }
                    break;
                }
            default:
                {
                    break;
                }
        }



        /* if (selecting)
         {
             if (countDownRound > 0f)
             {
                 countDownRound -= Time.deltaTime;
                 //Debug.Log(countDownRound);
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
             else
             {

             }
                 //doAnimations();
                 CompareActions();
             }                
         }*/
    }
    #endregion

    #region CHANGE ROUND STATE
    void ChangeRoundSate(RoundState newState)
    {
        switch (newState)
        {
            case RoundState.NONE:
                {
                    break;
                }            
            case RoundState.SELECTING_ACTION:
                {
                    Debug.Log("CHANGING TO SELECTING ACTIONS");
                    ResetCountDownRound();
                    roundState = RoundState.SELECTING_ACTION;
                    myPlayer.canSelect = true;

                    mesageText.text = "GOOOOOO!!! SELECT YOUR ACTIONS!!";

                    waitingRoundTimer = roundDuration;

                    break;
                }
            case RoundState.DOING_ACTIONS:
                {
                    Debug.Log("CHANGING TO DOING ACTIONS");
                    roundState = RoundState.DOING_ACTIONS;
                    myPlayer.canSelect = false;

                    waitingRoundTimer = roundDuration;

                    //imprimimos por pantalla la lista de acciones del jugador
                    if (myPlayer.myActions.Count > 0 )
                    {
                        mesageText.text = "";
                        for (int i = 0; i < myPlayer.myActions.Count; i++)
                        {
                            mesageText.text += myPlayer.myActions[i] + "\n";
                        }
                    }
                    else
                    {
                        //si no has puesto acciones lo ponemos en exhausto
                        mesageText.text = "";

                        for (int i = 0; i < numRound; i++)
                        {
                            mesageText.text +=actions.EXHAUST + "\n";
                        }

                    }

                    //pedimos las acciones al enemigo
                    enemy.GetNewActions(numRound);

                    enemyActionsText.text = "";
                    for (int i = 0; i < myPlayer.myActions.Count; i++)
                    {
                        enemyActionsText.text += enemy.enemyActions[i] + "\n";
                    }

                    //TO DO: Llamar a comparar las acciones y modificarla en funcion de sus sinergias

                    break;
                }
            case RoundState.GOING_NEXT_ROUND:
                {
                    Debug.Log("CHANGING TO GOING NEXT ROUND");

                    roundState = RoundState.GOING_NEXT_ROUND;
                    //Reseteamos acciones y tiempos
                    ResetCountDownRound();
                    myPlayer.ClearActions();
                    //Limpiamos las acciones del enemigo
                    enemy.ClearEnemyActions();
                    enemyActionsText.text = "";

                    numRound++;

                    roundText.text = "Round " + numRound;
                    mesageText.text = "GOING TO THE ROUND  " + numRound + "!  WAITING...";

                    waitingRoundTimer = timeStartingRound;
                    break;
                }
            default:
                {
                    break;
                }
        }
    }
    #endregion

    #region RESET COUNT DOWN ROUND
    void ResetCountDownRound()
        {
            countDownRound = roundDuration;
        }
    #endregion

    #region DO ANIMATIONS
    void doAnimations()
    {        
        myPlayer.DoAction(myPlayer.myActions[aux]);
        //activar animacion de ia
    }
    #endregion

    #region COMPARE ACTIONS
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
    #endregion

    #region FINISH ROUND
    void FinishRound()
    {
        ResetCountDownRound();
        myPlayer.ClearActions();
        numRound++;
    }
    #endregion
               
    #endregion
}
