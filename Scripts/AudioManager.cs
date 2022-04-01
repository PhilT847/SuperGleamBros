// AudioManager
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
	public Sound currentMusic;

	public Sound[] sounds;

	public static AudioManager instance;

	private void Awake()
	{
		if (instance == null)
		{
			instance = this;
			UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
			Sound[] array = sounds;
			foreach (Sound sound in array)
			{
				sound.source = base.gameObject.AddComponent<AudioSource>();
				sound.source.clip = sound.clip;
				sound.source.volume = sound.volume;
				sound.source.pitch = sound.pitch;
				sound.source.loop = sound.looping;
			}
		}
		else
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	public void Play(string name)
	{
		Sound sound2 = Array.Find(sounds, (Sound sound) => sound.name == name);
		sound2?.source.PlayOneShot(sound2.clip);
	}

	public void SetMusic(string name)
	{
		Sound sound2 = Array.Find(sounds, (Sound sound) => sound.name == name);
		if (sound2 != null)
		{
			if (currentMusic.name != "")
			{
				currentMusic.source.Stop();
			}
			currentMusic = sound2;
			currentMusic.source.Play();
		}
	}
}
