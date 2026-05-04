using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    
    [Header("Player")]
    public GameObject player;

    [Header("Game")]
    public bool isGameOver;
    
    [Header("Level")] 
    public ElementGame start;
    public ElementGame end;
    
    public List<LevelData> levels;
    public int actualLevel = 0;

    [Header("LevelMaker")] 
    public string levelMaker;
    
    void Awake()
    {
        if (!instance)
        {
            instance = this;
        }
        else
        {
            Debug.LogError("Multiple GameManager script attached to " + gameObject.name);
            Destroy(this);
        }
        DontDestroyOnLoad(gameObject);
    }

    public void AddStart(ElementGame element)
    {
        if(start) return;
        start = element;
        if (PlayerPalindrome.instance) PlayerPalindrome.instance.gameObject.transform.position = start.gameObject.transform.position;
        else Instantiate(player, start.transform.position, Quaternion.identity);
        
    }

    public void AddEnd(ElementGame element)
    {
        end = element;
    }

    public void NextLevel()
    {
        actualLevel++;
        if(actualLevel > levels.Count)
        {
            PauseMenu.instance.Quit();
            return;
        }
        
        AudioManager.instance.PlayFinish();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    

    public void GameOver(bool newGameOver)
    {
        isGameOver = newGameOver;
    }
}
