using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomAudioManager : MonoBehaviour
{
    public AudioClip earnRewards;
    public AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        if(!audioSource)
        {
            audioSource = gameObject.GetComponent<AudioSource>();
        }
    }

    public void playEarnRewards() {
        audioSource.clip = earnRewards;
        audioSource.Play();
    }
}
