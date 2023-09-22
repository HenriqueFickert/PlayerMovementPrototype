using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Audio
{
    public class AudioFeedback : MonoBehaviour
    {
        public AudioClip audioClip;
        public AudioSource audioSource;
        [Range(0.0f, 1.0f)]
        public float volume = 1.0f;

        public void PlayClip()
        {
            if (audioClip == null) return;

            audioSource.volume = volume;
            audioSource.PlayOneShot(audioClip);
        }

        public void PlaySpecificClip(AudioClip clipToPlay = null)
        {
            if (clipToPlay == null)
                clipToPlay = audioClip;

            if (clipToPlay == null) return;

            audioSource.volume = volume;
            audioSource.PlayOneShot(clipToPlay);
        }
    }
}

