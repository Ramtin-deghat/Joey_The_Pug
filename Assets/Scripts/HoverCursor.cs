using UnityEngine;

public class HoverCursor : MonoBehaviour
{
    [Header("Cursor Textures")]
    [SerializeField] private Texture2D defaultCursor;
    [SerializeField] private Texture2D hoverCursor;

    [Header("Cursor Settings")]
    [SerializeField] private Vector2 defaultHotspot = Vector2.zero;
    [SerializeField] private Vector2 hoverHotspot = Vector2.zero;
    [SerializeField] private CursorMode cursorMode = CursorMode.Auto;

    private void Start()
    {
        // Set the default cursor when the game starts
        if (defaultCursor != null)
            Cursor.SetCursor(defaultCursor, defaultHotspot, cursorMode);
    }

    private void OnMouseEnter()
    {
        if (hoverCursor != null)
            Cursor.SetCursor(hoverCursor, hoverHotspot, cursorMode);
    }

    private void OnMouseExit()
    {
        if (defaultCursor != null)
            Cursor.SetCursor(defaultCursor, defaultHotspot, cursorMode);
    }
}
