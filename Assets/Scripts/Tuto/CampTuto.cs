using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class CampTuto : MonoBehaviour
{
    public LayerMask TargetLayer;
    public bool IsSoldier;
    public bool FirstTent;

    public static bool Contains(LayerMask mask, int layer)
    {
        return mask == (mask | (1 << layer));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (Contains(TargetLayer, other.gameObject.layer))
        {
            if (FirstTent == false)
            {
                if (IsSoldier)
                {
                    GameData.HasSavesSoldier = true;
                }
                else
                {
                    GameData.HasSavesSoldier = false;
                }
            }
            else
            {
                GetComponent<Tent>().StartInTent();
            }
        }
    }

    private void Update()
    {
        if (Gamepad.current.buttonSouth.wasPressedThisFrame && FirstTent == false)
        {
            print("soldat : " + GameData.HasSavesSoldier);
        }
    }
}