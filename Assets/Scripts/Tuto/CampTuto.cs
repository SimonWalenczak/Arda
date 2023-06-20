using UnityEngine;
using UnityEngine.InputSystem;

public class CampTuto : MonoBehaviour
{
    public DaytimePlayerCtrler daytimePlayerCtrler;
    public LayerMask TargetLayer;
    public bool IsSoldier;
    public bool FirstTent;
    public bool SecondTent;

    public GameObject barrier;
    
    public static bool Contains(LayerMask mask, int layer)
    {
        return mask == (mask | (1 << layer));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (Contains(TargetLayer, other.gameObject.layer))
        {
            daytimePlayerCtrler.actualTent = this;
            
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
                if (GetComponent<Tent>().IsEnter == false)
                {
                    GetComponent<Tent>().StartInTent();
                }
            }
        }
    }

    private void Update()
    {
        // if (Gamepad.current.buttonSouth.wasPressedThisFrame && SecondTent)
        // {
        //     barrier.SetActive(false);
        // }

        if (FirstTent)
        {
            LaunchTuto();
        }
    }

    private void LaunchTuto()
    {
        print("Lancement du Tuto");
    }
}