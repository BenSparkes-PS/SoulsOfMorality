using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    public static AudioManager Instance { get; private set; }


    public AudioClip[] AudioClips;
    private AudioSource[] AudioSources;

    void Awake()
    {
        if (Instance != null)
            Destroy(Instance);
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        AudioSources = GetComponents<AudioSource>();
    }

   
    public void ToggleRoundBackground()
    {
        if (AudioSources[0].isPlaying)
        {
            AudioSources[0].Stop();
        }
        else
        {
            AudioSources[0].Play();
        }
    }
    public void PlayDeathSound()
    {
        AudioSources[1].clip = AudioClips[0];
        AudioSources[1].Play();
    }
    public void PlaySoulPickup()
    {
        AudioSources[1].clip = AudioClips[1];
        AudioSources[1].Play();
    }
    public void PlayMenuClick()
    {
        AudioSources[1].clip = AudioClips[2];
        AudioSources[1].Play();
    }
    public void PlayPortal()
    {
        AudioSources[1].clip = AudioClips[3];
        AudioSources[1].Play();
    }
}
