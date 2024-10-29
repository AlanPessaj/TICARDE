using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager_FG : MonoBehaviour
{
    public Generator_FG generator;
    public AudioSource themeSource;
    public AudioSource effectsSource;
    public AudioClip[] mainTheme;
    public AudioClip dropDown;
    public AudioClip carRunOver;
    public AudioClip waterFalling;
    public AudioClip lionBite;
    int level = 0;
    void Update()
    {
        if (level != generator.Level)
        {
            level = generator.Level;
            float time = themeSource.time;
            float duration = themeSource.clip.length;
            themeSource.clip = mainTheme[level];
            themeSource.time = Mathf.Lerp(0, themeSource.clip.length, Mathf.InverseLerp(0, duration, time));
            themeSource.Play();
            effectsSource.Play();
        }
    }

    public void EndSound()
    {
        themeSource.Stop();
        effectsSource.PlayOneShot(dropDown);
    }

    public void PlaySound(AudioClip sound)
    {
        effectsSource.PlayOneShot(sound);
    }
}
