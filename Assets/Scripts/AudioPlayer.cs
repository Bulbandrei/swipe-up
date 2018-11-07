using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioPlayer : MonoBehaviour
{
    [SerializeField] private AudioType audioType;
    private AudioSource[] audioSources;
    private void Awake()
    {
        audioSources = GetComponents<AudioSource>();
        
    }

    private void OnDestroy()
    {
        
    }

    public void OnRequestPlayAudio(AudioType audioType)
    {
        if (audioType == this.audioType)
        {
            PlayRandom();
        }
    }

    public void PlayRandom()
    {
        int randIndex = Random.Range(0, audioSources.Length);
        audioSources[randIndex].Play();
    }
}
