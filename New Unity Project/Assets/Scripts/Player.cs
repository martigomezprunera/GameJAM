using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    #region VARIABLES

    public List<actions> myActions;
    GameManager myGameManager;

    [SerializeField] private int life = 100;

    public bool canSelect = false;
    #endregion

    #region METHODS

    #region START
    private void Start()
    {
        myGameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }
    #endregion

    #region UPDATE
    private void Update()
     {

        if (canSelect)
        {
            //ataque
            if ((Input.GetKeyDown("1")) && (myActions.Count < myGameManager.numRound))
                myActions.Add(actions.ATACAR);
            //AtaqueFuerte
            else if ((Input.GetKeyDown("2")) && ((myActions.Count + 1) < myGameManager.numRound))
            {
                myActions.Add(actions.ATACARFUERTE1);
                myActions.Add(actions.ATACARFUERTE2);
            }
            //Parry
            else if ((Input.GetKeyDown("3")) && (myActions.Count < myGameManager.numRound))
                myActions.Add(actions.PARRY1);
            //Esquivar
            else if ((Input.GetKeyDown("4")) && (myActions.Count < myGameManager.numRound))
                myActions.Add(actions.ESQUIVAR);
        }
         
     }
    #endregion

    #region CLEAR ACTIONS
    public void ClearActions()
    {
        myActions.Clear();
    }
    #endregion

    #region DO ACTION
    public void DoAction(actions toDO)
    {
        //Se activa la animacion que toque
        switch (toDO)
        {
            case actions.ATACAR:
                //activar animacion
                break;
            case actions.ATACARFUERTE1:
                //activar animacion
                break;
            case actions.ATACARFUERTE2:
                //activar animacion
                break;
            case actions.PARRY1:
                //activar animacion
                break;
            case actions.PARRY2:
                //activar animacion
                break;
            case actions.ESQUIVAR:
                //activar animacion
                break;
            default:
                break;
        }
    }
    #endregion

    #region GET DAMAGE
    public void getDamage(int damage)
    {
        life -= damage;
    }
    #endregion

    #endregion
}