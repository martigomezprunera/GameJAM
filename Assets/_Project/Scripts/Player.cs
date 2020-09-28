using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class Player : MonoBehaviour
{
    #region VARIABLES
    [Header("References")]
    [SerializeField] private GameObjectSounds _characterSounds = null;
    public GameObjectSounds Sounds => _characterSounds;

    [Header("VFX")]
    [SerializeField] private BloodVFXSpawner _bloodVFXSpawner = null;
    [SerializeField] private Transform _bloodSpawnTransform = null;

    [SerializeField] private VisualEffect _parrySparks = null;

    [SerializeField] private CharacterAnimations _characterAnimations = null;

    [Header("Settings")]
    public List<actions> myActions;
    public actions extraAction = actions.NONE;

    GameManager myGameManager;

    public Enemy myEnemy;

    [SerializeField] private int life = 100;

    public bool canSelect = false;

    //HUD
    public HUD myHud;
    #endregion

    #region METHODS

    #region AWAKE

    private void Awake()
    {
        if (_characterSounds == null)
            throw new NullReferenceException("_characterSounds has not been assigned at" + GetType());
        if (_bloodVFXSpawner == null)
            throw new NullReferenceException("_bloodVFXSpawner has not been assigned at" + GetType());
        if (_bloodSpawnTransform == null)
            throw new NullReferenceException("_bloodSpawn has not been assigned at" + GetType());
        if (_parrySparks == null)
            throw new NullReferenceException("_parrySparks has not been assigned at" + GetType());
        if (_characterAnimations == null)
            throw new NullReferenceException("_characterAnimations has not been assigned at" + GetType());

        _characterAnimations.OnHit += CheckNextAnimation;
        _characterAnimations.OnSlash += PlaySlashSound;
        _characterAnimations.OnParrySlash += PlayParrySlashSound;
        _characterAnimations.OnParry += PlayParry;
        _characterAnimations.OnUnsheathe += PlayUnsheathe;
    }

    #endregion

    #region START
    private void Start()
    {
        myGameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }
    #endregion

    #region UPDATE
    private void Update()
     {

        /*if (canSelect)
        {
            //ataque
            if ((Input.GetKeyDown("1")) && (myActions.Count < myGameManager.numRound))
                myActions.Add(actions.ATACAR);
            //AtaqueFuerte
            else if ((Input.GetKeyDown("2")) && (myActions.Count  < myGameManager.numRound))
            {
                if (myActions.Count + 1 == myGameManager.numRound)
                {
                    myActions.Add(actions.ATACARFUERTE1);
                    //Lo metemos para la siguiente ronda
                    extraAction = actions.ATACARFUERTE2;
                }
                else
                {
                    myActions.Add(actions.ATACARFUERTE1);
                    myActions.Add(actions.ATACARFUERTE2);
                }                
            }
            //Parry
            else if ((Input.GetKeyDown("3")) && (myActions.Count < myGameManager.numRound))
                myActions.Add(actions.PARRY1);
            //Esquivar
            else if ((Input.GetKeyDown("4")) && (myActions.Count < myGameManager.numRound))
                myActions.Add(actions.ESQUIVAR);
        }*/
         
     }
    #endregion

    #region CLEAR ACTIONS
    public void ClearActions()
    {
        myActions.Clear();

        if (extraAction != actions.NONE)
        {
            myActions.Add(extraAction);

            extraAction = actions.NONE;
        }
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

    #region GET LIFE
    public int GetLife()
    {
        return life;
    }
    #endregion

    #region LIGHT ATTACK
    public void LightAttack()
    {
        int aux = myGameManager.numRound;

        if (aux > myEnemy.numActionsToAdd)
            aux = myEnemy.numActionsToAdd;

        if (canSelect && myActions.Count < aux)
        {
            myActions.Add(actions.ATACAR);
            FillHUDPlayer(actions.ATACAR);
        }
    }
    #endregion

    #region HEAVY ATTACK
    public void HeavyAttack()
    {
        int aux = myGameManager.numRound;
        if (aux > myEnemy.numActionsToAdd)
            aux = myEnemy.numActionsToAdd;


        if (canSelect && myActions.Count < aux)
        {
            myActions.Add(actions.ATACARFUERTE1);
            FillHUDPlayer(actions.ATACARFUERTE1);
        }

        /*if (canSelect && ((myActions.Count+1) < aux))
        {            
            myActions.Add(actions.ATACARFUERTE1);
            myActions.Add(actions.ATACARFUERTE2);
            FillHUDPlayer(actions.ATACARFUERTE1);
        }*/
        //else
        //do animation can't select
    }
    #endregion

    #region PARRY
    public void Parry()
    {
        int aux = myGameManager.numRound;


        if (aux > myEnemy.numActionsToAdd)
            aux = myEnemy.numActionsToAdd;


        if (canSelect && myActions.Count < aux)
        {
            myActions.Add(actions.PARRY1);
            FillHUDPlayer(actions.PARRY1);
        }
    }
    #endregion

    #region BACK STEP
    public void BackStep()
    {
        int aux = myGameManager.numRound;

        if (aux > 5)
            aux = 5;

        if (canSelect && myActions.Count < aux)
        {
            myActions.Add(actions.ESQUIVAR);
            FillHUDPlayer(actions.ESQUIVAR);
        }
            

    }
    #endregion

    #region Fill HUD Player
    void FillHUDPlayer(actions myAction)
    {
        switch (myAction)
        {
            case actions.ATACAR:
                myHud.actionTextPlayer1[myActions.Count - 1].text = "1";
                myHud.actionImagePlayer[myActions.Count - 1].sprite = myHud.lightimage;
                break;
            case actions.ATACARFUERTE1:
                //myHud.actionTextPlayer1[myActions.Count - 2].text = "C";
                myHud.actionTextPlayer1[myActions.Count - 1].text = "2";
                myHud.actionImagePlayer[myActions.Count - 1].sprite = myHud.heavyImage;
                break;
            case actions.PARRY1:
                myHud.actionTextPlayer1[myActions.Count - 1].text = "3";
                myHud.actionImagePlayer[myActions.Count - 1].sprite = myHud.parryImage;
                break;
            case actions.ESQUIVAR:
                myHud.actionTextPlayer1[myActions.Count - 1].text = "D";
                break;
            default:
                myHud.actionTextPlayer1[myActions.Count - 1].text = "E";
                break;
        }
    }
    #endregion


    #region Comprobe
    public void CheckNextAnimation()
    {
       myGameManager.CheckNextAnimationPlayer(0);
    }
    #endregion


    public void SpawnBlood()
    {
        _bloodVFXSpawner.SpawnBlood(_bloodSpawnTransform);
        Sounds.PlaySound("Flesh");
    }

    private void PlaySlashSound()
    {
        _characterSounds.PlaySound("Slash");
    }
    private void PlayParrySlashSound()
    {
        _characterSounds.PlaySound("Parry Slash");
    }

    public void PlayParry()
    {
        _characterSounds.PlaySound("Parry");
        _parrySparks.Play();
    }

    public void PlayUnsheathe()
    {
        _characterSounds.PlaySound("Unsheathe");

    }

    #endregion

}
