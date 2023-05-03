using UnityEngine;

public class Radio : MonoBehaviour
{
    public float speed;
    public LayerMask TargetLayer;
    public Bullet ActualBullet;
    
    void Update()
    {
        //float x = Input.GetAxis("Horizontal");
        //float z = Input.GetAxis("Vertical");
        
        //Vector3 newPosition =
            //transform.position + new Vector3(x * speed * Time.deltaTime, 0f, z * speed * Time.deltaTime);

        //transform.position = newPosition;
    }

    public static bool Contains(LayerMask mask, int layer)
    {
        return mask == (mask | (1 << layer));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (Contains(TargetLayer, other.gameObject.layer))
        {
            if (other.GetComponent<Bullet>().IsFound == false)
            {
                Debug.Log("balle trouvée !");
                other.GetComponent<Bullet>().IsDetected = true;
                ActualBullet = other.GetComponent<Bullet>();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (Contains(TargetLayer, other.gameObject.layer))
        {
            if (other.GetComponent<Bullet>().IsFound == false)
            {
                Debug.Log("balle perdue !");
                other.GetComponent<Bullet>().IsDetected = false;
                ActualBullet = null;
            }
        }
    }
}