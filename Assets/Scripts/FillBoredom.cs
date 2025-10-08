using UnityEngine;
using UnityEngine.UI;

public class FillBoredom : MonoBehaviour
{
    
    [SerializeField] private Slider boredSlider;
    [SerializeField] private float fillSpeed = 10f;   // units per second
    [SerializeField] private ParticleSystem hearts;

    public bool isHeld = false;
    [SerializeField] private GameObject uiTutorial;

    private Animator animator;

    private float counter = 0;
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    private void OnMouseDown()
    {
        isHeld = true;
        DisablePettingUI();
        if (hearts != null)
            hearts.Play();
    }

    private void OnMouseUp()
    {
        isHeld = false;

        if (hearts != null)
            hearts.Stop();
    }

    private void DisablePettingUI()
    {
        uiTutorial.SetActive(false);
    }
    private void Update()
    {
        if (isHeld && boredSlider != null)
        {
            //boredSlider.value += fillSpeed * Time.deltaTime;
            PetVariable.Instance.ChangeValue(fillSpeed);
        }
        if (isHeld)
        {
            counter = 0;
            animator.SetFloat("Sneaky",counter);
        }
        else
        {
            counter += 0.1f * Time.deltaTime;
            animator.SetFloat("Sneaky", counter);
        }
    }
}
