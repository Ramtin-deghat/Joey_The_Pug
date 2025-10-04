using UnityEngine;
using UnityEngine.UI;

public class FillBoredom : MonoBehaviour
{
    
    [SerializeField] private Slider boredSlider;
    [SerializeField] private float fillSpeed = 10f;   // units per second
    [SerializeField] private ParticleSystem hearts;

    public bool isHeld = false;


    private void OnMouseDown()
    {
        isHeld = true;

        if (hearts != null)
            hearts.Play();
    }

    private void OnMouseUp()
    {
        isHeld = false;

        if (hearts != null)
            hearts.Stop();
    }

    private void Update()
    {
        if (isHeld && boredSlider != null)
        {
            //boredSlider.value += fillSpeed * Time.deltaTime;
            PetVariable.Instance.ChangeValue(fillSpeed);
        }
    }
}
