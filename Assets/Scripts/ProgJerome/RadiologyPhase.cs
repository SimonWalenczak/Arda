using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class RadiologyPhase : MonoBehaviour
{
    [Header("Radiology Mode")] [SerializeField]
    bool Scan = true;

    [SerializeField] bool Survey;


    [Space(10)] public CanvasGroup Fader;
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
    [HideInInspector] public bool isInteractable = false;

    [SerializeField] int currentSoldier = 0;

    [Space(10)] [Header("Soldat Visuel")] public Image FaceUp;
    public Image Beard;
    public Image Body;
    public Image Nose;
    public List<Sprite> bodySoldierPortrait;

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
        for (int i = 0; i < DataCenterDay.Instance.CurrentSoldiers.Count; i++)
        {
            DataCenterDay.Instance.CurrentInfoSoldiers[i].gameObject.SetActive(true);

            DataCenterDay.Instance.CurrentInfoSoldiers[i].NameText.text =
                DataCenterDay.Instance.CurrentSoldiers[i].Name;
            DataCenterDay.Instance.CurrentInfoSoldiers[i].AgeText.text = DataCenterDay.Instance.CurrentSoldiers[i].Age;
            DataCenterDay.Instance.CurrentInfoSoldiers[i].MilitaryRankText.text =
                DataCenterDay.Instance.CurrentSoldiers[i].Rank.ToString();

            switch (DataCenterDay.Instance.CurrentSoldiers[i].Rank)
            {
                case MilitaryRank.Génie:
                    DataCenterDay.Instance.CurrentInfoSoldiers[i].Body.sprite = bodySoldierPortrait[0];
                    break;
                case MilitaryRank.Officier:
                    DataCenterDay.Instance.CurrentInfoSoldiers[i].Body.sprite = bodySoldierPortrait[1];
                    break;
                case MilitaryRank.SecondeClasse:
                    DataCenterDay.Instance.CurrentInfoSoldiers[i].Body.sprite = bodySoldierPortrait[2];
                    break;
            }
            
            DataCenterDay.Instance.CurrentInfoSoldiers[i].FaceUp.sprite =
                DataCenterDay.Instance.CurrentSoldiers[i].FaceUp;
            
            DataCenterDay.Instance.CurrentInfoSoldiers[i].Beard.sprite =
                DataCenterDay.Instance.CurrentSoldiers[i].Beard;
            
            DataCenterDay.Instance.CurrentInfoSoldiers[i].Nose.sprite = DataCenterDay.Instance.CurrentSoldiers[i].Nose;


            DataCenterDay.Instance.CurrentInfoSoldiers[i].FaceUp.color =
                DataCenterDay.Instance.CurrentSoldiers[i].BeardColor;
            DataCenterDay.Instance.CurrentInfoSoldiers[i].Beard.color =
                DataCenterDay.Instance.CurrentSoldiers[i].BeardColor;
        }

        Mask.transform.position = maskStartPos;
        Skeleton.transform.position = skeletonStartPos;
        UiRadioUpdate.Instance.UpdateUI(currentSoldier);
        isInteractable = true;

        if (Survey)
        {
            Mask.SetActive(false);
        }
        else if (Scan)
        {
            BulletHandler.Instance.SetupBullets(currentSoldier);
            canMove = true;
        }

        //Affichage Soldat (face + uniforme)
        FaceUp.sprite = DataCenterDay.Instance.CurrentSoldiers[currentSoldier].FaceUp;
        Beard.sprite = DataCenterDay.Instance.CurrentSoldiers[currentSoldier].Beard;
        Nose.sprite = DataCenterDay.Instance.CurrentSoldiers[currentSoldier].Nose;
        Body.sprite = DataCenterDay.Instance.CurrentSoldiers[currentSoldier].Body;

        FaceUp.color = DataCenterDay.Instance.CurrentSoldiers[currentSoldier].BeardColor;
        Beard.color = DataCenterDay.Instance.CurrentSoldiers[currentSoldier].BeardColor;
        //Fin affichage 
    }

    private void Update()
    {
        if (Gamepad.current.buttonWest.wasReleasedThisFrame && isInteractable)
        {
            UpdateSoldier();
        }

        if (Gamepad.current.buttonEast.wasReleasedThisFrame && isInteractable)
        {
            LeaveTent();
        }

        if (Gamepad.current.buttonSouth.wasReleasedThisFrame && Survey && isInteractable)
        {
            HealSoldier();
        }

        if (Gamepad.current.buttonSouth.wasReleasedThisFrame && Scan && isInteractable)
        {
            RemoveBulllet();
        }
    }

    void LeaveTent()
    {
        canMove = false;
        isInteractable = false;
        currentSoldier = 0;
        PlayerManager.GetComponent<DaytimePlayerCtrler>().arcadeCar.controllable = true;
        PlayerManager.GetComponent<DaytimePlayerCtrler>().isDriving = true;
        DataCenterDay.Instance.CurrentTent.Enterable = false;
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
        if (currentSoldier < DataCenterDay.Instance.CurrentSoldiers.Count - 1)
        {
            //print(DataCenterDay.Instance.CurrentSoldiers.Count);
            currentSoldier++;
            for (int i = 0; i < DataCenterDay.Instance.CurrentBullets.Count; i++)
            {
                Destroy(DataCenterDay.Instance.CurrentBullets[i]);
            }

            DataCenterDay.Instance.CurrentBullets.Clear();
            DataCenterDay.Instance.BulletsFound = 0;
            BulletHandler.Instance.SetupBullets(currentSoldier);
            UiRadioUpdate.Instance.UpdateUI(currentSoldier);

            //Affichage Soldat (face + uniforme)

            FaceUp.sprite = DataCenterDay.Instance.CurrentSoldiers[currentSoldier].FaceUp;
            Beard.sprite = DataCenterDay.Instance.CurrentSoldiers[currentSoldier].Beard;
            Nose.sprite = DataCenterDay.Instance.CurrentSoldiers[currentSoldier].Nose;
            Body.sprite = DataCenterDay.Instance.CurrentSoldiers[currentSoldier].Body;

            FaceUp.color = DataCenterDay.Instance.CurrentSoldiers[currentSoldier].BeardColor;
            Beard.color = DataCenterDay.Instance.CurrentSoldiers[currentSoldier].BeardColor;

            //Fin affichage
        }
        else
        {
            LeaveTent();
        }
    }

    void HealSoldier()
    {
        DayManager.Instance.Timer += DataCenterDay.Instance.CurrentSoldiers[currentSoldier].MinutesConsumed * 60;
        DayManager.Instance.CurrentSeconds +=
            DataCenterDay.Instance.CurrentSoldiers[currentSoldier].MinutesConsumed * 60;
        //GlobalManager.Instance.GaugesValues[((int)DataCenterDay.Instance.CurrentSoldiers[currentSoldier].Rank)];
        if (GameData.NumberDays == 2)
            GlobalManager.Instance.UpdateSucceededValue(((int)DataCenterDay.Instance.CurrentSoldiers[currentSoldier]
                .Rank));
        UpdateSoldier();
    }

    void RemoveBulllet()
    {
        foreach (var item in DataCenterDay.Instance.CurrentBullets)
        {
            if (item.GetComponent<Bastos>().isDetected)
            {
                item.SetActive(false);
                DataCenterDay.Instance.BulletsFound++;
                if (DataCenterDay.Instance.BulletsFound == DataCenterDay.Instance.CurrentBullets.Count)
                {
                    if (GameData.NumberDays == 2)
                        GlobalManager.Instance.UpdateSucceededValue(((int)DataCenterDay.Instance
                            .CurrentSoldiers[currentSoldier].Rank));
                }
            }
        }
    }
}