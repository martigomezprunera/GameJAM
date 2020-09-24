using System;
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
    public Text lifePlayerText;
    public Enemy enemy;
    public Text lifeEnemyText;
    [Header("TEXTS")]
    public Text roundText;
    public Text mesageText;
    public Text timerText;
    public Text enemyActionsText;
    [Header("TIMERS")]
    public float timeStartingRound = 3f;
    [SerializeField] private float roundDuration = 5f;
    [Header("DAMAGES")]
    public int lightDamage = 10;
    public int heavyDamage = 20;

    //Private//////////////////////
    private float countDownRound;
    private List<actions> iaActions;
    private int aux;

    private RoundState roundState  = RoundState.NONE;
    private float waitingRoundTimer = 0f;

    public event Action OnStartGame;
    public event Action OnActionGame;
    public event Action OnFightGame;

    #endregion

    #region METHODS

    #region START
    void Start()
    {
        //Initialize
        countDownRound = roundDuration;
        aux = 0;

        OnStartGame?.Invoke();
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
        UpdateLife();

        switch (newState)
        {
            case RoundState.NONE:
                {
                    break;
                }            
            case RoundState.SELECTING_ACTION:
                {
                    ResetCountDownRound();
                    roundState = RoundState.SELECTING_ACTION;
                    myPlayer.canSelect = true;

                    //mesageText.text = "GOOOOOO!!! SELECT YOUR ACTIONS!!";

                    waitingRoundTimer = roundDuration;
                    OnActionGame?.Invoke();

                    break;
                }
            case RoundState.DOING_ACTIONS:
                {
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
                            myPlayer.myActions.Add(actions.EXHAUST);
                        }

                    }

                    //pedimos las acciones al enemigo
                    enemy.GetNewActions(numRound);

                    enemyActionsText.text = "";
                    for (int i = 0; i < myPlayer.myActions.Count; i++)
                    {
                        enemyActionsText.text += enemy.enemyActions[i] + "\n";
                    }

                    CompareActions();
                    OnFightGame?.Invoke();

                    break;
                }
            case RoundState.GOING_NEXT_ROUND:
                {
                    roundState = RoundState.GOING_NEXT_ROUND;
                    //Reseteamos acciones y tiempos
                    ResetCountDownRound();
                    myPlayer.ClearActions();
                    //Limpiamos las acciones del enemigo
                    enemy.ClearEnemyActions();
                    enemyActionsText.text = "";

                    numRound++;

                    roundText.text = "Round " + numRound;
                    //mesageText.text = "GOING TO THE ROUND  " + numRound + "!  WAITING...";
                    OnStartGame?.Invoke();

                    waitingRoundTimer = timeStartingRound;

                    aux = 0;
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
        Debug.Log("Player Actions: ");
        for (int i = 0; i < myPlayer.myActions.Count; i++)
        {
            Debug.Log(myPlayer.myActions[i] +"\n");
        }
        Debug.Log("Enemy Actions: " + enemy.enemyActions);
        for (int i = 0; i < enemy.enemyActions.Count; i++)
        {
            Debug.Log(enemy.enemyActions[i] + "\n");
        }

        do
        {
            switch (myPlayer.myActions[aux])
            {
                case actions.ATACAR:
                    switch (enemy.enemyActions[aux])
                    {
                        case actions.ATACAR:
                            myPlayer.getDamage(lightDamage);
                            enemy.getDamage(lightDamage);
                            break;
                        case actions.ATACARFUERTE1:
                            enemy.getDamage(lightDamage);

                            if (aux == numRound - 1)
                            {
                                enemy.extraAction = actions.EXHAUST;
                            }
                            else
                            {
                                enemy.enemyActions[aux + 1] = actions.EXHAUST;
                            }
                            break;
                        case actions.ATACARFUERTE2:
                            myPlayer.getDamage(heavyDamage);
                            enemy.getDamage(lightDamage);
                            break;
                        case actions.PARRY1:
                            myPlayer.getDamage(lightDamage);
                            break;
                        case actions.PARRY2:
                            enemy.getDamage(lightDamage);
                            break;
                        case actions.ESQUIVAR:
                            //nothing
                            break;
                        case actions.EXHAUST:
                            enemy.getDamage(lightDamage);
                            break;
                        default:
                            break;
                    }
                    break;
                case actions.ATACARFUERTE1:
                    switch (enemy.enemyActions[aux])
                    {
                        case actions.ATACAR:
                            myPlayer.getDamage(lightDamage);

                            if (aux == numRound - 1)
                            {
                                myPlayer.extraAction = actions.EXHAUST;
                            }
                            else
                            {
                                myPlayer.myActions[aux + 1] = actions.EXHAUST;
                            }
                            break;
                        case actions.ATACARFUERTE1:
                            //nada
                            break;
                        case actions.ATACARFUERTE2:
                            myPlayer.getDamage(heavyDamage);

                            if (aux == numRound - 1)
                            {
                                myPlayer.extraAction = actions.EXHAUST;
                            }
                            else
                            {
                                myPlayer.myActions[aux + 1] = actions.EXHAUST;
                            }
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
                        case actions.EXHAUST:
                            //Nothing
                            break;
                        default:
                            break;
                    }
                    break;
                case actions.ATACARFUERTE2:
                    switch (enemy.enemyActions[aux])
                    {
                        case actions.ATACAR:
                            myPlayer.getDamage(lightDamage);
                            enemy.getDamage(heavyDamage);
                            break;
                        case actions.ATACARFUERTE1:
                            enemy.getDamage(heavyDamage);

                            if (aux < numRound - 1)
                            {
                                enemy.enemyActions[aux + 1] = actions.EXHAUST;
                            }
                            else
                            {
                                enemy.extraAction = actions.EXHAUST;                                
                            }
                            break;
                        case actions.ATACARFUERTE2:
                            myPlayer.getDamage(heavyDamage);
                            enemy.getDamage(heavyDamage);
                            break;
                        case actions.PARRY1:
                            myPlayer.getDamage(lightDamage);
                            break;
                        case actions.PARRY2:
                            enemy.getDamage(heavyDamage);
                            break;
                        case actions.ESQUIVAR:
                            //nothing
                            break;
                        case actions.EXHAUST:
                            enemy.getDamage(heavyDamage);
                            break;
                        default:
                            break;
                    }
                    break;
                case actions.PARRY1:
                    switch (enemy.enemyActions[aux])
                    {
                        case actions.ATACAR:
                            enemy.getDamage(lightDamage);
                            break;
                        case actions.ATACARFUERTE1:
                            //nada
                            break;
                        case actions.ATACARFUERTE2:
                            enemy.getDamage(heavyDamage);
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
                        case actions.EXHAUST:
                            //nada
                            break;
                        default:
                            break;
                    }
                    break;
                case actions.PARRY2:
                    switch (enemy.enemyActions[aux])
                    {
                        case actions.ATACAR:
                            myPlayer.getDamage(lightDamage);
                            break;
                        case actions.ATACARFUERTE1:
                            //nothing
                            break;
                        case actions.ATACARFUERTE2:
                            myPlayer.getDamage(heavyDamage);
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
                        case actions.EXHAUST:
                            //nada
                            break;
                        default:
                            break;
                    }
                    break;
                case actions.ESQUIVAR:
                    switch (enemy.enemyActions[aux])
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
                        case actions.EXHAUST:
                            //nada
                            break;
                        default:
                            break;
                    }
                    break;
                case actions.EXHAUST:
                    switch (enemy.enemyActions[aux])
                    {
                        case actions.ATACAR:
                            myPlayer.getDamage(lightDamage);
                            break;
                        case actions.ATACARFUERTE1:
                            break;
                        case actions.ATACARFUERTE2:
                            myPlayer.getDamage(heavyDamage);
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
                        case actions.EXHAUST:
                            //nada
                            break;
                        default:
                            break;
                    }
                    break;
                default:
                    break;
            }
            aux++;
            //doAnimations();
            UpdateLife();
        }while (aux < numRound);

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

    #region  UPDATE LIFE
    void UpdateLife()
    {
        lifePlayerText.text = "Life: " + myPlayer.GetLife();
        lifeEnemyText.text = "Life: " + enemy.GetLife();
    }
    #endregion

    #region  GET COUNTDOWNROUND
    public float GetCountDownRound()
    {
        return waitingRoundTimer;
    }
    #endregion

    #region  GET ROUNDSTATE
    public RoundState GetroundState()
    {
        return roundState;
    }
    #endregion

    #endregion
}
