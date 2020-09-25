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
    private List<actions> playerActions;
    private List<actions> enemyActions;

    //SLIDERS
    [Header("Sliders")]
    public Slider sliderLifePlayer;
    public Slider sliderLifeEnemy;
    public Slider sliderTimePlayer;
    public Slider sliderTimeEnemy;

    //IMAGES PLAYER
    [Header("Images player")]
    public List<Image> actionImagePlayer;

    //TEXT PLAYER ACTIONS
    [Header("Actions text player")]
    public List<TextMeshProUGUI> actionTextPlayer1;

    //IMAGES ENEMY
    [Header("Images enemy")]
    public List<Image> actionImageEnemy;

    //TEXT PLAYER ACTIONS
    [Header("Actions text enemy")]
    public List<TextMeshProUGUI> actionTextEnemy1;

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

        //ACTIONS PLAYER
        playerActions = _player.myActions;

        //ACTIONS ENEMY
        enemyActions = _enemy.enemyActions;

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
        ActionsHUD();
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
                textRound.text = "READY)";
                break;
            case RoundState.SELECTING_ACTION:
                textRound.text = "CHOOSE YOUR ACTIONS%";
                break;
            case RoundState.DOING_ACTIONS:
                textRound.text = "FIGHT%";
                break;
        }
    }
    #endregion

    #region ActionsHUD
    void ActionsHUD()
    {
        //IMAGE ALPHA
        ImageActions();
    }
    #endregion

    #region ImageActions
    void ImageActions()
    {
        switch (numRound)
        {
            case 1:
                actionImagePlayer[0].color = new Color(actionImagePlayer[0].color.r, actionImagePlayer[0].color.g, actionImagePlayer[0].color.b, 1f);
                actionImagePlayer[1].color = new Color(actionImagePlayer[1].color.r, actionImagePlayer[1].color.g, actionImagePlayer[1].color.b, 0.2f);
                actionImagePlayer[2].color = new Color(actionImagePlayer[2].color.r, actionImagePlayer[2].color.g, actionImagePlayer[2].color.b, 0.2f);
                actionImagePlayer[3].color = new Color(actionImagePlayer[3].color.r, actionImagePlayer[3].color.g, actionImagePlayer[3].color.b, 0.2f);
                actionImagePlayer[4].color = new Color(actionImagePlayer[4].color.r, actionImagePlayer[4].color.g, actionImagePlayer[4].color.b, 0.2f);

                actionImageEnemy[0].color = new Color(actionImageEnemy[0].color.r, actionImageEnemy[0].color.g, actionImageEnemy[0].color.b, 1f);
                actionImageEnemy[1].color = new Color(actionImageEnemy[1].color.r, actionImageEnemy[1].color.g, actionImageEnemy[1].color.b, 0.2f);
                actionImageEnemy[2].color = new Color(actionImageEnemy[2].color.r, actionImageEnemy[2].color.g, actionImageEnemy[2].color.b, 0.2f);
                actionImageEnemy[3].color = new Color(actionImageEnemy[3].color.r, actionImageEnemy[3].color.g, actionImageEnemy[3].color.b, 0.2f);
                actionImageEnemy[4].color = new Color(actionImageEnemy[4].color.r, actionImageEnemy[4].color.g, actionImageEnemy[4].color.b, 0.2f);

                break;
            case 2:
                actionImagePlayer[0].color = new Color(actionImagePlayer[0].color.r, actionImagePlayer[0].color.g, actionImagePlayer[0].color.b, 1f);
                actionImagePlayer[1].color = new Color(actionImagePlayer[1].color.r, actionImagePlayer[1].color.g, actionImagePlayer[1].color.b, 1f);
                actionImagePlayer[2].color = new Color(actionImagePlayer[2].color.r, actionImagePlayer[2].color.g, actionImagePlayer[2].color.b, 0.2f);
                actionImagePlayer[3].color = new Color(actionImagePlayer[3].color.r, actionImagePlayer[3].color.g, actionImagePlayer[3].color.b, 0.2f);
                actionImagePlayer[4].color = new Color(actionImagePlayer[4].color.r, actionImagePlayer[4].color.g, actionImagePlayer[4].color.b, 0.2f);

                actionImageEnemy[0].color = new Color(actionImageEnemy[0].color.r, actionImageEnemy[0].color.g, actionImageEnemy[0].color.b, 1f);
                actionImageEnemy[1].color = new Color(actionImageEnemy[1].color.r, actionImageEnemy[1].color.g, actionImageEnemy[1].color.b, 1f);
                actionImageEnemy[2].color = new Color(actionImageEnemy[2].color.r, actionImageEnemy[2].color.g, actionImageEnemy[2].color.b, 0.2f);
                actionImageEnemy[3].color = new Color(actionImageEnemy[3].color.r, actionImageEnemy[3].color.g, actionImageEnemy[3].color.b, 0.2f);
                actionImageEnemy[4].color = new Color(actionImageEnemy[4].color.r, actionImageEnemy[4].color.g, actionImageEnemy[4].color.b, 0.2f);

                break;
            case 3:
                actionImagePlayer[0].color = new Color(actionImagePlayer[0].color.r, actionImagePlayer[0].color.g, actionImagePlayer[0].color.b, 1f);
                actionImagePlayer[1].color = new Color(actionImagePlayer[1].color.r, actionImagePlayer[1].color.g, actionImagePlayer[1].color.b, 1f);
                actionImagePlayer[2].color = new Color(actionImagePlayer[2].color.r, actionImagePlayer[2].color.g, actionImagePlayer[2].color.b, 1f);
                actionImagePlayer[3].color = new Color(actionImagePlayer[3].color.r, actionImagePlayer[3].color.g, actionImagePlayer[3].color.b, 0.2f);
                actionImagePlayer[4].color = new Color(actionImagePlayer[4].color.r, actionImagePlayer[4].color.g, actionImagePlayer[4].color.b, 0.2f);

                actionImageEnemy[0].color = new Color(actionImageEnemy[0].color.r, actionImageEnemy[0].color.g, actionImageEnemy[0].color.b, 1f);
                actionImageEnemy[1].color = new Color(actionImageEnemy[1].color.r, actionImageEnemy[1].color.g, actionImageEnemy[1].color.b, 1f);
                actionImageEnemy[2].color = new Color(actionImageEnemy[2].color.r, actionImageEnemy[2].color.g, actionImageEnemy[2].color.b, 1f);
                actionImageEnemy[3].color = new Color(actionImageEnemy[3].color.r, actionImageEnemy[3].color.g, actionImageEnemy[3].color.b, 0.2f);
                actionImageEnemy[4].color = new Color(actionImageEnemy[4].color.r, actionImageEnemy[4].color.g, actionImageEnemy[4].color.b, 0.2f);

                break;
            case 4:
                actionImagePlayer[0].color = new Color(actionImagePlayer[0].color.r, actionImagePlayer[0].color.g, actionImagePlayer[0].color.b, 1f);
                actionImagePlayer[1].color = new Color(actionImagePlayer[1].color.r, actionImagePlayer[1].color.g, actionImagePlayer[1].color.b, 1f);
                actionImagePlayer[2].color = new Color(actionImagePlayer[2].color.r, actionImagePlayer[2].color.g, actionImagePlayer[2].color.b, 1f);
                actionImagePlayer[3].color = new Color(actionImagePlayer[3].color.r, actionImagePlayer[3].color.g, actionImagePlayer[3].color.b, 1f);
                actionImagePlayer[4].color = new Color(actionImagePlayer[4].color.r, actionImagePlayer[4].color.g, actionImagePlayer[4].color.b, 0.2f);

                actionImageEnemy[0].color = new Color(actionImageEnemy[0].color.r, actionImageEnemy[0].color.g, actionImageEnemy[0].color.b, 1f);
                actionImageEnemy[1].color = new Color(actionImageEnemy[1].color.r, actionImageEnemy[1].color.g, actionImageEnemy[1].color.b, 1f);
                actionImageEnemy[2].color = new Color(actionImageEnemy[2].color.r, actionImageEnemy[2].color.g, actionImageEnemy[2].color.b, 1f);
                actionImageEnemy[3].color = new Color(actionImageEnemy[3].color.r, actionImageEnemy[3].color.g, actionImageEnemy[3].color.b, 1f);
                actionImageEnemy[4].color = new Color(actionImageEnemy[4].color.r, actionImageEnemy[4].color.g, actionImageEnemy[4].color.b, 0.2f);

                break;
            case 5:
                actionImagePlayer[0].color = new Color(actionImagePlayer[0].color.r, actionImagePlayer[0].color.g, actionImagePlayer[0].color.b, 1f);
                actionImagePlayer[1].color = new Color(actionImagePlayer[1].color.r, actionImagePlayer[1].color.g, actionImagePlayer[1].color.b, 1f);
                actionImagePlayer[2].color = new Color(actionImagePlayer[2].color.r, actionImagePlayer[2].color.g, actionImagePlayer[2].color.b, 1f);
                actionImagePlayer[3].color = new Color(actionImagePlayer[3].color.r, actionImagePlayer[3].color.g, actionImagePlayer[3].color.b, 1f);
                actionImagePlayer[4].color = new Color(actionImagePlayer[4].color.r, actionImagePlayer[4].color.g, actionImagePlayer[4].color.b, 1f);

                actionImageEnemy[0].color = new Color(actionImageEnemy[0].color.r, actionImageEnemy[0].color.g, actionImageEnemy[0].color.b, 1f);
                actionImageEnemy[1].color = new Color(actionImageEnemy[1].color.r, actionImageEnemy[1].color.g, actionImageEnemy[1].color.b, 1f);
                actionImageEnemy[2].color = new Color(actionImageEnemy[2].color.r, actionImageEnemy[2].color.g, actionImageEnemy[2].color.b, 1f);
                actionImageEnemy[3].color = new Color(actionImageEnemy[3].color.r, actionImageEnemy[3].color.g, actionImageEnemy[3].color.b, 1f);
                actionImageEnemy[4].color = new Color(actionImageEnemy[4].color.r, actionImageEnemy[4].color.g, actionImageEnemy[4].color.b, 1f);

                break;
        }
    }
    #endregion
}
