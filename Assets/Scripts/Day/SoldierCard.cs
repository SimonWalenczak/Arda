using DG.Tweening;
using UnityEngine;

public class SoldierCard : MonoBehaviour
{
    public bool isSelected;
    public int index;
    
    private void Update()
    {
        if (isSelected)
        {
            transform.DOScale(1.2f,0.2f);
        }
        else
        {
            transform.DOScale(1, 0.2f);
        }
    }
}
