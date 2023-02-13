using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Compass : MonoBehaviour
{
    public GameObject iconPrefab;
    List<MapMarker> _mapMarkers = new List<MapMarker>();

    public RawImage compassImage;
    public Transform player;

    public float maxDistance = 20000;
    
    private float compassUnit;

    public MapMarker tent1;
    public MapMarker tent2;
    public MapMarker tent3;

    private void Start()
    {
        compassUnit = compassImage.rectTransform.rect.width / 360f;
        
        AddMapMarker(tent1);
        AddMapMarker(tent2);
        AddMapMarker(tent3);

    }

    private void Update()
    {
        compassImage.uvRect = new Rect(player.localEulerAngles.y / 360f, 0f, 1f, 1f);

        foreach (MapMarker marker in _mapMarkers)
        {
            marker.image.rectTransform.anchoredPosition = GetPosOnCompass(marker);

            float dist = Vector2.Distance(new Vector2(player.transform.position.x, player.transform.position.z),
                marker.position);
            float scale = 0;

            if (dist < maxDistance)
            {
                scale = 1 - (dist / maxDistance);
            }

            if (scale <= 0.5f)
                scale = 0.5f;
            
            marker.image.rectTransform.localScale = Vector3.one * scale;
        }
    }

    public void AddMapMarker(MapMarker marker)
    {
        GameObject newMarker = Instantiate(iconPrefab, compassImage.transform);
        marker.image = newMarker.GetComponent<Image>();
        marker.image.sprite = marker.icon;

        _mapMarkers.Add(marker);
    }

    Vector2 GetPosOnCompass(MapMarker marker)
    {
        Vector2 playerPos = new Vector2(player.transform.position.x, player.transform.position.z);
        Vector2 playerFwd = new Vector2(player.transform.forward.x, player.transform.forward.z);

        float angle = Vector2.SignedAngle(marker.position - playerPos, playerFwd);

        return new Vector2(compassUnit * angle, 0f);
    }
}
