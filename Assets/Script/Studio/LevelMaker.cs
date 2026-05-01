using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class LevelMaker : MonoBehaviour
{
    public static LevelMaker instance;
    [Header("Tabler")]
    public int nbrLigne = 6;
    public int nbrColone = 6;
    public float margeLigne = 1.4f;
    public float margeColone = 1.4f;
    
    public Vector2 startPos;
    public GameObject placeHolder;

    [Header("Interface")] 
    public GameObject buttonParent;
    
    [Header("Studio")]
    public ElementData[] elements;
    public ElementData voidElement;
    [HideInInspector] public ElementData actualElement;
    public List<TablerStudio> tablerStudios;

    void Awake()
    {
        if (!instance)
        {
            instance = this;
        }
        else
        {
            Debug.LogError("Multiple LevelMaker script attached to " + gameObject.name);
            Destroy(this);
        }
    }
    
    void Start()
    {
        NewTabler();
        InstanciateInterface();
        
        
    }

    void NewTabler()
    {
        Vector2 _startPos = startPos;
        tablerStudios =  new List<TablerStudio>();
        for (int i = 0; i < nbrColone; i++)
        {
            tablerStudios.Add(new TablerStudio());
            for (int j = 0; j < nbrLigne; j++)
            {
                GameObject element = Instantiate(placeHolder, new Vector3(_startPos.x, _startPos.y, 0), Quaternion.identity);
                tablerStudios[^1].lignes.Add(element.GetComponentInChildren<ElementStudio>());
                _startPos.x +=  margeLigne;
            }
            _startPos.x = startPos.x;
            _startPos.y += margeColone;
        }
    }

    void InstanciateInterface()
    {
        int index = 0;
        foreach (ButtonChoiceElement button in buttonParent.GetComponentsInChildren<ButtonChoiceElement>())
        {
            if(index < elements.Length)
            {
                button.Instanciate(elements[index]);
                index++;
            }
            else
            {
                button.gameObject.SetActive(false);
            }
            
        }
    }

    public void ChangeElement(ElementData element)
    {
        actualElement = element;
    }

    public void CreateNewLevel()
    {
        LevelData levelData = ScriptableObject.CreateInstance<LevelData>();
        for (int i = 0; i < tablerStudios.Count; i++)
        {
            levelData.tablers.Add(new Tabler());
            for (int j = 0; j < tablerStudios[i].lignes.Count; j++)
            {
                levelData.tablers[^1].lignes.Add(tablerStudios[i].lignes[j].element);
                levelData.code += tablerStudios[i].lignes[j].element.id;
            }
        }
        
        #if UNITY_EDITOR
        AssetDatabase.CreateAsset(levelData, AssetDatabase.GenerateUniqueAssetPath("Assets/Data/Objects/Level.asset"));
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        Debug.Log("Level Created");
        #endif
    }
}
[System.Serializable]
public class TablerStudio
{
    public List<ElementStudio> lignes =  new List<ElementStudio>();
}