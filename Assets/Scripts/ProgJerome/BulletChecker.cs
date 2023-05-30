using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BulletChecker : MonoBehaviour
{
    public Camera MainCamera;

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
            Vector2 localPoint = item.transform.position; 

            RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, item.transform.position, MainCamera, out localPoint);

            if (rectTransform.rect.Contains(item.transform.position))
            {
                isABulletFound = true;
                print("SALAMALEKOUM");
            }

            //if (rectTransform.rect.Overlaps(item.GetComponent<Image>().rectTransform.rect))
            //{
            //    isABulletFound = true;
            //    print("SALAMALEKOUM");
            //}
        }
    }

    private void OnTriggerStay(Collider other)
    {
        print("LOOOOOOOOOOL");

        if (other.tag == "Bullet")
        {
            isABulletFound = true;
            print("SALAMALEKOUM");
        }
    }
}
