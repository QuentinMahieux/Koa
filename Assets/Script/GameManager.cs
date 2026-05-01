using UnityEngine;

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

    }

    public void AddStart(ElementGame element)
    {
        if(start) return;
        start = element;
        Instantiate(player, start.transform.position, Quaternion.identity);
    }

    public void AddEnd(ElementGame element)
    {
        end = element;
    }

    public void GameOver(bool newGameOver)
    {
        isGameOver = newGameOver;
    }
}
