using UnityEngine;
using UnityEngine.UI;

public class ButtonToggle : MonoBehaviour
{
    public Sprite toggleOnSprite;
    public Sprite toggleOffSprite;
    private bool isToggledOn;
    private Image buttonImage;

    void Start()
    {
        buttonImage = GetComponent<Image>();
        UpdateButtonSprite();
    }

    public void ToggleButton()
    {
        isToggledOn = !isToggledOn;
        UpdateButtonSprite();
    }

    private void UpdateButtonSprite()
    {
        buttonImage.sprite = isToggledOn ? toggleOnSprite : toggleOffSprite;
    }
}
