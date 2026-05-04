using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;

public class DefaultGenerator : MonoBehaviour
{
    [Header("Tabler")]
    public GeneratorData  generatorData;
    public ElementData[] elements;
    
    [Header("Information")]
    public TMP_Text playerName;
    public TMP_Text levelName;
    
    public TMP_InputField editPlayerName;
    public TMP_InputField editLevelName;
    
    public TMP_Text seedName;
    protected string seed;

    
    protected ElementData LettreToElement(string letter)
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

    protected virtual string Decoder(string code)
    {
        var match = Regex.Match(code, @"^\[(?<player>[^\]]+)\]\{(?<map>[^\}]+)\}\((?<pattern>\d+)\)(?<seed>.+)$");
        
        playerName.text = match.Groups["player"].Value;
        levelName.text = match.Groups["map"].Value;
        seedName.text = match.Groups["seed"].Value;
        
        if (editPlayerName) editPlayerName.text = match.Groups["player"].Value;
        if (editLevelName) editLevelName.text = match.Groups["map"].Value;
        
        return match.Groups["seed"].Value;
    }
    
    protected virtual int DecoderPattern(string code)
    {
        var match = Regex.Match(code, @"^\[(?<player>[^\]]+)\]\{(?<map>[^\}]+)\}\((?<pattern>\d+)\)(?<seed>.+)$");
        return int.Parse(match.Groups["pattern"].Value);
    }
}
