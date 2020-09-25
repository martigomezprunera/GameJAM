using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]  public List<actions> enemyActions;
    public actions extraAction = actions.NONE;


    [SerializeField] private int life = 100;
    [SerializeField] public int id = 1;

    #region GET NEW ACTION
    public void GetNewActions(int numRound)
    {

        int numActionsToAdd = numRound;
        int action;
        action = Random.Range(1, 5);

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
                                enemyActions.Add(actions.PARRY1);
                                break;

                            case 2:
                                enemyActions.Add(actions.ESQUIVAR);
                                enemyActions.Add(actions.ATACAR);
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
                                enemyActions.Add(actions.PARRY1);
                                enemyActions.Add(actions.ATACAR);
                                enemyActions.Add(actions.ESQUIVAR);
                                break;

                            case 2:
                                enemyActions.Add(actions.PARRY1);
                                enemyActions.Add(actions.ATACAR);
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
                                enemyActions.Add(actions.ATACAR);
                                enemyActions.Add(actions.ATACAR);
                                enemyActions.Add(actions.ESQUIVAR);
                                enemyActions.Add(actions.ATACAR);
                                break;

                            case 3:
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
                                enemyActions.Add(actions.ATACARFUERTE1);
                                enemyActions.Add(actions.ATACARFUERTE2);
                                break;

                            case 2:
                                enemyActions.Add(actions.ATACAR);
                                enemyActions.Add(actions.PARRY1);
                                enemyActions.Add(actions.ATACARFUERTE1);
                                enemyActions.Add(actions.ATACARFUERTE2);
                                enemyActions.Add(actions.ESQUIVAR);
                                break;

                            case 3:
                                enemyActions.Add(actions.ESQUIVAR);
                                enemyActions.Add(actions.ATACAR);
                                enemyActions.Add(actions.ESQUIVAR);
                                enemyActions.Add(actions.ATACARFUERTE1);
                                enemyActions.Add(actions.ATACARFUERTE2);
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
                                enemyActions.Add(actions.PARRY1);
                                enemyActions.Add(actions.ESQUIVAR);
                                enemyActions.Add(actions.ATACAR);
                                enemyActions.Add(actions.ATACAR);
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
}
