using UnityEngine;
using UnityEngine.UI;

public class InterfaceUniquePattern : MonoBehaviour
{
    public ElementData element;
    public Sprite noElementSprite;
    
    public Image image;

    void Start()
    {
        image.sprite = noElementSprite;
    }

    public void Add(ElementData newElement)
    {
        element = newElement;

        image.sprite = element.sprite;
    }

    public void Remove()
    {
        element = null;
        
        image.sprite = noElementSprite;
    }
    
}
