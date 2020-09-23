using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public List<actions> enemyActions;
    actions extraAction = actions.NONE;

    #region GET NEW ACTION
    public void GetNewActions(int numRound)
    {
        int action;
        for (int i = 0; i < numRound; i++)
        {
            action = Random.Range(0,3);

            switch (action)
            {
                case 0:
                    {
                        enemyActions.Add(actions.ATACAR);
                        break;
                    }
                case 1:
                    {
                        if (i == numRound - 1)
                        {
                            enemyActions.Add(actions.ATACARFUERTE1);
                            //Nos guardamos el ataque fuerte dos para añadir en la siguiente
                            extraAction = actions.ATACARFUERTE2;
                        }
                        else
                        {
                            enemyActions.Add(actions.ATACARFUERTE1);
                            enemyActions.Add(actions.ATACARFUERTE2);
                        }                        
                        break;
                    }
                case 2:
                    {
                        enemyActions.Add(actions.PARRY1);
                        break;
                    }
                case 3:
                    {
                        enemyActions.Add(actions.ESQUIVAR);
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
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
}
