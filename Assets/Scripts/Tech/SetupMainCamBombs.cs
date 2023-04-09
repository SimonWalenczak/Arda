using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetupMainCamBombs : MonoBehaviour
{
    [SerializeField] List<Bomb> bombList = new List<Bomb>();
    [SerializeField] GameObject mainCamera;

    private void Awake()
    {
        foreach (var item in bombList)
        {
            item.MainCamera = mainCamera;
        }
    }


}
