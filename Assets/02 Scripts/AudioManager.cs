using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public AudioSource audioSource;
    public AudioClip[] clips;
    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        audioSource = this.GetComponent<AudioSource>();
    }

    public void PlaySound(int index)
    {
        if (!audioSource.isPlaying)
        {
            audioSource.clip = clips[index];
            audioSource.Play();
        }
    }
}
