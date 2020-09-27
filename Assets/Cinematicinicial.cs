using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cinematicinicial : MonoBehaviour
{
    public CharacterAnimations characterAnimations;
    public Canvas canvas;
    // Start is called before the first frame update
    void Start()
    {
        characterAnimations.InitialAnimation();
        canvas.enabled = false;
        Invoke("DeactivateCinematic", 5.4f);

    }

    void DeactivateCinematic()
    {
        canvas.enabled = true;
        AudioManager.Instance.PlaySound("InitialGong");
    }
}
