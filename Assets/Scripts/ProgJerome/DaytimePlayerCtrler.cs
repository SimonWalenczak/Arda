using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DaytimePlayerCtrler : MonoBehaviour
{
    [Header("Reference")]
    [HideInInspector] public ArcadeCar arcadeCar;
    [HideInInspector] public bool isDriving = true;

    [Space(10)][Header("Debug")]
    public GameObject AButtonDebug;
    public CampTuto actualTent;

    [Space(10)] [Header("Respawn")] public LayerMask TargetLayer;
    public List<GameObject> Respawns;
    public GameObject NearestRespawn;

    public CanvasGroup canvasGroup;
    public float fadeDuration = 1.0f;
    public float targetAlpha = 0.0f;

    private float initialAlpha;
    Rigidbody rigidbody;

    public static DaytimePlayerCtrler Instance;

    private void Awake()
    {
        Instance = this;
        rigidbody = GetComponent<Rigidbody>();
    }

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
            if (isDriving && other.GetComponent<Tent>().Enterable)
            {
                AButtonDebug.SetActive(true);
                actualTent = other.GetComponent<CampTuto>();
            }

            if (Gamepad.current.buttonSouth.isPressed && isDriving)
            {
                if (other.gameObject.GetComponent<Tent>().Enterable)
                {
                    AButtonDebug.SetActive(false);

                    isDriving = false;
                    arcadeCar.controllable = false;
                    rigidbody.constraints = RigidbodyConstraints.FreezeAll;
                    other.gameObject.GetComponent<Tent>().GoToTent();
                    
                }
            }
        }
    }

    public void UnfreezePosition()
    {
        rigidbody.constraints = RigidbodyConstraints.None;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("TentTrigger"))
        {
            AButtonDebug.SetActive(false);
        }
    }
}