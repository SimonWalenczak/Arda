using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelFX : MonoBehaviour
{
    ArcadeCar arcadeCar;

    public List<ParticleSystem> mudLeft = new List<ParticleSystem>();
    public List<ParticleSystem> mudRight = new List<ParticleSystem>(); 

    float maxSpeedValue;

    private void Awake()
    {
        arcadeCar = GetComponent<ArcadeCar>();
        maxSpeedValue = GetMaxValueFromCurve(arcadeCar.accelerationCurve);

    }

    private void Update()
    {
        for (int i = 0; i < arcadeCar.axles.Length; i++)
        {
            if (arcadeCar.axles[i].wheelDataL.isOnGround)
            {
                mudLeft[i].Play();
            }
            else
            {
                mudLeft[i].Stop();
            }

            if (arcadeCar.axles[i].wheelDataR.isOnGround)
            {
                mudRight[i].Play();
            }
            else
            {
                mudRight[i].Stop();
            }
        }
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
