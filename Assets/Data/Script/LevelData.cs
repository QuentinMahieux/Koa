using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelData", menuName = "Scriptable Objects/LevelData")]
public class LevelData : ScriptableObject
{
    
    [HideInInspector] public List<Tabler>  tablers =  new List<Tabler>();

    [Header("MetaData")] 
    public string code;
}

[System.Serializable]
public class Tabler
{
    public List<ElementData> lignes = new List<ElementData>();
}
