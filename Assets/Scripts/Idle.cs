using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Idle : MonoBehaviour
{
    [SerializeField] private Animator animator;

    [SerializeField] private string sneakyParam = "Sneaky"; // Animator float param
    [SerializeField] private string blendTreeStateName = "IdleBlendTree"; // Blend tree state
    [SerializeField] private float delay = 2f;

    [SerializeField] private List<GameObject> theThingsInTheEyes = new List<GameObject>();
    [SerializeField] private List<GameObject> happyEyes = new List<GameObject>();
    [SerializeField] private List<GameObject> tiredEyes = new List<GameObject>();
    [SerializeField] private List<string> sadnessStateNames = new List<string>();
    [SerializeField] private List<string> tiredStateNames = new List<string>();

    private string happinessStateName = "Joey_Happy_Animation";

    private AnimatorStateInfo stateInfo;
    private float sneaky = 0f;
    private bool isChecking = false;
    private bool interrupted = false;

    [SerializeField] float timeForStandingUp = 1.1f;

    void Update()
    {
        stateInfo = animator.GetCurrentAnimatorStateInfo(0);

        // Start idle coroutine if in blend tree and not already running
        if (stateInfo.IsName(blendTreeStateName) && !isChecking)
        {
            StartCoroutine(CheckForIdling());
        }

        // If sadness or tiredness or player click occurs, stop sneaky loop
        if (IsInterrupted())
        {
            interrupted = true;
            StopAllCoroutines();
            isChecking = false;
            ResetBlendTree();
        }

        CheckForSadness();
        CheckForHappy();
        CheckForTiredness();
    }

    private void OnMouseDown()
    {
        interrupted = true;
        StopAllCoroutines();
        isChecking = false;
        ResetBlendTree();
    }

    private IEnumerator CheckForIdling()
    {
        isChecking = true;
        interrupted = false;
        sneaky = 0f;
        animator.SetFloat(sneakyParam, sneaky);

        // Fast transition: first two animations
        yield return new WaitForSeconds(5f);
        sneaky = 0.5f;
        animator.SetFloat(sneakyParam, sneaky);
        yield return new WaitForSeconds(timeForStandingUp);
        sneaky = 1f;
        animator.SetFloat(sneakyParam, sneaky);
        // Slow transition to last animation
        float target = 3f;
        float duration = 15f;
        float elapsed = 0.1f;
        float start = sneaky;

        while (elapsed < duration && !interrupted)
        {
            elapsed += Time.deltaTime;
            sneaky = Mathf.Lerp(start, target, elapsed / duration);
            animator.SetFloat(sneakyParam, sneaky);
            yield return null;
        }

        animator.SetFloat(sneakyParam, 6);

        // Freeze at last animation if not interrupted
        if (!interrupted)
        {
            sneaky = 1f;
            animator.SetFloat(sneakyParam, sneaky);

            // Stay at last animation until interrupted
            while (!interrupted)
                yield return null;
        }

        isChecking = false;
    }

    private void ResetBlendTree()
    {
        sneaky = 2f;
        animator.SetFloat(sneakyParam, sneaky);
    }

    private bool IsInterrupted()
    {
        // Check sadness or tiredness states
        return
            stateInfo.IsName(sadnessStateNames[0]) ||
            stateInfo.IsName(sadnessStateNames[1]) ||
            stateInfo.IsName(tiredStateNames[0]) ||
            stateInfo.IsName(tiredStateNames[1]);
    }

    private void CheckForSadness()
    {
        bool isSad = stateInfo.IsName(sadnessStateNames[0]) || stateInfo.IsName(sadnessStateNames[1]);
        foreach (var item in theThingsInTheEyes) item.SetActive(isSad);
    }

    private void CheckForHappy()
    {
        bool isHappy = stateInfo.IsName(happinessStateName);
        foreach (var item in happyEyes) item.SetActive(isHappy);
    }

    private void CheckForTiredness()
    {
        bool isTired = stateInfo.IsName(tiredStateNames[0]) || stateInfo.IsName(tiredStateNames[1]);
        foreach (var item in tiredEyes) item.SetActive(isTired);
    }
}
