using System;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : DefaultGenerator
{
    public static LevelGenerator instance;
    public LevelData levelData;
    public GameObject placeHolder;
    
    public List<ElementData> elements;

    void Awake()
    {
        if (!instance)
        {
            instance = this;
        }
        else
        {
            Debug.LogError("Multiple LevelGenerator script attached to " + gameObject.name);
            Destroy(this);
        }
    }

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
                    elementGame.Refresh(Decripter(code[index].ToString()), true);
                }
                
                _startPos.x += generatorData.marge;
                index++;
            }
            _startPos.x = generatorData.startPos.x;
            _startPos.y += generatorData.marge;
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
