using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class ChoixDirection : MonoBehaviour
{
    public GameObject player;
    public LayerMask TargetLayer;
    public GameObject JeannePenseeParent;

    public float startValue = 0.0f;
    public float endValue = 1.0f;
    public float durationInSeconds = 3.0f;
    private float elapsedTime = 0.0f;
    private bool canSlowTime;

    public static bool Contains(LayerMask mask, int layer)
    {
        return mask == (mask | (1 << layer));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (Contains(TargetLayer, other.gameObject.layer))
        {
            StartCoroutine(MakeChoice());
        }
    }

    private void Update()
    {
        if (canSlowTime)
        {
            elapsedTime += Time.deltaTime;

            float t = Mathf.Clamp01(elapsedTime / durationInSeconds);

            float currentValue = Mathf.Lerp(startValue, endValue, t);
        }
    }

    IEnumerator MakeChoice()
    {
        player.GetComponent<DaytimePlayerCtrler>().arcadeCar.controllable = false;
        canSlowTime = true;
        RadiologyPhase.Instance.Fader.DOFade(0.5f, 3f);
        yield return new WaitForSeconds(durationInSeconds);
        player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition;
        JeannePenseeParent.SetActive(true);
    }
    
    public IEnumerator ChoiceFinish()
    {
        player.GetComponent<DaytimePlayerCtrler>().arcadeCar.controllable = true;
        canSlowTime = false;
        RadiologyPhase.Instance.Fader.DOFade(1f, 1.5f);
        yield return new WaitForSeconds(2);
        DaytimePlayerCtrler.Instance.UnfreezePosition();
        JeannePenseeParent.SetActive(false);
    }
}