using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [HideInInspector]
    public EventInstance engineInstance;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {

    }

    public void PlayOneShot(EventReference sound, Vector3 worldPos)
    {
        RuntimeManager.PlayOneShot(sound, worldPos);
    }

    public void SetParameter(StudioEventEmitter emitter, string parameterName, float value)
    {
        emitter.SetParameter(parameterName, value, true);
    }
}
