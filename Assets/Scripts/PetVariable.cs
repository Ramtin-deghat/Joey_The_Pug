using System.Collections;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Manages a specific variable for a virtual pet, such as hunger or sleepiness.
/// Attach this to a child GameObject of the main Pet.
/// The GameObject's name will be used as the identifier for this variable and its Animator parameter.
/// </summary>
public class PetVariable : MonoBehaviour
{
    [Header("Variable Settings")]
    [Tooltip("The current value of this variable.")]
    public float value = 0f;

    [Tooltip("The minimum allowed value for this variable.")]
    public float minValue = 0f;

    [Tooltip("The maximum allowed value for this variable.")]
    public float maxValue = 100f;

    [Tooltip("The amount this variable changes per second (e.g., -1 for hunger, 0.5 for energy).")]
    public float changePerSecond = 0f;

    [Tooltip("If true, the value changes in whole-second steps. If false, it changes smoothly every frame.")]
    public bool updateDiscrete = true;

    [Header("UI & Event Coupling")]
    [Tooltip("Event triggered when the PetVariable's value changes, passing the new value.")]
    public UnityEvent<float> onValueChange;

    private Animator animator;
    private string varName = "";
    private float currentTime = 0f;
    public static PetVariable Instance { get; private set; }

    public float valueToRunSadAnimation = 16;
    public float valueToRunHappyAnimation = 90;
    private bool doesSadAnimRunned = false;
    private bool doesHappyAnimRunned = false;

    private bool VariableExistsInAnimator(Animator animator, string parameterName)
    {
        for (int i = 0; i < animator.parameterCount; i++)
        {
            if (animator.GetParameter(i).name == parameterName)
                return true;
        }
        return false;
    }

    private void Awake()
    {

        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        varName = gameObject.name;

        animator = GetComponentInParent<Animator>();
        if (animator == null)
        {
            Debug.LogError(
                $"PetVariable '{varName}' could not find an Animator in its parent hierarchy. " +
                "Please ensure the main Pet GameObject has an Animator component.",
                this
            );
            return;
        }

        if (!VariableExistsInAnimator(animator, varName))
        {
            Debug.LogWarning(
                $"Animator parameter '{varName}' (derived from GameObject name) was not found in the Animator Controller of '{animator.name}'. " +
                "Please ensure a Float parameter with this exact name exists in the Animator.",
                this
            );
            animator = null; // safety
        }

        SetValue(value);
    }

    private void Update()
    {
        if (updateDiscrete)
        {
            // Discrete update: step once per full second
            currentTime += Time.deltaTime;
            if (currentTime >= 1f)
            {
                int steps = Mathf.FloorToInt(currentTime);
                currentTime -= steps;
                SetValue(value + changePerSecond * steps);
            }
        }
        else
        {
            // Continuous update: smooth change per frame
            SetValue(value + changePerSecond * Time.deltaTime);
        }
        TryToExitIdleAnimationState();
    }

    /// <summary>
    /// Changes the PetVariable's value by a relative amount.
    /// </summary>
    public void ChangeValue(float amount)
    {
        SetValue(value + amount);
    }

    /// <summary>
    /// Sets the PetVariable's value to an absolute new value, clamped between min and max.
    /// </summary>
    public void SetValue(float newValue)
    {
        float clampedValue = Mathf.Clamp(newValue, minValue, maxValue);

        if (!Mathf.Approximately(value, clampedValue))
        {
            value = clampedValue;

            if (animator != null)
                animator.SetFloat(varName, value);

            onValueChange?.Invoke(value);
        }
    }

    /// <summary>
    /// Sets the PetVariable's value to a random value between minValue and maxValue.
    /// </summary>
    public void SetRandomValue()
    {
        SetValue(UnityEngine.Random.Range(minValue, maxValue));
    }
    public void TryToExitIdleAnimationState()
    {
        if (value < valueToRunSadAnimation&&!doesSadAnimRunned)
        {
            animator.SetTrigger("isSad");
            doesSadAnimRunned = true;

        }
        if(value > valueToRunHappyAnimation&&!doesHappyAnimRunned)
        {
            animator.SetTrigger("isHappy");
            doesHappyAnimRunned = true;
        }
        if (value > valueToRunSadAnimation)
        {
            doesSadAnimRunned = false;
        }
        if (value < valueToRunHappyAnimation)
        {
            doesHappyAnimRunned = false;
        }
    }

}
