using UnityEngine;

public class ElementGame : MonoBehaviour
{
    public ElementData actualElement;
    public ElementData defaultElement;
    public ElementData removeElement;
    public SpriteRenderer SpriteRenderer;
    void Start()
    {
        Refresh(defaultElement, true);
    }
    
    public void Refresh(ElementData newElement, bool isNewElement = false)
    {
        if(isNewElement) defaultElement = newElement;
        
        actualElement = newElement;
        
        
        if (actualElement.id == "S")
        {
            GameManager.instance.AddStart(this);
        }
        else if (actualElement.id == "E")
        {
            GameManager.instance.AddEnd(this);
        }
        
        SpriteRenderer.sprite = actualElement.sprite;
    }

    public void Remove()
    {
        Refresh(removeElement);
    }

    public void Back()
    {
        Refresh(defaultElement);
    }
}
