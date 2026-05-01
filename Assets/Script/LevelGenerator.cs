using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : DefaultGenerator
{
    public LevelData levelData;
    public GameObject placeHolder;
    
    public List<ElementData> elements;
    
    
    void Start()
    {
        NewTabler(levelData.code);
    }

    void NewTabler(string code)
    {
        int index = 0;
        Vector2 _startPos = generatorData.startPos;
        for (int i = 0; i < generatorData.nbrColone; i++)
        {
            for (int j = 0; j < generatorData.nbrLigne; j++)
            {
                GameObject element = Instantiate(placeHolder, new Vector3(_startPos.x, _startPos.y, 0), Quaternion.identity);
                ElementGame elementGame = element.GetComponent<ElementGame>();

                if (elementGame != null)
                {
                    elementGame.Refresh(Decripter(code[index].ToString()));
                }
                
                _startPos.x += generatorData.margeLigne;
                index++;
            }
            _startPos.x = generatorData.startPos.x;
            _startPos.y += generatorData.margeColone;
        }
    }

    ElementData Decripter(string letter)
    {
        foreach (ElementData data in elements)
        {
            if (data.id == letter)
            {
                return data;
            }
        }
        Debug.LogError("Element not found: " + letter);
        return null;
    }
    
}
