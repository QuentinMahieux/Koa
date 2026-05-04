using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelMakerManager : MonoBehaviour
{
    public GameObject copierFeedback;

    void Start()
    {
        copierFeedback.SetActive(false);
    }
    
    public void CopieSeed()
    {
        GUIUtility.systemCopyBuffer = LevelMaker.instance.CreateNewLevel();
        StartCoroutine(CopieFeedback());
    }

    IEnumerator CopieFeedback()
    {
        copierFeedback.SetActive(true);
        yield return new WaitForSeconds(0.45f);
        copierFeedback.SetActive(false);
    }

    public void ChargeLevel()
    {
        GameManager.instance.levelMaker = LevelMaker.instance.CreateNewLevel();
        SceneManager.LoadScene("MakerTest");
    }
}
