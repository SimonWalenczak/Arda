using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Tent : MonoBehaviour
{
    public List<SoldierInfo> Soldiers = new List<SoldierInfo>();

    public CanvasGroup Fader;
    public CanvasGroup Radiology;

    [HideInInspector] public MeshRenderer meshRenderer;
    [HideInInspector] public MeshFilter meshFilter;

    public bool Enterable = true;
    [HideInInspector] public bool IsEnter = false;


    [Header("Reference")] public DaytimePlayerCtrler daytimePlayerCtrler;

    private void Awake()
    {
        Fader.alpha = 0;
        Radiology.alpha = 0;
        meshRenderer = GetComponent<MeshRenderer>();
        meshFilter = GetComponent<MeshFilter>();
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
        IsEnter = true;
        
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
        if (daytimePlayerCtrler.actualTent.SecondTent == true)
            GetComponent<CampTuto>().barrier.SetActive(false);
        
        Fader.DOFade(0f, 1f);
        Radiology.alpha = 1;
        RadiologyPhase.Instance.Setup();
        yield return null;
    }
}