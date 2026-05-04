using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static PauseMenu instance;
    public GameObject[] playObjects;
    public GameObject[] pauseObjects;
    
    public bool isPaused;

    void Awake()
    {
        if (!instance)
        {
            instance = this;
        }
        else
        {
            Debug.LogError("Multiple PauseMenu script attached to " + gameObject.name);
            Destroy(this);
        }
    }
    
    void Start()
    {
        isPaused = false;
        PlayMode();
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                PlayMode();
            }
            else
            {
                PauseMode();
            }
        }
    }

    public void PlayMode()
    {
        isPaused = false;
        foreach (var obj in playObjects)
        {
            obj.SetActive(true);
        }
        foreach (var obj in pauseObjects)
        {
            obj.SetActive(false);
        }
        PlayerPalindrome.instance.gameObject.SetActive(true);
    }

    public void PauseMode()
    {
        isPaused = true;
        foreach (var obj in playObjects)
        {
            obj.SetActive(false);
        }
        foreach (var obj in pauseObjects)
        {
            obj.SetActive(true);
        }
        PlayerPalindrome.instance.gameObject.SetActive(false);

    }

    public void Quit()
    {
        SceneManager.LoadScene("Start");
    }

    public void ReturnMaker()
    {
        SceneManager.LoadScene("LevelMaker");
    }
}
