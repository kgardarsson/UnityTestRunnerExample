using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.TestTools;

namespace Test {
    public class AudioSource_Test {

        [UnityTest]
        public IEnumerator AudioSource_WhenTrue_LoopsAtLeastTwice() {
            // This test checks if an audio source is still playing after two times its clip duration
            
            // Setup
            var testObj = new GameObject();
            AudioSource audio = testObj.AddComponent<AudioSource>();
            audio.clip = LoadClip("Audio/ShortDrumLoop");
            float leewayDuration = .1f;
            
            // Action
            audio.loop = true;
            audio.Play();

            yield return new WaitForSeconds(audio.clip.length + leewayDuration);

            // Test whether audio playes after its duration
            Assert.AreEqual(true, audio.isPlaying);

            // Test whether it loops at least a couple of times
            yield return new WaitForSeconds(audio.clip.length);
            Assert.AreEqual(true, audio.isPlaying);

            yield return null;
        }

        [UnityTest]
        public IEnumerator AudioSource_WithClip_WillAttenuateWithVolume() {
            // Check if AudioSource output samples data represents clip samples data attenuated by the volume property

            // Setup
            var testObj = new GameObject();
            AudioSource audio = testObj.AddComponent<AudioSource>();
            AudioListener listener = testObj.AddComponent<AudioListener>();
            audio.clip = LoadClip("Audio/MusicLoop");
            int numSamples = 256; // Must be a power of 2
            int numChecks = 3; // number of samples to test
            audio.volume = .6f;

            // Action
            audio.loop = true;
            audio.Play();

            // Get output
            float[] actualOutput = new float[numSamples];
            audio.GetOutputData(actualOutput, 1);

            yield return new WaitForSeconds(.1f);

            int currSample = audio.timeSamples;
            audio.GetOutputData(actualOutput, 1);

            float[] samples = new float[numSamples];
            audio.clip.GetData(samples, currSample - numSamples);

            // Extract left channel from stereo track
            float[] samples_left = new float[numSamples/2];
            for (int i = 0; i < samples_left.Length; i++) {
                samples_left[i] = samples[i*2];
            }

            // Test 
            for (int i = 0; i < numChecks; i++) {
                Assert.AreEqual(samples_left[i] * audio.volume, actualOutput[i]);
            }

            yield return null;

        }

        AudioClip LoadClip(string path) {
            AudioClip clip = Resources.Load<AudioClip>(path);
            if (clip == null)
                throw new System.NullReferenceException("No clip exists at Resources/" + path);
            return clip;
        }
    }
}