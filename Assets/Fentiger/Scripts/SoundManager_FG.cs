using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager_FG : MonoBehaviour
{
    public Generator_FG generator;
    public AudioClip[] mainTheme;
    int level = 0;
    void Update()
    {
        if (level != generator.Level)
        {
            level = generator.Level;
            float time = GetComponent<AudioSource>().time;
            float duration = GetComponent<AudioSource>().clip.length;
            GetComponent<AudioSource>().clip = mainTheme[level];
            GetComponent<AudioSource>().time = Mathf.Lerp(0, GetComponent<AudioSource>().clip.length, Mathf.InverseLerp(0, duration, time));
            GetComponent<AudioSource>().Play();
        }
    }
}
