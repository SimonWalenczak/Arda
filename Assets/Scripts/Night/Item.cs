using System;
using UnityEngine;

public class Item : MonoBehaviour
{
    public int index;
    public string itemName;
    public bool isSelected;
    public bool isPicked;
    public string previewEffect = "";
    public string effect = "";

    public void Excute()
    {
        switch (index)
        {
            case 1:
                
                break;
            case 2:

                break;
            case 3:
                GameData.healTimeReduc = 1;
                break;
            case 4:
                GameData.speed = GameData.speed * 1.5f;
                break;
            case 5:
                GameData.turnSpeed += 15;
                break;
            case 6:

                break;
        }
    }
}
