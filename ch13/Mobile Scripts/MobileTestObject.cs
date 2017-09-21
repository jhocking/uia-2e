using UnityEngine;
using System.Collections;

public class MobileTestObject : MonoBehaviour {
	private string _message;
	
	void Awake() {
		TestPlugin.Initialize();
	}
	
	// Use this for initialization
	void Start() {
		_message = "START: " + TestPlugin.TestString("ThIs Is A tEsT");
	}
	
	// Update is called once per frame
	void Update() {
		
		// Make sure the user touched the screen
		if (Input.touchCount==0){return;}
		
		Touch touch = Input.GetTouch(0);
		if (touch.phase == TouchPhase.Began) {
			_message = "TOUCH: " + TestPlugin.TestNumber();
		}
	}
	
	void OnGUI() {
		GUI.Label(new Rect(10, 10, 200, 20), _message);
	}
}
