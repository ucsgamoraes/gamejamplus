using UnityEngine;

public class AutoDestroy : MonoBehaviour
{
    public float time;

    void Start()
    {
        Destroy(gameObject, time);
    }
}
