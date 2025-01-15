using DG.Tweening;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.Collections;

public class MaskTransition : MonoBehaviour
{
    internal float defaultScale = 2;

    public RectTransform topImageRect;
    public RectTransform bottomImageRect;

    private void OnEnable()
    {
   
    }
    private void OnDisable()
    {
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
            FadeIn();

        if (Input.GetKeyDown(KeyCode.O))
            FadeOut();
    }

    public void FadeOut()
    {
        StartCoroutine(CompleteLevelTransitionOut());
    }

    public void FadeIn()
    {
        CompleteLevelTransitionIn();
    }

    public void CompleteLevelTransitionIn()
    {
        topImageRect.anchoredPosition = new Vector2(0, Screen.height);  // Start at the top
        Vector2 topTargetPosition = new Vector2(0, Screen.height / 4); // Move to the middle of the screen

        bottomImageRect.anchoredPosition = new Vector2(0, -Screen.height); // Start at the bottom
        Vector2 bottomTargetPosition = new Vector2(0, -Screen.height / 4); // Move to the middle of the screen

        topImageRect.DOAnchorPos(topTargetPosition, 0.5f)
            .SetEase(Ease.OutQuad)
            .OnComplete(() =>
            {
            });

        bottomImageRect.DOAnchorPos(bottomTargetPosition, 0.5f)
            .SetEase(Ease.OutQuad)
            .OnComplete(() =>
            {
            });
    }

    IEnumerator CompleteLevelTransitionOut()
    {
        yield return new WaitForSeconds(0.3f);

        topImageRect.anchoredPosition = new Vector2(0, Screen.height / 4);  // Start at the top
        Vector2 topTargetPosition = new Vector2(0, Screen.height); // Move to the middle of the screen

        bottomImageRect.anchoredPosition = new Vector2(0, -Screen.height / 4); // Start at the bottom
        Vector2 bottomTargetPosition = new Vector2(0, -Screen.height); // Move to the middle of the screen

        topImageRect.DOAnchorPos(topTargetPosition, 1f)
            .SetEase(Ease.OutQuad)
            .OnComplete(() =>
            {
            });

        bottomImageRect.DOAnchorPos(bottomTargetPosition, 1f)
            .SetEase(Ease.OutQuad)
            .OnComplete(() =>
            {
               
            });
    }
}
