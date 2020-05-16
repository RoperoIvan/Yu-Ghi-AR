using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayOnclick : MonoBehaviour
{
    AudioSource audio_source;
    //public AudioClip click_fx;
    // Start is called before the first frame update
    void Start()
    {
        audio_source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnClick()
    {
        audio_source.Play();
    }
}
