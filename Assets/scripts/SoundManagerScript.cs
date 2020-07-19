using UnityEngine.Audio;
using System;
using UnityEngine;

public class SoundManagerScript : MonoBehaviour
{
    public new Audio[] audio;

    void Awake(){
        DontDestroyOnLoad(gameObject);
        ServiceLocator.Register<SoundManagerScript>(this);
    }

    void Start(){
        foreach (Audio a in audio)
        {
            a.source = gameObject.AddComponent<AudioSource>();
            a.source.clip = a.clip;
            a.source.volume = a.volume;
            a.source.pitch = a.pitch;
            a.source.loop = a.loop;
        }
    }

    public void Play(string name)
    {
        Audio a = Array.Find(audio, audios => audios.Name == name);
        if (a == null)
        {
            Debug.LogWarning("b-bbakaa: "+name+" , not found!");
        }
        a.source.Play();
    }
}
