using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public Sound[] musicSounds, sfxSounds;
    public AudioSource musicSource, sfxSource;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayMusic(string name){
        Sound s = Array.Find(musicSounds, x => x.soundName == name);

        if(s == null)
        {
            Debug.Log("Som não encontrado");
        }
        else
        {
            musicSource.clip = s.clip;
            musicSource.Play();
        }
    }

    public void PlaySFX(string name){
        Sound s = Array.Find(sfxSounds, x => x.soundName == name);

        if(s == null)
        {
            Debug.Log("Som não encontrado");
        }
        else
        {
            sfxSource.PlayOneShot(s.clip);
        }
    }

    public void MusicVolume(float volume)
    {
        musicSource.volume = volume;
    }

    public void SFXVolume(float volume)
    {
        sfxSource.volume = volume;
    }

    public AudioSource GetSFXAudioSource(string name)
{
    Sound s = Array.Find(sfxSounds, x => x.soundName == name);

    if (s == null)
    {
        Debug.Log("Som não encontrado: " + name);
        return null;
    }
    
    // Criar um novo AudioSource para esse efeito sonoro
    GameObject soundObject = new GameObject("SFX_" + name);
    AudioSource newSource = soundObject.AddComponent<AudioSource>();
    
    newSource.clip = s.clip;
    newSource.volume = sfxSource.volume; // Herda o volume do gerenciador
    newSource.loop = false;
    newSource.playOnAwake = false;

    // O som será destruído quando terminar de tocar
    Destroy(soundObject, s.clip.length + 0.1f);

    return newSource;
}
}
