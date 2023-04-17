using UnityEngine;

public class Radio : MonoBehaviour
{
    public float speed;

    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        
        Vector3 newPosition = transform.position + new Vector3(x * speed * Time.deltaTime, 0f, z * speed * Time.deltaTime);
        
        transform.position = newPosition;
    }
}