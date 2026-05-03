using System.Collections.Generic;
using UnityEngine;

public class PlayerPalindrome : MonoBehaviour
{
    public static PlayerPalindrome instance;
    public int maxTaille = 2;
    public List<ElementData> patterns = new List<ElementData>();

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
                    Debug.Log("Palindrome");
                }
                else
                {
                    GameManager.instance.GameOver(true);
                    Debug.Log("💀 Game Over");
                }
                
                break;
            }
            if (i == patterns.Count - 1 && patterns[i])
            {
                Reset();
                AddPattern(pattern);
                return;
            }
        }
    }

    public void RemovePattern()
    {
        for (int i = patterns.Count - 1; i >= 0; i--)
        {
            if (patterns[i])
            {
                patterns[i] = null;
                uniquePatterns[i].Remove();
                return;
            }
        }
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
        
        Debug.Log(palindromeTest);
        
        for (int i = 0; i < palindromeTest.Length; i++)
        {
            if (i > (palindromeTest.Length - 1) / 2) return true;
            if (palindromeTest[i].ToString() == "." ||
                palindromeTest[palindromeTest.Length - 1 - i].ToString() == ".") return true;
            if (palindromeTest[i] != palindromeTest[palindromeTest.Length - 1 - i]) return false;
        }
        return true;
    }
}
