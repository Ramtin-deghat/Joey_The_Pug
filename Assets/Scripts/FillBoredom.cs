using UnityEngine;
using UnityEngine.UI;

public class FillBoredom : MonoBehaviour
{
    private Slider boredSlider;
    [SerializeField] private int add;
    [SerializeField] private ParticleSystem hearts;
    private void OnMouseDrag()
    {
        boredSlider.value = (boredSlider.value + add);
        hearts.Play();
    }
    private void OnMouseUp()
    {
        hearts.Stop();
    }
}
