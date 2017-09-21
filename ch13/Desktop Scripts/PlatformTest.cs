using UnityEngine;
using System.Collections;

public class PlatformTest : MonoBehaviour {
	void OnGUI() {
#if UNITY_EDITOR
		GUI.Label(new Rect(10, 10, 200, 20), "Running in Editor");
#elif UNITY_STANDALONE
		GUI.Label(new Rect(10, 10, 200, 20), "Running on Desktop");
#else
		GUI.Label(new Rect(10, 10, 200, 20), "Running on other platform");
#endif
	}
}
