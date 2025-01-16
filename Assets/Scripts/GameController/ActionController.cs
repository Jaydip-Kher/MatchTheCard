using System;

public class ActionController : Singleton<ActionController>
{
    public Action<int> generateLevel;
    public Action levelGenerated;
    public Action onLevelReset;
    public Action onLevelComplete;
    public Action<Card> onShowComplete;
    public Action showLoading;
    public Action showLoadingComplete;
    public Action hideLoading;
    public Action hideLoadingComplete;
    public Action<bool> UpdateScrore;
    public Action<Action> displayScore;
}