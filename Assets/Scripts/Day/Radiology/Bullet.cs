using UnityEngine;

public class Bullet : MonoBehaviour
{
    public bool IsDetected;
    public bool IsFound;

    private void Update()
    {
        if(IsFound)
            Destroy(gameObject);
    }
}
