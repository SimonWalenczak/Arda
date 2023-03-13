using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Letter : MonoBehaviour
{
    public Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }
    
    public IEnumerator Appearing()
    {
        _animator.SetBool("Appear", true);
        yield return new WaitForSeconds(1.5f);
    }
    
    public IEnumerator Disappearing()
    {       
        _animator.SetBool("Appear", false);
        _animator.SetBool("Disappear", true);
        yield return new WaitForSeconds(1);
        _animator.SetBool("Disappear", false);
    }
}
