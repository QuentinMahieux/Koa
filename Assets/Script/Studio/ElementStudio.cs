using UnityEngine;

public class ElementStudio : MonoBehaviour
{
    public ElementData element;
    public SpriteRenderer SpriteRenderer;
    void Start()
    {
        Refresh(element);
    }

    public void Refresh(ElementData newElement)
    {
        element = newElement;
        
        SpriteRenderer.color = element.color;
    }
}
