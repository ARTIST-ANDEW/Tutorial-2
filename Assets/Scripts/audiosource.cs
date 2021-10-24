using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audiosource : MonoBehaviour
{
    public AudioClip Backroundmusic;  
    public AudioSource musicSource;

    // Update is called once per frame
    void Start()
    {
    musicSource.clip = Backroundmusic;
    musicSource.Play();  
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
