using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public GameObject Player;

    private void FixedUpdate()
    {
        gameObject.transform.position = new Vector3(Player.transform.position.x, transform.position.y, Player.transform.position.z);
    }



}
