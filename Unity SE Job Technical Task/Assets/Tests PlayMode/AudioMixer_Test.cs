using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.TestTools;

namespace Test {
    public class AudioMixer_Test {

        [UnityTest]
        public IEnumerator ExposedParameter_WhenSet_WillHaveTargetVolume() {
            
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
        
        [UnityTest]
        public IEnumerator Snapshot_WhenTransistionedTo_WillReachTargetVolume() {
            
            // Setup
            AudioMixer mixer = Resources.Load<AudioMixer>("Audio/AudioMixer");
            AudioMixerSnapshot snapshot = mixer.FindSnapshot("Underwater");
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
}
