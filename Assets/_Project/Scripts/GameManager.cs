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
    [SerializeField] private List<actions> lastPlayerActions;
    [SerializeField] private List<actions> lastEnemyActions;
    private bool copyActions = false;
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

    [Header("HUDPlayer")]
    public HUD myHud;

    [Header("ANIMATIONS")]
    public CharacterAnimations characterAnimations;
    public CharacterAnimations enemyAnimations;

    public float timerAnimations = 2.5f;
    public float timerAux = 0;

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

    #region UPDATE
    void Update()
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

                    if (aux < numRound)
                    {
                        CompareActions();
                        if (myPlayer.GetLife() <= 0 || enemy.GetLife() <= 0)
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
                    else
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

                    //UNFILLHUD PLAYER
                    UnfillHUDPlayer();

                    //UNFILLHUD ENEMY
                    //UnfillHUDEnemy();

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

                    lastPlayerActions.Clear();
                    lastEnemyActions.Clear();

                    fadeInGO.SetActive(false);

                    ResetCountDownRound();
                    roundState = RoundState.SELECTING_ACTION;
                    myPlayer.canSelect = true;

                    //mesageText.text = "GOOOOOO!!! SELECT YOUR ACTIONS!!";

                    waitingRoundTimer = roundDuration;
                    OnActionGame?.Invoke();
                    aux = 0;
                    copyActions = true;
                    break;
                }
            case RoundState.DOING_ACTIONS:
                {
                    roundState = RoundState.DOING_ACTIONS;
                    myPlayer.canSelect = false;

                    waitingRoundTimer = roundDuration;

                    //comprobamos si faltan
                    while (myPlayer.myActions.Count < numRound)
                    {
                        myPlayer.myActions.Add(actions.EXHAUST);
                    }

                    //pedimos las acciones al enemigo
                    //enemy.GetNewActions(numRound);                    
                    //enemy.GetNewActions(numRound);

                    //FILLHUDENEMY
                    enemy.FillHUDEnemy();

                    //Comprobar si player esta vacio
                    //CompareActions();


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

                    if (numRound > enemy.numRound)
                        numRound = enemy.numRound;

                    roundText.text = "Round " + numRound;
                    //mesageText.text = "GOING TO THE ROUND  " + numRound + "!  WAITING...";
                    OnStartGame?.Invoke();

                    waitingRoundTimer = timeStartingRound;
                    enemy.GetNewActions(numRound);

                    //aux = 0;
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
        timerAux += Time.deltaTime;
        if (timerAux >= timerAnimations)
        {
            timerAux = 0;
            switch (myPlayer.myActions[aux])
            {
                case actions.ATACAR:
                    switch (enemy.enemyActions[aux])
                    {
                        case actions.ATACAR:
                            //LLamamos a las animaciones
                            characterAnimations.LighAttack();
                            enemyAnimations.LighAttack();

                            //myPlayer.getDamage(lightDamage);
                            //enemy.getDamage(lightDamage);
                            break;

                        case actions.ATACARFUERTE1:
                            //LLamamos a las animaciones
                            characterAnimations.LighAttack();
                            enemyAnimations.ChargingHeavy();

                            //Enemy get damage and an exhaust on next round
                            //enemy.getDamage(lightDamage);
                            //if (aux == numRound - 1)
                            //    enemy.extraAction = actions.EXHAUST;
                            //else
                            //    enemy.enemyActions[aux + 1] = actions.EXHAUST;
                            break;

                        case actions.ATACARFUERTE2:
                            //LLamamos a las animaciones
                            characterAnimations.LighAttack();
                            enemyAnimations.HeavyAttack();

                            //Both get Damage
                            //myPlayer.getDamage(heavyDamage);
                            //enemy.getDamage(lightDamage);
                            break;

                        case actions.PARRY1:
                            //LLamamos a las animaciones
                            characterAnimations.LighAttack();
                            enemyAnimations.Parry();

                            //Player get Damage
                            //myPlayer.getDamage(lightDamage);
                            break;

                        case actions.PARRY2:
                            //LLamamos a las animaciones
                            characterAnimations.LighAttack();

                            //Enemy get damage
                            //enemy.getDamage(lightDamage);
                            break;

                        case actions.ESQUIVAR:
                            //LLamamos a las animaciones
                            characterAnimations.LighAttack();
                            enemyAnimations.Dodge();

                            //nothing
                            break;

                        case actions.EXHAUST:
                            //LLamamos a las animaciones
                            characterAnimations.LighAttack();
                            enemyAnimations.Exhaust();

                            //Enemy get damage
                            //enemy.getDamage(lightDamage);
                            break;

                        default:
                            break;
                    }
                    break;
                case actions.ATACARFUERTE1:
                    switch (enemy.enemyActions[aux])
                    {
                        case actions.ATACAR:
                            //LLamamos a las animaciones
                            characterAnimations.ChargingHeavy();
                            enemyAnimations.LighAttack();

                            //Player get damage and exahust on next turn
                            //myPlayer.getDamage(lightDamage);
                            break;

                        case actions.ATACARFUERTE1:
                            //LLamamos a las animaciones
                            characterAnimations.ChargingHeavy();
                            enemyAnimations.ChargingHeavy();
                            //nothing
                            break;
                        case actions.PARRY1:
                            //LLamamos a las animaciones
                            characterAnimations.ChargingHeavy();
                            enemyAnimations.ParryFail();
                            
                            break;

                        case actions.PARRY2:
                            //LLamamos a las animaciones
                            characterAnimations.ChargingHeavy();

                            //Nothing
                            break;
                        case actions.ESQUIVAR:
                            //LLamamos a las animaciones
                            characterAnimations.ChargingHeavy();
                            enemyAnimations.Dodge();

                            //Nothing
                            break;
                        case actions.EXHAUST:
                            //LLamamos a las animaciones
                            characterAnimations.ChargingHeavy();
                            enemyAnimations.Exhaust();
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
                            //LLamamos a las animaciones
                            characterAnimations.GoToIdle();
                            characterAnimations.HeavyAttack();
                            enemyAnimations.LighAttack();

                            //Both get damage
                            //myPlayer.getDamage(lightDamage);
                            //enemy.getDamage(heavyDamage);
                            break;

                        case actions.ATACARFUERTE1:
                            //LLamamos a las animaciones
                            characterAnimations.GoToIdle();
                            characterAnimations.HeavyAttack();
                            enemyAnimations.ChargingHeavy();

                            //Enemy get damage + exahust
                            //enemy.getDamage(heavyDamage);
                            break;

                        case actions.ATACARFUERTE2:
                            //LLamamos a las animaciones
                            characterAnimations.GoToIdle();
                            characterAnimations.HeavyAttack();
                            enemyAnimations.HeavyAttack();

                            //both get damage
                            //myPlayer.getDamage(heavyDamage);
                            //enemy.getDamage(heavyDamage);
                            break;

                        case actions.PARRY1:
                            //LLamamos a las animaciones
                            characterAnimations.GoToIdle();
                            characterAnimations.HeavyAttack();
                            enemyAnimations.Parry();

                            //Player get damage
                            //myPlayer.getDamage(lightDamage);
                            break;

                        case actions.PARRY2:
                            //LLamamos a las animaciones
                            characterAnimations.GoToIdle();
                            characterAnimations.HeavyAttack();

                            //Enemy get damage
                            //enemy.getDamage(heavyDamage);
                            break;

                        case actions.ESQUIVAR:
                            //LLamamos a las animaciones
                            characterAnimations.GoToIdle();
                            characterAnimations.HeavyAttack();
                            enemyAnimations.Dodge();

                            //Enemy get damage
                            //enemy.getDamage(heavyDamage);
                            break;

                        case actions.EXHAUST:
                            //LLamamos a las animaciones
                            characterAnimations.GoToIdle();
                            characterAnimations.HeavyAttack();
                            enemyAnimations.Exhaust();

                            //enemy get damage
                            //enemy.getDamage(heavyDamage);
                            break;

                        default:
                            break;
                    }
                    break;

                case actions.PARRY1:
                    switch (enemy.enemyActions[aux])
                    {
                        case actions.ATACAR:
                            //LLamamos a las animaciones
                            characterAnimations.Parry();
                            enemyAnimations.LighAttack();

                            //enemy get damage
                            //enemy.getDamage(lightDamage);
                            break;

                        case actions.ATACARFUERTE1:
                            //LLamamos a las animaciones
                            characterAnimations.ParryFail();
                            enemyAnimations.ChargingHeavy();
                            break;

                        case actions.ATACARFUERTE2:
                            //LLamamos a las animaciones
                            characterAnimations.Parry();
                            enemyAnimations.HeavyAttack();

                            //Enemy get damage
                            //enemy.getDamage(heavyDamage);
                            break;

                        case actions.PARRY1:
                            //LLamamos a las animaciones
                            characterAnimations.ParryFail();
                            enemyAnimations.ParryFail();
                            //enemyAnimations.Parry();                         

                            break;
                        case actions.EXHAUST:
                            //LLamamos a las animaciones
                            characterAnimations.ParryFail();
                            enemyAnimations.Exhaust();
                            break;

                        default:
                            break;
                    }
                    break;
                case actions.EXHAUST:
                    switch (enemy.enemyActions[aux])
                    {
                        case actions.ATACAR:
                            enemyAnimations.LighAttack();
                            break;

                        case actions.ATACARFUERTE1:
                            enemyAnimations.ChargingHeavy();
                            break;
                        case actions.PARRY1:
                            enemyAnimations.ParryFail();
                            break;
                        case actions.EXHAUST:
                            break;

                        default:
                            break;
                    }
                    break;
                default:
                    break;
            }
            aux++;
           // Debug.Log("Aux: " + aux);
            if (copyActions)
            {
                //nos guardamos las acciones para las animaciones
                for (int i=0; i < myPlayer.myActions.Count; i++)
                {
                    lastPlayerActions.Add(myPlayer.myActions[i]);
                    lastEnemyActions.Add(enemy.enemyActions[i]);
                }
                copyActions = false;
            }
        }
        waitingRoundTimer = roundDuration;
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

    #region UnfillHUDPlayer
    void UnfillHUDPlayer()
    {
        for (int i = 0; i < numRound; i++)
        {
            myHud.actionTextPlayer1[i].text = " ";
        }

        if(myPlayer.myActions.Count > 0)
        {
            myHud.actionTextPlayer1[0].text = "E";
        }
    }
    #endregion

    #region UnfillHUDEnemy
    void UnfillHUDEnemy()
    {
        for (int i = 0; i < numRound; i++)
        {
            myHud.actionTextEnemy1[i].text = " ";
        }

        if (myPlayer.myActions.Count > 0)
        {
            myHud.actionTextEnemy1[0].text = "E";
        }
    }
    #endregion

    #region CheckNextAnimationPlayer
    public void CheckNextAnimationPlayer(int id)
    {
        if (id == 0)
        {
            if (lastPlayerActions[aux - 1] == actions.ATACAR)
            {
                if (lastEnemyActions[aux - 1] == actions.ATACARFUERTE1)
                {
                    //enemy hitted
                    enemyAnimations.Hit();
                    enemy.getDamage(lightDamage);
                    //ps

                }
                else if (lastEnemyActions[aux - 1] == actions.ATACAR)
                {
                    enemyAnimations.Hit();
                    characterAnimations.Hit();
                }
            }
            else if (lastPlayerActions[aux - 1] == actions.ATACARFUERTE1)
            {
                if (lastEnemyActions[aux - 1] != actions.ATACAR)
                {
                    //enemy hitted
                    enemyAnimations.Hit();
                    enemy.getDamage(heavyDamage);
                    Debug.Log("HOSTIA PA TI!!");
                    //ps
                }
            }

            //Check si hace parry
            if (lastPlayerActions[aux - 1] == actions.PARRY1)
            {
                if ((lastEnemyActions[aux - 1] == actions.ATACAR) )
                {
                    //Enemy Hitted
                    enemyAnimations.Hit();
                    
                    enemy.getDamage(lightDamage);
                    //ps
                }
            }


            myPlayer.Sounds.PlaySound("Slash");

        }
        else
        {
            //Check si player esta atacando            
            if (lastEnemyActions[aux - 1] == actions.ATACAR)
            {
                if (lastPlayerActions[aux - 1] == actions.ATACARFUERTE1)
                {
                    //Character hitted
                    characterAnimations.Hit();
                    myPlayer.getDamage(lightDamage);
                    //ps
                }
                else if (lastPlayerActions[aux - 1] == actions.ATACAR)
                {
                    enemyAnimations.Hit();
                    characterAnimations.Hit();
                }
                else if (lastPlayerActions[aux - 1] == actions.EXHAUST)
                {
                    //Character hitted
                    characterAnimations.Hit();
                    myPlayer.getDamage(lightDamage);
                    //ps
                }
            }
            else if (lastEnemyActions[aux - 1] == actions.ATACARFUERTE1)
            {
                if (lastPlayerActions[aux - 1] != actions.ATACAR)
                {
                    //Character hitted
                    characterAnimations.Hit();
                    myPlayer.getDamage(heavyDamage);
                    //ps
                }
                else if (lastPlayerActions[aux - 1] == actions.EXHAUST)
                {
                    //Character hitted
                    characterAnimations.Hit();
                    myPlayer.getDamage(heavyDamage);
                    //ps
                }
            }

            //Check si hace parry
            if (lastEnemyActions[aux - 1] == actions.PARRY1)
            {
                if (lastPlayerActions[aux - 1] == actions.ATACAR)
                {
                    //Character hitted
                    characterAnimations.Hit();
                   
                    myPlayer.getDamage(lightDamage);
                    //ps
                }
            }

            enemy.Sounds.PlaySound("Slash");
        }

    }
    #endregion

}
