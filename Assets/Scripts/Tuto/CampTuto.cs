using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class CampTuto : MonoBehaviour
{
    public LayerMask TargetLayer;
    public bool IsSoldier;
    
    public static bool Contains(LayerMask mask, int layer)
    {
        return mask == (mask | (1 << layer));
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (Contains(TargetLayer, other.gameObject.layer))
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
    }

    private void Update()
    {
        if (Gamepad.current.buttonSouth.wasPressedThisFrame)
        {
            print("soldat : " + GameData.HasSavesSoldier);
        }
    }
}
