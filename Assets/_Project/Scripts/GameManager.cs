using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum actions { ATACAR, ATACARFUERTE1, ATACARFUERTE2, PARRY1, PARRY2, ESQUIVAR, EXHAUST, NONE };

public enum RoundState
{
    NONE,
    SELECTING_ACTION,
    DOING_ACTIONS,
    GOING_NEXT_ROUND,
    FINISH_STAGE
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
    [SerializeField] private float roundDuration = 3f;
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

    [Header("SCENE MANAGMENT")]
    public string nextStage;
    public Animator fadeOut;
    public Text finishText;
    bool youWin = false;

    public GameObject fadeInGO;
    public GameObject fadeOutGO;

    #endregion


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
                        ///Miramos si siguen los dos vivos
                        if (myPlayer.GetLife() > 0 && enemy.GetLife() > 0)
                        {
                            ChangeRoundSate(RoundState.GOING_NEXT_ROUND);
                        }
                        else
                        {
                            if (enemy.GetLife() <= 0)
                            {
                                finishText.text = "YOU WIN%";
                                youWin = true;
                            }
                            else
                            {
                                finishText.text = "YOU LOSE%";
                                youWin = false;
                            }

                            ChangeRoundSate(RoundState.FINISH_STAGE);
                        }
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
            case RoundState.FINISH_STAGE:
                {
                    if (Input.anyKeyDown || Input.mousePresent)
                    {
                        fadeOut.SetBool("Active", true);
                        Invoke("LoadNExtScene", 1f);
                    }
                    break;
                }
            default:
                {
                    break;
                }
        }        
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
                    fadeInGO.SetActive(false);

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

                    //Comprobamos que no este vacio
                    /*
                    if (myPlayer.myActions.Count <= 0)
                    {
                        //si no has puesto acciones lo ponemos en exhausto
                        mesageText.text = "";

                        for (int i = 0; i < numRound; i++)
                        {
                            mesageText.text += actions.EXHAUST + "\n";
                            myPlayer.myActions.Add(actions.EXHAUST);
                        }

                    }
                    */
                    //comprobamos si faltan
                    while (myPlayer.myActions.Count < numRound)
                    {
                        myPlayer.myActions.Add(actions.EXHAUST);
                    }

                    //pedimos las acciones al enemigo
                    enemy.GetNewActions(numRound);
                    //Comprobar si player esta vacio
                    CompareActions();

                    //imprimimos por pantalla la lista de acciones del jugador
                    if (myPlayer.myActions.Count > 0)
                    {
                        mesageText.text = "";
                        for (int i = 0; i < myPlayer.myActions.Count; i++)
                        {
                            mesageText.text += myPlayer.myActions[i] + "\n";
                        }
                    }

                    enemyActionsText.text = "";
                    for (int i = 0; i < myPlayer.myActions.Count; i++)
                    {
                        enemyActionsText.text += enemy.enemyActions[i] + "\n";
                    }
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
                    if (numRound > 5)
                        numRound = 5;

                    roundText.text = "Round " + numRound;
                    //mesageText.text = "GOING TO THE ROUND  " + numRound + "!  WAITING...";
                    OnStartGame?.Invoke();

                    waitingRoundTimer = timeStartingRound;

                    aux = 0;
                    break;
                }
            case RoundState.FINISH_STAGE:
                {
                    fadeOutGO.SetActive(true);
                    roundState = RoundState.FINISH_STAGE;
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
      //  Debug.Log("Player Actions: ");
        for (int i = 0; i < myPlayer.myActions.Count; i++)
        {
            Debug.Log(myPlayer.myActions[i] +"\n");
        }
       // Debug.Log("Enemy Actions: " + enemy.enemyActions);
        for (int i = 0; i < enemy.enemyActions.Count; i++)
        {
            Debug.Log(enemy.enemyActions[i] + "\n");
        }

      //  Debug.Log("Comparamos acciones");

        do
        {
            switch (myPlayer.myActions[aux])
            {
                case actions.ATACAR:
                    switch (enemy.enemyActions[aux])
                    {
                        case actions.ATACAR:
                            //both get damage
                            myPlayer.getDamage(lightDamage);
                            enemy.getDamage(lightDamage);
                            break;

                        case actions.ATACARFUERTE1:
                            //Enemy get damage and an exhaust on next round
                            enemy.getDamage(lightDamage);
                            if (aux == numRound - 1)
                                enemy.extraAction = actions.EXHAUST;
                            else
                                enemy.enemyActions[aux + 1] = actions.EXHAUST;
                            break;

                        case actions.ATACARFUERTE2:
                            //Both get Damage
                            myPlayer.getDamage(heavyDamage);
                            enemy.getDamage(lightDamage);
                            break;

                        case actions.PARRY1:
                            //Player get Damage
                            myPlayer.getDamage(lightDamage);
                            break;

                        case actions.PARRY2:
                            //Enemy get damage
                            enemy.getDamage(lightDamage);
                            break;

                        case actions.ESQUIVAR:
                            //nothing
                            break;

                        case actions.EXHAUST:
                            //Enemy get damage
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
                            //Player get damage and exahust on next turn
                            myPlayer.getDamage(lightDamage);
                            myPlayer.myActions[aux + 1] = actions.EXHAUST;
                            break;

                        case actions.ATACARFUERTE1:
                            //nothing
                            break;

                        case actions.ATACARFUERTE2:
                            //Player get damage + exahust on next
                            myPlayer.getDamage(heavyDamage);
                            myPlayer.myActions[aux + 1] = actions.EXHAUST;
                            break;

                        case actions.PARRY1:
                            //falla el parry el enemigo
                            if ((aux + 1) == numRound)
                                enemy.extraAction = actions.EXHAUST;
                            else
                            {
                                //Check ataquefuerte in next
                                if (enemy.enemyActions[aux + 1] == actions.ATACARFUERTE1)
                                {
                                    enemy.enemyActions[aux + 1] = actions.EXHAUST;
                                    enemy.enemyActions[aux + 2] = actions.ATACAR;
                                }
                                else
                                    enemy.enemyActions[aux + 1] = actions.EXHAUST;
                            }
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
                            //Both get damage
                            myPlayer.getDamage(lightDamage);
                            enemy.getDamage(heavyDamage);
                            break;

                        case actions.ATACARFUERTE1:
                            //Enemy get damage + exahust
                            enemy.getDamage(heavyDamage);
                            enemy.enemyActions[aux + 1] = actions.EXHAUST;
                            break;

                        case actions.ATACARFUERTE2:
                            //both get damage
                            myPlayer.getDamage(heavyDamage);
                            enemy.getDamage(heavyDamage);
                            break;

                        case actions.PARRY1:
                            //Player get damage
                            myPlayer.getDamage(lightDamage);
                            break;

                        case actions.PARRY2:
                            //Enemy get damage
                            enemy.getDamage(heavyDamage);
                            break;

                        case actions.ESQUIVAR:
                            //Enemy get damage
                            enemy.getDamage(heavyDamage);
                            break;

                        case actions.EXHAUST:
                            //enemy get damage
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
                            //enemy get damage
                            enemy.getDamage(lightDamage);
                            break;

                        case actions.ATACARFUERTE1:
                            //next turn exahust 
                            if ((aux + 1) == numRound)
                                myPlayer.extraAction = actions.EXHAUST;
                            else
                            {
                                //Check ataquefuerte in next
                                if (myPlayer.myActions[aux + 1] == actions.ATACARFUERTE1)
                                {
                                    myPlayer.myActions[aux + 1] = actions.EXHAUST;
                                    myPlayer.myActions[aux + 2] = actions.ATACAR;
                                }
                                else
                                    myPlayer.myActions[aux + 1] = actions.EXHAUST;
                            }
                            break;

                        case actions.ATACARFUERTE2:
                            //Enemy get damage
                            enemy.getDamage(heavyDamage);
                            break;

                        case actions.PARRY1:
                            //Player next turn exahust 
                            if ((aux + 1) == numRound)
                            {
                                myPlayer.extraAction = actions.EXHAUST;
                                enemy.extraAction = actions.EXHAUST;
                            }
                            else
                            {
                                //Check ataquefuerte in next
                                if (myPlayer.myActions[aux + 1] == actions.ATACARFUERTE1)
                                {
                                    myPlayer.myActions[aux + 1] = actions.EXHAUST;
                                    myPlayer.myActions[aux + 2] = actions.ATACAR;
                                }
                                else
                                    myPlayer.myActions[aux + 1] = actions.EXHAUST;

                                if (enemy.enemyActions[aux + 1] == actions.ATACARFUERTE1)
                                {
                                    enemy.enemyActions[aux + 1] = actions.EXHAUST;
                                    enemy.enemyActions[aux + 2] = actions.ATACAR;
                                }
                                else
                                    enemy.enemyActions[aux + 1] = actions.EXHAUST;
                            }
                            
                            break;

                        case actions.PARRY2:
                            //next turn exahust 
                            if ((aux + 1) == numRound)
                                myPlayer.extraAction = actions.EXHAUST;
                            else
                            {
                                //Check ataquefuerte in next
                                if (myPlayer.myActions[aux + 1] == actions.ATACARFUERTE1)
                                {
                                    myPlayer.myActions[aux + 1] = actions.EXHAUST;
                                    myPlayer.myActions[aux + 2] = actions.ATACAR;
                                }
                                else
                                    myPlayer.myActions[aux + 1] = actions.EXHAUST;
                            }
                            break;

                        case actions.ESQUIVAR:
                            //next turn exahust 
                            if ((aux + 1) == numRound)
                                myPlayer.extraAction = actions.EXHAUST;
                            else
                            {
                                //Check ataquefuerte in next
                                if (myPlayer.myActions[aux + 1] == actions.ATACARFUERTE1)
                                {
                                    myPlayer.myActions[aux + 1] = actions.EXHAUST;
                                    myPlayer.myActions[aux + 2] = actions.ATACAR;
                                }
                                else
                                    myPlayer.myActions[aux + 1] = actions.EXHAUST;
                            }
                            break;

                        case actions.EXHAUST:
                            //next turn exahust 
                            if ((aux + 1) == numRound)
                                myPlayer.extraAction = actions.EXHAUST;
                            else
                            {
                                //Check ataquefuerte in next
                                if (myPlayer.myActions[aux + 1] == actions.ATACARFUERTE1)
                                {
                                    myPlayer.myActions[aux + 1] = actions.EXHAUST;
                                    myPlayer.myActions[aux + 2] = actions.ATACAR;
                                }
                                else
                                    myPlayer.myActions[aux + 1] = actions.EXHAUST;
                            }
                            break;

                        default:
                            break;
                    }
                    break;

                case actions.PARRY2:
                    switch (enemy.enemyActions[aux])
                    {
                        case actions.ATACAR:
                            //Player get damage
                            myPlayer.getDamage(lightDamage);
                            break;

                        case actions.ATACARFUERTE1:
                            //nothing
                            break;

                        case actions.ATACARFUERTE2:
                            //Player get damage
                            myPlayer.getDamage(heavyDamage);
                            break;

                        case actions.PARRY1:
                            //falla el parry el enemigo
                            if ((aux + 1) == numRound)
                                enemy.extraAction = actions.EXHAUST;
                            else
                            {
                                //Check ataquefuerte in next
                                if (enemy.enemyActions[aux + 1] == actions.ATACARFUERTE1)
                                {
                                    enemy.enemyActions[aux + 1] = actions.EXHAUST;
                                    enemy.enemyActions[aux + 2] = actions.ATACAR;
                                }
                                else
                                    enemy.enemyActions[aux + 1] = actions.EXHAUST;
                            }
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
                            //Nada
                            break;

                        case actions.ATACARFUERTE1:
                            //Nada
                            break;

                        case actions.ATACARFUERTE2:
                            //Player get damage
                            myPlayer.getDamage(heavyDamage);
                            break;

                        case actions.PARRY1:
                            //falla el parry el enemigo
                            if ((aux + 1) == numRound)
                                enemy.extraAction = actions.EXHAUST;
                            else
                            {
                                //Check ataquefuerte in next
                                if (enemy.enemyActions[aux + 1] == actions.ATACARFUERTE1)
                                {
                                    enemy.enemyActions[aux + 1] = actions.EXHAUST;
                                    enemy.enemyActions[aux + 2] = actions.ATACAR;
                                }
                                else
                                    enemy.enemyActions[aux + 1] = actions.EXHAUST;
                            }
                            break;

                        case actions.PARRY2:
                            //Nada
                            break;

                        case actions.ESQUIVAR:
                            //Nada
                            break;

                        case actions.EXHAUST:
                            //Nada
                            break;

                        default:
                            break;
                    }
                    break;

                case actions.EXHAUST:
                    switch (enemy.enemyActions[aux])
                    {
                        case actions.ATACAR:
                            //Player get damage
                            myPlayer.getDamage(lightDamage);
                            break;

                        case actions.ATACARFUERTE1:
                            //nothing
                            break;

                        case actions.ATACARFUERTE2:
                            //Player get damage
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

    #region LOAD NEXT SCENE
    void LoadNExtScene()
    {
        if (youWin)
        {
            SceneManager.LoadScene(nextStage);
        }
        else
        {
            SceneManager.LoadScene("Menu_Scene");
        }
    }
    #endregion

}
