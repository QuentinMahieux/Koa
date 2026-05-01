using UnityEngine;
using UnityEngine.UI;

public class ButtonChoiceElement : MonoBehaviour
{
    public ElementData elementData;
    
    [Header("Interface")]
    public Image buttonImage;

    public void Instanciate(ElementData newElementData)
    {
        elementData = newElementData;
        
        buttonImage.color = elementData.color;
    }

    public void ApplyNewElement()
    {
        LevelMaker.instance.ChangeElement(elementData);
    }
}
