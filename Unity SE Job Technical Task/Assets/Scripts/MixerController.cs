using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[ExecuteAlways]
public class MixerController : MonoBehaviour {

    public AudioMixer _mixer;
    public AudioMixerSnapshot[] _snapshots;

    private float _volume = 1;

    void Start() {
        if (!_mixer)
            Debug.LogError("AudioMixer has not yet been applied to " + gameObject.name);
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.DownArrow))
            UpdateVolume(_volume - 0.1f);
        else if (Input.GetKeyDown(KeyCode.UpArrow))
            UpdateVolume(_volume + 0.1f);
    }

    void UpdateVolume(float newVolume) {
        
        if (newVolume <= 0)
            newVolume = 0;
        else if (newVolume >= 1)
            newVolume = 1;
        
        SetExposedVolumeValue(newVolume);
        _volume = newVolume;
    }

    void SetExposedVolumeValue(float volume) {
        _mixer.SetFloat("MusicVol", LINEAR_TO_DECIBEL(volume));
    }

    public static float LINEAR_TO_DECIBEL(float linearVolume) {
        // Thanks to https://answers.unity.com/questions/283192/how-to-convert-decibel-number-to-audio-source-volu.html
         float dB;
         
         if (linearVolume != 0)
             dB = 20.0f * Mathf.Log10(linearVolume);
         else
             dB = -144.0f;
         
         return dB;
    }
}
