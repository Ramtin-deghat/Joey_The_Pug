using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.InputSystem.Android;

public class Idle : MonoBehaviour
{
    [SerializeField] private Animator animator;

    [SerializeField] private string sneakyParam = "Sneaky"; // name of float parameter in Animator
    [SerializeField] private string blendTreeStateName = "IdleBlendTree"; // name of state that holds your blend tree
    [SerializeField] private float delay = 2f;

    
    [SerializeField]private float counter = 0f;
    private float sneaky = 2f;

    [SerializeField] List<GameObject> theThingsInTheEyes = new List<GameObject>();
    [SerializeField] private List<string> sadnessStateName = new List<string>();

    private bool eyesMoved = false;

    void Update()
    {
        CheckForIdling();
        CheckForSadness();
    }

    private void CheckForIdling()
    {
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0); // 0 = base layer

        if (stateInfo.IsName(blendTreeStateName))
        {
            counter += delay * Time.deltaTime;

            if (counter > 2.5f && sneaky > 0)
            {
                sneaky -= delay * Time.deltaTime;
                animator.SetFloat(sneakyParam, sneaky); animator.SetFloat(sneakyParam, sneaky);
            }
            else if (counter > 10f && sneaky > 0f)
            {

            }
        }
        else
        {
            counter = 0f;
            sneaky = 2f;
            animator.SetFloat(sneakyParam, sneaky);
        }
    }

    private void CheckForSadness()
    {
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        if (stateInfo.IsName(sadnessStateName[0]) || stateInfo.IsName(sadnessStateName[1]))
        {
            //if (eyesMoved)
            //{
            //    foreach (var item in theThingsInTheEyes)
            //    {
            //        item.transform.Translate(new Vector3(0, 0, 0.1f));
            //    }
            //    eyesMoved = false;
            //}
            foreach (var item in theThingsInTheEyes)
            {
                item.SetActive(true);
            }

        }
        else
        {
            //if (!eyesMoved)
            //{
            //    foreach (var item in theThingsInTheEyes)
            //    {
            //        item.transform.Translate(new Vector3(0, 0, -0.1f));
            //    }
            //    eyesMoved = true;
            //}
            foreach (var item in theThingsInTheEyes)
            {
                item.SetActive(false);
            }

        }
    }
}
