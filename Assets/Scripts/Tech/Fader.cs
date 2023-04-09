using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Fader : MonoBehaviour
{

    MeshRenderer meshRenderer;
    [SerializeField] float fadeTime;

    // Start is called before the first frame update
    void Start()
    {
        meshRenderer = gameObject.GetComponent<MeshRenderer>();
        //meshRenderer.material.DOFade(0, fadeTime);

    }
    
}
