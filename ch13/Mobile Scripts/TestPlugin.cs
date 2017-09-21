using UnityEngine;
using System;
using System.Collections;
using System.Runtime.InteropServices;

/* plugin tutorials
http://docs.unity3d.com/Manual/Plugins.html

ios:
http://blog.mediarain.com/2013/03/creating-ios-plugins-for-unity/
http://stackoverflow.com/questions/14834626/how-to-build-unity3d-plugin-for-ios
http://stackoverflow.com/questions/9712807/creating-ios-plugin-in-unity

android:
http://www.theskyway.net/en/androidunitytutorial/
http://forum.unity3d.com/threads/78598-Android-Plugin-Call-C-from-Java
refer to init() in NerdGPG.cs for how to launch activity http://www.nerdiacs.com/2013/05/17/google-play-game-services-for-unity/
although http://stackoverflow.com/questions/13908712/starting-another-activity-without-pausing-the-previous-one-android
external libraries will cause java.lang.NoClassDefFoundError unless their .jar is also brought in
adb logcat -s Unity (add other log tags for testing plugins)
*/

public class TestPlugin : MonoBehaviour {
	private static TestPlugin _instance;
	
	// need an object in the scene to target UnitySendMessage from native code
	public static void Initialize() {
		if (_instance != null) {
			Debug.Log("TestPlugin instance was found. Already initialized");
			return;
		}
		Debug.Log("TestPlugin instance not found. Initializing...");
		
		GameObject owner = new GameObject("TestPlugin_instance");
		_instance = owner.AddComponent<TestPlugin>();
		DontDestroyOnLoad(_instance);
	}
	
	#region iOS
	// private interface to native implementation
	[DllImport("__Internal")]
	private static extern float _TestNumber();
	
	[DllImport("__Internal")]
	private static extern string _TestString(string test);
	#endregion iOS
	
#if UNITY_ANDROID
	private static Exception _pluginError;
	private static AndroidJavaClass _pluginClass;
	private static AndroidJavaClass GetPluginClass() {
		if (_pluginClass == null && _pluginError == null) {
			AndroidJNI.AttachCurrentThread();
			try {
				_pluginClass = new AndroidJavaClass("com.testcompany.testplugin.TestPlugin");
			} catch (Exception e) {
				_pluginError = e;
			}
		}
		return _pluginClass;
	}
	
	private static AndroidJavaObject _unityActivity;
	private static AndroidJavaObject GetUnityActivity() {
		if (_unityActivity == null) {
			AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
			_unityActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
		}
		return _unityActivity;
	}
#endif
	
	// public interface for use inside C# / JS code
	public static float TestNumber() {
		float val = 0f;
		if (Application.platform == RuntimePlatform.IPhonePlayer)
			val = _TestNumber();
#if UNITY_ANDROID
		if (!Application.isEditor && _pluginError == null)
			val = GetPluginClass().CallStatic<int>("getNumber");
#endif
		return val;
	}
	
	public static string TestString(string test) {
		string val = "";
		if (Application.platform == RuntimePlatform.IPhonePlayer)
			val = _TestString(test);
#if UNITY_ANDROID
		if (!Application.isEditor && _pluginError == null)
			val = GetPluginClass().CallStatic<string>("getString", test);
#endif
		return val;
	}
}