using UnityEngine;

public class FinDialogCredits : MonoBehaviour
{
    [SerializeField] private GameObject _finalTextGeorges;
    [SerializeField] private GameObject _finalTextGeneral;

    private void Start()
    {
        if (GameData.SaveGeorges)
            _finalTextGeorges.SetActive(true);
        else
            _finalTextGeneral.SetActive(true);
    }
}
