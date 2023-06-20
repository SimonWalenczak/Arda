using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DaytimePlayerCtrler : MonoBehaviour
{
    [HideInInspector] public ArcadeCar arcadeCar;
    [HideInInspector] public bool isDriving = true;

    public GameObject AButtonDebug;

    [Space(10)] [Header("Respawn")] public LayerMask TargetLayer;
    public List<GameObject> Respawns;
    public GameObject NearestRespawn;

    public CanvasGroup canvasGroup;
    public float fadeDuration = 1.0f;
    public float targetAlpha = 0.0f;
    private float initialAlpha;

    void Start()
    {
        arcadeCar = GetComponent<ArcadeCar>();
        NearestRespawn = Respawns[0];
    }

    public static bool Contains(LayerMask mask, int layer)
    {
        return mask == (mask | (1 << layer));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (Contains(TargetLayer, other.gameObject.layer))
        {
            StartCoroutine(WaitingForRespawn());

            foreach (var respawn in Respawns)
            {
                float dist = Vector3.Distance(arcadeCar.transform.position, respawn.transform.position);
                float bestDist = Vector3.Distance(arcadeCar.transform.position, NearestRespawn.transform.position);

                if (dist < bestDist)
                {
                    bestDist = dist;
                    NearestRespawn = respawn;
                }
            }
        }
    }

    private IEnumerator WaitingForRespawn()
    {
        StartCoroutine(FadeCanvasGroup(0, 1));
        yield return new WaitForSeconds(0.5f);
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition;
        yield return new WaitForSeconds(1.5f);
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        arcadeCar.transform.position = NearestRespawn.transform.position;
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(FadeCanvasGroup(1, 0));
    }


    private IEnumerator FadeCanvasGroup(float start, float end)
    {
        float elapsedTime = 0.0f;
        while (elapsedTime < fadeDuration + 1)
        {
            float normalizedTime = elapsedTime / fadeDuration;
            float currentAlpha = Mathf.Lerp(start, end, normalizedTime);

            canvasGroup.alpha = currentAlpha;

            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("TentTrigger"))
        {
            if (isDriving)
                AButtonDebug.SetActive(true);

            if (Gamepad.current.buttonSouth.isPressed && isDriving)
            {
                if (other.gameObject.GetComponent<Tent>().Enterable)
                {
                    AButtonDebug.SetActive(false);

                    isDriving = false;
                    arcadeCar.controllable = false;
                    other.gameObject.GetComponent<Tent>().GoToTent();
                }
                //print("coucou c'est une tente");            
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("TentTrigger"))
        {
            AButtonDebug.SetActive(false);
        }
    }
}