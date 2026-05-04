using System.Collections.Generic;
using JetBrains.Annotations;
using TMPro;
using UnityEditor;
using UnityEngine;

public class LevelMaker : DefaultGenerator
{
    public static LevelMaker instance;
    public GameObject parentLevelElement;
    public GameObject placeHolder;
    public LevelData chargeLevelData;

    [Header("Interface")] 
    public GameObject buttonParent;
    
    [Header("Studio")]
    public ElementData voidElement;
    [HideInInspector] public ElementData actualElement;
    public List<TablerStudio> tablerStudios;


    public int partternTaille;


    [Header("Special Condition")] 
    public ElementStudio start;
    public ElementStudio end;
    

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
        if (GameManager.instance.levelMaker.Length != 0) NewTabler(Decoder(GameManager.instance.levelMaker));
        else NewTabler("VVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVV");
        InstanciateInterface();
    }

    void NewTabler(string code)
    {
        Vector2 _startPos = generatorData.startPos;
        tablerStudios =  new List<TablerStudio>();
        foreach (ElementGame element in parentLevelElement.GetComponentsInChildren<ElementGame>())
        {
            Destroy(element.gameObject);
        }
        
        int index = 0;
        Debug.Log(code);
        for (int i = 0; i < generatorData.nbrColone; i++)
        {
            tablerStudios.Add(new TablerStudio());
            for (int j = 0; j < generatorData.nbrLigne; j++)
            {
                GameObject element = Instantiate(placeHolder,new Vector3(_startPos.x, _startPos.y, 0), Quaternion.identity, parentLevelElement.transform);
                tablerStudios[^1].lignes.Add(element.GetComponentInChildren<ElementStudio>());
                element.GetComponent<ElementStudio>().Refresh(LettreToElement(code[index].ToString()));
                _startPos.x +=  generatorData.marge;
                Debug.Log(LettreToElement(code[index].ToString()));
                index++;
            }
            _startPos.x = generatorData.startPos.x;
            _startPos.y += generatorData.marge;
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

    public string CreateNewLevel()
    {
        if (!start || !end)
        {
            Debug.LogError("LevelMaker cannot create new level");
            return null;
        }
        
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

        levelData.code = $"[{editPlayerName.text}]{{{editLevelName.text}}}({partternTaille}){levelData.code}";
        
        #if UNITY_EDITOR
        AssetDatabase.CreateAsset(levelData, AssetDatabase.GenerateUniqueAssetPath("Assets/Data/Objects/Level/Level.asset"));
        
        AssetDatabase.RenameAsset(AssetDatabase.GetAssetPath(levelData), levelName.text + "Data");
        
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        Debug.Log("Level Created");
        #endif
        
        return levelData.code;
    }

    public void ChangeStart(ElementStudio element)
    {
        if(start) start.Refresh(voidElement);
        start = element;
    }

    public void ChangeEnd(ElementStudio element)
    {
        if(end) end.Refresh(voidElement);
        end = element;
    }
}
[System.Serializable]
public class TablerStudio
{
    public List<ElementStudio> lignes =  new List<ElementStudio>();
}