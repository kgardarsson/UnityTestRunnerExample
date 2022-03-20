using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.TestTools;

public class snapshot_volume {

    [UnityTest]
    public IEnumerator is_expected_value() {
        
        // Setup
        AudioMixer mixer = Resources.Load<AudioMixer>("Audio/AudioMixer");
        AudioMixerSnapshot snapshot = mixer.FindSnapshot("Underwater");
        // AudioMixerGroup master = mixer.FindMatchingGroups("Master")[0];
        float transitionTime = 1;
        float expectedValue = -3;
        float actualValue;
        float acceptedError = .05f;

        // Action
        snapshot.TransitionTo(transitionTime);

        yield return new WaitForSeconds(transitionTime);

        mixer.GetFloat("MasterVol", out actualValue); // The only way I could find to access volume parameter was to expose it. The snapshots will run as long as the exposed value is not set.

        // Test whether expected and actual values are more or less the same
        Assert.AreEqual(expectedValue, actualValue, acceptedError);

        yield return null;

        // Code for my private purposes to see the change in mixer UI
        // yield return new WaitForSeconds(3f);
    }
}
