using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] public static SoundManager instance;
    [SerializeField] public AudioSource aud;
    private void Awake()
    {
        instance = this;
        aud = GetComponent<AudioSource>();
    }
    public void PlaySound(AudioClip source)
    {
        if (!source) return;
        aud.PlayOneShot(source);
    }
    public void ChangeVolume(float change)
    {
        float curVolume = PlayerPrefs.GetFloat("soundVolume", aud.volume);
        curVolume += change;
        if (curVolume < 0) curVolume = 1;
        else if (curVolume > 1) curVolume = 0;
        aud.volume = curVolume;
        PlayerPrefs.SetFloat("soundVolume", curVolume);
    }
}
