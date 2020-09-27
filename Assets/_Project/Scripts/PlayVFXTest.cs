using UnityEngine;
using UnityEngine.VFX;

public class PlayVFXTest : MonoBehaviour
{
    public VisualEffect VisualEffect;
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.W))
        {
            Debug.Log("PRESSED");
            VisualEffect.Play();
        }
    }
}
