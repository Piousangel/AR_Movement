using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SampleSettings : MonoBehaviour
{
    
    private Animator animator;
    private Animator _animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        //_animator = GameObject.Find("SampleCanvas").transform.Find("EmotionSelect").transform.GetComponent<Animator>();
        
    }

   public void Close()
    {
        StartCoroutine(CloseAfterDelay());
    }

    IEnumerator CloseAfterDelay()
    {
        //_animator.enabled = true;
        animator.SetTrigger("close");
        yield return new WaitForSeconds(0.1f);
        gameObject.SetActive(false);
        animator.ResetTrigger("close");
    }
}
