using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]  public List<actions> enemyActions;
    public actions extraAction = actions.NONE;

    [SerializeField] private int life = 100;

    #region GET NEW ACTION
    public void GetNewActions(int numRound)
    {
        int action;
        for (int i = 0; i < numRound; i++)
        {
            if ((enemyActions.Count > 0)&& (i==0))
                i++;

            action = Random.Range(0,3);
            if (numRound == 2)
                action = 1;
            switch (action)
            {
                case 0:
                    {
                        enemyActions.Add(actions.ATACAR);
                        break;
                    }
                case 1:
                    {
                        if ((i + 2) <= numRound)
                        {
                            enemyActions.Add(actions.ATACARFUERTE1);
                            i++;
                            enemyActions.Add(actions.ATACARFUERTE2);
                        }
                        else
                            enemyActions.Add(actions.ATACAR);
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
