using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIScreens : MonoBehaviour
{
    public UIScreenName screenName; // This links to the enum
    private Canvas canvas;

    private void Awake()
    {
        canvas = GetComponent<Canvas>();
    }
    /// <summary>
    /// Called by the UIManager when we want to show this screen.
    /// </summary>
    public virtual void Show()
    {
        canvas.enabled = true;
    }

    /// <summary>
    /// Called by the UIManager when we want to hide this screen.
    /// </summary>
    public virtual void Hide()
    {
        canvas.enabled = false;
    }
}
