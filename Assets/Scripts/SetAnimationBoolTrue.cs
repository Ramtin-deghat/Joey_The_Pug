using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class SetAnimationBoolTrue : MonoBehaviour
{
    public Animator animator;
    public List<GameObject> food = new List<GameObject> ();
    private bool isEating=false;
    private bool isAnimationPlaying =false;
    public void SetBoolTrue()
    {
        isEating = true;
        animator.SetBool("isEating", isEating);
        StartCoroutine(SetBoolFalse());
    }

    private IEnumerator SetBoolFalse()
    {
        yield return new WaitUntil(() => isAnimationPlaying);
        yield return new WaitForSeconds(9);
        isEating = false;
        animator.SetBool("isEating",isEating);

    }

    public void EnableFood()
    {
        foreach (var item in food)
        {
            item.SetActive(true);   
        }
    }


    private void Update()
    {
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        if (stateInfo.IsName("Joey_Eating_Animation"))
        {
            isAnimationPlaying=true;
        }
        if (isAnimationPlaying)
        {
            StartCoroutine(DissableFood());
        }
    }

    private IEnumerator DissableFood()
    {
        foreach (var item in food)
        {
            yield return new WaitForSeconds(3f);
            item.SetActive(false);
        }
        isAnimationPlaying = false;
    }
    
}
