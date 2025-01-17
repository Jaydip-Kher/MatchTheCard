using UnityEngine;
using TMPro;

public class GameStartView : UIScreens
{
    [SerializeField] private CanvasGroup buttonPlay;
    [SerializeField] private Canvas gamePlayView;
    [SerializeField] private TextMeshProUGUI textTitle;

    public void Btn_PlayClicked()
    {
        ActionController.Instance.generateLevel?.Invoke(PlayerController.Instance.currentLevel);
        UIController.Instance.Show(UIScreenName.Gameplay);
    }
}
