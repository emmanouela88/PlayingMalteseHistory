using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayMusic : MonoBehaviour
{
    public AudioClip clipLeaflet;
    public AudioClip clipClick;
    public AudioSource source;

    private void Start()
    {
        source = Camera.main.GetComponent<AudioSource>();
        
    }
    public void PlayTheMusic()
    {
        //if (!source.isPlaying)
            source.PlayOneShot(clipClick);
        /* else
            source.Stop();*/
    }

    public void PlayOnInfo()
    {
        if(!source.isPlaying)
        source.PlayOneShot(clipLeaflet);
        else
            source.Stop();
    }

    
}
