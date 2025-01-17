using UnityEngine;

public class SetAnchorScript : MonoBehaviour
{
    #region VARIABLES
    #region PUBLIC_VAR
    public Anchor anchore;
    public Camera cam = null;
    public Vector2 offset;
    #endregion
    #region PRIVATE_VAR
    private int screenHeight;
    private int screenWidth;
    private float farFromCamera = 0f;
    #endregion
    #endregion

    #region METHODS
    public void SetUI(Anchor anch)
    {

        screenHeight = Screen.height;
        screenWidth = Screen.width;


        if (cam == null)
            cam = Camera.main;

        Vector3 pos = transform.position;
        Vector3 newPos = Vector3.zero;
        switch (anch)
        {
            case Anchor.TopLeft:
                newPos = cam.ScreenToWorldPoint(new Vector3(0, screenHeight, farFromCamera));
                break;
            case Anchor.TopCenter:
                newPos = cam.ScreenToWorldPoint(new Vector3(screenWidth / 2, screenHeight, farFromCamera));
                break;
            case Anchor.TopRight:
                newPos = cam.ScreenToWorldPoint(new Vector3(screenWidth, screenHeight, farFromCamera));
                break;
            case Anchor.BottomLeft:
                newPos = cam.ScreenToWorldPoint(new Vector3(0, 0, farFromCamera));
                break;
            case Anchor.BottomCenter:
                newPos = cam.ScreenToWorldPoint(new Vector3(screenWidth / 2, 0, farFromCamera));
                break;
            case Anchor.BottomRight:
                newPos = cam.ScreenToWorldPoint(new Vector3(screenWidth, 0, farFromCamera));
                break;
            case Anchor.MiddleLeft:
                newPos = cam.ScreenToWorldPoint(new Vector3(0, screenHeight / 2, farFromCamera));
                break;
            case Anchor.MiddleCenter:
                newPos = cam.ScreenToWorldPoint(new Vector3(screenWidth / 2, screenHeight / 2, farFromCamera));
                break;
            case Anchor.MiddleRight:
                newPos = cam.ScreenToWorldPoint(new Vector3(screenWidth, screenHeight / 2, farFromCamera));
                break;

            default:
                break;
        }
        newPos.x += offset.x;
        newPos.y += offset.y;
        newPos.z = pos.z;

        transform.position = newPos;
    }
    #region UNITY_CALLBACKS

    public void OnResolutionChange()
    {
        SetUI(anchore);
    }

    private float GetRatio()
    {
        return (Screen.width + Screen.height) / 2f;
    }

    private void Awake()
    {
        SetUI(anchore);
    }

    private float ratio = 0f;
    private void LateUpdate()
    {
        float currentRatio = GetRatio();
        if (ratio != currentRatio)
        {
            ratio = currentRatio;
            OnResolutionChange();
        }

#if UNITY_EDITOR
        SetUI(anchore);
#endif
    }
    #endregion
    #endregion
}
public enum Anchor
{
    None, TopLeft, TopCenter, TopRight, BottomLeft, BottomCenter, BottomRight, MiddleLeft, MiddleCenter, MiddleRight
}
