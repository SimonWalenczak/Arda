using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarEngine : MonoBehaviour
{
    ArcadeCar arcadeCar;
    float maxSpeedValue;
    float currentSpeed;

    public StudioEventEmitter CarFullEngine;

    private void Awake()
    {
        arcadeCar = GetComponent<ArcadeCar>();
        maxSpeedValue = GetMaxValueFromCurve(arcadeCar.accelerationCurve);
    }

    private void Start()
    {

    }

    private void Update()
    {
        currentSpeed = Mathf.Abs(arcadeCar.GetSpeed() * 4);
        float normalizedValue;

        //zi = (xi – min(x)) / (max(x) – min(x))
        //where:
        //zi: The ith normalized value in the dataset
        //xi: The ith value in the dataset
        //min(x): The minimum value in the dataset
        //max(x): The maximum value in the dataset

        normalizedValue = (currentSpeed - 0) / (maxSpeedValue - 0);

        if (normalizedValue > 1)
        {
            normalizedValue = 1;
        }

        FMODUnity.RuntimeManager.StudioSystem.setParameterByName("VehicleMomentum", normalizedValue);

    }


    float GetMaxValueFromCurve(AnimationCurve curve)
    {
        float maxValue = float.MinValue;

        // Iterate through all the keys in the curve
        for (int i = 0; i < curve.length; i++)
        {
            Keyframe keyframe = curve[i];

            // Compare the value of the current keyframe with the current maximum value
            if (keyframe.value > maxValue)
            {
                maxValue = keyframe.value;
            }
        }

        return maxValue;
    }

}
