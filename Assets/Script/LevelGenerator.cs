using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using TMPro;
using Unity.VectorGraphics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelGenerator : DefaultGenerator
{
    public static LevelGenerator instance;
    public GameObject tabler;
    public GameObject placeHolder;
    
    public int taille;
    
    public bool isTesingZone;
    
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

    private void Start()
    {
        if (!isTesingZone)
        {
            NewTabler(GameManager.instance.levels[GameManager.instance.actualLevel].code);
            taille = DecoderPattern(GameManager.instance.levels[GameManager.instance.actualLevel].code);
        }
        else
        {
            NewTabler(GameManager.instance.levelMaker);
            taille = DecoderPattern(GameManager.instance.levelMaker);
        }
    }

    public void NewTabler(string code)
    {
        seed = Decoder(code);
        int index = 0;
        Vector2 _startPos = generatorData.startPos;
        for (int i = 0; i < generatorData.nbrColone; i++)
        {
            for (int j = 0; j < generatorData.nbrLigne; j++)
            {
                GameObject element = Instantiate(placeHolder, new Vector3(_startPos.x, _startPos.y, 0), Quaternion.identity);
                ElementGame elementGame = element.GetComponent<ElementGame>();

                if (elementGame != null && index < seed.Length)
                {
                    elementGame.Refresh(LettreToElement(seed[index].ToString()), true);
                }
                
                _startPos.x += generatorData.marge;
                index++;
            }
            _startPos.x = generatorData.startPos.x;
            _startPos.y += generatorData.marge;
        }
    }
}
