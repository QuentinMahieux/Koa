using System.Collections.Generic;
using UnityEngine;

public class PlayerPalindrome : MonoBehaviour
{
    public static PlayerPalindrome instance;
    public int maxTaille = 2;
    public List<ElementData> patterns = new List<ElementData>();
    
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
    }
    
    public void AddPattern(ElementData pattern)
    {
        if(pattern.id == "S" || pattern.id == "E") return;
        
        for (int i = 0; i < patterns.Count; i++)
        {
            if (!patterns[i] && i < maxTaille)
            {
                patterns[i] =  pattern;
                
                if (DetectPalindrome(patterns))
                {
                    Debug.Log("Palindrome");
                }
                else
                {
                    Debug.Log("Do not Palindrome");
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
    
    bool DetectPalindrome(List<ElementData> newPattern)
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
