using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.TestTools;

public class exposed_volume {

    [UnityTest]
    public IEnumerator is_expected_value() {
        
        // Setup
        AudioMixer mixer = Resources.Load<AudioMixer>("Audio/AudioMixer");
        float expectedValue = -3;
        float actualValue;

        // Action
        mixer.SetFloat("MusicVol", expectedValue);

        // Test whether expected and actual values are the same
        mixer.GetFloat("MusicVol", out actualValue);
        Assert.AreEqual(expectedValue, actualValue);

        yield return null;

        // Code for private purposes to assess the change in mixer UI
        // yield return new WaitForSeconds(10f);
    }
}
