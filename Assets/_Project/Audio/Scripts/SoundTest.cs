using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundTest : MonoBehaviour
{ 
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.W))
        {
            AudioManager.Instance.PlaySound("Button Hover");
        }
    }
}
