using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructible : MonoBehaviour
{
    [SerializeField] GameObject DestroyedVersion;

    CapsuleCollider boxCollider;

    private void Awake()
    {
        boxCollider = gameObject.GetComponent<CapsuleCollider>();
    }


    public void Destruction()
    {
        boxCollider.enabled = false;
        //DestroyedVersion.GetComponentInChildren<Material>().SetFloat("_Ammount_Red", 40);
        Instantiate(DestroyedVersion, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
