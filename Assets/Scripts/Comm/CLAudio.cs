using UnityEngine;
using System.Collections;

public class CLAudio
{
    public static void Play( AudioSource audio, AudioClip clip)
    {
        if (audio != null && clip != null)
        {
            audio.loop = false;
            audio.clip = clip;
            audio.Play();
        }
    }

    public static void PlayLoop(AudioSource audio, AudioClip clip)
    {
        if (audio != null && clip != null)
        {
            audio.loop = true;
            audio.clip = clip;
            audio.Play();
        }
    }

    public static void PlayOneShot(AudioSource audio, AudioClip clip)
    {
        if (audio != null && clip != null)
        {
            audio.loop = false;
            audio.PlayOneShot(clip);
        }
    }

    public static void Stop(AudioSource audio)
    {
        if (audio != null)
        {
            audio.Stop();
        }
    }
}