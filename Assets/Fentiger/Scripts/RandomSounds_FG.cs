using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSounds_FG : MonoBehaviour
{
    public AudioClip[] sounds;

    private void Start()
    {
        InvokeRepeating("PlaySound", 0, 1);
    }

    void PlaySound()
    {
        if (Random.Range(1, 101) <= 1 && Time.timeScale == 1)
        {
            GetComponent<AudioSource>().PlayOneShot(sounds[Random.Range(0, sounds.Length)]);
        }
    }
}
