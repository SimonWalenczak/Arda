using System;
using UnityEngine;
using UnityEngine.UI;

public class HospitalController : MonoBehaviour
{
    [SerializeField] private bool _canHeal;
    [SerializeField] private GameObject _healingBar;
    [SerializeField] private Image _healBar;
    [SerializeField] private float _timeOfHealMax;
    [SerializeField] private float _timeOfHeal;
    [SerializeField] private float _healEffectiveness;
    private float _resetTime = 1;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 7)
        {
            _healBar.fillAmount = 0;
            _canHeal = true;
        }
    }

    private void Start()
    {
        _timeOfHeal = _timeOfHealMax;
    }

    private void Update()
    {
        if(_canHeal)
            HealingTroup();
    }

    private void HealingTroup()
    {
        _healingBar.SetActive(true);
        _timeOfHeal -= Time.deltaTime;
        if (_timeOfHeal <= 0)
        {
            _resetTime -= _healEffectiveness;
            _healBar.fillAmount += _healEffectiveness;
            _timeOfHeal = _timeOfHealMax;
        }

        if (_resetTime <= 0 - _healEffectiveness)
        {
            _canHeal = false;
            _healingBar.SetActive(false);
            _timeOfHeal = _timeOfHealMax;
            _resetTime = 1;
        }
    }
}