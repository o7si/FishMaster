using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour {

	private static AudioManager _instance;

	public static AudioManager Instance {
		get { 
			return _instance;
		}
	}

	void Awake() {
		_instance = this;
	}

	public Toggle music;

	void Start() {
		music.isOn = PlayerPrefs.GetInt ("Music", 1) == 1 ? true : false;
		SwitchMute ();
	}

	public AudioSource bgmAudioSource;
	public AudioClip fireClip;
	public AudioClip waveClip;
	public AudioClip changeGunClip;
	public AudioClip getGoldClip;
	public AudioClip lvUpClip;

	public void SwitchMute() {
		if (music.isOn) {
			bgmAudioSource.Play ();
		} else {
			bgmAudioSource.Pause ();
		}
	}

	public void PlayEffectSound(AudioClip clip) {
		if (music.isOn) {
			AudioSource.PlayClipAtPoint (clip, Vector3.zero);
		}
	}

}
