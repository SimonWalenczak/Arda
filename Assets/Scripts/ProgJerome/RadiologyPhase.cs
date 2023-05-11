using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.InputSystem;

public class RadiologyPhase : MonoBehaviour
{
    public CanvasGroup Fader;
    public CanvasGroup Radiology;

    public GameObject Mask;
    public GameObject Skeleton;
    public GameObject PlayerManager;

    public float RadioMoveSpeed;

    Vector3 maskStartPos;
    Vector3 skeletonStartPos;

    Vector2 radioMov;

    public static RadiologyPhase Instance;

    [HideInInspector] public bool canMove = false;

    int currentSoldier = 0;

    private void Awake()
    {
        Instance = this;
        maskStartPos = Mask.transform.position;
        skeletonStartPos = Skeleton.transform.position;
    }

    private void FixedUpdate()
    {
        if (canMove)
        {
            Mask.transform.Translate(radioMov * RadioMoveSpeed * Time.deltaTime);
            Skeleton.transform.Translate(-radioMov * RadioMoveSpeed * Time.deltaTime);
        }
    }

    public void GetXYvalue(InputAction.CallbackContext ctx)
    {
        radioMov = ctx.ReadValue<Vector2>();
    }

    public void Setup()
    {
        Mask.transform.position = maskStartPos;
        Skeleton.transform.position = skeletonStartPos;
        UiRadioUpdate.Instance.UpdateUI(currentSoldier);

        BulletHandler.Instance.SetupBullets(currentSoldier);
        canMove = true;
    }

    private void Update()
    {
        UpdateSoldier();
        if (Gamepad.current.buttonEast.wasReleasedThisFrame && canMove)
        {
            LeaveTent();
        }
    }

    void LeaveTent()
    {

        canMove = false;
        currentSoldier = 0;
        PlayerManager.GetComponent<DaytimePlayerCtrler>().arcadeCar.controllable = true;
        PlayerManager.GetComponent<DaytimePlayerCtrler>().isDriving = true;
        DataCenterDay.Instance.Clean();
        StartCoroutine(Fading());
    }

    IEnumerator Fading()
    {
        Fader.DOFade(1f, 1f);
        yield return new WaitForSeconds(1.5f);
        Fader.DOFade(0f, 1f);
        Radiology.alpha = 0;
        yield return null;
    }

    void UpdateSoldier()
    {
        if (Gamepad.current.buttonWest.wasReleasedThisFrame && canMove)
        {
            if (currentSoldier < DataCenterDay.Instance.CurrentSoldiers.Count-1)
            {
                //print(DataCenterDay.Instance.CurrentSoldiers.Count);
                currentSoldier++;
                UiRadioUpdate.Instance.UpdateUI(currentSoldier);
            }
            else
            {
                LeaveTent();
            }
        }
    }

}
