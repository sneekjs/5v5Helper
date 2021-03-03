using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorSelector : MonoBehaviour
{
    public Texture2D defaultCursor;
    public Texture2D oldCursor;

    private bool defaultActive = true;

    private void Start()
    {
        Cursor.SetCursor(defaultCursor, Vector2.zero, CursorMode.ForceSoftware);
    }

    public void SwapCursor()
    {
        if (defaultActive)
        {
            Cursor.SetCursor(oldCursor, Vector2.zero, CursorMode.ForceSoftware);
        }
        else
        {
            Cursor.SetCursor(defaultCursor, Vector2.zero, CursorMode.ForceSoftware);
        }

        defaultActive = !defaultActive;
    }
}
