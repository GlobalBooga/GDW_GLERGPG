using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using FMODUnity;

public class EmissiveButtons : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler, IPointerDownHandler
{
    [SerializeField] Material hoverMaterial;
    [SerializeField] float clickScale = 0.9f;
    [SerializeField] EventReference selectClip;
    [SerializeField] EventReference confirmClip;
    Image image;

    private void Awake()
    {
        image = GetComponent<Image>();
    }

    public virtual void OnPointerClick(PointerEventData eventData)
    {
        transform.localScale = Vector3.one;

        if (selectClip.IsNull) return;
        AudioManager.instance.PlayOneShot(selectClip, transform.position);
        
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        transform.localScale = Vector3.one * clickScale;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (hoverMaterial)
        {
            image.material = hoverMaterial;
        }

        if (selectClip.IsNull) return;
        AudioManager.instance.PlayOneShot(selectClip, transform.position);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.localScale = Vector3.one;
        image.material = null;
    }
}
