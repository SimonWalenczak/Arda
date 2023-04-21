using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Radio : MonoBehaviour
{
    public float speed;
    public LayerMask TargetLayer;

    public List<Bullet> BulletsList;
    public List<Bullet> SafeList;
    public Bullet ActualBullet;

    private Vector2 vector;
    void FixedUpdate()
    {
        // Vector3 newPosition =
        //     transform.position + new Vector3(x * speed * Time.deltaTime, 0f, z * speed * Time.deltaTime);

        Vector3 newPosition = transform.position + new Vector3(vector.x * speed * Time.deltaTime, 0f, vector.y * speed * Time.deltaTime);
        
        transform.position = newPosition;
    }

    public void StickMove(InputAction.CallbackContext ctx)
    {
        vector = ctx.ReadValue<Vector2>();
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
                other.GetComponent<Bullet>().IsDetected = true;
                BulletsList.Add(other.GetComponent<Bullet>());
                SafeList = BulletsList;
                if (BulletsList.Count >= 1)
                    ActualBullet = BulletsList[0];
                else
                    ActualBullet = null;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (Contains(TargetLayer, other.gameObject.layer))
        {
            if (other.GetComponent<Bullet>().IsFound == false)
            {
                other.GetComponent<Bullet>().IsDetected = false;
                BulletsList.Remove(other.GetComponent<Bullet>());
                RemoveBullet();
            }
        }
    }

    public void ValidBullet()
    {
        BulletsList.Remove(ActualBullet);
        ActualBullet.gameObject.SetActive(false);
        RemoveBullet();
    }

    private void RemoveBullet()
    {
        BulletsList = SafeList;
        if (BulletsList.Count >= 1)
        {
            ActualBullet = BulletsList[0];
        }
        else
            ActualBullet = null;
    }
}