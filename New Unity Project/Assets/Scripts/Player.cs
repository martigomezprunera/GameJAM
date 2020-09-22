using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    #region VARIABLES
    enum actions {ATACAR, ATACARFUERTE, PARRY, ESQUIVAR};

    [SerializeField] List<actions>  myActions;
    GameManager myGameManager;
    #endregion

    #region METHODS
    private void Start()
    {
        myGameManager= GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    public void ClearActions()
    {
        myActions.Clear();
    }

    private void Update()
    {
        //ataque
        if ( (Input.GetKeyDown("1")) && (myActions.Count<myGameManager.numRound))
            myActions.Add(actions.ATACAR);
        //AtaqueFuerte
        else if ((Input.GetKeyDown("2")) && (myActions.Count < myGameManager.numRound))
            myActions.Add(actions.ATACARFUERTE);
        //Parry
        else if ((Input.GetKeyDown("3")) && (myActions.Count < myGameManager.numRound))
            myActions.Add(actions.ESQUIVAR);
        //Esquivar
        else if ((Input.GetKeyDown("4")) && (myActions.Count < myGameManager.numRound))
            myActions.Add(actions.PARRY);
    }
    #endregion
}
