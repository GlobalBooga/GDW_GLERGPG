using UnityEngine;
using UnityEngine.UI;

public class SettingsBackground : MonoBehaviour
{
    [SerializeField] Image[] borders = new Image[5];
    [SerializeField] Image panel;
    [SerializeField] float animationSpeed = 3;
    [SerializeField] Image[] menuButtons;
    [SerializeField] Image[] settingsControls;
    [SerializeField] RectTransform menuTitle;
    bool midpoint;
    Image currentBorder;
    int currentIndex;
    int direction = 0;
    [SerializeField] Vector3 titleStart;
    [SerializeField] Vector3 titleEnd;
    AnimationCurve titleCurve = AnimationCurve.EaseInOut(0,0,1,1);
    float titleAnimTime;

    private void Awake()
    {
        foreach (var border in borders)
        {
            border.fillAmount = 0;
        }
        foreach (var menu in settingsControls)
        {
            menu.color = new Color(menu.color.r, menu.color.g, menu.color.b, 0);
        }
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        currentIndex = 0;
        currentBorder = borders[currentIndex];
        direction = 1;
        midpoint = false;
        titleAnimTime = 0f;
    }

    public void DisableSelf()
    {
        currentIndex = borders.Length - 1;
        currentBorder = borders[currentIndex];
        direction = -1;
        midpoint = false;
        titleAnimTime = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (direction == 0) return; 


        // hide/show menu btns
        if (direction > 0 || (direction < 0 && midpoint == true))
        {
            for (int i = 0; i < menuButtons.Length; i++)
            {
                Color c = menuButtons[i].color;
                menuButtons[i].color = new Color(c.r, c.g, c.b, Mathf.Lerp(c.a, Mathf.Clamp01(-direction), Time.unscaledDeltaTime * animationSpeed*1f));
            }
        }

        //animate borders
        if ((direction > 0 && currentBorder.fillAmount < 1) || (direction < 0 && currentBorder.fillAmount > 0))
        {
            currentBorder.fillAmount += Time.unscaledDeltaTime * animationSpeed * direction;
        }
        else if ((currentIndex += direction) < borders.Length && currentIndex >= 0)
        {
            currentBorder.fillAmount = direction > 0 ? 1 : 0;
            currentBorder = borders[currentIndex];
        }
        else
        {
            if (direction < 0) gameObject.SetActive(false);
            direction = 0;
        }

        // midpoint
        if ((direction > 0 && borders[2].fillAmount > 0f) || (direction < 0 && borders[2].fillAmount < 1f) && !midpoint)
        {
            midpoint = true;
        }

        // hide/show settings
        if (direction < 0 || (direction > 0 && midpoint == true))
        {
            for (int i = 0; i < settingsControls.Length; i++)
            {
                Color c = settingsControls[i].color;
                settingsControls[i].color = new Color(c.r, c.g, c.b, Mathf.Lerp(c.a, Mathf.Clamp01(direction), Time.unscaledDeltaTime * animationSpeed * 1f));
            }
        }

        // move title up and down
        if (!menuTitle || titleAnimTime > 1) return;
        titleAnimTime += Time.unscaledDeltaTime * animationSpeed * 0.5f;
        menuTitle.localPosition = Vector3.Lerp(direction > 0 ? titleStart : titleEnd, direction > 0 ? titleEnd : titleStart, titleCurve.Evaluate(titleAnimTime));
    }
}
