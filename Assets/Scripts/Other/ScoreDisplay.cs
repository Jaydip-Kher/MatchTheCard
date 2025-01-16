using System;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class ScoreDisplay : MonoBehaviour
{
    public TextMeshProUGUI textScore;
    private Action scoreDisplayCompleteCallback;

    private void OnEnable()
    {
        ActionController.Instance.displayScore += displayScore;
    }
    private void OnDisable()
    {
        if (ActionController.Instance == null) return;
        ActionController.Instance.displayScore -= displayScore;


    }
    private void displayScore(Action callBack)
    {
        scoreDisplayCompleteCallback = callBack;
        AnimateScoreIncrement(PlayerController.Instance.score, ScoreConfigController.Instance.GetCurrentScore());
    }
    private void AnimateScoreIncrement(int fromScore, int toScore)
    {
        // Animates the score from `fromScore` to `toScore` over 0.5 seconds
        DOVirtual.Int(fromScore, toScore, 0.5f, value =>
        {
            UpdateScoreText(value);
        }).SetEase(Ease.Linear).OnComplete(OnCompleteDisplayScore);
    }
    private void OnCompleteDisplayScore()
    {
        scoreDisplayCompleteCallback?.Invoke();
    }
    private void UpdateScoreText(int score)
    {
        textScore.text = score.ToString();
    }
}