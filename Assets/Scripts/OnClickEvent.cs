using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public class OnClickEvent : MonoBehaviour
{
    [SerializeField] private UnityEvent onClicked;

    private void OnMouseDown()
    {
        onClicked?.Invoke();
    }
}

