using UnityEngine;

[CreateAssetMenu(fileName = "ElementData", menuName = "Scriptable Objects/ElementData")]
public class ElementData : ScriptableObject
{
    public string id = "A";
    public string elementName;
    public Color color;

    [Header("Special Element")] 
    public bool isIndestructible;
    public bool isObstacle;
}
