using UnityEngine;

public enum TextType
{
    NoType,
    EndRadiologie,
    End
}
public class TutoManager : MonoBehaviour
{
    public PlayerManager PlayerManager;
    public LayerMask TargetLayer;

    private void Awake()
    {
        PlayerManager = GetComponent<PlayerManager>();
    }

    public static bool Contains(LayerMask mask, int layer)
    {
        return mask == (mask | (1 << layer));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (Contains(TargetLayer, other.gameObject.layer))
        {
            PlayerManager.CurrentCamp.StartHeal();
            PlayerManager.SoldierCardPanel.SetActive(true);
            PlayerManager.CamPlayer.enabled = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (Contains(TargetLayer, other.gameObject.layer))
        {
            PlayerManager.CanHeal = false;
            PlayerManager.CurrentCamp = null;
        }
    }
}
