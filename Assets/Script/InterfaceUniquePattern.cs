using UnityEngine;
using UnityEngine.UI;

public class InterfaceUniquePattern : MonoBehaviour
{
    public ElementData element;
    
    public Image image;

    public void Add(ElementData newElement)
    {
        element = newElement;

        image.sprite = element.sprite;
    }

    public void Remove()
    {
        element = null;
        
        image.color = Color.white;
    }
    
}
