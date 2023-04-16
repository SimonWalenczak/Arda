using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructible : MonoBehaviour
{
    [SerializeField] GameObject DestroyedVersion;

    BoxCollider boxCollider;

    private void Awake()
    {
        boxCollider = gameObject.GetComponent<BoxCollider>();
    }


    public void Destruction()
    {
        boxCollider.enabled = false;
        Instantiate(DestroyedVersion, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}