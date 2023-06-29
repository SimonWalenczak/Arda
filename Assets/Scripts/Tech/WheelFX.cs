using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelFX : MonoBehaviour
{
    ArcadeCar arcadeCar;

    public List<ParticleSystem> mudLeft = new List<ParticleSystem>();
    public List<ParticleSystem> mudRight = new List<ParticleSystem>(); 

    float maxSpeedValue;
    float currentSpeed;
    float vfxStartSpeed;

    List<Vector3> frontPositionL = new List<Vector3>();
    List<Vector3> frontPositionR = new List<Vector3>();

    public Vector3 rotationAngles = new Vector3(0, 180, 0);

    private void Awake()
    {
        arcadeCar = GetComponent<ArcadeCar>();
        maxSpeedValue = GetMaxValueFromCurve(arcadeCar.accelerationCurve);
        var mainModule = mudLeft[0].main;
        vfxStartSpeed = mainModule.startSpeed.constant;

        for (int i = 0; i < mudLeft.Count; i++)
        {
            frontPositionL.Add(mudLeft[i].gameObject.transform.rotation.eulerAngles);
            var module = mudLeft[i].main;
            module.startSpeed = 0;
        }
        for (int i = 0; i < mudRight.Count; i++)
        {
            frontPositionR.Add(mudRight[i].gameObject.transform.rotation.eulerAngles);
            var module = mudRight[i].main;
            module.startSpeed = 0;
        }
    }

    private void Update()
    {
        //Rotation();

        currentSpeed = Mathf.Abs(arcadeCar.GetSpeed() * 4);
        //print("VRRRRRRRR" + currentSpeed);

        float mudStartSpeed = VFXvalue(vfxStartSpeed);

        for (int i = 0; i < arcadeCar.axles.Length; i++)
        {
            if (arcadeCar.axles[i].wheelDataL.isOnGround && currentSpeed > 7)
            {
                mudLeft[i].Play();
                var mainModule = mudLeft[i].main;
                mainModule.startSpeed = mudStartSpeed;
            }
            else
            {
                mudLeft[i].Stop();
            }

            if (arcadeCar.axles[i].wheelDataR.isOnGround && currentSpeed > 7)
            {
                mudRight[i].Play();
                var mainModule = mudRight[i].main;
                mainModule.startSpeed = mudStartSpeed;
            }
            else
            {
                mudRight[i].Stop();
            }
        }
    }

    float VFXvalue(float VFXvalue)
    {
        float value;

        value = VFXvalue * currentSpeed / maxSpeedValue;

        return value;
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

    void Rotation()
    {
        if (arcadeCar.GetSpeed() >= 0)
        {
            for (int i = 0; i < mudLeft.Count; i++)
            {
                mudLeft[i].gameObject.transform.rotation = Quaternion.Euler(frontPositionL[i]);
            }
            for (int i = 0; i < mudRight.Count; i++)
            {
                mudRight[i].gameObject.transform.rotation = Quaternion.Euler(frontPositionL[i]);
            }
        }
        else
        {
            for (int i = 0; i < mudLeft.Count; i++)
            {
                mudLeft[i].gameObject.transform.rotation = Quaternion.Euler(frontPositionL[i]) * new Quaternion(0, 0, 180, 0);
            }
            for (int i = 0; i < mudRight.Count; i++)
            {
                mudRight[i].gameObject.transform.rotation = Quaternion.Euler(frontPositionL[i]) * new Quaternion(0, 0, 180, 0);
            }
        }
    }

}
