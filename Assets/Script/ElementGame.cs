using UnityEngine;

public class ElementGame : MonoBehaviour
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
        
        if (element.id == "S")
        {
            GameManager.instance.AddStart(this);
        }
        else if (element.id == "E")
        {
            GameManager.instance.AddEnd(this);
        }
        
        SpriteRenderer.color = element.color;
    }

    void CreatePlayer()
    {
        
    }

    void EndLevel()
    {
        gameObject.tag = "EndLevel";
    }
}
