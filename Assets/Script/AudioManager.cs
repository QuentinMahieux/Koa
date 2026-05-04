using System;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    
    public AudioSource audioSource;
    public AudioClip audioEat;
    public AudioClip audiofinish;
    public AudioClip patternComplet;
    void Awake()
    {
        if (!instance)
        {
            instance = this;
        }
        else
        {
            Debug.LogError("Multiple AudioManager script attached to " + gameObject.name);
            Destroy(this);
        }
        DontDestroyOnLoad(gameObject);
    }

    

    public void PlayEat() { audioSource.PlayOneShot(audioEat); }
    public void PlayFinish() { audioSource.PlayOneShot(audiofinish); }
    public void PlayPatternComplet() { audioSource.PlayOneShot(patternComplet); }
}
