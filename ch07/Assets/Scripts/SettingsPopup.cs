using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SettingsPopup : MonoBehaviour {
	[SerializeField] private Slider speedSlider;
	
	void Start() {
		speedSlider.value = PlayerPrefs.GetFloat("speed", 1);
	}

	public void Open() {
		gameObject.SetActive(true);
	}
	public void Close() {
		gameObject.SetActive(false);
	}

	public void OnSubmitName(string name) {
		Debug.Log(name);
	}
	
	public void OnSpeedValue(float speed) {
		Messenger<float>.Broadcast(GameEvent.SPEED_CHANGED, speed);
		PlayerPrefs.SetFloat("speed", speed);
	}
}
