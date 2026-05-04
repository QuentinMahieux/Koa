using UnityEngine;

public class ElementStudio : MonoBehaviour
{
    public ElementData element;
    public SpriteRenderer SpriteRenderer;
    void Start()
    {
    }

    public void Refresh(ElementData newElement)
    {
        element = newElement;
        SpriteRenderer.sprite = element.sprite;
        
        if (element.id == "S")
        {
            LevelMaker.instance.ChangeStart(this);
        }
        else if (element.id == "E")
        {
            LevelMaker.instance.ChangeEnd(this);
        }
        
    }
}
