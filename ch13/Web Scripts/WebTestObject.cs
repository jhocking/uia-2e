using UnityEngine;
using System.Runtime.InteropServices;

public class WebTestObject : MonoBehaviour {
	private string _message;

	[DllImport("__Internal")]
	private static extern void ShowAlert(string msg);

	void Start() {
		_message = "No message yet";
	}

	void Update() {
		if (Input.GetMouseButtonDown(0)) {
			ShowAlert("Hello out there!");
		}
	}

	void OnGUI() {
		GUI.Label(new Rect(10, 10, 200, 20), _message);
	}

	public void RespondToBrowser(string message) {
		_message = message;
	}
}
