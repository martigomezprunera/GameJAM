using System.Collections;
using System.Collections.Generic;
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
        lifePlayer = 100 - _player.GetLife();
        lifeEnemy = 100 - _enemy.GetLife();
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
    }

    void AnimateTextReady()
    {
        textRound.transform.localScale = Vector3.one / 2;
        LeanTween.scale(textRound.gameObject, new Vector3(1, 1, 1), 1.5f).setEaseOutBounce();
    }

    void AnimateTextActions()
    {
        textRound.transform.localScale = Vector3.one / 2;
        LeanTween.scale(textRound.gameObject, new Vector3(1, 1, 1), 0.8f).setEaseOutBounce();
    }

    void AnimateTextFight()
    {
        textRound.transform.localScale = Vector3.one / 2;
        LeanTween.scale(textRound.gameObject, new Vector3(1, 1, 1), 0.8f).setEaseOutBounce().setOnComplete(FadeFinished);
    }

    void FadeFinished()
    {
        LeanTween.scale(textRound.gameObject, new Vector3(0, 0, 0), 0.5f).setEaseInQuint();
    }

    void SliderPlayerLife()
    {
        sliderLifePlayer.value = lifePlayer;
    }

    void SliderEnemyLife()
    {
        sliderLifeEnemy.value = lifeEnemy;
    }

    void SliderPlayerTime()
    {
        if (roundState == RoundState.SELECTING_ACTION)
        {
            sliderTimePlayer.value = countDownTime;
        }
    }

    void SliderEnemyTime()
    {
        if (roundState == RoundState.SELECTING_ACTION)
        {
            sliderTimeEnemy.value = countDownTime;
        }
    }

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
}
