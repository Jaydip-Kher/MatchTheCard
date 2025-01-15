using DG.Tweening;
using UnityEngine;
using System.Collections;

public class MaskTransition : MonoBehaviour
{
    [SerializeField] private RectTransform topImageRect;
    [SerializeField] private RectTransform bottomImageRect;
    [SerializeField] private Canvas canvas;

    private void OnEnable()
    {
    }

    private void OnDisable()
    {
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
            MoveIn();

        if (Input.GetKeyDown(KeyCode.O))
            MoveOut();
    }

    public void MoveOut()
    {
        StartCoroutine(CompleteLevelTransitionOut());
    }

    public void MoveIn()
    {
        CompleteLevelTransitionIn();
    }

    public void CompleteLevelTransitionIn()
    {
        SetImageSizeAtRuntime();

        topImageRect.anchoredPosition = new Vector2(0, GetScreenHeight());
        Vector2 topTargetPosition = new Vector2(0, GetScreenHeight() / 4); // Target the center

        bottomImageRect.anchoredPosition = new Vector2(0, -GetScreenHeight());
        Vector2 bottomTargetPosition = new Vector2(0, -GetScreenHeight() / 4); // Target the center

        topImageRect.DOAnchorPos(topTargetPosition, 0.5f).SetEase(Ease.OutQuad);
        bottomImageRect.DOAnchorPos(bottomTargetPosition, 0.5f).SetEase(Ease.OutQuad);
    }

    IEnumerator CompleteLevelTransitionOut()
    {
        yield return new WaitForSeconds(0.3f); // Optional delay

        SetImageSizeAtRuntime();
        topImageRect.anchoredPosition = new Vector2(0, GetScreenHeight() / 4);
        Vector2 topTargetPosition = new Vector2(0, GetScreenHeight()); // Move to the top off-screen

        bottomImageRect.anchoredPosition = new Vector2(0, -GetScreenHeight() / 4);
        Vector2 bottomTargetPosition = new Vector2(0, -GetScreenHeight()); // Move to the bottom off-screen

        topImageRect.DOAnchorPos(topTargetPosition, 1f).SetEase(Ease.OutQuad);
        bottomImageRect.DOAnchorPos(bottomTargetPosition, 1f).SetEase(Ease.OutQuad);
    }

    private void SetImageSizeAtRuntime()
    {
        topImageRect.sizeDelta = new Vector2(GetScreenWidth(), GetScreenHeight() / 2);
        bottomImageRect.sizeDelta = new Vector2(GetScreenWidth(), GetScreenHeight() / 2);
    }

    private float GetScreenHeight()
    {
        return canvas.GetComponent<RectTransform>().rect.height;
    }

    private float GetScreenWidth()
    {
        return canvas.GetComponent<RectTransform>().rect.width;
    }
}
