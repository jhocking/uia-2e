using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System;

public class NetworkService {
	private const string webImage = "http://upload.wikimedia.org/wikipedia/commons/c/c5/Moraine_Lake_17092005.jpg";
	private const string localApi = "http://localhost/uia/api.php";

	// weather api list https://www.programmableweb.com/category/weather/api
	private const string jsonApi = "http://api.openweathermap.org/data/2.5/weather?q=Chicago,us&APPID=<your api key>";
	private const string xmlApi = "http://api.openweathermap.org/data/2.5/weather?q=Chicago,us&mode=xml&APPID=<your api key>";

	private IEnumerator CallAPI(string url, WWWForm form, Action<string> callback) {
		using (UnityWebRequest request = (form == null) ?
			UnityWebRequest.Get(url) : UnityWebRequest.Post(url, form)) {

			yield return request.Send();

			if (request.isNetworkError) {
				Debug.LogError("network problem: " + request.error);
			} else if (request.responseCode != (long)System.Net.HttpStatusCode.OK) {
				Debug.LogError("response error: " + request.responseCode);
			} else {
				callback(request.downloadHandler.text);
			}
		}
	}

	public IEnumerator GetWeatherXML(Action<string> callback) {
		return CallAPI(xmlApi, null, callback);
	}
	public IEnumerator GetWeatherJSON(Action<string> callback) {
		return CallAPI(jsonApi, null, callback);
	}

	public IEnumerator LogWeather(string name, float cloudValue, Action<string> callback) {
		WWWForm form = new WWWForm();
		form.AddField("message", name);
		form.AddField("cloud_value", cloudValue.ToString());
		form.AddField("timestamp", DateTime.UtcNow.Ticks.ToString());

		return CallAPI(localApi, form, callback);
	}
	public IEnumerator DownloadImage(Action<Texture2D> callback) {
		UnityWebRequest request = UnityWebRequestTexture.GetTexture(webImage);
		yield return request.Send();
		callback(DownloadHandlerTexture.GetContent(request));
	}
}
