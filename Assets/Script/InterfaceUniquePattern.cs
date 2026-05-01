using UnityEngine;
using UnityEngine.UI;

public class InterfaceUniquePattern : MonoBehaviour
{
    public ElementData element;
    
    public Image image;

    public void Add(ElementData newElement)
    {
        element = newElement;

        image.color = element.color;
    }

    public void Remove()
    {
        element = null;
        
        image.color = Color.white;
    }
    
}
