using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Controller {

    public AudioMixer _mixer;

    public Controller() {
        _mixer = Resources.Load<AudioMixer>("Audio/AudioMixer");
    }

    public void ChangeExposedValue(string name, float value) {
        _mixer.SetFloat(name, value);
    }
    
}
