using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreenStage : MonoBehaviour
{
    public string newScene;
    public float timeToNextScene;

    float timer = 0;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= timeToNextScene)
        {
            SceneManager.LoadScene(newScene);
        }
    }
}
