﻿using System;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObjectSounds _characterSounds = null;
    public GameObjectSounds Sounds => _characterSounds;
    [SerializeField] private CharacterAnimations _characterAnimations = null;

    [Header("Settings")]
    [SerializeField]  public List<actions> enemyActions;
    public actions extraAction = actions.NONE;


    [SerializeField] private int life = 100;
    [SerializeField] public int id = 1;
    [SerializeField] public int numRound;

    //HUD
    public HUD myHud;

    //Game Manager
    GameManager myGameManager;

    private void Awake()
    {
        if (_characterSounds == null)
            throw new NullReferenceException("_characterSounds has not been assigned at" + GetType());
        if (_characterAnimations == null)
            throw new NullReferenceException("_characterAnimations has not been assigned at" + GetType());

        _characterAnimations.OnHit += CheckNextAnimation;
    }

    private void Start()
    {
        myGameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    #region GET NEW ACTION
    public void GetNewActions(int numRound)
    {
        
        int numActionsToAdd = numRound;
        int action;
        action = UnityEngine.Random.Range(1, 5);

        //Fijamos en 5 el maximo
        if (numRound > 5)
            numActionsToAdd = 5;

        //Si arrastyramos exahust añadimos una accion menos
        if (enemyActions.Count > 0)
            numActionsToAdd--;

        //Dependiendo del ID
        switch (id)
        {
            #region PRIMER ENEMIGO 
            case 1:
                switch (numActionsToAdd)
                {
                    #region RONDA 1
                    case 1:
                        action = UnityEngine.Random.Range(1, 3);
                        switch (action)
                        {
                            case 1:
                                enemyActions.Add(actions.ATACAR);
                                break;

                            case 2:
                                enemyActions.Add(actions.ATACARFUERTE1);
                                break;

                            case 3:
                                enemyActions.Add(actions.PARRY1);
                                break;

                            /*case 4:
                                enemyActions.Add(actions.PARRY1);
                                break;

                            case 5:
                                enemyActions.Add(actions.ATACAR);
                                break;*/

                            default:
                                break;
                        }
                        break;
                    #endregion

                    #region RONDA 2
                    case 2:
                        action = UnityEngine.Random.Range(1, 2);
                        switch (action)
                        {
                            case 1:
                                enemyActions.Add(actions.ATACAR);
                                enemyActions.Add(actions.PARRY1);
                                break;

                            case 2:
                                enemyActions.Add(actions.ATACARFUERTE1);
                                enemyActions.Add(actions.ATACAR);
                                break;

                            /*case 3:
                                enemyActions.Add(actions.PARRY1);
                                enemyActions.Add(actions.ATACAR);
                                break;

                            case 4:
                                enemyActions.Add(actions.ATACARFUERTE1);
                                enemyActions.Add(actions.ATACARFUERTE2);
                                break;

                            case 5:
                                enemyActions.Add(actions.ATACAR);
                                enemyActions.Add(actions.ESQUIVAR);
                                break;*/

                            default:
                                break;
                        }
                        break;
                    #endregion

                    #region RONDA 3
                    case 3:
                        action = UnityEngine.Random.Range(1, 2);
                        switch (action)
                        {
                            case 1:
                                enemyActions.Add(actions.PARRY1);
                                enemyActions.Add(actions.ATACAR);
                                enemyActions.Add(actions.ATACARFUERTE1);
                                break;

                            case 2:
                                enemyActions.Add(actions.PARRY1);
                                enemyActions.Add(actions.ATACAR);
                                enemyActions.Add(actions.ATACAR);
                                break;

                            /*case 3:
                                enemyActions.Add(actions.ATACARFUERTE1);
                                enemyActions.Add(actions.ATACARFUERTE2);
                                enemyActions.Add(actions.ATACAR);
                                break;

                            case 4:
                                enemyActions.Add(actions.ESQUIVAR);
                                enemyActions.Add(actions.ATACAR);
                                enemyActions.Add(actions.ESQUIVAR);
                                break;

                            case 5:
                                enemyActions.Add(actions.ATACAR);
                                enemyActions.Add(actions.ATACARFUERTE1);
                                enemyActions.Add(actions.ATACARFUERTE2);
                                break;*/

                            default:
                                break;
                        }
                        break;
                    #endregion

                    #region RONDA 4
                    case 4:
                        action = UnityEngine.Random.Range(1, 2);
                        switch (action)
                        {
                            case 1:
                                enemyActions.Add(actions.PARRY1);
                                enemyActions.Add(actions.ATACAR);
                                enemyActions.Add(actions.ATACARFUERTE1);
                                enemyActions.Add(actions.PARRY1);
                                break;

                            case 2:
                                enemyActions.Add(actions.PARRY1);
                                enemyActions.Add(actions.ATACAR);
                                enemyActions.Add(actions.ATACARFUERTE1);
                                enemyActions.Add(actions.ATACAR);
                                break;

                            /*case 3:
                                enemyActions.Add(actions.ATACARFUERTE1);
                                enemyActions.Add(actions.ATACARFUERTE2);
                                enemyActions.Add(actions.ESQUIVAR);
                                enemyActions.Add(actions.PARRY1);
                                break;

                            case 4:
                                enemyActions.Add(actions.ESQUIVAR);
                                enemyActions.Add(actions.PARRY1);
                                enemyActions.Add(actions.ATACARFUERTE1);
                                enemyActions.Add(actions.ATACARFUERTE2);
                                break;

                            case 5:
                                enemyActions.Add(actions.ATACAR);
                                enemyActions.Add(actions.PARRY1);
                                enemyActions.Add(actions.ATACAR);
                                enemyActions.Add(actions.ESQUIVAR);
                                break;*/

                            default:
                                break;
                        }
                        break;
                    #endregion

                    #region RONDA 5
                    case 5:
                        action = UnityEngine.Random.Range(1, 3);
                        switch (action)
                        {
                            case 1:
                                enemyActions.Add(actions.ATACARFUERTE1);
                                enemyActions.Add(actions.ATACAR);
                                enemyActions.Add(actions.PARRY1);
                                enemyActions.Add(actions.ATACARFUERTE1);
                                enemyActions.Add(actions.PARRY1);
                                break;

                            case 2:
                                enemyActions.Add(actions.ATACAR);
                                enemyActions.Add(actions.PARRY1);
                                enemyActions.Add(actions.ATACARFUERTE1);
                                enemyActions.Add(actions.PARRY1);
                                enemyActions.Add(actions.ATACAR);
                                break;

                            case 3:
                                enemyActions.Add(actions.PARRY1);
                                enemyActions.Add(actions.ATACAR);
                                enemyActions.Add(actions.ATACARFUERTE1);
                                enemyActions.Add(actions.ATACAR);
                                enemyActions.Add(actions.ATACAR);
                                break;

                            /*case 4:
                                enemyActions.Add(actions.ATACAR);
                                enemyActions.Add(actions.ATACARFUERTE1);
                                enemyActions.Add(actions.ATACARFUERTE2);
                                enemyActions.Add(actions.ESQUIVAR);
                                enemyActions.Add(actions.PARRY1);
                                break;

                            case 5:
                                enemyActions.Add(actions.ESQUIVAR);
                                enemyActions.Add(actions.PARRY1);
                                enemyActions.Add(actions.ESQUIVAR);
                                enemyActions.Add(actions.ATACAR);
                                enemyActions.Add(actions.ATACAR);
                                break;*/

                            default:
                                break;
                        }
                        break;
                    #endregion

                    default:
                        break;
                }
                break;
            #endregion

            #region SEGUNDO ENEMIGO
            case 2:
                switch (numRound)
                {
                    #region RONDA 1
                    case 1:
                        switch (action)
                        {
                            case 1:
                                enemyActions.Add(actions.ATACAR);
                                break;

                            case 2:
                                enemyActions.Add(actions.ESQUIVAR);
                                break;

                            case 3:
                                enemyActions.Add(actions.PARRY1);
                                break;

                            case 4:
                                enemyActions.Add(actions.ESQUIVAR);
                                break;

                            case 5:
                                enemyActions.Add(actions.ATACAR);
                                break;

                            default:
                                break;
                        }
                        break;
                    #endregion

                    #region RONDA 2
                    case 2:
                        switch (action)
                        {
                            case 1:
                                enemyActions.Add(actions.ATACAR);
                                enemyActions.Add(actions.ATACAR);
                                break;

                            case 2:
                                enemyActions.Add(actions.ATACAR);
                                enemyActions.Add(actions.ESQUIVAR);
                                break;

                            case 3:
                                enemyActions.Add(actions.PARRY1);
                                enemyActions.Add(actions.ATACAR);
                                break;

                            case 4:
                                enemyActions.Add(actions.ATACARFUERTE1);
                                enemyActions.Add(actions.ATACARFUERTE2);
                                break;

                            case 5:
                                enemyActions.Add(actions.ESQUIVAR);
                                enemyActions.Add(actions.ESQUIVAR);
                                break;

                            default:
                                break;
                        }
                        break;
                    #endregion

                    #region RONDA 3
                    case 3:
                        switch (action)
                        {
                            case 1:
                                enemyActions.Add(actions.ATACAR);
                                enemyActions.Add(actions.ATACAR);
                                enemyActions.Add(actions.ESQUIVAR);
                                break;

                            case 2:
                                enemyActions.Add(actions.PARRY1);
                                enemyActions.Add(actions.ATACARFUERTE1);
                                enemyActions.Add(actions.ATACARFUERTE2);
                                break;

                            case 3:
                                enemyActions.Add(actions.ATACARFUERTE1);
                                enemyActions.Add(actions.ATACARFUERTE2);
                                enemyActions.Add(actions.ESQUIVAR);
                                break;

                            case 4:
                                enemyActions.Add(actions.ESQUIVAR);
                                enemyActions.Add(actions.ATACAR);
                                enemyActions.Add(actions.ATACAR);
                                break;

                            case 5:
                                enemyActions.Add(actions.ATACAR);
                                enemyActions.Add(actions.ATACARFUERTE1);
                                enemyActions.Add(actions.ATACARFUERTE2);
                                break;

                            default:
                                break;
                        }
                        break;
                    #endregion

                    #region RONDA 4
                    case 4:
                        switch (action)
                        {
                            case 1:
                                enemyActions.Add(actions.PARRY1);
                                enemyActions.Add(actions.ATACAR);
                                enemyActions.Add(actions.ATACARFUERTE1);
                                enemyActions.Add(actions.ATACARFUERTE2);
                                break;

                            case 2:
                                enemyActions.Add(actions.ESQUIVAR);
                                enemyActions.Add(actions.ATACAR);
                                enemyActions.Add(actions.ESQUIVAR);
                                enemyActions.Add(actions.ATACAR);
                                break;

                            case 3:
                                enemyActions.Add(actions.ATACARFUERTE1);
                                enemyActions.Add(actions.ATACARFUERTE2);
                                enemyActions.Add(actions.PARRY1);
                                enemyActions.Add(actions.ATACAR);
                                break;

                            case 4:
                                enemyActions.Add(actions.ATACARFUERTE1);
                                enemyActions.Add(actions.ATACARFUERTE2);
                                enemyActions.Add(actions.ATACAR);
                                enemyActions.Add(actions.PARRY1);
                                break;

                            case 5:
                                enemyActions.Add(actions.ATACAR);
                                enemyActions.Add(actions.PARRY1);
                                enemyActions.Add(actions.ATACAR);
                                enemyActions.Add(actions.ESQUIVAR);
                                break;

                            default:
                                break;
                        }
                        break;
                    #endregion

                    #region RONDA 5
                    case 5:
                        switch (action)
                        {
                            case 1:
                                enemyActions.Add(actions.ATACARFUERTE1);
                                enemyActions.Add(actions.ATACARFUERTE2);
                                enemyActions.Add(actions.PARRY1);
                                enemyActions.Add(actions.ATACAR);
                                enemyActions.Add(actions.ATACAR);
                                break;

                            case 2:
                                enemyActions.Add(actions.ATACAR);
                                enemyActions.Add(actions.PARRY1);
                                enemyActions.Add(actions.ATACAR);
                                enemyActions.Add(actions.PARRY1);
                                enemyActions.Add(actions.ATACAR);
                                break;

                            case 3:
                                enemyActions.Add(actions.ATACARFUERTE1);
                                enemyActions.Add(actions.ATACARFUERTE2);
                                enemyActions.Add(actions.ATACAR);
                                enemyActions.Add(actions.ESQUIVAR);
                                enemyActions.Add(actions.ESQUIVAR);
                                break;

                            case 4:
                                enemyActions.Add(actions.ATACAR);
                                enemyActions.Add(actions.ATACARFUERTE1);
                                enemyActions.Add(actions.ATACARFUERTE2);
                                enemyActions.Add(actions.ESQUIVAR);
                                enemyActions.Add(actions.PARRY1);
                                break;

                            case 5:
                                enemyActions.Add(actions.ESQUIVAR);
                                enemyActions.Add(actions.ATACARFUERTE1);
                                enemyActions.Add(actions.ATACARFUERTE2);
                                enemyActions.Add(actions.ATACAR);
                                enemyActions.Add(actions.ESQUIVAR);
                                break;

                            default:
                                break;
                        }
                        break;
                    #endregion

                    default:
                        break;
                }
                break;
            #endregion

            #region TERCER ENEMIGO
            case 3:
                switch (numRound)
                {
                    #region RONDA 1
                    case 1:
                        switch (action)
                        {
                            case 1:
                                enemyActions.Add(actions.ATACAR);
                                break;

                            case 2:
                                enemyActions.Add(actions.ESQUIVAR);
                                break;

                            case 3:
                                enemyActions.Add(actions.PARRY1);
                                break;

                            case 4:
                                enemyActions.Add(actions.ESQUIVAR);
                                break;

                            case 5:
                                enemyActions.Add(actions.ATACAR);
                                break;

                            default:
                                break;
                        }
                        break;
                    #endregion

                    #region RONDA 2
                    case 2:
                        switch (action)
                        {
                            case 1:
                                enemyActions.Add(actions.ESQUIVAR);
                                enemyActions.Add(actions.ESQUIVAR);
                                break;

                            case 2:
                                enemyActions.Add(actions.ATACAR);
                                enemyActions.Add(actions.ESQUIVAR);
                                break;

                            case 3:
                                enemyActions.Add(actions.PARRY1);
                                enemyActions.Add(actions.ATACAR);
                                break;

                            case 4:
                                enemyActions.Add(actions.ATACARFUERTE1);
                                enemyActions.Add(actions.ATACARFUERTE2);
                                break;

                            case 5:
                                enemyActions.Add(actions.ATACAR);
                                enemyActions.Add(actions.ATACAR);
                                break;

                            default:
                                break;
                        }
                        break;
                    #endregion

                    #region RONDA 3
                    case 3:
                        switch (action)
                        {
                            case 1:
                                enemyActions.Add(actions.ESQUIVAR);
                                enemyActions.Add(actions.ATACAR);
                                enemyActions.Add(actions.ESQUIVAR);
                                break;

                            case 2:
                                enemyActions.Add(actions.ATACAR);
                                enemyActions.Add(actions.PARRY1);
                                enemyActions.Add(actions.ATACAR);
                                break;

                            case 3:
                                enemyActions.Add(actions.ATACARFUERTE1);
                                enemyActions.Add(actions.ATACARFUERTE2);
                                enemyActions.Add(actions.ATACAR);
                                break;

                            case 4:
                                enemyActions.Add(actions.ESQUIVAR);
                                enemyActions.Add(actions.ATACAR);
                                enemyActions.Add(actions.ESQUIVAR);
                                break;

                            case 5:
                                enemyActions.Add(actions.ESQUIVAR);
                                enemyActions.Add(actions.ATACARFUERTE1);
                                enemyActions.Add(actions.ATACARFUERTE2);
                                break;

                            default:
                                break;
                        }
                        break;
                    #endregion

                    #region RONDA 4
                    case 4:
                        switch (action)
                        {
                            case 1:
                                enemyActions.Add(actions.PARRY1);
                                enemyActions.Add(actions.ATACAR);
                                enemyActions.Add(actions.ATACAR);
                                enemyActions.Add(actions.ESQUIVAR);
                                break;

                            case 2:
                                enemyActions.Add(actions.ATACAR);
                                enemyActions.Add(actions.ATACAR);
                                enemyActions.Add(actions.ATACAR);
                                enemyActions.Add(actions.ATACAR);
                                break;

                            case 3:
                                enemyActions.Add(actions.ATACARFUERTE1);
                                enemyActions.Add(actions.ATACARFUERTE2);
                                enemyActions.Add(actions.ATACARFUERTE1);
                                enemyActions.Add(actions.ATACARFUERTE2);
                                break;

                            case 4:
                                enemyActions.Add(actions.ATACARFUERTE1);
                                enemyActions.Add(actions.ATACARFUERTE2);
                                enemyActions.Add(actions.ESQUIVAR);
                                enemyActions.Add(actions.ATACAR);
                                break;

                            case 5:
                                enemyActions.Add(actions.ATACAR);
                                enemyActions.Add(actions.PARRY1);
                                enemyActions.Add(actions.ATACAR);
                                enemyActions.Add(actions.ESQUIVAR);
                                break;

                            default:
                                break;
                        }
                        break;
                    #endregion

                    #region RONDA 5
                    case 5:
                        switch (action)
                        {
                            case 1:
                                enemyActions.Add(actions.ATACARFUERTE1);
                                enemyActions.Add(actions.ATACARFUERTE2);
                                enemyActions.Add(actions.ATACAR);
                                enemyActions.Add(actions.PARRY1);
                                enemyActions.Add(actions.ATACAR);
                                break;

                            case 2:
                                enemyActions.Add(actions.PARRY1);
                                enemyActions.Add(actions.ATACAR);
                                enemyActions.Add(actions.ATACAR);
                                enemyActions.Add(actions.ATACAR);
                                enemyActions.Add(actions.PARRY1);
                                break;

                            case 3:
                                enemyActions.Add(actions.ATACAR);
                                enemyActions.Add(actions.ESQUIVAR);
                                enemyActions.Add(actions.ATACARFUERTE1);
                                enemyActions.Add(actions.ATACARFUERTE2);
                                enemyActions.Add(actions.ESQUIVAR);
                                break;

                            case 4:
                                enemyActions.Add(actions.ATACAR);
                                enemyActions.Add(actions.ATACARFUERTE1);
                                enemyActions.Add(actions.ATACARFUERTE2);
                                enemyActions.Add(actions.ESQUIVAR);
                                enemyActions.Add(actions.PARRY1);
                                break;

                            case 5:
                                enemyActions.Add(actions.ESQUIVAR);
                                enemyActions.Add(actions.ATACARFUERTE1);
                                enemyActions.Add(actions.ATACARFUERTE2);
                                enemyActions.Add(actions.ATACAR);
                                enemyActions.Add(actions.ESQUIVAR);
                                break;

                            default:
                                break;
                        }
                        break;
                    #endregion

                    default:
                        break;
                }
                break;
            #endregion

            default:
                break;
        }
        
        //HUD first action
        switch (enemyActions[0])
        {
            case actions.ATACAR:
                myHud.actionTextEnemy1[0].text = "A";
                myHud.actionImageEnemy[0].sprite = myHud.lightimage;
                break;
            case actions.ATACARFUERTE1:
                myHud.actionTextEnemy1[0].text = "H";
                myHud.actionImageEnemy[0].sprite = myHud.heavyImage;
                break;
            case actions.PARRY1:
                myHud.actionTextEnemy1[0].text = "P";
                myHud.actionImageEnemy[0].sprite = myHud.parryImage;
                break;
            case actions.ESQUIVAR:
                myHud.actionTextEnemy1[0].text = "D";
                break;
            default:
                myHud.actionTextEnemy1[0].text = "E";
                break;
        }

        //INTERROGANTES
        for(int i = 1; i < (numRound); i++)
        {
            myHud.actionTextEnemy1[i].text = ")";
            myHud.actionImageEnemy[i].sprite = myHud.emptyImage;
        }
    }
    #endregion

    #region CLEAR ENEMY ACTIONS
    public void ClearEnemyActions()
    {
        enemyActions.Clear();

        if (extraAction != actions.NONE)
        {
            enemyActions.Add(extraAction);

            extraAction = actions.NONE;
        }
    }
    #endregion

    #region GET DAMAGE
    public void getDamage(int damage)
    {
        life -= damage;
    }
    #endregion

    #region GET LIFE
    public int GetLife()
    {
        return life;
    }
    #endregion

    #region Fill HUD Enemy
    public void FillHUDEnemy()
    {
        for (int i = 0; i < enemyActions.Count; i++)
        {
            switch (enemyActions[i])
            {
                case actions.ATACAR:
                    myHud.actionTextEnemy1[i].text = "A";
                    myHud.actionImageEnemy[i].sprite = myHud.lightimage;
                    break;
                case actions.ATACARFUERTE1:
                    //myHud.actionTextEnemy1[i].text = "C";
                    //i++;
                    myHud.actionTextEnemy1[i].text = "H";
                    myHud.actionImageEnemy[i].sprite = myHud.heavyImage;
                    break;
                case actions.PARRY1:
                    myHud.actionTextEnemy1[i].text = "P";
                    myHud.actionImageEnemy[i].sprite = myHud.parryImage;
                    break;
                case actions.ESQUIVAR:
                    myHud.actionTextEnemy1[i].text = "D";
                    break;
                default:
                    myHud.actionTextEnemy1[i].text = "E";
                    break;
            }
        }        
    }
    #endregion

    #region Comprobe
    public void CheckNextAnimation()
    {
        myGameManager.CheckNextAnimationPlayer(1);
    }
    #endregion
}
