using UnityEngine;
using System.Collections;

public class SettingsPopup : MonoBehaviour {
	[SerializeField] private AudioClip sound;

	public void OnSoundToggle() {
		Managers.Audio.soundMute = !Managers.Audio.soundMute;
		Managers.Audio.PlaySound(sound);
	}

	public void OnSoundValue(float volume) {
		Managers.Audio.soundVolume = volume;
	}

	public void OnMusicToggle() {
		Managers.Audio.musicMute = !Managers.Audio.musicMute;
		Managers.Audio.PlaySound(sound);
	}
	
	public void OnMusicValue(float volume) {
		Managers.Audio.musicVolume = volume;
	}

	public void OnPlayMusic(int selector) {
		Managers.Audio.PlaySound(sound);

		switch (selector) {
		case 1:
			Managers.Audio.PlayIntroMusic();
			break;
		case 2:
			Managers.Audio.PlayLevelMusic();
			break;
		default:
			Managers.Audio.StopMusic();
			break;
		}
	}
}
