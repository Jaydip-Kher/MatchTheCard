using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStartView : UIScreens
{
    [SerializeField] private CanvasGroup buttonPlay;
    [SerializeField] private Canvas gamePlayView;

    public void Btn_PlayClicked()
    {
        ActionController.Instance.generateLevel?.Invoke(PlayerController.Instance.currentLevel);
        UIController.Instance.Show(UIScreenName.Gameplay);
    }
}
