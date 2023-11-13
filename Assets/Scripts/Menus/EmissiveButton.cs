using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EmissiveButtons : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler, IPointerDownHandler
{
    [SerializeField] Material hoverMaterial;
    [SerializeField] float clickScale = 1.1f;

    Image image;

    private void Awake()
    {
        image = GetComponent<Image>();
    }

    public virtual void OnPointerClick(PointerEventData eventData)
    {
        transform.localScale = Vector3.one;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        transform.localScale = Vector3.one * clickScale;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!hoverMaterial) return;
        image.material = hoverMaterial;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.localScale = Vector3.one;
        image.material = null;
    }
}
