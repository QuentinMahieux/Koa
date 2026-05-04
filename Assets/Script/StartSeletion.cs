using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartSeletion : MonoBehaviour
{
    public TMP_Text customLevel;
    public GameObject[] menuObjects;
    public GameObject[] customObjects;

    void Start()
    {
        OpenCustomLevel();
    }
    
    public void StartGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void LevelMaker()
    {
        SceneManager.LoadScene("LevelMaker");
    }

    public void CustomLevel()
    {
        if(customLevel.text.Length <= 46) return;
        
        GameManager.instance.levelMaker = customLevel.text;
        SceneManager.LoadScene("GameCustom");
    }

    public void OpenCustomLevel()
    {
        foreach (var obj in menuObjects)
        {
            obj.SetActive(true);
        }
        foreach (var obj in customObjects)
        {
            obj.SetActive(false);
        }
    }

    public void CloseCustomLevel()
    {
        foreach (var obj in menuObjects)
        {
            obj.SetActive(false);
        }
        foreach (var obj in customObjects)
        {
            obj.SetActive(true);
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
