using UnityEngine;

[CreateAssetMenu(fileName = "GeneratorData", menuName = "Scriptable Objects/GeneratorData")]
public class GeneratorData : ScriptableObject
{
    public int nbrLigne = 6;
    public int nbrColone = 6;
    public float margeLigne = 1.3f;
    public float margeColone = 1.3f;
    
    public Vector2 startPos =  new Vector2(-3.02f, -3.58f);
}
