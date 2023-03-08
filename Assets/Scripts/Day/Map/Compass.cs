using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Compass : MonoBehaviour
{
    private Vector3 NorthDirection;
    [SerializeField] private Transform _player;
    public Quaternion _markerDirection;

    [SerializeField] private RectTransform _compassLayer;
    [SerializeField] private RectTransform _markerLayer;

    [SerializeField] private Transform _markerPlace;

    private void Update()
    {
        if (_player.gameObject.GetComponent<PlayerController>()._mapMarkerObject != null)
        {
            _markerPlace = _player.gameObject.GetComponent<PlayerController>()._mapMarkerObject.transform;
            _markerLayer.gameObject.SetActive(true);
            ChangeMarkerDirection();
        }
        else
        {
            _markerLayer.gameObject.SetActive(false);
        }

        ChangeCompassDirection();
    }

    private void ChangeCompassDirection()
    {
        NorthDirection.z = _player.eulerAngles.y;
        _compassLayer.localEulerAngles = NorthDirection;
    }

    private void ChangeMarkerDirection()
    {
        Vector3 dir = _player.position - _markerPlace.position;

        _markerDirection = Quaternion.LookRotation(-dir);

        _markerDirection.z = -_markerDirection.y;
        _markerDirection.x = 0;
        _markerDirection.y = 0;

        _markerLayer.localRotation = _markerDirection * Quaternion.Euler(NorthDirection);
    }
}