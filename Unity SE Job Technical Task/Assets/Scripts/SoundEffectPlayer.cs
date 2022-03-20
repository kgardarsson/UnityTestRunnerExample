using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundEffectPlayer : MonoBehaviour {

    AudioSource _audio;

    private void Start() {
        _audio = GetComponent<AudioSource>();
    }

    void Update() {
        if (Input.GetMouseButtonDown(0))
            _audio.PlayOneShot(_audio.clip);
    }
}
