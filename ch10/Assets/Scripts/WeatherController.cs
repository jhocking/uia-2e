using UnityEngine;
using System.Collections;

public class WeatherController : MonoBehaviour {
	[SerializeField] private Material sky;
	[SerializeField] private Light sun;
	
	private float _fullIntensity;
	
	void Awake() {
		Messenger.AddListener(GameEvent.WEATHER_UPDATED, OnWeatherUpdated);
	}
	void OnDestroy() {
		Messenger.RemoveListener(GameEvent.WEATHER_UPDATED, OnWeatherUpdated);
	}
	
	// Use this for initialization
	void Start() {
		_fullIntensity = sun.intensity;
	}
	
	private void OnWeatherUpdated() {
		SetOvercast(Managers.Weather.cloudValue);
	}
	
	private void SetOvercast(float value) {
		sky.SetFloat("_Blend", value);
		sun.intensity = _fullIntensity - (_fullIntensity * value);
	}
}
