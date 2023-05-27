using UnityEngine;
using UnityEngine.EventSystems;

public class MainButton : MonoBehaviour, ISelectHandler
{
    [Header("Reference")] 
    public GameObject GridButton;
    public string Title;
    public string DescriptionText;
    public Effect effect;
    
    [Header("Debug")] public bool IsLeftSide;
    public bool IsSelect;

    public void OnSelect(BaseEventData eventData)
    {
        IsSelect = true;
    }

    public void DefineCarEffect()
    {
        switch (effect)
        {
            case Effect.Frein1:
                GameData.HaveFrein1 = true;
                break;
            case Effect.Frein2:
                GameData.HaveFrein2 = true;
                break;
            case Effect.Frein3:
                GameData.HaveFrein3 = true;
                break;
            case Effect.Moteur1:
                GameData.HaveMoteur1 = true;
                break;
            case Effect.Moteur2:
                GameData.HaveMoteur2 = true;
                break;
            case Effect.Moteur3:
                GameData.HaveMoteur3 = true;
                break;
            case Effect.Pneu1:
                GameData.HavePneu1 = true;
                break;
            case Effect.Pneu2:
                GameData.HavePneu2 = true;
                break;
            case Effect.Pneu3:
                GameData.HavePneu3 = true;
                break;
            case Effect.Chassis1:
                GameData.HaveChassis1 = true;
                break;
            case Effect.Chassis2:
                GameData.HaveChassis2 = true;
                break;
            case Effect.Chassis3:
                GameData.HaveChassis3 = true;
                break;
        }
    }

    public void DefineRadioEffect()
    {
        switch (effect)
        {
            case Effect.Casque1:
                GameData.HaveCasque1 = true;
                break;
            case Effect.Casque2:
                GameData.HaveCasque2 = true;
                break;
   
            case Effect.Tube1:
                GameData.HaveTube1 = true;
                break;
            case Effect.Tube2:
                GameData.HaveTube2 = true;
                break;
        }
    }
}