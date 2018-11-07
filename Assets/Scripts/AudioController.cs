using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioController : MonoBehaviour {

    public static AudioController Instance { get; private set; }

    public delegate void SoundSwitchDelegate();
    public SoundSwitchDelegate OnMusicSwitch;
    public SoundSwitchDelegate OnSoundFxSwitch;

    [SerializeField]
    AudioMixer masterMixer;

    public bool musicOn { get; private set; }

    public bool soundOn { get; private set; }

    private void Awake()
    {
        Instance = this;
        musicOn = true;
        soundOn = true;
    }

    public void TurnMusic()
    {
        musicOn = !musicOn;
        masterMixer.SetFloat("musicVol", musicOn ? 0 : -80); // Exposed var in Sound Mixer
        if (OnMusicSwitch != null)
            OnMusicSwitch();
    }

    public void TurnSound()
    {
        soundOn = !soundOn;
        masterMixer.SetFloat("soundFxVol", soundOn ? 0 : -80); // Exposed var in Sound Mixer
        if (OnSoundFxSwitch != null)
            OnSoundFxSwitch();
    }
}
