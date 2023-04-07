using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructible : MonoBehaviour
{
    [SerializeField] GameObject DestroyedVersion; 


    public void Destruction()
    {
        Instantiate(DestroyedVersion, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
