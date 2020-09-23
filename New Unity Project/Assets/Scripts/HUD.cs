using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUD : MonoBehaviour
{
    //VARIABLES
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

    // Start is called before the first frame update
    void Start()
    {
        _gameManager = gameManager.GetComponent<GameManager>();
        _player = player.GetComponent<Player>();
        _enemy = enemy.GetComponent<Enemy>();
    }

    // Update is called once per frame
    void Update()
    {
        //VARIABLES TO USE IN HUD
        lifePlayer = _player.GetLife();
        lifeEnemy = _enemy.GetLife();
        countDownTime = _gameManager.GetCountDownRound();
        numRound = _gameManager.numRound;


    }
}
