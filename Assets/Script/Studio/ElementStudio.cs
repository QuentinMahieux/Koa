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

        if (element.id == "S")
        {
            LevelMaker.instance.ChangeStart(this);
        }
        else if (element.id == "E")
        {
            LevelMaker.instance.ChangeEnd(this);
        }
        
        SpriteRenderer.sprite = element.sprite;
    }
}
