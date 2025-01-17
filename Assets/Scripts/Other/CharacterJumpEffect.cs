using UnityEngine;
using DG.Tweening;
using System.Collections;

public class CharacterJumpEffect : MonoBehaviour
{
    public float jumpHeight = 0.5f;    // Height of the jump
    public float jumpDuration = 0.5f; // Duration of the jump
    public float delayBetweenJumps = 0.2f;  // Delay between each jump animation
    public float repeatDelay = 1f;   // Delay before repeating the animation

    private Transform[] children;

    void Start()
    {
        children = GetComponentsInChildren<Transform>();
        StartCoroutine(JumpChildrenSequentially());
    }

    IEnumerator JumpChildrenSequentially()
    {
        // Loop forever for the repeating animation
        while (true)
        {
            for (int i = 1; i < children.Length; i++) // Start from 1 to skip the parent object
            {
                JumpChild(children[i], children[i].position.y);
                yield return new WaitForSeconds(delayBetweenJumps);
            }

            yield return new WaitForSeconds(repeatDelay);
        }
    }

    void JumpChild(Transform child, float initPosY)
    {
        child.DOMoveY(child.position.y + jumpHeight, jumpDuration / 2).SetEase(Ease.OutQuad)
            .OnComplete(() =>
            {
                // Move the child back down after jumping
                child.DOMoveY(initPosY, jumpDuration / 2).SetEase(Ease.InQuad);
            });
    }
}
