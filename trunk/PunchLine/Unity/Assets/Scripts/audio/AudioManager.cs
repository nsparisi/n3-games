using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour {

	public List<AudioSource> AudioSources;

	public enum SoundTypes
		{
			PlayerDash,
			PlayerDying,
			PlayerFall,
			PlayerHurt,
			SwordSwipe1,
			SwordSwipe2
		}

	public void PlaySound(SoundTypes type) 
	{
		foreach(AudioSource audio in AudioSources)
		{
			if(audio.name.Equals(type.ToString()))
			{
				audio.Play();
				return;
			}
		}
	}

	public void StopSound(SoundTypes type) 
	{
		foreach(AudioSource audio in AudioSources)
		{
			if(audio.name.Equals(type.ToString()))
			{
				audio.Stop();
				return;
			}
		}
	}

	public static AudioManager Instance;

	void Awake() {
		Instance = this;
	}
}