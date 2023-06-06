using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonUpgrade : MonoBehaviour, ISelectHandler, IDeselectHandler
{
    [Header("Informations")]
    public string TextButton;
    public string TextDescription;
    public Effect effect;
    
    [Space (10)][Header("Reference")]
    public TMP_Text MainButtonText;
    public TMP_Text ButtonText;

    [Space (10)][Header("Debug")]
    
    public bool IsSelect;
    public bool IsOccuped;


    private Color color;
    private string _buttonTextSave;
    private string _textDescriptionSave;
    private Effect _effectSave;


    private void Start()
    {
        ButtonText = GetComponentInChildren<TMP_Text>();
        color = ButtonText.gameObject.GetComponentInParent<Image>().color;

        IsOccuped = TextButton != "";
    }

    private void Update()
    {
        if (IsOccuped == false)
        {
            ButtonText.text = "";
            color.a = 0.1f;
        }
        else
        {
            ButtonText.text = TextButton;
            color.a = 1;
        }

        ButtonText.gameObject.GetComponentInParent<Image>().color = color;
    }

    private void SwapNode()
    {
        //Description
        MainButtonText.gameObject.GetComponentInParent<MainButton>().DescriptionText = TextDescription;
        MainButtonText.gameObject.GetComponentInParent<MainButton>().effect = effect;
        MainButtonText.text = TextButton;
        
        //Ping
        Vector3 pos = GetComponent<RectTransform>().position;

        UpgradeCarGridManager gridManager = FindObjectOfType<UpgradeCarGridManager>();
        
        foreach (var ping in gridManager.PingPrefab)
        {
            ping.SetActive(false);
        }

        gridManager.PingPrefab[MainButtonText.GetComponentInParent<MainButton>().index].SetActive(true);
        
        gridManager.PingPrefab[MainButtonText.GetComponentInParent<MainButton>().index].GetComponent<RectTransform>().position =
            new Vector3(pos.x, pos.y + 40, 0);
    }

    public void ApplyEffect()
    {
        if (IsOccuped)
        {
            SwapNode();
        }
    }
    
    public void OnSelect(BaseEventData eventData)
    {
        IsSelect = true;
    }

    public void OnDeselect(BaseEventData eventData)
    {
        IsSelect = false;
    }
}