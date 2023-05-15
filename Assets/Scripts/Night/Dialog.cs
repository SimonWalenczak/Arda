using UnityEngine;

public class Dialog : MonoBehaviour
{
    public int index;
    public string BodyDialog;
    public string DialogText;
    
    public bool HavePaper;
    public bool PaperActif;
    public GameObject Paper;

    private void Start()
    {
        if (HavePaper)
        {
            PaperActif = false;
        }
    }
}
