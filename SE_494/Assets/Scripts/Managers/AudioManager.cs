using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }
    [SerializeField] private AudioSource backgroundMusic;
    [SerializeField] private Slider volumeSlider;
    
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        backgroundMusic = GetComponent<AudioSource>();
        
    }
    
    public float GetVolume()
    {
        return backgroundMusic.volume;
    }

    public void SetVolume(float volume)
    {
        backgroundMusic.volume = volume;
    }
}

