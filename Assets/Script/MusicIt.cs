using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicIt : MonoBehaviour
{
    public AudioManager audioManager;

    // Start is called before the first frame update
    void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
        audioManager.musicSource.clip = audioManager.backGround;
        audioManager.musicSource.Play();
    }


}
