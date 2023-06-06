using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class Tent : MonoBehaviour
{
    public List<SoldierInfo> Soldiers = new List<SoldierInfo>();

    public CanvasGroup Fader;
    public CanvasGroup Radiology;

    public bool Enterable = true;
    
    //public int TentNum;

    private void Awake()
    {
        Fader.alpha = 0;
        Radiology.alpha = 0;
    }

    public void GoToTent()
    {
        DataCenterDay.Instance.CurrentTent = this;
        foreach (var item in Soldiers)
        {
            DataCenterDay.Instance.CurrentSoldiers.Add(item);
        }
        StartCoroutine(Fading());
    }

    public void StartInTent()
    {
        DataCenterDay.Instance.CurrentTent = this;
        foreach (var item in Soldiers)
        {
            DataCenterDay.Instance.CurrentSoldiers.Add(item);
        }
        Radiology.alpha = 1;
        RadiologyPhase.Instance.Setup();
    }

    IEnumerator Fading()
    {
        Fader.DOFade(1f, 1f);
        yield return new WaitForSeconds(1.5f);
        Fader.DOFade(0f, 1f);
        Radiology.alpha = 1;
        RadiologyPhase.Instance.Setup();
        yield return null;
    }
}