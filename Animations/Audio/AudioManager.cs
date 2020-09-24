using System;
using UnityEngine.Audio;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;
    public static AudioManager instance;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);

        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        foreach(Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    void Start()
    {
        Debug.Log("Play theme song");
        Play("theme");
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if(s != null)
        {
            s.source.Play();
        }
    }
	
	public void SetVolume()
	{
		AudioSource[] sources = GetComponents<AudioSource>();
		
		for(int i = 0; i < sources.Length; ++i)
		{
			if(sources[i].loop) // only music is looped
			{
				sources[i].volume = PlayerPrefs.GetFloat("music", 0.5f);
			}
			else
			{
				sources[i].volume = PlayerPrefs.GetFloat("sound", 0.5f);
			}
		}
	}
}
