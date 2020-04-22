using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;


public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;



    public Sound[] sounds;
    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null) instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        foreach(Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.pitch = s.pitch;
            s.source.volume = s.volume;
            s.source.loop = s.loop;
        }
    }

    void Start()
    {
        Play("background");
    }

    Sound Find(string name)
    {
        bool found = false;
        foreach (Sound s in sounds)
        {
            if (s.name == name)
            {
                found = true;
                return s;
            }
        }
        if (found == false)
        {
            Debug.LogWarning($"Audio '{name}' não encontrado!");
            return null;
        }
        return null;
    }

    public void Play(string name)
    {
        Sound sound = Find(name);
        if(sound != null)
        {
            sound.source.Play();
        }
    }

    public void SetPitch(string name, float pitch)
    {
        Sound sound = Find(name);
        if (sound != null)
        {
            sound.pitch = pitch;
            sound.source.pitch = sound.pitch;
        }
    }
}
