using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SetVolume : MonoBehaviour
{
    public AudioMixer mixer;

    public void SetMasterVolume(float slidervalue)
    {
        mixer.SetFloat("MasterVolume", Mathf.Log10(slidervalue)*20);
    }
    public void SetMusicVolume(float slidervalue)
    {
        mixer.SetFloat("MusicVolume", Mathf.Log10(slidervalue) * 20);
    }
    public void SetSFXVolume(float slidervalue)
    {
        mixer.SetFloat("SFXVolume", Mathf.Log10(slidervalue) * 20);
    }
}
