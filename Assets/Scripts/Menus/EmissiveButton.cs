using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EmissiveButtons : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler, IPointerDownHandler
{
    [SerializeField] Material hoverMaterial;
    [SerializeField] float clickScale = 0.9f;
    [SerializeField] AudioClip selectClip;
    [SerializeField] AudioClip confirmClip;
    AudioSource sound;
    Image image;

    private void Awake()
    {
        sound = GetComponent<AudioSource>();
        image = GetComponent<Image>();
    }

    public virtual void OnPointerClick(PointerEventData eventData)
    {
        transform.localScale = Vector3.one;

        if (sound && confirmClip)
        {
            sound.clip = confirmClip;
            sound.Play();
        }
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

        if (sound && selectClip)
        {
            sound.clip = selectClip;
            sound.Play();
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.localScale = Vector3.one;
        image.material = null;
    }
}
