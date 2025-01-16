using UnityEngine;
using System.Collections.Generic;

public class UIController : Singleton<UIController>
{
    private Dictionary<UIScreenName, UIScreens> totalScreen = new Dictionary<UIScreenName, UIScreens>();
    private void Awake()
    {
        RegisterAllScreens();
    }
    private void RegisterAllScreens()
    {
        UIScreens[] foundScreens = GetComponentsInChildren<UIScreens>(true);
        foreach (UIScreens screen in foundScreens)
        {
            RegisterScreen(screen);
        }
    }

    private void RegisterScreen(UIScreens screen)
    {
        if (!totalScreen.ContainsKey(screen.screenName))
        {
            totalScreen.Add(screen.screenName, screen);
        }
        else
        {
            Debug.LogWarning($"Screen with ID {screen.screenName} is already registered.");
        }
    }

    public void Show(UIScreenName screenName, bool hideOthers = true)
    {
        if (!totalScreen.ContainsKey(screenName))
        {
            Debug.LogWarning($"No UIScreen registered with ID: {screenName}");
            return;
        }

        if (hideOthers)
        {
            foreach (var kvp in totalScreen)
            {
                kvp.Value.Hide();
            }
        }

        totalScreen[screenName].Show();
    }

    public void Hide(UIScreenName screenID)
    {
        if (totalScreen.ContainsKey(screenID))
        {
            totalScreen[screenID].Hide();
        }
        else
        {
            Debug.LogWarning($"No UIScreen registered with ID: {screenID}");
        }
    }
}