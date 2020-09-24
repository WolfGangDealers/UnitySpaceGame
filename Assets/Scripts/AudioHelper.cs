using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AudioHelper
{
    public static AudioSource PlayClip2D(AudioClip clip, float volume)
    {
        // create our new AudioSource
        GameObject audioObject = new GameObject("2DAudio");
        AudioSource audioSource = audioObject.AddComponent<AudioSource>();
        // configure to be 2D
        audioSource.clip = clip;
        audioSource.volume = volume;

        audioSource.Play();
        // Destroy once done
        Object.Destroy(audioObject, clip.length);
        // Return it
        return audioSource;
    }
}
