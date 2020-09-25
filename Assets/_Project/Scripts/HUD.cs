using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    //VARIABLES
    [Header("GameObjects")]
    public GameObject gameManager;
    public GameObject player;
    public GameObject enemy;

    //REFERENCED SCRIPTS
    private Enemy _enemy;
    private Player _player;
    private GameManager _gameManager;

    //PRIVATE
    private int lifePlayer;
    private int lifeEnemy;
    private float countDownTime;
    private int numRound;
    private RoundState roundState;

    //SLIDERS
    [Header("Sliders")]
    public Slider sliderLifePlayer;
    public Slider sliderLifeEnemy;
    public Slider sliderTimePlayer;
    public Slider sliderTimeEnemy;

    //IMAGES PLAYER
    [Header("Images player")]
    public Image actionImagePlayer1;
    public Image actionImagePlayer2;
    public Image actionImagePlayer3;
    public Image actionImagePlayer4;
    public Image actionImagePlayer5;

    //TEXT PLAYER ACTIONS
    [Header("Actions text player")]
    public TextMeshProUGUI actionTextPlayer1;
    public TextMeshProUGUI actionTextPlayer2;
    public TextMeshProUGUI actionTextPlayer3;
    public TextMeshProUGUI actionTextPlayer4;
    public TextMeshProUGUI actionTextPlayer5;

    //IMAGES ENEMY
    [Header("Images enemy")]
    public Image actionImageEnemy1;
    public Image actionImageEnemy2;
    public Image actionImageEnemy3;
    public Image actionImageEnemy4;
    public Image actionImageEnemy5;

    //TEXT PLAYER ACTIONS
    [Header("Actions text enemy")]
    public TextMeshProUGUI actionTextEnemy1;
    public TextMeshProUGUI actionTextEnemy2;
    public TextMeshProUGUI actionTextEnemy3;
    public TextMeshProUGUI actionTextEnemy4;
    public TextMeshProUGUI actionTextEnemy5;

    //TEXT 
    [Header("Text")]
    public Text textRound;

    // Start is called before the first frame update
    void Awake()
    {
        _gameManager = gameManager.GetComponent<GameManager>();
        _player = player.GetComponent<Player>();
        _enemy = enemy.GetComponent<Enemy>();

        _gameManager.OnStartGame += AnimateTextReady;
        _gameManager.OnActionGame += AnimateTextActions;
        _gameManager.OnFightGame += AnimateTextFight;
    }

    // Update is called once per frame
    void Update()
    {
        //VARIABLES TO USE IN HUD
        lifePlayer = _player.GetLife();
        lifeEnemy = _enemy.GetLife();
        countDownTime = _gameManager.GetCountDownRound();
        numRound = _gameManager.numRound;
        roundState = _gameManager.GetroundState();

        //SLIDER PLAYER
        SliderPlayerLife();

        //SLIDER ENEMY
        SliderEnemyLife();

        //SLIDER TIME PLAYER
        SliderPlayerTime();

        //SLIDER TIME ENEMY
        SliderEnemyTime();

        //TEXT
        TextRound();

        //ACTIONS ROUND
        ActionsImagesActivate();
    }

    #region AnimateTextReady
    void AnimateTextReady()
    {
        textRound.transform.localScale = Vector3.one / 2;
        LeanTween.scale(textRound.gameObject, new Vector3(1, 1, 1), 1.5f).setEaseOutBounce();
    }
    #endregion

    #region AnimateTextActions
    void AnimateTextActions()
    {
        textRound.transform.localScale = Vector3.one / 2;
        LeanTween.scale(textRound.gameObject, new Vector3(1, 1, 1), 0.8f).setEaseOutBounce();
    }
    #endregion

    #region AnimateTextFight
    void AnimateTextFight()
    {
        textRound.transform.localScale = Vector3.one / 2;
        LeanTween.scale(textRound.gameObject, new Vector3(1, 1, 1), 0.8f).setEaseOutBounce().setOnComplete(FadeFinished);
    }
    #endregion

    #region FadeFinished
    void FadeFinished()
    {
        LeanTween.scale(textRound.gameObject, new Vector3(0, 0, 0), 0.5f).setEaseInQuint();
    }
    #endregion

    #region SliderPlayerLife
    void SliderPlayerLife()
    {
        sliderLifePlayer.value = lifePlayer;
    }
    #endregion

    #region SliderEnemyLife
    void SliderEnemyLife()
    {
        sliderLifeEnemy.value = lifeEnemy;
    }
    #endregion

    #region SliderPlayerTime
    void SliderPlayerTime()
    {
        if (roundState == RoundState.SELECTING_ACTION)
        {
            sliderTimePlayer.value = countDownTime;
        }
    }
    #endregion

    #region SliderEnemyTime
    void SliderEnemyTime()
    {
        if (roundState == RoundState.SELECTING_ACTION)
        {
            sliderTimeEnemy.value = countDownTime;
        }
    }
    #endregion

    #region TextRound
    void TextRound()
    {
        switch (roundState)
        {
            case RoundState.GOING_NEXT_ROUND:
                textRound.text = "READY%";

                //ANIMATION
                

                break;
            case RoundState.SELECTING_ACTION:
                textRound.text = "ACTIONS%";
                break;
            case RoundState.DOING_ACTIONS:
                textRound.text = "FIGHT%";
                break;
        }
    }
    #endregion

    #region ActionsImagesActivate
    void ActionsImagesActivate()
    {
        switch(numRound)
        {
            case 1:
                //IMAGE ALPHA
                actionImagePlayer1.color = new Color(actionImagePlayer1.color.r, actionImagePlayer1.color.g, actionImagePlayer1.color.b, 1f);
                actionImagePlayer2.color = new Color(actionImagePlayer1.color.r, actionImagePlayer1.color.g, actionImagePlayer1.color.b, 0.2f);
                actionImagePlayer3.color = new Color(actionImagePlayer1.color.r, actionImagePlayer1.color.g, actionImagePlayer1.color.b, 0.2f);
                actionImagePlayer4.color = new Color(actionImagePlayer1.color.r, actionImagePlayer1.color.g, actionImagePlayer1.color.b, 0.2f);
                actionImagePlayer5.color = new Color(actionImagePlayer1.color.r, actionImagePlayer1.color.g, actionImagePlayer1.color.b, 0.2f);

                //TEXT ALPHA
                actionTextPlayer1.color = new Color(actionImagePlayer1.color.r, actionImagePlayer1.color.g, actionImagePlayer1.color.b, 1f);
                actionTextPlayer2.color = new Color(actionImagePlayer1.color.r, actionImagePlayer1.color.g, actionImagePlayer1.color.b, 0.2f);
                actionTextPlayer3.color = new Color(actionImagePlayer1.color.r, actionImagePlayer1.color.g, actionImagePlayer1.color.b, 0.2f);
                actionTextPlayer4.color = new Color(actionImagePlayer1.color.r, actionImagePlayer1.color.g, actionImagePlayer1.color.b, 0.2f);
                actionTextPlayer5.color = new Color(actionImagePlayer1.color.r, actionImagePlayer1.color.g, actionImagePlayer1.color.b, 0.2f);

                break;
            case 2:

                break;
            case 3:

                break;
            case 4:

                break;
            case 5:

                break;
        }
    }
    #endregion
}
