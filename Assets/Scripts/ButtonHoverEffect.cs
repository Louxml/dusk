using UnityEngine;
using UnityEngine.EventSystems; // EventSystems must be included to use event interfaces
using UnityEngine.UI; // This is required to access UI components

public class ButtonHoverEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Sprite normalImage; // Assign this in the inspector with your normal image
    public Sprite hoverImage; // Assign this in the inspector with your larger image

    private Image buttonImage; // This will reference the image component of the button

    private void Start()
    {
        buttonImage = GetComponent<Image>();
        if (buttonImage == null)
        {
            Debug.LogError("ButtonHoverEffect requires an Image component on the same GameObject.");
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // Change the image to hoverImage when the mouse enters
        buttonImage.sprite = hoverImage;
        buttonImage.rectTransform.sizeDelta = new Vector2(32, 36);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // Change the image back to normalImage when the mouse exits
        buttonImage.sprite = normalImage;
        buttonImage.rectTransform.sizeDelta = new Vector2(32,32);
    }
}