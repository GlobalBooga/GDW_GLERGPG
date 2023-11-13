using UnityEngine;
using UnityEngine.UI;

public class SettingsBackground : MonoBehaviour
{
    [SerializeField] Image[] borders = new Image[5];
    [SerializeField] Image panel;
    [SerializeField] float animationSpeed = 3;
    [SerializeField] Image[] menuButtons;
    [SerializeField] Image[] settingsControls;
    bool midpoint;
    Image currentBorder;
    int currentIndex;
    int direction = 0;

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
    }

    public void DisableSelf()
    {
        currentIndex = borders.Length - 1;
        currentBorder = borders[currentIndex];
        direction = -1;
        midpoint = false;
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
                menuButtons[i].color = new Color(c.r, c.g, c.b, Mathf.Lerp(c.a, Mathf.Clamp01(-direction), Time.deltaTime * animationSpeed*1f));
            }
        }

        //animate borders
        if ((direction > 0 && currentBorder.fillAmount < 1) || (direction < 0 && currentBorder.fillAmount > 0))
        {
            currentBorder.fillAmount += Time.deltaTime * animationSpeed * direction;
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

        // show settings
        if (direction < 0 || (direction > 0 && midpoint == true))
        {
            for (int i = 0; i < settingsControls.Length; i++)
            {
                Color c = settingsControls[i].color;
                settingsControls[i].color = new Color(c.r, c.g, c.b, Mathf.Lerp(c.a, Mathf.Clamp01(direction), Time.deltaTime * animationSpeed * 1f));
            }
        }
    }
}
