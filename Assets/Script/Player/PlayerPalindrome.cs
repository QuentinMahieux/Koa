using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPalindrome : MonoBehaviour
{
    public static PlayerPalindrome instance;
    public int maxTaille;
    public List<ElementData> patterns = new List<ElementData>();
    public List<Pull> historicPulls = new List<Pull>();

    [Header("Interface")] 
    public GameObject parentUniquePattern;
    public List<InterfaceUniquePattern> uniquePatterns = new List<InterfaceUniquePattern>();
    
    void Awake()
    {
        if (!instance)
        {
            instance = this;
        }
        else
        {
            Debug.LogError("Multiple PlayerPalindrome script attached to " + gameObject.name);
            Destroy(this);
        }
    }

    void Start()
    {
        maxTaille = LevelGenerator.instance.taille;
        Reset();
    }

    void Reset()
    {
        patterns.Clear();
        for (int i = 0; i < maxTaille; i++)
        {
            patterns.Add(null);
        }

        int index = 0;
        uniquePatterns.Clear();
        foreach (InterfaceUniquePattern uniquePattern in parentUniquePattern.GetComponentsInChildren<InterfaceUniquePattern>())
        {
            if (index < maxTaille)
            {
                uniquePattern.gameObject.SetActive(true);
                uniquePattern.Remove();
                uniquePatterns.Add(uniquePattern);
            }
            else uniquePattern.gameObject.SetActive(false);
            index++;
        }
    }
    
    public void AddPattern(ElementData pattern)
    {
        if(pattern.isIndestructible) return;
        
        for (int i = 0; i < patterns.Count; i++)
        {
            if (!patterns[i] && i < maxTaille)
            {
                patterns[i] =  pattern;
                
                uniquePatterns[i].Add(pattern);
                
                if (DetectPalindrome(patterns))
                {
                    GameManager.instance.GameOver(false);
                    PlayerMovement.instance.gameOver.SetActive(false);
                }
                else
                {
                    GameManager.instance.GameOver(true);
                    PlayerMovement.instance.gameOver.SetActive(true);
                }
                
                break;
            }
            if (i == patterns.Count - 1 && patterns[i])
            {
                AudioManager.instance.PlayPatternComplet();
                AddPull(patterns);
                Reset();
                AddPattern(pattern);
                return;
            }
        }
    }

    public void RemovePattern()
    {
        bool isFind = false;
        for (int i = patterns.Count - 1; i >= 0; i--)
        {
            if (patterns[i] && !isFind)
            {
                patterns[i] = null;
                uniquePatterns[i].Remove();
                isFind =  true;
            }
        }

        if (historicPulls.Count > 0 && CountPattern(patterns) == 0)
        {
            foreach (ElementData element in historicPulls[^1].elements)
            {
                AddPattern(element);
            }
            historicPulls.Remove(historicPulls[^1]);
        }
    }

    public int CountPattern(List<ElementData> listPatterns)
    {
        int count = 0;
        foreach (ElementData element in listPatterns)
        {
            if (element)
            {
                count++;
            }
        }
        Debug.Log(count);
        return count;
    }
    
    public bool DetectPalindrome(List<ElementData> newPattern)
    {
        string palindromeTest = "";
        foreach (ElementData element in newPattern)
        {
            if (!element)
            {
                palindromeTest += ".";
            }
            else
            {
                palindromeTest += element.id;
            }
        }
        
        
        for (int i = 0; i < palindromeTest.Length; i++)
        {
            if (i > (palindromeTest.Length - 1) / 2) return true;
            if (palindromeTest[i].ToString() == "." ||
                palindromeTest[palindromeTest.Length - 1 - i].ToString() == ".") return true;
            if (palindromeTest[i] != palindromeTest[palindromeTest.Length - 1 - i]) return false;
        }
        return true;
    }

    public void AddPull(List<ElementData> newPull)
    {
        historicPulls.Add(new Pull());
        foreach (ElementData element in newPull)
        {
            historicPulls[^1].elements.Add(element);
        }
    }
}

[Serializable]
public class Pull
{
    public List<ElementData> elements = new List<ElementData>();
}
