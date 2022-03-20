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
            
            // Arrange
            AudioMixer mixer = LoadMixer("Audio/AudioMixer");
            float expectedValue = -3;
            float actualValue;

            // Act
            mixer.SetFloat("MusicVol", expectedValue);

            // Assert
            mixer.GetFloat("MusicVol", out actualValue);
            Assert.AreEqual(expectedValue, actualValue);

            yield return null;

            // Code for private purposes to assess the change in mixer UI
            // yield return new WaitForSeconds(10f);
        }
        
        [UnityTest]
        public IEnumerator Snapshot_WhenTransistionedTo_WillReachTargetVolume() {
            
            // Arrange
            AudioMixer mixer = LoadMixer("Audio/AudioMixer");
            AudioMixerSnapshot snapshot = mixer.FindSnapshot("Underwater");
            float transitionTime = 1;
            float expectedValue = -3;
            float actualValue;
            float acceptedError = .05f;

            // Act
            snapshot.TransitionTo(transitionTime);

            yield return new WaitForSeconds(transitionTime);

            mixer.GetFloat("MasterVol", out actualValue); // The only way I could find to access volume parameter was to expose it. The snapshots will run as long as the exposed value is not set.

            // Assert
            // Test whether expected and actual values are more or less the same
            Assert.AreEqual(expectedValue, actualValue, acceptedError);

            yield return null;

            // Code for my private purposes to see the change in mixer UI
            // yield return new WaitForSeconds(3f);
        }

        //*** EXTRA TESTS ***//

        [UnityTest]
        public IEnumerator ExposedParameter_WhenSetTooHigh_SetsToMaximum() {
            
            // Arrange
            AudioMixer mixer = LoadMixer("Audio/AudioMixer");
            float setVolumeTo = 30;
            float maxVol = 20;
            float actualValue;

            // Act
            mixer.SetFloat("MusicVol", setVolumeTo);

            // Assert
            mixer.GetFloat("MusicVol", out actualValue);
            Assert.AreEqual(maxVol, actualValue);

            yield return null;
        }

        [UnityTest]
        public IEnumerator ExposedParameter_WhenSetTooLow_SetsToMinimum() {
            
            // Arrange
            AudioMixer mixer = LoadMixer("Audio/AudioMixer");
            float setVolumeTo = -90;
            float minVol = -80;
            float actualValue;

            // Act
            mixer.SetFloat("MusicVol", setVolumeTo);

            // Assert
            mixer.GetFloat("MusicVol", out actualValue);
            Assert.AreEqual(minVol, actualValue);

            yield return null;
        }

        AudioMixer LoadMixer(string path) {
            AudioMixer mixer = Resources.Load<AudioMixer>(path);
            if (mixer == null)
                throw new System.NullReferenceException("No mixer exists at Resources/" + path);
            return mixer;
        }
    }
}
