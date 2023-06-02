using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class BulletChecker : MonoBehaviour
{
    //public Camera MainCamera;
    public float DetectionDistance;

    private RectTransform rectTransform;
    public bool isABulletFound = false;

    public static BulletChecker Instance;


    private void Awake()
    {
        Instance = this;
        rectTransform = GetComponent<RectTransform>();
    }

    private void Update()
    {
        foreach (var item in DataCenterDay.Instance.CurrentBullets)
        {
            float dist = Vector3.Distance(transform.position, item.transform.position);

            if (dist <= DetectionDistance)
            {
                item.GetComponent<Bastos>().isDetected = true;
            }

            else
            {
                item.GetComponent<Bastos>().isDetected = false;
            }
        }

        foreach (var item in DataCenterDay.Instance.CurrentBullets)
        {
            if (item.GetComponent<Bastos>().isDetected)
            {
                isABulletFound = true;
                break;
            }

            isABulletFound = false;
        }

    }
}
