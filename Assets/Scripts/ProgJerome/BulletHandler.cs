using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletHandler : MonoBehaviour
{
    [Header("List must be same size and order than bullet spawn zones")]
    public List<Collider2D> BulletZones = new List<Collider2D>();

    [Space(10)]
    [Header("Bullet Prefab")]
    public GameObject Bullet;

    [Space(10)]
    [Header("Mask")]
    public GameObject Parent;

    public static BulletHandler Instance;

    Vector3 spawnPos;
    Vector3 cameraRelative;

    private void Awake()
    {
        Instance = this;
    }

    public void SetupBullets(int index)
    {
        SoldierInfo currentSoldier = DataCenterDay.Instance.CurrentSoldiers[index];
        if (currentSoldier.Bullets.Count == BulletZones.Count)
        {
            for (int i = 0; i < currentSoldier.Bullets.Count; i++)
            {
                if (currentSoldier.Bullets[i] != 0)
                {
                    float minXPoint = BulletZones[i].bounds.min.x;
                    float maxXPoint = BulletZones[i].bounds.max.x;

                    float minYPoint = BulletZones[i].bounds.min.y;
                    float maxYPoint = BulletZones[i].bounds.max.y;

                    float xPos = Random.Range(minXPoint, maxXPoint);
                    float yPos = Random.Range(minYPoint, maxYPoint);

                    print(minXPoint);
                    print(maxXPoint);

                    print(xPos);
                    print(yPos);

                    spawnPos = new Vector3(xPos, yPos, 0);


                    //Instantiate(Bullet, spawnPos, Quaternion.Euler(-90f, 0f, 0f));
                    GameObject bullet = Instantiate(Bullet, spawnPos, transform.rotation, Parent.transform);

                    //GameObject bullet = Instantiate(Bullet, Parent.transform, false);
                    //bullet.transform.position = spawnPos;

                    //cameraRelative = bullet.transform.InverseTransformPoint(bullet.transform.position);
                }
            }
        }

    }
}
