using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
    public Sound[] musicSounds, sfxSounds, flipperSounds;
    public AudioSource musicSource, sfxSource, flipperSource;

    internal bool DoesPlayMusic = true;
    internal bool DoesPlaySFX = true;

    public void PlayMusic(string name)
    {
        Sound s = Array.Find(musicSounds, x => x.soundName == name);

        if (s == null)
        {
            Debug.Log("Sound Not Found");
        }

        else
        {
            musicSource.clip = s.clip;
            musicSource.Play();
        }
    }

    public void StopMusic(string name)
    {
        Sound s = Array.Find(musicSounds, x => x.soundName == name);

        if (s == null)
        {
            Debug.Log("Sound Not Found");
        }

        else
        {
            musicSource.clip = s.clip;
            musicSource.Stop();
        }
    }

    public void PlaySFX(string name)
    {
        Sound s = Array.Find(sfxSounds, x => x.soundName == name);

        if (s == null)
        {
            Debug.Log("Sound Not Found");
        }

        else
        {
            sfxSource.PlayOneShot(s.clip);
        }
    }

    public void PlayFlipper(string name)
    {
        Sound s = Array.Find(flipperSounds, x => x.soundName == name);

        if (s == null)
        {
            Debug.Log("Sound Not Found");
        }

        else
        {
            flipperSource.PlayOneShot(s.clip);
        }
    }

    public void ToggleMusic()
    {
        DoesPlayMusic = !DoesPlayMusic;
        
        if (DoesPlayMusic)
        {
            //For some reason the music source is set to .5 by default
            musicSource.volume = .5f;
        }
        else
        {
            musicSource.volume = 0;
        }
    }

    public void ToggleSFX()
    {
        DoesPlaySFX = !DoesPlaySFX;

        if(DoesPlaySFX)
        {
            sfxSource.volume = 1;
            flipperSource.volume = 1;
        }
        else
        {
            sfxSource.volume = 0;
            flipperSource.volume = 0;
        }
    }
}
