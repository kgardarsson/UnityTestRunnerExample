using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Water : MonoBehaviour {

    private MixerController _mixer;

    private void Start() {
        _mixer = FindObjectOfType<MixerController>();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Player") {
            _mixer._mixer.FindSnapshot("Underwater").TransitionTo(.2f);
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.tag == "Player") {
            _mixer._mixer.FindSnapshot("OnGround").TransitionTo(.2f);
        }
    }
}
