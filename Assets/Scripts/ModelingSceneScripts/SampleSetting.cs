using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SampleSetting : MonoBehaviour
{

    private Animator animator;
    private Animator _animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        

    }

    public void Close()
    {
        StartCoroutine(CloseAfterDelay());
    }

    IEnumerator CloseAfterDelay()
    {
        
        animator.SetTrigger("close");
        yield return new WaitForSeconds(0.1f);
        gameObject.SetActive(false);
        animator.ResetTrigger("close");
    }
}