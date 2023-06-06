using System.Collections.Generic;
using UnityEngine;

public class Ping : MonoBehaviour
{
    public List<ButtonUpgrade> ButtonUpgrades;

    private void Start()
    {
        ButtonUpgrades[0].IsEquiped = true;
    }

    private void Update()
    {
        foreach (var buttonUpgrade in ButtonUpgrades)
        {
            if (buttonUpgrade.IsEquiped)
            {
                Vector3 pos = buttonUpgrade.GetComponent<RectTransform>().position;
                GetComponent<RectTransform>().position = new Vector3(pos.x, pos.y + 40, pos.z);
                break;
            }
        }
    }
}
