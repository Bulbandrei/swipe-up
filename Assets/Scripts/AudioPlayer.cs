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
        AudioController.OnRequestPlayAudio += OnRequestPlayAudio;
    }

    private void OnDestroy()
    {
        AudioController.OnRequestPlayAudio -= OnRequestPlayAudio;
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
        Debug.Log("playing " + gameObject.name);
        int randIndex = Random.Range(0, audioSources.Length);
        audioSources[randIndex].Play();
    }
}
