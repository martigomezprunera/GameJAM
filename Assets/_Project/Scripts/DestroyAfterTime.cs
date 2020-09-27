using UnityEngine;

public class DestroyAfterTime : MonoBehaviour
{
    private void Awake()
    {
        Destroy(gameObject, 1);
    }
}